using Sistema_UTD.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sistema_UTD.Negocio;
using Sistema_UTD.Dominio;
using Sistema_UTD.Dominio.Entidades;
using Sistema_UTD.Dominio.DTOs;
using BCrypt.Net;
using System.Security.Cryptography;


namespace Sistema_UTD.Admin
{
    public partial class Usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<BreadcrumbItem> listaRutas = new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Titulo = "Inicio", Url = "~/Default.aspx", EsActivo = false },
                    new BreadcrumbItem { Titulo = "Usuarios", Url = "", EsActivo = true }
                };

                if (Master != null)
                {
                    Master.ActualizarBreadcrumb(listaRutas);
                }

                RolNegocio negocioRol = new RolNegocio();
                List<RolResumenDTO> lista = negocioRol.Listar();

                ddlRol.DataSource = lista;
                ddlRol.DataValueField = "Id";
                ddlRol.DataTextField = "Descripcion";
                ddlRol.DataBind();

                CargarTarjetasUsuarios();

            }
        }

        protected void lnkBtnCrearUsuario_Click(object sender, EventArgs e)
        {
            //string accion = "Crear";
            //Response.Redirect("~/Admin/FormularioUsuario.aspx?accion=" + accion, false);

            // Setear y abrir el modal
            lblTituloModalUsuario.Text = "Crear Usuario";
            lblDescripcionModalUsuario.InnerText = "Cree un usuario para dar acceso el sistema";

            BloquearNotificacion();
            
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "AbrirModal", "abrirModalUsuario();", true);
        }

        protected void lnkBtnModificarUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/FormularioUsuario.aspx", false);
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                //// INYECCIÓN DE PRUEBA: Pausa el hilo del servidor por 2 segundos (2000 milisegundos)
                //System.Threading.Thread.Sleep(6000);

                //// 1. INYECCIÓN DE PRUEBA: Fuerza el error inmediatamente
                //throw new Exception("Simulación de fallo crítico para probar la alerta roja.");

                UsuarioResumenDTO usuarioActual = (UsuarioResumenDTO)Session["UsuarioLogueado"];

                UsuarioNegocio negocio = new UsuarioNegocio();

                // Cargar el objeto usuario nuevo
                //Usuario nuevo = new Usuario();
                //nuevo.NombUsuario = txtUsuario.Text;
                //nuevo.Contrasenia = txtContrasenia.Text;
                //nuevo.Rol = new Rol();
                //nuevo.Rol.Id = int.Parse(ddlRol.SelectedValue);
                //nuevo.Apellido = txtApellido.Text;
                //nuevo.Nombre = txtNombre.Text;
                //nuevo.Activo = true;

                // Cargar el DTO
                UsuarioCreacionDTO nuevoUsuarioDTO = new UsuarioCreacionDTO();
                nuevoUsuarioDTO.NombUsuario = txtUsuario.Text.Trim();
                nuevoUsuarioDTO.Contrasenia = txtContrasenia.Text.Trim();
                nuevoUsuarioDTO.RolId = int.Parse(ddlRol.SelectedValue);
                nuevoUsuarioDTO.Apellido = txtApellido.Text.Trim();
                nuevoUsuarioDTO.Nombre = txtNombre.Text.Trim();
                nuevoUsuarioDTO.Activo = true;
               

                negocio.AgregarUsuario(nuevoUsuarioDTO, usuarioActual.Id);

                MostrarNotificacionVerde();

                LimpiarCampos();

                CargarTarjetasUsuarios();

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LimpiarFondo", "limpiarFondoModal();", true);
            }
            catch (Exception ex)
            {
                alertaNoSatisfactoria.InnerText = "El usuario no ha sido creado debido a un error: " + ex.Message;
                MostraNotificacionRoja();

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LimpiarFondo", "limpiarFondoModal();", true);
            }
        } // DONE: Pasar a DTO
          // DONE: Validar que no funcione el doble click al aceptar
          // DONE: Verificar el feedback del botón Aceptar

        protected void CargarTarjetasUsuarios()
        {
            UsuarioNegocio negocioUsuario = new UsuarioNegocio();
            lblNumAdministrador.Text = negocioUsuario.CargarTarjetaAdministradores();
            lblNumTecnico.Text = negocioUsuario.CargarTarjetaTecnicos();
            lblNumCliente.Text = negocioUsuario.CargarTrajetaClientes();
        }

        protected void BloquearNotificacion()
        {
            // Para eliminar la carga fantasma del modal y la ejecución del update panel
            alertaSatisfactoria.Style["display"] = "none";
            alertaNoSatisfactoria.Style["display"] = "none";
        }

        protected void MostrarNotificacionVerde()
        {
            alertaSatisfactoria.Style["display"] = "block";
            alertaNoSatisfactoria.Style["display"] = "none";
        }

        protected void MostraNotificacionRoja()
        {
            alertaSatisfactoria.Style["display"] = "none";
            alertaNoSatisfactoria.Style["display"] = "block";
        }

        protected void LimpiarCampos()
        {
            txtUsuario.Text = string.Empty;
            txtContrasenia.Text = string.Empty;
            ddlRol.SelectedIndex = 0;
            txtApellido.Text = string.Empty;
            txtNombre.Text = string.Empty;
        }
    }
}