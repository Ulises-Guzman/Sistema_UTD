using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sistema_UTD.Dominio.DTOs;
using Sistema_UTD.Helpers;
using Sistema_UTD.Negocio;

namespace Sistema_UTD.GestionInstrumentos
{
    public partial class FormularioEstado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<BreadcrumbItem> listaRutas = new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Titulo = "Inicio", Url = "~/Default.aspx", EsActivo = false },
                    new BreadcrumbItem { Titulo = "Gestión de Componentes Asociados", Url = "~/GestionInstrumentos/CompAsocInstrumentos.aspx", EsActivo = false},
                    new BreadcrumbItem { Titulo = "Modificar Estado", Url = "", EsActivo = true}
                };

                if (Master != null)
                {
                    Master.ActualizarBreadcrumb(listaRutas);
                }

                // Cargar el gv con la lista de estados
                EstadoNegocio estadoNegocio = new EstadoNegocio();
                Session.Add("listaEstado", estadoNegocio.ListarEstado());
                gvEstados.DataSource = Session["listaEstado"];
                gvEstados.DataBind();
            }
        }

        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BloquearNotificacion();

            try
            {
                //// 1. INYECCIÓN DE PRUEBA: Fuerza el error inmediatamente
                //throw new Exception("Simulación de fallo crítico para probar la alerta roja.");

                List<EstadoResumenDTO> lista = (List<EstadoResumenDTO>)Session["listaEstado"];
                List<EstadoResumenDTO> listaBusqueda = new List<EstadoResumenDTO>();
                listaBusqueda = lista.FindAll(x => x.Descripcion.ToLower().Contains(txtBuscar.Text.ToLower()));
                gvEstados.DataSource = listaBusqueda;
                gvEstados.DataBind();

            }
            catch (Exception ex)
            {
                alertaNoSatisfactoria.InnerText = "La sección no ha sido encontrado debido a un error: " + ex.Message;
                MostrarNotificacionRoja();
            }
        }

        protected void lnkBtnActualizar_Click(object sender, EventArgs e)
        {
            BloquearNotificacion();
            ActualizarGrilla();
        }

        protected void gvEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //// 1. INYECCIÓN DE PRUEBA: Fuerza el error inmediatamente
                //throw new Exception("Simulación de fallo crítico para probar la alerta roja.");

                BloquearNotificacion();

                List<EstadoResumenDTO> lista = (List<EstadoResumenDTO>)Session["listaEstado"];

                int id = int.Parse(gvEstados.SelectedDataKey.Value.ToString());

                EstadoResumenDTO estadoSeleccionado = lista.Find(x => x.Id == id);

                if (estadoSeleccionado == null)
                {
                    throw new Exception("El registro ya no se encuentra en la memoria o la sesión expiró.");
                }

                Session.Add("estadoSeleccionado", estadoSeleccionado);

                // Cargar datos a los TextBox
                txtEstado.Text = estadoSeleccionado.Descripcion;

                // Activar o Desactivar por asignación directa
                chkEstadoActivo.Checked = estadoSeleccionado.Activo;
                txtEstado.Enabled = estadoSeleccionado.Activo;

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "AbrirModal", "abrirModalEstado();", true);
            }
            catch (Exception ex)
            {
                alertaNoSatisfactoria.InnerText = "El estado no ha sido encontrado debido a un error: " + ex.Message;
                MostrarNotificacionRoja();

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LimpiarFondo", "limpiarFondoModal();", true);
            }
        }

        protected void gvEstados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEstados.PageIndex = e.NewPageIndex;
            EnlazarPaginaGridView(sender, e);
            gvEstados.DataBind();

            BloquearNotificacion();
        }

        protected void EnlazarPaginaGridView(object sender, EventArgs e)
        {
            EstadoNegocio estadoNegocio = new EstadoNegocio();
            Session.Add("listaEstado", estadoNegocio.ListarEstado());
            gvEstados.DataSource = Session["listaEstado"];
            gvEstados.DataBind();
        }

        protected void btnAceptarModificarEstado_Click(object sender, EventArgs e)
        {
            // INYECCIÓN DE PRUEBA: Pausa el hilo del servidor por 2 segundos (2000 milisegundos)
            //System.Threading.Thread.Sleep(6000);

            //// 1. INYECCIÓN DE PRUEBA: Fuerza el error inmediatamente
            //throw new Exception("Simulación de fallo crítico para probar la alerta roja.");

            UsuarioResumenDTO usuarioLogueado = (UsuarioResumenDTO)Session["UsuarioLogueado"];

            EstadoResumenDTO estadoSeleccionado = (EstadoResumenDTO)Session["estadoSeleccionado"];

            EstadoNegocio estadoNegocio = new EstadoNegocio();

            if (!chkEstadoActivo.Checked)
            {
                estadoNegocio.DesactivarEstado(estadoSeleccionado.Id, usuarioLogueado.Id);

                MostrarNotificacionVerde();

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LimpiarFondo", "limpiarFondoModal();", true);
            }
            else
            {
                EstadoEdicionDTO modificadoEstadoDTO = new EstadoEdicionDTO();
                modificadoEstadoDTO.Id = estadoSeleccionado.Id;
                modificadoEstadoDTO.Descripcion = txtEstado.Text.Trim();
                modificadoEstadoDTO.Activo = chkEstadoActivo.Checked;

               estadoNegocio.ModificarEstado(modificadoEstadoDTO, usuarioLogueado.Id);

                MostrarNotificacionVerde();

                LimpiarCampos();

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LimpiarFondo", "limpiarFondoModal();", true);
            }

            ActualizarGrilla();
        }

        protected void BloquearNotificacion()
        {
            alertaSatisfactoria.Style["display"] = "none";
            alertaNoSatisfactoria.Style["display"] = "none";
        }

        protected void MostrarNotificacionVerde()
        {
            alertaSatisfactoria.Style["display"] = "block";
            alertaNoSatisfactoria.Style["display"] = "none";
        }

        protected void MostrarNotificacionRoja()
        {
            alertaSatisfactoria.Style["display"] = "none";
            alertaNoSatisfactoria.Style["display"] = "block";
        }

        protected void LimpiarCampos()
        {
            txtEstado.Text = string.Empty;
        }

        protected void ActualizarGrilla()
        {
            EstadoNegocio estadoNegocio = new EstadoNegocio();
            Session.Add("listaEstado", estadoNegocio.ListarEstado());
            gvEstados.DataSource = Session["listaEstado"];
            gvEstados.DataBind();
        }
    }
}