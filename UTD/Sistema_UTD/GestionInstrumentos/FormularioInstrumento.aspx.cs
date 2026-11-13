using Sistema_UTD.Dominio.DTOs;
using Sistema_UTD.Helpers;
using Sistema_UTD.Negocio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistema_UTD.GestionInstrumentos
{
    public partial class FormularioInstrumento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<BreadcrumbItem> listaRutas = new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Titulo = "Inicio", Url = "~/Default.aspx", EsActivo = false },
                    new BreadcrumbItem { Titulo = "Gestión de Instrumentos", Url = "GestionInstrumentos/Instrumento.aspx", EsActivo = false},
                    new BreadcrumbItem { Titulo = "Modificar Modelo de Instrumento", Url = "", EsActivo = true}
                };

                if (Master != null)
                {
                    Master.ActualizarBreadcrumb(listaRutas);
                }

                // Cargar los ddl para el instrumento recibido
                EstadoNegocio estadoNegocio = new EstadoNegocio();
                Session.Add("listadoEstado", estadoNegocio.ListarEstado());
                ddlEstado.DataSource = Session["listadoEstado"];
                ddlEstado.DataTextField = "Descripcion";
                ddlEstado.DataValueField = "Id";
                ddlEstado.DataBind();
                ddlEstado.Items.Insert(0, new ListItem("Seleccione...", "0"));

                DestinoNegocio destinoNegocio = new DestinoNegocio();
                Session.Add("listadoDestino", destinoNegocio.ListarDestino());
                ddlDestino.DataSource = Session["listadoDestino"];
                ddlDestino.DataTextField = "CuatrigramaYDescripcion";
                ddlDestino.DataValueField = "Id";
                ddlDestino.DataBind();
                ddlDestino.Items.Insert(0, new ListItem("Seleccione...", "0"));

                SeccionNegocio seccionNegocio = new SeccionNegocio();
                Session.Add("listadoSeccion", seccionNegocio.ListarSeccion());
                ddlSeccion.DataSource = Session["listadoSeccion"];
                ddlSeccion.DataTextField = "Descripcion";
                ddlSeccion.DataValueField = "Id";
                ddlSeccion.DataBind();
                ddlSeccion.Items.Insert(0, new ListItem("Seleccione...", "0"));

                UsuarioNegocio clienteNegocio = new UsuarioNegocio();
                Session.Add("listadoCliente", clienteNegocio.ListarCliente());
                ddlUsuarioCliente.DataSource = Session["listadoCliente"];
                ddlUsuarioCliente.DataTextField = "ApellidoYNombre";
                ddlUsuarioCliente.DataValueField = "Id";
                ddlUsuarioCliente.DataBind();
                ddlUsuarioCliente.Items.Insert(0, new ListItem("Seleccione...", "0"));

                // Cargar los txt y selección de dll's
                InstrumentoResumenDTO instrumentoSeleccionado = (InstrumentoResumenDTO)Session["instrumentoSeleccionado"];
                txtNumSerie.Text = instrumentoSeleccionado.NumSerie;
                txtFechaRecepcion.Text = instrumentoSeleccionado.FechaRecepcion.ToString("yyyy-MM-dd");
                ddlEstado.SelectedValue = instrumentoSeleccionado.EstadoId.ToString();
                ddlDestino.Text = instrumentoSeleccionado.DestinoId.ToString();
                ddlSeccion.Text = instrumentoSeleccionado.SeccionId.ToString();
                ddlUsuarioCliente.SelectedValue = instrumentoSeleccionado.UsuarioId.ToString();
                chkPatronPrimario.Checked = instrumentoSeleccionado.PatronPrimario;

                // Activar o Desactivar por asignación directa
                chkInstrumentoActivo.Checked = instrumentoSeleccionado.Activo;
                txtNumSerie.Enabled = instrumentoSeleccionado.Activo;
                txtFechaRecepcion.Enabled = instrumentoSeleccionado.Activo;
                ddlEstado.Enabled = instrumentoSeleccionado.Activo;
                ddlDestino.Enabled = instrumentoSeleccionado.Activo;
                ddlSeccion.Enabled = instrumentoSeleccionado.Activo;
                ddlUsuarioCliente.Enabled = instrumentoSeleccionado.Activo;
            }
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

            InstrumentoResumenDTO instrumentoSeleccionado = (InstrumentoResumenDTO)Session["instrumentoSeleccionado"];

            InstrumentoNegocio instrumentoNegocio = new InstrumentoNegocio();

            if (!chkInstrumentoActivo.Checked)
            {
                instrumentoNegocio.DesactivarInstrumento(instrumentoSeleccionado.Id, usuarioLogueado.Id);

                MostrarNotificacionVerde();



                // Delega la redirección a JavaScript con una pausa de 3 segundos (3000 ms)
                string scriptRedirect = "setTimeout(function() { window.location.href = '/GestionInstrumentos/Instrumento.aspx'; }, 3000);";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "RedirigirDesactivar", scriptRedirect, true);

                //Response.Redirect("~/GestionInstrumentos/ModelosInstrumento.aspx", false);
            }
            else
            {
                InstrumentoEdicionDTO modificadoInstrumentoDTO = new InstrumentoEdicionDTO
                {
                    Id = instrumentoSeleccionado.Id,
                    PatronPrimario = chkPatronPrimario.Checked,
                    NumSerie = txtNumSerie.Text.Trim(),
                    FechaRecepcion = DateTime.ParseExact(txtFechaRecepcion.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    DestinoId = int.Parse(ddlDestino.SelectedValue),
                    SeccionId = int.Parse(ddlSeccion.SelectedValue),
                    UsuarioId = int.Parse(ddlUsuarioCliente.SelectedValue),
                    EstadoId = int.Parse(ddlEstado.SelectedValue),
                    Activo = chkInstrumentoActivo.Checked
                };

                instrumentoNegocio.ModificarInstrumento(modificadoInstrumentoDTO, usuarioLogueado.Id);

                Session.Add("modificado", true);

                //MostrarNotificacionVerde();

                ReestablecerCampos();

                // Delega la redirección a JavaScript con una pausa de 3 segundos (3000 ms)
                //string scriptRedirect = "setTimeout(function() { window.location.href = '/GestionInstrumentos/ModelosInstrumento.aspx'; }, 3000);";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "RedirigirModificar", scriptRedirect, true);

                Response.Redirect("~/GestionInstrumentos/Instrumento.aspx", false);
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            ReestablecerCampos();
            Response.Redirect("~/GestionInstrumentos/Instrumento.aspx", false);
        }

        protected void ReestablecerCampos()
        {
            if (chkInstrumentoActivo.Checked)
            {
                txtNumSerie.Text = string.Empty;
                txtFechaRecepcion.Text = string.Empty;
                ddlEstado.SelectedIndex = 0;
                ddlDestino.SelectedIndex = 0;
                ddlSeccion.SelectedIndex = 0;
                ddlUsuarioCliente.SelectedIndex = 0;

                txtNumSerie.Enabled = true;
                txtFechaRecepcion.Enabled = true;
                ddlEstado.Enabled = true;
                ddlDestino.Enabled = true;
                ddlSeccion.Enabled = true;
                ddlUsuarioCliente.Enabled = true;
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

    }
}