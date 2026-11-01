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
    public partial class GestionCompAsocInst : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<BreadcrumbItem> listaRutas = new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Titulo = "Inicio", Url = "~/Default.aspx", EsActivo = false },
                    new BreadcrumbItem { Titulo = "Gestión de componentes asociados", Url = "", EsActivo = true}
                };

                if (Master != null)
                {
                    Master.ActualizarBreadcrumb(listaRutas);
                }
            }
        }

        protected void lnkAgregarDestino_Click(object sender, EventArgs e)
        {
            BloquearNotificacion();

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "AbrirModal", "abrirModalDestino();", true);
        }
        protected void lnkAgregarSeccion_Click(object sender, EventArgs e)
        {
            BloquearNotificacion();

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "AbrirModal", "abrirModalSeccion();", true);
        }
        protected void lnkAgregarEstadoInst_Click(object sender, EventArgs e)
        {
            BloquearNotificacion();

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "AbrirModal", "abrirModalEstadoInst();", true);
        }

        protected void lnkModificarDestino_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/GestionInstrumentos/FormularioDestino.aspx");
        }
        protected void lnkModificarSeccion_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/GestionInstrumentos/FormularioSeccion.aspx");
        }

        protected void btnAceptarNuevoDestino_Click(object sender, EventArgs e)
        {
            try
            {
                ////// INYECCIÓN DE PRUEBA: Pausa el hilo del servidor por 2 segundos (2000 milisegundos)
                //System.Threading.Thread.Sleep(6000);

                //// 1. inyección de prueba: fuerza el error inmediatamente
                //throw new Exception("simulación de fallo crítico para probar la alerta roja.");

                UsuarioResumenDTO usuarioActual = (UsuarioResumenDTO)Session["UsuarioLogueado"];
                DestinoCreacionDTO nuevoDestinoDTO = new DestinoCreacionDTO();
                DestinoNegocio DestinoNegocio = new DestinoNegocio();

                nuevoDestinoDTO.Cuatrigrama = txtCuatrigrama.Text.ToUpper();
                nuevoDestinoDTO.Descripcion = txtDestino.Text.Trim();

                DestinoNegocio.AgregarDestino(nuevoDestinoDTO, usuarioActual.Id);

                MostrarNotificacionVerde();

                LimpiarCampos();

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LimpiarFondo", "limpiarFondoModal();", true);
            }
            catch (Exception ex)
            {
                alertaNoSatisfactoria.InnerText = "El destino no ha sido creado debido a un error: " + ex.Message;
                MostrarNotificacionRoja();

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LimpiarFondo", "limpiarFondoModal();", true);
            }
        }
        protected void btnAceptarNuevaSeccion_Click(object sender, EventArgs e)
        {
            try
            {
                ////// INYECCIÓN DE PRUEBA: Pausa el hilo del servidor por 2 segundos (2000 milisegundos)
                //System.Threading.Thread.Sleep(6000);

                //// 1. inyección de prueba: fuerza el error inmediatamente
                //throw new Exception("simulación de fallo crítico para probar la alerta roja.");

                UsuarioResumenDTO usuarioActual = (UsuarioResumenDTO)Session["UsuarioLogueado"];
                SeccionCreacionDTO nuevaSeccionDTO = new SeccionCreacionDTO();
                SeccionNegocio seccionNegocio = new SeccionNegocio();

                nuevaSeccionDTO.Descripcion = txtSeccion.Text.Trim();

                seccionNegocio.AgregarSeccion(nuevaSeccionDTO, usuarioActual.Id);

                MostrarNotificacionVerde();

                LimpiarCampos();

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LimpiarFondo", "limpiarFondoModal();", true);
            }
            catch (Exception ex)
            {
                alertaNoSatisfactoria.InnerText = "La sección no ha sido creado debido a un error: " + ex.Message;
                MostrarNotificacionRoja();

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LimpiarFondo", "limpiarFondoModal();", true);
            }
        }

        protected void BloquearNotificacion()
        {
            // Para eliminar la carga fantasma del modal y la ejecución del update panel
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
            txtCuatrigrama.Text = string.Empty;
            txtDestino.Text = string.Empty;
            txtSeccion.Text = string.Empty;
        }

    }
}