using Sistema_UTD.Admin;
using Sistema_UTD.Dominio;
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
    public partial class ModeloInstrumento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<BreadcrumbItem> listaRutas = new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Titulo = "Inicio", Url = "~/Default.aspx", EsActivo = false },
                    new BreadcrumbItem { Titulo = "Gestión Modelos de Instrumentos", Url = "", EsActivo = true }
                };

                if (Master != null)
                {
                    Master.ActualizarBreadcrumb(listaRutas);
                }

                VerificarModificado();

                // Cargar el gv con la lista de modelos de instrumentos
                ModeloInstrumentoNegocio modeloInstrumentoNegocio = new ModeloInstrumentoNegocio();
                Session.Add("listaModeloInstrumento", modeloInstrumentoNegocio.ListarModeloInstrumento());
                gvModelosInstrumento.DataSource = Session["listaModeloInstrumento"];
                gvModelosInstrumento.DataBind();

                // Cargar los ddl's
                ModeloNegocio modeloNegocio = new ModeloNegocio();
                Session.Add("listaModelo", modeloNegocio.ListarModelo());
                ddlModelo.DataSource = Session["listaModelo"];
                ddlModelo.DataTextField = "Descripcion";
                ddlModelo.DataValueField = "Id";
                ddlModelo.DataBind();
                ddlModelo.Items.Insert(0, new ListItem("Seleccione...", "0"));

                RangoNegocio rangoNegocio = new RangoNegocio();
                Session.Add("listaRango", rangoNegocio.ListarRango());
                ddlRango.DataSource = Session["listaRango"];
                ddlRango.DataTextField = "Descripcion";
                ddlRango.DataValueField = "Id";
                ddlRango.DataBind();
                ddlRango.Items.Insert(0, new ListItem("Seleccione...", "0"));

                UnidadMedidaNegocio unidadMedidaNegocio = new UnidadMedidaNegocio();
                Session.Add("listaUnidadMedida", unidadMedidaNegocio.ListarUnidadMedida());
                ddlUnidadMedida.DataSource = Session["listaUnidadMedida"];
                ddlUnidadMedida.DataTextField = "Descripcion";
                ddlUnidadMedida.DataValueField = "Id";
                ddlUnidadMedida.DataBind();
                ddlUnidadMedida.Items.Insert(0, new ListItem("Seleccione...", "0"));
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                ////// INYECCIÓN DE PRUEBA: Pausa el hilo del servidor por 2 segundos (2000 milisegundos)
                //System.Threading.Thread.Sleep(6000);

                //// 1. inyección de prueba: fuerza el error inmediatamente
                //throw new Exception("simulación de fallo crítico para probar la alerta roja.");

                UsuarioResumenDTO usuarioActual = (UsuarioResumenDTO)Session["UsuarioLogueado"];
                ModeloInstrumentoNegocio modeloInstrumentoNegocio = new ModeloInstrumentoNegocio();

                ModeloInstrumentoCreacionDTO nuevoModeloInstrumentoDTO = new ModeloInstrumentoCreacionDTO
                {
                    NumParte = txtNumeroParte.Text.Trim(),
                    Nomenclatura = txtNomenclatura.Text.Trim(),
                    Marca = txtMarca.Text.Trim(),
                    ModeloId = int.Parse(ddlModelo.SelectedValue),
                    RangoId = int.Parse(ddlRango.SelectedValue),
                    UnidadMedidaId = int.Parse(ddlUnidadMedida.SelectedValue)
                };

                modeloInstrumentoNegocio.AgregarModeloInstrumento(nuevoModeloInstrumentoDTO, usuarioActual.Id);

                MostrarNotificacionVerde();

                LimpiarCampos();

                ActualizarGrilla();
            }
            catch (Exception ex)
            {
                alertaNoSatisfactoria.InnerText = "El modelo de instrumento no ha sido creado debido a un error: " + ex.Message;
                MostrarNotificacionRoja();
            }
        }

        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BloquearNotificacion();

            try
            {
                //// 1. INYECCIÓN DE PRUEBA: Fuerza el error inmediatamente
                //throw new Exception("Simulación de fallo crítico para probar la alerta roja.");

                List<ModeloInstrumentoResumenDTO> lista = (List<ModeloInstrumentoResumenDTO>)Session["listaModeloInstrumento"];
                List<ModeloInstrumentoResumenDTO> listaBusqueda = new List<ModeloInstrumentoResumenDTO>();
                listaBusqueda = lista.FindAll(x => x.Nomenclatura.ToLower().Contains(txtBuscar.Text.ToLower()));
                gvModelosInstrumento.DataSource = listaBusqueda;
                gvModelosInstrumento.DataBind();

                LimpiarCampoBuscar();
            }
            catch (Exception ex)
            {
                alertaNoSatisfactoria.InnerText = "El modelo de instrumento no ha sido encontrado debido a un error: " + ex.Message;
                MostrarNotificacionRoja();

                LimpiarCampoBuscar();
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

        protected void LimpiarCampoBuscar()
        {
            txtBuscar.Text = string.Empty;
        }

        protected void LimpiarCampos()
        {
            txtNumeroParte.Text = string.Empty;
            txtNomenclatura.Text = string.Empty;
            txtMarca.Text = string.Empty;
            ddlModelo.SelectedIndex = 0;
            ddlRango.SelectedIndex = 0;
            ddlUnidadMedida.SelectedIndex = 0;
        }

        protected void lnkBtnActualizar_Click(object sender, EventArgs e)
        {
            BloquearNotificacion();
            ActualizarGrilla();
        }

        protected void ActualizarGrilla()
        {
            ModeloInstrumentoNegocio modeloInstrumentoNegocio = new ModeloInstrumentoNegocio();
            Session.Add("listaModeloInstrumento", modeloInstrumentoNegocio.ListarModeloInstrumento());
            gvModelosInstrumento.DataSource = Session["listaModeloInstrumento"];
            gvModelosInstrumento.DataBind();
        }

        protected void gvModelosInstrumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //// 1. INYECCIÓN DE PRUEBA: Fuerza el error inmediatamente
                //throw new Exception("Simulación de fallo crítico para probar la alerta roja.");

                BloquearNotificacion();

                List<ModeloInstrumentoResumenDTO> lista = (List<ModeloInstrumentoResumenDTO>)Session["listaModeloInstrumento"];

                int id = int.Parse(gvModelosInstrumento.SelectedDataKey.Value.ToString());

                ModeloInstrumentoResumenDTO modeloSeleccionado = lista.Find(x => x.Id == id);

                if (modeloSeleccionado == null)
                {
                    throw new Exception("El registro ya no se encuentra en la memoria o la sesión expiró.");
                }

                Session.Add("modeloSeleccionado", modeloSeleccionado);

                Response.Redirect("~/GestionInstrumentos/FormularioModeloInstrumento.aspx", false);

            }
            catch (Exception ex)
            {
                alertaNoSatisfactoria.InnerText = "El estado no ha sido encontrado debido a un error: " + ex.Message;
                MostrarNotificacionRoja();
            }
        }

        protected void gvModelosInstrumento_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvModelosInstrumento.PageIndex = e.NewPageIndex;
            EnlazarPaginaGridView(sender, e);
            gvModelosInstrumento.DataBind();

            BloquearNotificacion();
        }

        protected void EnlazarPaginaGridView(object sender, EventArgs e)
        {
            ModeloInstrumentoNegocio modeloInstrumentoNegocio = new ModeloInstrumentoNegocio();
            Session.Add("listaModeloInstrumento", modeloInstrumentoNegocio.ListarModeloInstrumento());
            gvModelosInstrumento.DataSource = Session["listaModeloInstrumento"];
            gvModelosInstrumento.DataBind();
        }

        protected void VerificarModificado()
        {
            // 1. Validar que la variable de sesión exista antes de leerla
            if (Session["modificado"] != null)
            {
                // 2. Ahora es seguro convertir el valor a bool
                bool esModificado = (bool)Session["modificado"];

                if (esModificado)
                {
                    alertaSatisfactoria.InnerText = "¡Los datos del modelo de instrumento han sido modificados!";
                    MostrarNotificacionVerde();

                    // 3. Eliminar la variable para evitar el "efecto fantasma" al recargar la página
                    Session.Remove("modificado");
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "CerrarNotificacion", "ocultarNotificaciones();", true);

                // Reestablece el alerta a su estado default
                alertaSatisfactoria.InnerText = "¡El nuevo modelo de instruemnto ha sido creado!";
            }
        }
    }
}