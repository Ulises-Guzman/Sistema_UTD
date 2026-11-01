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
    public partial class FormularioRango : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<BreadcrumbItem> listaRutas = new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Titulo = "Inicio", Url = "~/Default.aspx", EsActivo = false },
                    new BreadcrumbItem { Titulo = "Gestión de Componentes Asociados", Url = "~/GestionInstrumentos/CompAsocModelos.aspx", EsActivo = false},
                    new BreadcrumbItem { Titulo = "Modificar Rango", Url = "", EsActivo = true}
                };

                if (Master != null)
                {
                    Master.ActualizarBreadcrumb(listaRutas);
                }

                // Cargar el gv con la lista de rangos
                RangoNegocio rangoNegocio = new RangoNegocio();
                Session.Add("listaRango", rangoNegocio.ListarRango());
                gvRangos.DataSource = Session["listaRango"];
                gvRangos.DataBind();
            }
        }

        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BloquearNotificacion();

            try
            {
                //// 1. INYECCIÓN DE PRUEBA: Fuerza el error inmediatamente
                //throw new Exception("Simulación de fallo crítico para probar la alerta roja.");

                List<RangoResumenDTO> lista = (List<RangoResumenDTO>)Session["listaRango"];
                List<RangoResumenDTO> listaBusqueda = new List<RangoResumenDTO>();
                listaBusqueda = lista.FindAll(x => x.Descripcion.ToLower().Contains(txtBuscar.Text.ToLower()));
                gvRangos.DataSource = listaBusqueda;
                gvRangos.DataBind();

            }
            catch (Exception ex)
            {
                alertaNoSatisfactoria.InnerText = "El rango no ha sido encontrado debido a un error: " + ex.Message;
                MostraNotificacionRoja();
            }
        }

        protected void lnkBtnActualizar_Click(object sender, EventArgs e)
        {
            BloquearNotificacion();
            ActualizarGrilla();
        }

        protected void gvRangos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //// 1. INYECCIÓN DE PRUEBA: Fuerza el error inmediatamente
                //throw new Exception("Simulación de fallo crítico para probar la alerta roja.");

                BloquearNotificacion();

                List<RangoResumenDTO> lista = (List<RangoResumenDTO>)Session["listaRango"];

                int id = int.Parse(gvRangos.SelectedDataKey.Value.ToString());

                RangoResumenDTO rangoSeleccionado = lista.Find(x => x.Id == id);

                if (rangoSeleccionado == null)
                {
                    throw new Exception("El registro ya no se encuentra en la memoria o la sesión expiró.");
                }

                Session.Add("rangoSeleccionado", rangoSeleccionado);

                // Cargar datos a los TextBox
                txtRango.Text = rangoSeleccionado.Descripcion;

                // Activar o Desactivar por asignación directa
                chkRangoActivo.Checked = rangoSeleccionado.Activo;
                txtRango.Enabled = rangoSeleccionado.Activo;

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "AbrirModal", "abrirModalRango();", true);
            }
            catch (Exception ex)
            {
                alertaNoSatisfactoria.InnerText = "El rango no ha sido encontrado debido a un error: " + ex.Message;
                MostraNotificacionRoja();

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LimpiarFondo", "limpiarFondoModal();", true);
            }
        }

        protected void gvRangos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRangos.PageIndex = e.NewPageIndex;
            EnlazarPaginaGridView(sender, e);
            gvRangos.DataBind();

            BloquearNotificacion();
        }

        protected void EnlazarPaginaGridView(object sender, EventArgs e)
        {
            RangoNegocio rangoNegocio = new RangoNegocio();
            Session.Add("listaRango", rangoNegocio.ListarRango());
            gvRangos.DataSource = Session["listaRango"];
            gvRangos.DataBind();
        }

        protected void btnAceptarModificarRango_Click(object sender, EventArgs e)
        {
            // INYECCIÓN DE PRUEBA: Pausa el hilo del servidor por 2 segundos (2000 milisegundos)
            //System.Threading.Thread.Sleep(6000);

            //// 1. INYECCIÓN DE PRUEBA: Fuerza el error inmediatamente
            //throw new Exception("Simulación de fallo crítico para probar la alerta roja.");

            UsuarioResumenDTO usuarioLogueado = (UsuarioResumenDTO)Session["UsuarioLogueado"];

            RangoResumenDTO rangoSeleccionado = (RangoResumenDTO)Session["rangoSeleccionado"];

            RangoNegocio rangoNegocio = new RangoNegocio();

            if (!chkRangoActivo.Checked)
            {
                rangoNegocio.DesactivarRango(rangoSeleccionado.Id, usuarioLogueado.Id);

                MostrarNotificacionVerde();

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LimpiarFondo", "limpiarFondoModal();", true);
            }
            else
            {
                RangoEdicionDTO modificadoRangoDTO = new RangoEdicionDTO();
                modificadoRangoDTO.Id = rangoSeleccionado.Id;
                modificadoRangoDTO.Descripcion = txtRango.Text.Trim();
                modificadoRangoDTO.Activo = chkRangoActivo.Checked;

                rangoNegocio.ModificarRango(modificadoRangoDTO, usuarioLogueado.Id);

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

        protected void MostraNotificacionRoja()
        {
            alertaSatisfactoria.Style["display"] = "none";
            alertaNoSatisfactoria.Style["display"] = "block";
        }

        protected void LimpiarCampos()
        {
            txtRango.Text = string.Empty;
        }

        protected void ActualizarGrilla()
        {
            RangoNegocio rangoNegocio = new RangoNegocio();
            Session.Add("listaRango", rangoNegocio.ListarRango());
            gvRangos.DataSource = Session["listaRango"];
            gvRangos.DataBind();
        }
    }
}