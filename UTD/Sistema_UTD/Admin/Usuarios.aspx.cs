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

                RolNegocio negocio = new RolNegocio();
                List<Rol> lista = negocio.Listar();

                ddlRol.DataSource = lista;
                ddlRol.DataValueField = "Id";
                ddlRol.DataTextField = "Descripcion";
                ddlRol.DataBind();

            }
        }

        protected void lnkBtnCrearUsuario_Click(object sender, EventArgs e)
        {
            //string accion = "Crear";
            //Response.Redirect("~/Admin/FormularioUsuario.aspx?accion=" + accion, false);

            // Setear y abrir el modal
            lblTituloModalUsuario.Text = "Crear Usuario";
            lblDescripcionModalUsuario.InnerText = "Cree un usuario para dar acceso el sistema";

            // Para eliminar la carga fantasma del servidor con la notificación
            alertaSatisfactoria.Style["display"] = "none"; 

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
                // Ejecución de lógica para guardar datos
                Usuario nuevo = new Usuario();
                UsuarioNegocio negocio = new UsuarioNegocio();

                // Cargar el objeto usuario nuevo
                nuevo.NombUsuario = txtUsuario.Text;
                nuevo.Contrasenia = txtContrasenia.Text;
                nuevo.Rol = new Rol();
                nuevo.Rol.Id = int.Parse(ddlRol.SelectedValue);
                nuevo.Apellido = txtApellido.Text;
                nuevo.Nombre = txtNombre.Text;
                nuevo.Activo = true;

                // Agregar usuario nuevo
                negocio.AgregarUsuario(nuevo);

                // 2. Muestrar la alerta (le quitas el display:none)
                alertaSatisfactoria.Style["display"] = "block";

                // Limpiar campos
                txtUsuario.Text = string.Empty;
                txtContrasenia.Text = string.Empty;
                ddlRol.SelectedIndex = 0;
                txtApellido.Text = string.Empty;
                txtNombre.Text = string.Empty;

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LimpiarFondo", "limpiarFondoModal();", true);
            }
            catch (Exception ex)
            {

                throw ex;
                // Manejo de errores por si falla la ejecución
                // 1. lblError.Text = "Ocurrió un error: " + ex.Message;
                // 2. Utilizando las notificaciones
                // 3. Pagina de error
            }

        }

        
    }
}