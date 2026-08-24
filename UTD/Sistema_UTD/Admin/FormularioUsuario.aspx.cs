using Sistema_UTD.Helpers;
using Sistema_UTD.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sistema_UTD.Dominio;
using Sistema_UTD.Dominio.Entidades;


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
                    new BreadcrumbItem { Titulo = "Modificar", Url = "", EsActivo = true}
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
                List<Rol> lista = rolNegocio.Listar();
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
        }

        protected void gvUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Setear y abrir el modal
            lblTituloModalUsuario.Text = "Modificar Usuario";
            lblDescripcionModalUsuario.InnerText = "Modifique los datos del usuario seleccionado";

            // Para eliminar la carga fantasma del modal con la selección del checkbox y la ejecución del update panel
            chkCambioContrasenia.Checked = false;
            BloquearNotificacion();

            // Recupero datos
            List<Usuario> lista = (List<Usuario>)Session["listaUsuario"];

            // Obterner el id del usuario seleccionado
            int id = int.Parse(gvUsuarios.SelectedDataKey.Value.ToString());

            // búsqueda del usuario seleccionado
            Usuario usuarioSeleccionado = lista.Find(x => x.Id == id);

            if (usuarioSeleccionado != null)
            {
                Session.Add("usuarioSeleccionado", usuarioSeleccionado);

                txtUsuario.Text = usuarioSeleccionado.NombUsuario;
                ddlRol.SelectedValue = usuarioSeleccionado.Rol.Id.ToString();
                txtApellido.Text = usuarioSeleccionado.Apellido;
                txtNombre.Text = usuarioSeleccionado.Nombre;

                if (usuarioSeleccionado.Activo)
                {
                    chkUsuarioActivo.Checked = true;
                    txtUsuario.Enabled = true;
                    ddlRol.Enabled = true;
                    txtApellido.Enabled = true;
                    txtNombre.Enabled = true;
                }
                else
                {
                    chkUsuarioActivo.Checked = false;
                    txtUsuario.Enabled = false;
                    ddlRol.Enabled = false;
                    txtApellido.Enabled = false;
                    txtNombre.Enabled = false;
                }

            }
            else
            {
                // Ante error mensaje de alerta, o refactorizar a try catch
            }

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "AbrirModal", "abrirModalUsuario();", true);
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuarioSeleccionado = (Usuario)Session["usuarioSeleccionado"];
                UsuarioNegocio negocio = new UsuarioNegocio();

                if (!chkUsuarioActivo.Checked)
                {
                    int usuarioId = usuarioSeleccionado.Id;
                    negocio.DesactivarUsuario(usuarioId);

                    alertaSatisfactoria.Style["display"] = "block";

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LimpiarFondo", "limpiarFondoModal();", true);
                }
                else if (chkUsuarioActivo.Checked && chkCambioContrasenia.Checked)
                {
                    int usuarioId = usuarioSeleccionado.Id;
                    negocio.ActivarUsuario(usuarioId);

                    Usuario modificado = new Usuario();

                    modificado.NombUsuario = txtUsuario.Text;
                    modificado.Contrasenia = txtContrasenia.Text;
                    modificado.Rol = new Rol();
                    modificado.Rol.Id = int.Parse(ddlRol.SelectedValue);
                    modificado.Apellido = txtApellido.Text;
                    modificado.Nombre = txtNombre.Text;
                    modificado.Id = usuarioSeleccionado.Id;

                    negocio.ModificarUsuario(chkCambioContrasenia.Checked, modificado);

                    alertaSatisfactoria.Style["display"] = "block";

                    LimpiarCampos();

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LimpiarFondo", "limpiarFondoModal();", true);
                }
                else if (chkUsuarioActivo.Checked && !chkCambioContrasenia.Checked)
                {
                    int usuarioId = usuarioSeleccionado.Id;
                    negocio.ActivarUsuario(usuarioId);

                    Usuario modificado = new Usuario();

                    modificado.NombUsuario = txtUsuario.Text;
                    modificado.Rol = new Rol();
                    modificado.Rol.Id = int.Parse(ddlRol.SelectedValue);
                    modificado.Apellido = txtApellido.Text;
                    modificado.Nombre = txtNombre.Text;
                    modificado.Id = usuarioSeleccionado.Id;

                    negocio.ModificarUsuario(chkCambioContrasenia.Checked, modificado);

                    alertaSatisfactoria.Style["display"] = "block";

                    LimpiarCampos();

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LimpiarFondo", "limpiarFondoModal();", true);
                }

                ActualizarGrilla();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void gvUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //Capturo el PageIndex que viene por valor del parametro GrigViewPageEventArgs
            gvUsuarios.PageIndex = e.NewPageIndex;
            EnlazarPaginaGridView(sender, e);
            gvUsuarios.DataBind();

            // Para eliminar la carga fantasma del modal con la selección del checkbox
            //chkCambioContrasenia.Checked = false;
            alertaSatisfactoria.Style["display"] = "none";
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
                List<Usuario> lista = (List<Usuario>)Session["listaUsuario"];
                List<Usuario> listaBusqueda = new List<Usuario>();
                listaBusqueda = lista.FindAll(x => x.NombUsuario.ToLower().Contains(txtBuscar.Text.ToLower()));
                gvUsuarios.DataSource = listaBusqueda;
                gvUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                // Redirigir a página de error o notificación
                throw ex;
            }
        }

        protected void btnBuscarAvanzado_Click(object sender, EventArgs e)
        {
            try
            {
                BloquearNotificacion();

                UsuarioNegocio negocio = new UsuarioNegocio();
                List<Usuario> listaBuscarAvanzado = new List<Usuario>();
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

                throw ex;
            }
        }

        protected void lnkBtnActualizar_Click(object sender, EventArgs e)
        {
            BloquearNotificacion();
            ActualizarGrilla();
        }

        protected void BloquearNotificacion() {
            // Para eliminar la carga fantasma del modal y la ejecución del update panel
            alertaSatisfactoria.Style["display"] = "none";
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