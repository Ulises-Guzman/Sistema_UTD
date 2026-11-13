using Sistema_UTD.Dominio.DTOs;
using Sistema_UTD.Helpers;
using Sistema_UTD.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistema_UTD.Calibraciones
{
    public partial class CalibrarInstrumentos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                List<BreadcrumbItem> listaRutas = new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Titulo = "Inicio", Url = "~/Default.aspx", EsActivo = false },
                    new BreadcrumbItem { Titulo = "Calibraciones", Url = "", EsActivo = true }
                };

                if (Master != null)
                {
                    Master.ActualizarBreadcrumb(listaRutas);
                }

                InstrumentoNegocio noHabilitadoNegocio = new InstrumentoNegocio();
                Session.Add("listaNoHabilitado", noHabilitadoNegocio.ListarNoHabilitados());
                gvNoHabilitados.DataSource = Session["listaNoHabilitado"];
                gvNoHabilitados.DataBind();

                InstrumentoNegocio habilitadoNegocio = new InstrumentoNegocio();
                Session.Add("listaHabilitado", habilitadoNegocio.ListarHabilitados());
                gvHabilitados.DataSource = Session["listaHabilitado"];
                gvHabilitados.DataBind();
            }
        }

        protected void gvInstrumentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //// 1. INYECCIÓN DE PRUEBA: Fuerza el error inmediatamente
                //throw new Exception("Simulación de fallo crítico para probar la alerta roja.");

                BloquearNotificacion();

                List<InstrumentoResumenDTO> lista = (List<InstrumentoResumenDTO>)Session["listaInstrumento"];

                int id = int.Parse(gvNoHabilitados.SelectedDataKey.Value.ToString());

                InstrumentoResumenDTO instrumentoSeleccionado = lista.Find(x => x.Id == id);

                if (instrumentoSeleccionado == null)
                {
                    throw new Exception("El registro ya no se encuentra en la memoria o la sesión expiró.");
                }

                Session.Add("instrumentoSeleccionado", instrumentoSeleccionado);

                Response.Redirect("~/GestionInstrumentos/FormularioInstrumento.aspx", false);

            }
            catch (Exception ex)
            {
                alertaNoSatisfactoria.InnerText = "El instrumento no ha sido encontrado debido a un error: " + ex.Message;
                MostrarNotificacionRoja();
            }
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

        protected void gvNoHabilitados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if(e.CommandName == "seleccion")
                //// 1. INYECCIÓN DE PRUEBA: Fuerza el error inmediatamente
                //throw new Exception("Simulación de fallo crítico para probar la alerta roja.");

                BloquearNotificacion();

                List<InstrumentoResumenDTO> lista = (List<InstrumentoResumenDTO>)Session["listaNoHabilitado"];

                //int id = int.Parse(gvInstrumentos.SelectedDataKey.Value.ToString());

                int id = Convert.ToInt32(e.CommandArgument);

                InstrumentoResumenDTO instrumentoSeleccionado = lista.Find(x => x.Id == id);

                if (instrumentoSeleccionado == null)
                {
                    throw new Exception("El registro ya no se encuentra en la memoria o la sesión expiró.");
                }

                Session.Add("instrumentoSeleccionado", instrumentoSeleccionado);

                Response.Redirect("~/Calibraciones/PlanillaCalibracion.aspx", false);

            }
            catch (Exception ex)
            {
                alertaNoSatisfactoria.InnerText = "El instrumento no ha sido encontrado debido a un error: " + ex.Message;
                MostrarNotificacionRoja();
            }

        }

        protected void gvNoHabilitados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvNoHabilitados.PageIndex = e.NewPageIndex;
            EnlazarPaginaGridViewNoHabilitado(sender, e);
            gvNoHabilitados.DataBind();

            BloquearNotificacion();
        }

        protected void EnlazarPaginaGridViewNoHabilitado(object sender, EventArgs e)
        {
            InstrumentoNegocio instrumentoNegocio = new InstrumentoNegocio();
            Session.Add("listaNoHabilitado", instrumentoNegocio.ListarNoHabilitados());
            gvNoHabilitados.DataSource = Session["listaNoHabilitado"];
            gvNoHabilitados.DataBind();
        }

        protected void gvHabilitados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //TODO: Generacion de certificados
        }

        protected void gvHabilitados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvHabilitados.PageIndex = e.NewPageIndex;
            EnlazarPaginaGridViewHabilitado(sender, e);
            gvHabilitados.DataBind();

            BloquearNotificacion();
        }

        protected void EnlazarPaginaGridViewHabilitado(object sender, EventArgs e)
        {
            InstrumentoNegocio instrumentoNegocio = new InstrumentoNegocio();
            Session.Add("listaHabilitado", instrumentoNegocio.ListarHabilitados());
            gvHabilitados.DataSource = Session["listaHabilitado"];
            gvHabilitados.DataBind();
        }

        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BloquearNotificacion();

            try
            {
                //// 1. INYECCIÓN DE PRUEBA: Fuerza el error inmediatamente
                //throw new Exception("Simulación de fallo crítico para probar la alerta roja.");

                List<InstrumentoResumenDTO> lista = (List<InstrumentoResumenDTO>)Session["listaNoHabilitado"];
                List<InstrumentoResumenDTO> listaBusqueda = new List<InstrumentoResumenDTO>();
                listaBusqueda = lista.FindAll(x => x.NumSerie.ToLower().Contains(txtBuscar.Text.ToLower()));
                gvNoHabilitados.DataSource = listaBusqueda;
                gvNoHabilitados.DataBind();

                LimpiarCampoBuscar();
            }
            catch (Exception ex)
            {
                alertaNoSatisfactoria.InnerText = "El instrumento no ha sido encontrado debido a un error: " + ex.Message;
                MostrarNotificacionRoja();

                LimpiarCampoBuscar();
            }
        }

        protected void txtBuscar1_TextChanged(object sender, EventArgs e)
        {
            BloquearNotificacion();

            try
            {
                //// 1. INYECCIÓN DE PRUEBA: Fuerza el error inmediatamente
                //throw new Exception("Simulación de fallo crítico para probar la alerta roja.");

                List<InstrumentoResumenDTO> lista1 = (List<InstrumentoResumenDTO>)Session["listaHabilitado"];
                List<InstrumentoResumenDTO> listaBusqueda1 = new List<InstrumentoResumenDTO>();
                listaBusqueda1 = lista1.FindAll(x => x.NumSerie.ToLower().Contains(txtBuscar1.Text.ToLower()));
                gvHabilitados.DataSource = listaBusqueda1;
                gvHabilitados.DataBind();

                LimpiarCampoBuscar();
            }
            catch (Exception ex)
            {
                alertaNoSatisfactoria.InnerText = "El instrumento no ha sido encontrado debido a un error: " + ex.Message;
                MostrarNotificacionRoja();

                LimpiarCampoBuscar();
            }
        }

        protected void LimpiarCampoBuscar()
        {
            txtBuscar.Text = string.Empty;
            txtBuscar1.Text = string.Empty;
        }

        protected void lnkBtnActualizar_Click(object sender, EventArgs e)
        {
            InstrumentoNegocio noHabilitadoNegocio = new InstrumentoNegocio();
            Session.Add("listaNoHabilitado", noHabilitadoNegocio.ListarNoHabilitados());
            gvNoHabilitados.DataSource = Session["listaNoHabilitado"];
            gvNoHabilitados.DataBind();
        }

        protected void lnkBtnActualizar1_Click(object sender, EventArgs e)
        {
            InstrumentoNegocio noHabilitadoNegocio = new InstrumentoNegocio();
            Session.Add("listaHabilitado", noHabilitadoNegocio.ListarHabilitados());
            gvHabilitados.DataSource = Session["listaHabilitado"];
            gvHabilitados.DataBind();
        }

    }
}