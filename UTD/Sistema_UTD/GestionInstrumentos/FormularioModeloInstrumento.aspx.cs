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
    public partial class FormularioModeloInstrumento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<BreadcrumbItem> listaRutas = new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Titulo = "Inicio", Url = "~/Default.aspx", EsActivo = false },
                    new BreadcrumbItem { Titulo = "Gestión de Instrumentos", Url = "GestionInstrumentos/ModelosInstrumento.aspx", EsActivo = false},
                    new BreadcrumbItem { Titulo = "Modificar Modelo de Instrumento", Url = "", EsActivo = true}
                };

                if (Master != null)
                {
                    Master.ActualizarBreadcrumb(listaRutas);
                }


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

                // Cargar los txt y selección de dll's
                ModeloInstrumentoResumenDTO modeloSeleccionado = (ModeloInstrumentoResumenDTO)Session["modeloSeleccionado"];
                txtNumeroParte.Text = modeloSeleccionado.NumParte;
                txtNomenclatura.Text = modeloSeleccionado.Nomenclatura;
                txtMarca.Text = modeloSeleccionado.Marca;
                ddlModelo.SelectedValue = modeloSeleccionado.ModeloId.ToString();
                ddlRango.SelectedValue = modeloSeleccionado.RangoId.ToString();
                ddlUnidadMedida.SelectedValue = modeloSeleccionado.UnidadMedidaId.ToString();
                chkModeloInstrumentoActivo.Checked = modeloSeleccionado.Activo;

                // Activar o Desactivar por asignación directa
                chkModeloInstrumentoActivo.Checked = modeloSeleccionado.Activo;
                txtNumeroParte.Enabled = modeloSeleccionado.Activo;
                txtNomenclatura.Enabled = modeloSeleccionado.Activo;
                txtMarca.Enabled = modeloSeleccionado.Activo;
                ddlModelo.Enabled = modeloSeleccionado.Activo;
                ddlRango.Enabled = modeloSeleccionado.Activo;
                ddlUnidadMedida.Enabled = modeloSeleccionado.Activo;

            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            ReestablecerCampos();
            Response.Redirect("~/GestionInstrumentos/ModelosInstrumento.aspx", false);
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            // INYECCIÓN DE PRUEBA: Pausa el hilo del servidor por 2 segundos (2000 milisegundos)
            //System.Threading.Thread.Sleep(6000);

            //// 1. INYECCIÓN DE PRUEBA: Fuerza el error inmediatamente
            //throw new Exception("Simulación de fallo crítico para probar la alerta roja.");
            //if (!IsPostBack)
            //{
            UsuarioResumenDTO usuarioLogueado = (UsuarioResumenDTO)Session["UsuarioLogueado"];

            ModeloInstrumentoResumenDTO modeloSeleccionado = (ModeloInstrumentoResumenDTO)Session["modeloSeleccionado"];

            ModeloInstrumentoNegocio modeloNegocio = new ModeloInstrumentoNegocio();

            if (!chkModeloInstrumentoActivo.Checked)
            {
                modeloNegocio.DesactivarModeloInstrumento(modeloSeleccionado.Id, usuarioLogueado.Id);

                MostrarNotificacionVerde();

                

                // Delega la redirección a JavaScript con una pausa de 3 segundos (3000 ms)
                string scriptRedirect = "setTimeout(function() { window.location.href = '/GestionInstrumentos/ModelosInstrumento.aspx'; }, 3000);";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "RedirigirDesactivar", scriptRedirect, true);
                
                //Response.Redirect("~/GestionInstrumentos/ModelosInstrumento.aspx", false);
            }
            else
            {
                ModeloInstrumentoEdicionDTO modificadoModeloDTO = new ModeloInstrumentoEdicionDTO
                {
                    Id = modeloSeleccionado.Id,
                    NumParte = txtNumeroParte.Text.Trim(),
                    Nomenclatura = txtNomenclatura.Text.Trim(),
                    Marca = txtMarca.Text.Trim(),
                    ModeloId = int.Parse(ddlModelo.SelectedValue),
                    RangoId = int.Parse(ddlRango.SelectedValue),
                    UnidadMedidaId = int.Parse(ddlUnidadMedida.SelectedValue),
                    Activo = chkModeloInstrumentoActivo.Checked
                };

                modeloNegocio.ModificarModeloInstrumento(modificadoModeloDTO, usuarioLogueado.Id);

                Session.Add("modificado", true);

                //MostrarNotificacionVerde();

                ReestablecerCampos();

                // Delega la redirección a JavaScript con una pausa de 3 segundos (3000 ms)
                //string scriptRedirect = "setTimeout(function() { window.location.href = '/GestionInstrumentos/ModelosInstrumento.aspx'; }, 3000);";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "RedirigirModificar", scriptRedirect, true);

                Response.Redirect("~/GestionInstrumentos/ModelosInstrumento.aspx", false);
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

        protected void LimpiarCampos()
        {
            txtNumeroParte.Text = string.Empty;
            txtNomenclatura.Text = string.Empty;
            txtMarca.Text = string.Empty;
            ddlModelo.SelectedIndex = 0;
            ddlRango.SelectedIndex = 0;
            ddlModelo.SelectedIndex = 0;
        }

        protected void ReestablecerCampos()
        {
            if (chkModeloInstrumentoActivo.Checked)
            {
                txtNumeroParte.Text = string.Empty;
                txtNomenclatura.Text = string.Empty;
                txtMarca.Text = string.Empty;
                ddlModelo.SelectedIndex = 0;
                ddlRango.SelectedIndex = 0;
                ddlModelo.SelectedIndex = 0;

                txtNumeroParte.Enabled = true;
                txtNomenclatura.Enabled = true;
                txtMarca.Enabled = true;
                ddlModelo.Enabled = true;
                ddlRango.Enabled = true;
                ddlModelo.Enabled = true;
            }
        }
    }
}