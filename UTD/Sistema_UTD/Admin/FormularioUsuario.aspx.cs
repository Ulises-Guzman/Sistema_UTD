using BCrypt.Net;
using Sistema_UTD.Dominio;
using Sistema_UTD.Dominio.DTOs;
using Sistema_UTD.Dominio.Entidades;
using Sistema_UTD.Helpers;
using Sistema_UTD.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Sistema_UTD.Admin
{
    public partial class FormularioUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<BreadcrumbItem> listaRutas = new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Titulo = "Inicio", Url = "~/Default.aspx", EsActivo = false },
                    new BreadcrumbItem { Titulo = "Usuarios", Url = "~/Admin/Usuarios.aspx",EsActivo = false },
                    new BreadcrumbItem { Titulo = "Modificar Usuario", Url = "", EsActivo = true}
                };

                if (Master != null)
                {
                    Master.ActualizarBreadcrumb(listaRutas);
                }

                // Refactoring
                // --- ddlCriterioCampo ---
                ddlCriterioCampo.Items.Clear();
                ddlCriterioCampo.Items.Add(new ListItem("Apellido"));
                ddlCriterioCampo.Items.Add(new ListItem("Nombre"));

                // --- ddlCriterioRol ---
                RolNegocio rolNegocio = new RolNegocio();
                List<RolResumenDTO> lista = rolNegocio.Listar();
                ddlCriterioRol.DataSource = lista;
                ddlCriterioRol.DataValueField = "Id";
                ddlCriterioRol.DataTextField = "Descripcion";
                ddlCriterioRol.DataBind();
                ddlCriterioRol.Items.Insert(0, new ListItem("-- Seleccione --", ""));
                ddlCriterioRol.SelectedIndex = 0;

                // --- ddlCriterioEstado ---
                ddlCriterioEstado.Items.Clear();
                //ddlCriterioEstado.Items.Add(new ListItem("", ""));
                ddlCriterioEstado.Items.Add(new ListItem("Activo"));
                ddlCriterioEstado.Items.Add(new ListItem("Inactivo"));
                ddlCriterioEstado.Items.Add(new ListItem("Todos"));
                ddlCriterioEstado.Items.Insert(0, new ListItem("-- Seleccione --", ""));
                ddlCriterioEstado.SelectedIndex = 0;

                // Cargar datos para el ddl del formulario modal
                ddlRol.DataSource = lista;
                ddlRol.DataValueField = "Id";
                ddlRol.DataTextField = "Descripcion";
                ddlRol.DataBind();

                // Cargar el gv con la lista de usuarios
                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                Session.Add("listaUsuario", usuarioNegocio.ListarUsuario());
                gvUsuarios.DataSource = Session["listaUsuario"];
                gvUsuarios.DataBind();

                txtContrasenia.Enabled = false;
            }
        } // DONE: Pasar a DTO los datos para ddl Rol

        protected void gvUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //// 1. INYECCIÓN DE PRUEBA: Fuerza el error inmediatamente
                //throw new Exception("Simulación de fallo crítico para probar la alerta roja.");

                BloquearNotificacion();

                // Inicia el Check Contrasenia del modal desactivado por defecto
                chkCambioContrasenia.Checked = false;

                // Recupero datos
                List<UsuarioResumenDTO> lista = (List<UsuarioResumenDTO>)Session["listaUsuario"];

                // Obterner el id del usuario seleccionado
                int id = int.Parse(gvUsuarios.SelectedDataKey.Value.ToString());

                // Búsqueda del usuario seleccionado
                UsuarioResumenDTO usuarioSeleccionado = lista.Find(x => x.Id == id);

                // Prevenir el NullReferenceException
                if (usuarioSeleccionado == null)
                {
                    throw new Exception("El registro ya no se encuentra en la memoria o la sesión expiró.");
                }

                Session.Add("usuarioSeleccionado", usuarioSeleccionado);

                // Cargar datos a los TextBox y DropDownList
                txtUsuario.Text = usuarioSeleccionado.NombUsuario;
                ddlRol.SelectedValue = usuarioSeleccionado.RolId.ToString();
                txtApellido.Text = usuarioSeleccionado.Apellido;
                txtNombre.Text = usuarioSeleccionado.Nombre;

                // Activar o Desactivar por asignación directa
                chkUsuarioActivo.Checked = usuarioSeleccionado.Activo;
                txtUsuario.Enabled = usuarioSeleccionado.Activo;
                ddlRol.Enabled = usuarioSeleccionado.Activo;
                txtApellido.Enabled = usuarioSeleccionado.Activo;
                txtNombre.Enabled = usuarioSeleccionado.Activo;

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "AbrirModal", "abrirModalUsuario();", true);
            }
            catch (Exception ex)
            {
                alertaNoSatisfactoria.InnerText = "El usuario no ha sido encontrado debido a un error: " + ex.Message;
                MostraNotificacionRoja();

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LimpiarFondo", "limpiarFondoModal();", true);
            }
        } // DONE: Pasar a DTO

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                //// 1. INYECCIÓN DE PRUEBA: Fuerza el error inmediatamente
                //throw new Exception("Simulación de fallo crítico para probar la alerta roja.");

                UsuarioResumenDTO usuarioLogueado = (UsuarioResumenDTO)Session["UsuarioLogueado"];

                UsuarioResumenDTO usuarioSeleccionado = (UsuarioResumenDTO)Session["usuarioSeleccionado"];

                UsuarioNegocio negocio = new UsuarioNegocio();

                if (!chkUsuarioActivo.Checked)
                {
                    negocio.DesactivarUsuario(usuarioSeleccionado.Id, usuarioLogueado.Id);

                    MostrarNotificacionVerde();

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LimpiarFondo", "limpiarFondoModal();", true);
                }
                else if (chkUsuarioActivo.Checked && chkCambioContrasenia.Checked)
                {
                    //int usuarioId = usuarioSeleccionado.Id;

                    //Usuario modificado = new Usuario();
                    //modificado.NombUsuario = txtUsuario.Text;
                    //modificado.Contrasenia = BCrypt.Net.BCrypt.HashPassword(txtContrasenia.Text);
                    //modificado.Rol = new Rol();
                    //modificado.Rol.Id = int.Parse(ddlRol.SelectedValue);
                    //modificado.Apellido = txtApellido.Text;
                    //modificado.Nombre = txtNombre.Text;
                    //modificado.Id = usuarioSeleccionado.Id;

                    UsuarioEdicionDTO modificadoUsuarioDTO = new UsuarioEdicionDTO();
                    modificadoUsuarioDTO.Id = usuarioSeleccionado.Id;
                    modificadoUsuarioDTO.NombUsuario = txtUsuario.Text.Trim();
                    modificadoUsuarioDTO.Contrasenia = txtContrasenia.Text.Trim();
                    modificadoUsuarioDTO.RolId = int.Parse(ddlRol.SelectedValue);
                    modificadoUsuarioDTO.Apellido = txtApellido.Text.Trim();
                    modificadoUsuarioDTO.Nombre = txtNombre.Text.Trim();

                    negocio.ModificarUsuario(chkCambioContrasenia.Checked, modificadoUsuarioDTO, usuarioLogueado.Id);

                    MostrarNotificacionVerde();

                    LimpiarCampos();

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LimpiarFondo", "limpiarFondoModal();", true);
                }
                else if (chkUsuarioActivo.Checked && !chkCambioContrasenia.Checked)
                {
                    //int usuarioId = usuarioSeleccionado.Id;

                    //Usuario modificado = new Usuario();
                    //modificado.NombUsuario = txtUsuario.Text;
                    //modificado.Rol = new Rol();
                    //modificado.Rol.Id = int.Parse(ddlRol.SelectedValue);
                    //modificado.Apellido = txtApellido.Text;
                    //modificado.Nombre = txtNombre.Text;
                    //modificado.Id = usuarioSeleccionado.Id;

                    UsuarioEdicionDTO modificadoUsuarioDTO = new UsuarioEdicionDTO();
                    modificadoUsuarioDTO.Id = usuarioSeleccionado.Id;
                    modificadoUsuarioDTO.NombUsuario = txtUsuario.Text.Trim();
                    modificadoUsuarioDTO.Contrasenia = txtContrasenia.Text.Trim();
                    modificadoUsuarioDTO.RolId = int.Parse(ddlRol.SelectedValue);
                    modificadoUsuarioDTO.Apellido = txtApellido.Text.Trim();
                    modificadoUsuarioDTO.Nombre = txtNombre.Text.Trim();

                    negocio.ModificarUsuario(chkCambioContrasenia.Checked, modificadoUsuarioDTO, usuarioLogueado.Id); 

                    MostrarNotificacionVerde();

                    LimpiarCampos();

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LimpiarFondo", "limpiarFondoModal();", true);
                }

                ActualizarGrilla();

            }
            catch (Exception ex)
            {
                alertaNoSatisfactoria.InnerText = "El usuario no ha sido modificado debido a un error: " + ex.Message;
                MostraNotificacionRoja();

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LimpiarFondo", "limpiarFondoModal();", true);
            }
        } // DONE: Pasar a DTO

        protected void gvUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //Capturo el PageIndex que viene por valor del parámetro GrigViewPageEventArgs
            gvUsuarios.PageIndex = e.NewPageIndex;
            EnlazarPaginaGridView(sender, e);
            gvUsuarios.DataBind();

            BloquearNotificacion();
        }

        protected void EnlazarPaginaGridView(object sender, EventArgs e)
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            Session.Add("listaUsuario", usuarioNegocio.ListarUsuario());
            gvUsuarios.DataSource = Session["listaUsuario"];
            gvUsuarios.DataBind();
        }

        protected void ActualizarGrilla()
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            Session.Add("listaUsuario", usuarioNegocio.ListarUsuario());
            gvUsuarios.DataSource = Session["listaUsuario"];
            gvUsuarios.DataBind();
            txtBuscar.Text = string.Empty;
        }

        protected void chkFiltro_CheckedChanged(object sender, EventArgs e)
        {
            BloquearNotificacion();
            txtBuscar.Text = "";
            txtBuscar.Enabled = !chkFiltro.Checked;
            pnlFiltroAvanzado.Visible = chkFiltro.Checked;
            txtBuscarAvanzado.Text = "";
        }

        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BloquearNotificacion();

            try
            {
                //// 1. INYECCIÓN DE PRUEBA: Fuerza el error inmediatamente
                //throw new Exception("Simulación de fallo crítico para probar la alerta roja.");

                List<UsuarioResumenDTO> lista = (List<UsuarioResumenDTO>)Session["listaUsuario"];
                List<UsuarioResumenDTO> listaBusqueda = new List<UsuarioResumenDTO>();
                listaBusqueda = lista.FindAll(x => x.NombUsuario.ToLower().Contains(txtBuscar.Text.ToLower()));
                gvUsuarios.DataSource = listaBusqueda;
                gvUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                alertaNoSatisfactoria.InnerText = "El usuario no ha sido encontrado debido a un error: " + ex.Message;
                MostraNotificacionRoja();
            }
        } // DONE: Pasar a DTO

        protected void btnBuscarAvanzado_Click(object sender, EventArgs e)
        {
            try
            {
                //// 1. INYECCIÓN DE PRUEBA: Fuerza el error inmediatamente
                //throw new Exception("Simulación de fallo crítico para probar la alerta roja.");

                BloquearNotificacion();

                UsuarioNegocio negocio = new UsuarioNegocio();
                List<UsuarioResumenDTO> listaBuscarAvanzado = new List<UsuarioResumenDTO>();
                listaBuscarAvanzado = negocio.BuscarAvanzado(
                                                             ddlCriterioCampo.SelectedItem.ToString(),
                                                             txtBuscarAvanzado.Text,
                                                             ddlCriterioRol.SelectedItem.ToString(),
                                                             ddlCriterioEstado.SelectedItem.ToString()
                                                             );
                gvUsuarios.DataSource = listaBuscarAvanzado;
                gvUsuarios.DataBind();

                // Piso la lista general para modificar usuario
                Session.Add("listaUsuario", listaBuscarAvanzado);
            }
            catch (Exception ex)
            {
                alertaNoSatisfactoria.InnerText = "El usuario no ha sido encontrado debido a un error: " + ex.Message;
                MostraNotificacionRoja();
            }
        } // DONE: Pasar a DTO

        protected void lnkBtnActualizar_Click(object sender, EventArgs e)
        {
            BloquearNotificacion();
            ActualizarGrilla();
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