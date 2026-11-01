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
    public partial class FormularioDestino : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<BreadcrumbItem> listaRutas = new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Titulo = "Inicio", Url = "~/Default.aspx", EsActivo = false },
                    new BreadcrumbItem { Titulo = "Gestión de Componentes Asociados", Url = "~/GestionInstrumentos/CompAsocInstrumentos.aspx", EsActivo = false},
                    new BreadcrumbItem { Titulo = "Modificar Destino", Url = "", EsActivo = true}
                };

                if (Master != null)
                {
                    Master.ActualizarBreadcrumb(listaRutas);
                }

                // Cargar el gv con la lista de destinos
                DestinoNegocio destinoNegocio = new DestinoNegocio();
                Session.Add("listaDestino", destinoNegocio.ListarDestino());
                gvDestinos.DataSource = Session["listaDestino"];
                gvDestinos.DataBind();
            }
        }

        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BloquearNotificacion();

            try
            {
                //// 1. INYECCIÓN DE PRUEBA: Fuerza el error inmediatamente
                //throw new Exception("Simulación de fallo crítico para probar la alerta roja.");

                List<DestinoResumenDTO> lista = (List<DestinoResumenDTO>)Session["listaDestino"];
                List<DestinoResumenDTO> listaBusqueda = new List<DestinoResumenDTO>();
                listaBusqueda = lista.FindAll(x => x.Descripcion.ToLower().Contains(txtBuscar.Text.ToLower()));
                gvDestinos.DataSource = listaBusqueda;
                gvDestinos.DataBind();

            }
            catch (Exception ex)
            {
                alertaNoSatisfactoria.InnerText = "El destino no ha sido encontrado debido a un error: " + ex.Message;
                MostraNotificacionRoja();
            }
        }

        protected void lnkBtnActualizar_Click(object sender, EventArgs e)
        {
            BloquearNotificacion();
            ActualizarGrilla();
        }

        protected void gvDestinos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //// 1. INYECCIÓN DE PRUEBA: Fuerza el error inmediatamente
                //throw new Exception("Simulación de fallo crítico para probar la alerta roja.");

                BloquearNotificacion();

                List<DestinoResumenDTO> lista = (List<DestinoResumenDTO>)Session["listaDestino"];

                int id = int.Parse(gvDestinos.SelectedDataKey.Value.ToString());

                DestinoResumenDTO destinoSeleccionado = lista.Find(x => x.Id == id);

                if (destinoSeleccionado == null)
                {
                    throw new Exception("El registro ya no se encuentra en la memoria o la sesión expiró.");
                }

                Session.Add("destinoSeleccionado", destinoSeleccionado);

                // Cargar datos a los TextBox
                txtCuatrigrama.Text = destinoSeleccionado.Cuatrigrama;
                txtDestino.Text = destinoSeleccionado.Descripcion;

                // Activar o Desactivar por asignación directa
                chkDestinoActivo.Checked = destinoSeleccionado.Activo;
                txtCuatrigrama.Enabled = destinoSeleccionado.Activo;
                txtDestino.Enabled = destinoSeleccionado.Activo;

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "AbrirModal", "abrirModalDestino();", true);
            }
            catch (Exception ex)
            {
                alertaNoSatisfactoria.InnerText = "El destino no ha sido encontrado debido a un error: " + ex.Message;
                MostraNotificacionRoja();

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LimpiarFondo", "limpiarFondoModal();", true);
            }
        }

        protected void gvDestinos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDestinos.PageIndex = e.NewPageIndex;
            EnlazarPaginaGridView(sender, e);
            gvDestinos.DataBind();

            BloquearNotificacion();
        }

        protected void EnlazarPaginaGridView(object sender, EventArgs e)
        {
            DestinoNegocio destinoNegocio = new DestinoNegocio();
            Session.Add("listaDestino", destinoNegocio.ListarDestino());
            gvDestinos.DataSource = Session["listaRango"];
            gvDestinos.DataBind();
        }

        protected void btnAceptarModificarDestino_Click(object sender, EventArgs e)
        {
            // INYECCIÓN DE PRUEBA: Pausa el hilo del servidor por 2 segundos (2000 milisegundos)
            //System.Threading.Thread.Sleep(6000);

            //// 1. INYECCIÓN DE PRUEBA: Fuerza el error inmediatamente
            //throw new Exception("Simulación de fallo crítico para probar la alerta roja.");

            UsuarioResumenDTO usuarioLogueado = (UsuarioResumenDTO)Session["UsuarioLogueado"];

            DestinoResumenDTO destinoSeleccionado = (DestinoResumenDTO)Session["destinoSeleccionado"];

            DestinoNegocio destinoNegocio = new DestinoNegocio();

            if (!chkDestinoActivo.Checked)
            {
                destinoNegocio.DesactivarDestino(destinoSeleccionado.Id, usuarioLogueado.Id);

                MostrarNotificacionVerde();

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LimpiarFondo", "limpiarFondoModal();", true);
            }
            else
            {
                DestinoEdicionDTO modificadoDestinoDTO = new DestinoEdicionDTO();
                modificadoDestinoDTO.Id = destinoSeleccionado.Id;
                modificadoDestinoDTO.Cuatrigrama = txtCuatrigrama.Text.ToUpper();
                modificadoDestinoDTO.Descripcion = txtDestino.Text.Trim();
                modificadoDestinoDTO.Activo = chkDestinoActivo.Checked;

                destinoNegocio.ModificarDestino(modificadoDestinoDTO, usuarioLogueado.Id);

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
            txtCuatrigrama.Text = string.Empty;
            txtDestino.Text = string.Empty;
        }

        protected void ActualizarGrilla()
        {
            DestinoNegocio destinoNegocio = new DestinoNegocio();
            Session.Add("listaDestino", destinoNegocio.ListarDestino());
            gvDestinos.DataSource = Session["listaDestino"];
            gvDestinos.DataBind();
        }
    }
}