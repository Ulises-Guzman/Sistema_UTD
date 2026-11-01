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
    public partial class FormularioUnidadMedida : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<BreadcrumbItem> listaRutas = new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Titulo = "Inicio", Url = "~/Default.aspx", EsActivo = false },
                    new BreadcrumbItem { Titulo = "Gestión de Componentes Asociados", Url = "~/GestionInstrumentos/CompAsocModelos.aspx", EsActivo = false},
                    new BreadcrumbItem { Titulo = "Modificar Unidad de Medida", Url = "", EsActivo = true}
                };

                if (Master != null)
                {
                    Master.ActualizarBreadcrumb(listaRutas);
                }

                // Cargar el gv con la lista de unidades de medida
                UnidadMedidaNegocio unidadMedidaNegocio = new UnidadMedidaNegocio();
                Session.Add("listaUnidadMedida", unidadMedidaNegocio.ListarUnidadMedida());
                gvUnidadesMedida.DataSource = Session["listaUnidadMedida"];
                gvUnidadesMedida.DataBind();
            }
        }

        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BloquearNotificacion();

            try
            {
                //// 1. INYECCIÓN DE PRUEBA: Fuerza el error inmediatamente
                //throw new Exception("Simulación de fallo crítico para probar la alerta roja.");

                List<UnidadMedidaResumenDTO> lista = (List<UnidadMedidaResumenDTO>)Session["listaUnidadMedida"];
                List<UnidadMedidaResumenDTO> listaBusqueda = new List<UnidadMedidaResumenDTO>();
                listaBusqueda = lista.FindAll(x => x.Descripcion.ToLower().Contains(txtBuscar.Text.ToLower()));
                gvUnidadesMedida.DataSource = listaBusqueda;
                gvUnidadesMedida.DataBind();

            }
            catch (Exception ex)
            {
                alertaNoSatisfactoria.InnerText = "La unidad de medida no ha sido encontrada debido a un error: " + ex.Message;
                MostraNotificacionRoja();
            }
        }
        protected void lnkBtnActualizar_Click(object sender, EventArgs e)
        {
            BloquearNotificacion();
            ActualizarGrilla();
        }

        protected void gvUnidadesMedida_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //// 1. INYECCIÓN DE PRUEBA: Fuerza el error inmediatamente
                //throw new Exception("Simulación de fallo crítico para probar la alerta roja.");

                BloquearNotificacion();

                List<UnidadMedidaResumenDTO> lista = (List<UnidadMedidaResumenDTO>)Session["listaUnidadMedida"];

                int id = int.Parse(gvUnidadesMedida.SelectedDataKey.Value.ToString());

                UnidadMedidaResumenDTO unidadMedidaSeleccionada = lista.Find(x => x.Id == id);

                if (unidadMedidaSeleccionada == null)
                {
                    throw new Exception("El registro ya no se encuentra en la memoria o la sesión expiró.");
                }

                Session.Add("unidadMedidaSeleccionada", unidadMedidaSeleccionada);

                // Cargar datos a los TextBox
                txtUnidadMedida.Text = unidadMedidaSeleccionada.Descripcion;

                // Activar o Desactivar por asignación directa
                chkUnidadMedidaActivo.Checked = unidadMedidaSeleccionada.Activo;
                txtUnidadMedida.Enabled = unidadMedidaSeleccionada.Activo;

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "AbrirModal", "abrirModalUnidadMedida();", true);
            }
            catch (Exception ex)
            {
                alertaNoSatisfactoria.InnerText = "La unidad de medida no ha sido encontrada debido a un error: " + ex.Message;
                MostraNotificacionRoja();

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LimpiarFondo", "limpiarFondoModal();", true);
            }
        }

        protected void gvUnidadesMedida_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUnidadesMedida.PageIndex = e.NewPageIndex;
            EnlazarPaginaGridView(sender, e);
            gvUnidadesMedida.DataBind();

            BloquearNotificacion();
        }

        protected void EnlazarPaginaGridView(object sender, EventArgs e)
        {
            UnidadMedidaNegocio unidadMedidaNegocio = new UnidadMedidaNegocio();
            Session.Add("listaUnidadMedida", unidadMedidaNegocio.ListarUnidadMedida());
            gvUnidadesMedida.DataSource = Session["listaUnidadMedida"];
            gvUnidadesMedida.DataBind();
        }

        protected void btnAceptarModificarUnidadMedida_Click(object sender, EventArgs e)
        {
            // INYECCIÓN DE PRUEBA: Pausa el hilo del servidor por 2 segundos (2000 milisegundos)
            //System.Threading.Thread.Sleep(6000);

            //// 1. INYECCIÓN DE PRUEBA: Fuerza el error inmediatamente
            //throw new Exception("Simulación de fallo crítico para probar la alerta roja.");

            UsuarioResumenDTO usuarioLogueado = (UsuarioResumenDTO)Session["UsuarioLogueado"];

            UnidadMedidaResumenDTO unidadMedidaSeleccionada = (UnidadMedidaResumenDTO)Session["unidadMedidaSeleccionada"];

            UnidadMedidaNegocio unidadMedidaNegocio = new UnidadMedidaNegocio();

            if (!chkUnidadMedidaActivo.Checked)
            {
                unidadMedidaNegocio.DesactivarUnidadMedida(unidadMedidaSeleccionada.Id, usuarioLogueado.Id);

                MostrarNotificacionVerde();

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LimpiarFondo", "limpiarFondoModal();", true);
            }
            else
            {
                UnidadMedidaEdicionDTO modificadaUnidadMedidaDTO = new UnidadMedidaEdicionDTO();
                modificadaUnidadMedidaDTO.Id = unidadMedidaSeleccionada.Id;
                modificadaUnidadMedidaDTO.Descripcion = txtUnidadMedida.Text.Trim();
                modificadaUnidadMedidaDTO.Activo = chkUnidadMedidaActivo.Checked;

                unidadMedidaNegocio.ModificarUnidadMedida(modificadaUnidadMedidaDTO, usuarioLogueado.Id);

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
            txtUnidadMedida.Text = string.Empty;
        }

        protected void ActualizarGrilla()
        {
            UnidadMedidaNegocio unidadMedidaNegocio = new UnidadMedidaNegocio();
            Session.Add("listaUnidadMedida", unidadMedidaNegocio.ListarUnidadMedida());
            gvUnidadesMedida.DataSource = Session["listaUnidadMedida"];
            gvUnidadesMedida.DataBind();
        }

    }
}