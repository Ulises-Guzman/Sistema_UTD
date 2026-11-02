using Sistema_UTD.Dominio.DTOs;
using Sistema_UTD.Helpers;
using Sistema_UTD.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistema_UTD.GestionInstrumentos
{
    public partial class FormularioTiempoHabilitacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<BreadcrumbItem> listaRutas = new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Titulo = "Inicio", Url = "~/Default.aspx", EsActivo = false },
                    new BreadcrumbItem { Titulo = "Gestión de Componentes Asociados", Url = "~/GestionInstrumentos/CompAsocInstrumentos.aspx", EsActivo = false},
                    new BreadcrumbItem { Titulo = "Modificar Tiempo de Habilitación", Url = "", EsActivo = true}
                };

                if (Master != null)
                {
                    Master.ActualizarBreadcrumb(listaRutas);
                }

                // Cargar el gv con la lista de tiempo de habilitación
                TiempoHabilitacionNegocio tiempoNegocio = new TiempoHabilitacionNegocio();
                Session.Add("listaTiempo", tiempoNegocio.ListarTiempo());
                gvTiempos.DataSource = Session["listaTiempo"];
                gvTiempos.DataBind();
            }
        }

        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BloquearNotificacion();

            try
            {
                //// 1. INYECCIÓN DE PRUEBA: Fuerza el error inmediatamente
                //throw new Exception("Simulación de fallo crítico para probar la alerta roja.");

                List<TiempoHabilitacionResumenDTO> lista = (List<TiempoHabilitacionResumenDTO>)Session["listaTiempo"];
                List<TiempoHabilitacionResumenDTO> listaBusqueda = new List<TiempoHabilitacionResumenDTO>();
                //listaBusqueda = lista.FindAll(x => x.CantidadMeses.Contains(int.Parse(txtBuscar.Text)));
                if (int.TryParse(txtBuscar.Text, out int mesesBuscados))
                {
                    listaBusqueda = lista.FindAll(x => x.CantidadMeses == mesesBuscados);
                }
                else
                {
                    ActualizarGrilla();
                }
                gvTiempos.DataSource = listaBusqueda;
                gvTiempos.DataBind();

            }
            catch (Exception ex)
            {
                alertaNoSatisfactoria.InnerText = "El tiempo de habilitación no ha sido encontrado debido a un error: " + ex.Message;
                MostraNotificacionRoja();
            }
        }

        protected void lnkBtnActualizar_Click(object sender, EventArgs e)
        {
            BloquearNotificacion();
            ActualizarGrilla();
        }

        protected void gvTiempos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //// 1. INYECCIÓN DE PRUEBA: Fuerza el error inmediatamente
                //throw new Exception("Simulación de fallo crítico para probar la alerta roja.");

                BloquearNotificacion();

                List<TiempoHabilitacionResumenDTO> lista = (List<TiempoHabilitacionResumenDTO>)Session["listaTiempo"];

                int id = int.Parse(gvTiempos.SelectedDataKey.Value.ToString());

                TiempoHabilitacionResumenDTO tiempoSeleccionado = lista.Find(x => x.Id == id);

                if (tiempoSeleccionado == null)
                {
                    throw new Exception("El registro ya no se encuentra en la memoria o la sesión expiró.");
                }

                Session.Add("tiempoSeleccionado", tiempoSeleccionado);

                // Cargar datos a los TextBox
                txtTiempo.Text = tiempoSeleccionado.CantidadMeses.ToString();

                // Activar o Desactivar por asignación directa
                chkTiempoActivo.Checked = tiempoSeleccionado.Activo;
                txtTiempo.Enabled = tiempoSeleccionado.Activo;

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "AbrirModal", "abrirModalTiempo();", true);
            }
            catch (Exception ex)
            {
                alertaNoSatisfactoria.InnerText = "La sección no ha sido encontrado debido a un error: " + ex.Message;
                MostraNotificacionRoja();

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LimpiarFondo", "limpiarFondoModal();", true);
            }
        }

        protected void gvTiempos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTiempos.PageIndex = e.NewPageIndex;
            EnlazarPaginaGridView(sender, e);
            gvTiempos.DataBind();

            BloquearNotificacion();
        }

        protected void EnlazarPaginaGridView(object sender, EventArgs e)
        {
            TiempoHabilitacionNegocio tiempoNegocio = new TiempoHabilitacionNegocio();
            Session.Add("listaTiempo", tiempoNegocio.ListarTiempo());
            gvTiempos.DataSource = Session["listaTiempo"];
            gvTiempos.DataBind();
        }

        protected void btnAceptarModificarTiempo_Click(object sender, EventArgs e)
        {
            // INYECCIÓN DE PRUEBA: Pausa el hilo del servidor por 2 segundos (2000 milisegundos)
            //System.Threading.Thread.Sleep(6000);

            //// 1. INYECCIÓN DE PRUEBA: Fuerza el error inmediatamente
            //throw new Exception("Simulación de fallo crítico para probar la alerta roja.");

            UsuarioResumenDTO usuarioLogueado = (UsuarioResumenDTO)Session["UsuarioLogueado"];

            TiempoHabilitacionResumenDTO tiempoSeleccionado = (TiempoHabilitacionResumenDTO)Session["tiempoSeleccionado"];

            TiempoHabilitacionNegocio tiempoNegocio = new TiempoHabilitacionNegocio();

            if (!chkTiempoActivo.Checked)
            {
                tiempoNegocio.DesactivarTiempo(tiempoSeleccionado.Id, usuarioLogueado.Id);

                MostrarNotificacionVerde();

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LimpiarFondo", "limpiarFondoModal();", true);
            }
            else
            {
                TiempoHabilitacionEdicionDTO modificadoTiempoDTO = new TiempoHabilitacionEdicionDTO();
                modificadoTiempoDTO.Id = tiempoSeleccionado.Id;
                modificadoTiempoDTO.CantidadMeses = int.Parse(txtTiempo.Text);
                modificadoTiempoDTO.Activo = chkTiempoActivo.Checked;

                tiempoNegocio.ModificarTiempo(modificadoTiempoDTO, usuarioLogueado.Id);

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
            txtTiempo.Text = string.Empty;
        }

        protected void ActualizarGrilla()
        {
            TiempoHabilitacionNegocio tiempoNegocio = new TiempoHabilitacionNegocio();
            Session.Add("listaTiempo", tiempoNegocio.ListarTiempo());
            gvTiempos.DataSource = Session["listaTiempo"];
            gvTiempos.DataBind();
        }
    }
}