using Sistema_UTD.Admin;
using Sistema_UTD.Dominio.Entidades;
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
    public partial class FormularioModelo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<BreadcrumbItem> listaRutas = new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Titulo = "Inicio", Url = "~/Default.aspx", EsActivo = false },
                    new BreadcrumbItem { Titulo = "Gestión de componentes asociados", Url = "~/GestionInstrumentos/CompAsocModelos.aspx", EsActivo = false},
                    new BreadcrumbItem { Titulo = "Modificar Modelo", Url = "", EsActivo = true}
                };

                if (Master != null)
                {
                    Master.ActualizarBreadcrumb(listaRutas);
                }

                // Cargar el gv con la lista de modelos
                ModeloNegocio modeloNegocio = new ModeloNegocio();
                Session.Add("listaModelo", modeloNegocio.ListarModelo());
                gvModelos.DataSource = Session["listaModelo"];
                gvModelos.DataBind();
            }

        }

        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BloquearNotificacion();

            try
            {
                //// 1. INYECCIÓN DE PRUEBA: Fuerza el error inmediatamente
                //throw new Exception("Simulación de fallo crítico para probar la alerta roja.");

                List<ModeloResumenDTO> lista = (List<ModeloResumenDTO>)Session["listaModelo"];
                List<ModeloResumenDTO> listaBusqueda = new List<ModeloResumenDTO>();
                listaBusqueda = lista.FindAll(x => x.Descripcion.ToLower().Contains(txtBuscar.Text.ToLower()));
                gvModelos.DataSource = listaBusqueda;
                gvModelos.DataBind();

            }
            catch (Exception ex)
            {
                alertaNoSatisfactoria.InnerText = "El modelo no ha sido encontrado debido a un error: " + ex.Message;
                MostraNotificacionRoja();
            }
        }

        protected void lnkBtnActualizar_Click(object sender, EventArgs e)
        {
            BloquearNotificacion();
            ActualizarGrilla();
        }

        protected void gvModelos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //// 1. INYECCIÓN DE PRUEBA: Fuerza el error inmediatamente
                //throw new Exception("Simulación de fallo crítico para probar la alerta roja.");

                BloquearNotificacion();

                List<ModeloResumenDTO> lista = (List<ModeloResumenDTO>)Session["listaModelo"];

                int id = int.Parse(gvModelos.SelectedDataKey.Value.ToString());

                ModeloResumenDTO modeloSeleccionado = lista.Find(x => x.Id == id);

                if (modeloSeleccionado == null)
                {
                    throw new Exception("El registro ya no se encuentra en la memoria o la sesión expiró.");
                }

                Session.Add("modeloSeleccionado", modeloSeleccionado);

                // Cargar datos a los TextBox
                txtModelo.Text = modeloSeleccionado.Descripcion;

                // Activar o Desactivar por asignación directa
                chkModeloActivo.Checked = modeloSeleccionado.Activo;
                txtModelo.Enabled = modeloSeleccionado.Activo;

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "AbrirModal", "abrirModalModelo();", true);
            }
            catch (Exception ex)
            {
                alertaNoSatisfactoria.InnerText = "El modelo no ha sido encontrado debido a un error: " + ex.Message;
                MostraNotificacionRoja();

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LimpiarFondo", "limpiarFondoModal();", true);
            }
        }

        protected void gvModelos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvModelos.PageIndex = e.NewPageIndex;
            EnlazarPaginaGridView(sender, e);
            gvModelos.DataBind();

            BloquearNotificacion();
        }

        protected void EnlazarPaginaGridView(object sender, EventArgs e)
        {
            ModeloNegocio modeloNegocio = new ModeloNegocio();
            Session.Add("listaModelo", modeloNegocio.ListarModelo());
            gvModelos.DataSource = Session["listaModelo"];
            gvModelos.DataBind();
        }

        protected void ActualizarGrilla()
        {
            ModeloNegocio modeloNegocio = new ModeloNegocio();
            Session.Add("listaModelo", modeloNegocio.ListarModelo());
            gvModelos.DataSource = Session["listaModelo"];
            gvModelos.DataBind();
            txtBuscar.Text = string.Empty;
        }

        protected void btnAceptarModificarModelo_Click(object sender, EventArgs e)
        {
            // INYECCIÓN DE PRUEBA: Pausa el hilo del servidor por 2 segundos (2000 milisegundos)
            //System.Threading.Thread.Sleep(6000);

            //// 1. INYECCIÓN DE PRUEBA: Fuerza el error inmediatamente
            //throw new Exception("Simulación de fallo crítico para probar la alerta roja.");

            UsuarioResumenDTO usuarioLogueado = (UsuarioResumenDTO)Session["UsuarioLogueado"];

            ModeloResumenDTO modeloSeleccionado = (ModeloResumenDTO)Session["modeloSeleccionado"];

            ModeloNegocio modeloNegocio = new ModeloNegocio();

            if (!chkModeloActivo.Checked)
            {
                modeloNegocio.DesactivarModelo(modeloSeleccionado.Id, usuarioLogueado.Id);

                MostrarNotificacionVerde();

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LimpiarFondo", "limpiarFondoModal();", true);
            }
            else
            {
                ModeloEdicionDTO modificadoModeloDTO = new ModeloEdicionDTO();
                modificadoModeloDTO.Id = modeloSeleccionado.Id;
                modificadoModeloDTO.Descripcion = txtModelo.Text.Trim();

                modeloNegocio.ModificarModelo(modificadoModeloDTO, usuarioLogueado.Id);

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
            txtModelo.Text = string.Empty;
        }

    } // TODO: Utilizar DTO
}