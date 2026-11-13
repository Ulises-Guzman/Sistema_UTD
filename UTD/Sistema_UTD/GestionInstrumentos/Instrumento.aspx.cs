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
    public partial class InstrumentoRecibido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               

                List<BreadcrumbItem> listaRutas = new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Titulo = "Inicio", Url = "~/Default.aspx", EsActivo = false },
                    new BreadcrumbItem { Titulo = "Gestión de Instrumentos Recibidos", Url = "", EsActivo = true }
                };

                if (Master != null)
                {
                    Master.ActualizarBreadcrumb(listaRutas);
                }

                VerificarModificado();

                CambiarSoloLectura();

                // Cargar el ddl con la lista de modelos de instrumentos
                ModeloInstrumentoNegocio modeloInstrumentoNegocio = new ModeloInstrumentoNegocio();
                Session.Add("listaModeloInstrumento", modeloInstrumentoNegocio.ListarModeloInstrumento());
                ddlNomenclatura.DataSource = Session["listaModeloInstrumento"];
                ddlNomenclatura.DataTextField = "Nomenclatura";
                ddlNomenclatura.DataValueField = "Id";
                ddlNomenclatura.DataBind();
                ddlNomenclatura.Items.Insert(0, new ListItem("Seleccione...", "0"));

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

                // Cargar el gv con la lista de instrumentos
                InstrumentoNegocio instrumentoNegocio = new InstrumentoNegocio();
                Session.Add("listaInstrumento", instrumentoNegocio.ListarInstrumento());
                gvInstrumentos.DataSource = Session["listaInstrumento"];
                gvInstrumentos.DataBind();

                //TODO: REALIZAR LA CARGA DE LOS DATOS DEL INSTRUMENTO A REGISTRAR
            }
        }

        protected void ddlNomenclatura_SelectedIndexChanged(object sender, EventArgs e)
        {
            BloquearNotificacion();

            int id = int.Parse(ddlNomenclatura.SelectedValue);

            // 2. Si el usuario vuelve a la opción "Seleccione...", limpia los TextBox y sale
            if (id == 0)
            {
                txtMarca.Text = string.Empty;
                txtModelo.Text = string.Empty;
                txtRango.Text = string.Empty;
                txtUnidadMedida.Text = string.Empty;
                return;
            }

            List<ModeloInstrumentoResumenDTO> lista = (List<ModeloInstrumentoResumenDTO>)Session["listaModeloInstrumento"];

            // El método Find busca el objeto
            ModeloInstrumentoResumenDTO modeloSeleccionado = lista.Find(x => x.Id == id);

            // 3. Validación de nulos
            if (modeloSeleccionado != null)
            {
                txtMarca.Text = modeloSeleccionado.Marca;
                txtModelo.Text = modeloSeleccionado.ModeloDescripcion;
                txtRango.Text = modeloSeleccionado.RangoDescripcion;
                txtUnidadMedida.Text = modeloSeleccionado.UnidadMedidaDescripcion;
            }

        }

        protected void CambiarSoloLectura()
        {
            txtMarca.Attributes.Add("readonly", "readonly");
            txtModelo.Attributes.Add("readonly", "readonly");
            txtRango.Attributes.Add("readonly", "readonly");
            txtUnidadMedida.Attributes.Add("readonly", "readonly");
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                ////// INYECCIÓN DE PRUEBA: Pausa el hilo del servidor por 2 segundos (2000 milisegundos)
                //System.Threading.Thread.Sleep(6000);

                //// 1. inyección de prueba: fuerza el error inmediatamente
                //throw new Exception("simulación de fallo crítico para probar la alerta roja.");

                UsuarioResumenDTO usuarioActual = (UsuarioResumenDTO)Session["UsuarioLogueado"];
                InstrumentoNegocio instrumentoNegocio = new InstrumentoNegocio();

                InstrumentoCreacionDTO nuevoInstrumentoDTO = new InstrumentoCreacionDTO
                {
                    ModeloInstrumentoId = int.Parse(ddlNomenclatura.SelectedValue),
                    NumSerie = txtNumSerie.Text.Trim(),
                    PatronPrimario = chkPatronPrimario.Checked,
                    FechaRecepcion = DateTime.ParseExact(txtFechaRecepcion.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    DestinoId = int.Parse(ddlDestino.SelectedValue),
                    SeccionId = int.Parse(ddlSeccion.SelectedValue),
                    UsuarioId = int.Parse(ddlUsuarioCliente.SelectedValue),
                    EstadoId = int.Parse(ddlEstado.SelectedValue),
                };

                instrumentoNegocio.AgregarInstrumento(nuevoInstrumentoDTO, usuarioActual.Id);

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
            ddlNomenclatura.SelectedIndex = 0;
            txtMarca.Text = string.Empty;
            txtModelo.Text= string.Empty;
            txtRango.Text = string.Empty;
            txtUnidadMedida.Text = string.Empty;

            txtNumSerie.Text = string.Empty;
            chkPatronPrimario.Checked = false;
            txtFechaRecepcion.Text = string.Empty;
            ddlDestino.SelectedIndex = 0;
            ddlSeccion.SelectedIndex = 0;
            ddlUsuarioCliente.SelectedIndex = 0;
            ddlEstado.SelectedIndex = 0;
        }

        protected void lnkBtnActualizar_Click(object sender, EventArgs e)
        {
            BloquearNotificacion();
            ActualizarGrilla();
        }

        protected void ActualizarGrilla()
        {
            InstrumentoNegocio instrumentoNegocio = new InstrumentoNegocio();
            Session.Add("listaInstrumento", instrumentoNegocio.ListarInstrumento());
            gvInstrumentos.DataSource = Session["listaInstrumento"];
            gvInstrumentos.DataBind();
        }

        protected void gvInstrumentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //// 1. INYECCIÓN DE PRUEBA: Fuerza el error inmediatamente
                //throw new Exception("Simulación de fallo crítico para probar la alerta roja.");

                BloquearNotificacion();

                List<InstrumentoResumenDTO> lista = (List<InstrumentoResumenDTO>)Session["listaInstrumento"];

                int id = int.Parse(gvInstrumentos.SelectedDataKey.Value.ToString());

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

        protected void gvInstrumentos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvInstrumentos.PageIndex = e.NewPageIndex;
            EnlazarPaginaGridView(sender, e);
            gvInstrumentos.DataBind();

            BloquearNotificacion();
        }

        protected void EnlazarPaginaGridView(object sender, EventArgs e)
        {
            InstrumentoNegocio instrumentoNegocio = new InstrumentoNegocio();
            Session.Add("listaInstrumento", instrumentoNegocio.ListarInstrumento());
            gvInstrumentos.DataSource = Session["listaInstrumento"];
            gvInstrumentos.DataBind();
        }

        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BloquearNotificacion();

            try
            {
                //// 1. INYECCIÓN DE PRUEBA: Fuerza el error inmediatamente
                //throw new Exception("Simulación de fallo crítico para probar la alerta roja.");

                List<InstrumentoResumenDTO> lista = (List<InstrumentoResumenDTO>)Session["listaInstrumento"];
                List<InstrumentoResumenDTO> listaBusqueda = new List<InstrumentoResumenDTO>();
                listaBusqueda = lista.FindAll(x => x.NumSerie.ToLower().Contains(txtBuscar.Text.ToLower()));
                gvInstrumentos.DataSource = listaBusqueda;
                gvInstrumentos.DataBind();

                LimpiarCampoBuscar();
            }
            catch (Exception ex)
            {
                alertaNoSatisfactoria.InnerText = "El instrumento no ha sido encontrado debido a un error: " + ex.Message;
                MostrarNotificacionRoja();

                LimpiarCampoBuscar();
            }
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