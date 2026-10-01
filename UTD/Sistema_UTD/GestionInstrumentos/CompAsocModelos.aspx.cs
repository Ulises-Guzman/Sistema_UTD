using Sistema_UTD.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sistema_UTD.Dominio;
using Sistema_UTD.Dominio.Entidades;
using Sistema_UTD.Negocio;

namespace Sistema_UTD.GestionInstrumentos
{
    public partial class GestionCompModInst : System.Web.UI.Page
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

        protected void lnkModificarModelo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/GestionInstrumentos/FormularioModelo.aspx");
        }

        protected void lnkAgregarModelo_Click(object sender, EventArgs e)
        {
            BloquearNotificacion();

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "AbrirModal", "abrirModalModelo();", true);
        }

        protected void BloquearNotificacion()
        {
            // Para eliminar la carga fantasma del modal y la ejecución del update panel
            alertaSatisfactoria.Style["display"] = "none";
        }

        //protected void btnAceptar_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Modelo nuevo = new Modelo();
        //        // Crear logica de negocio en la capa negocio
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        protected void btnAceptarNuevoModelo_Click(object sender, EventArgs e)
        {
            try
            {
                Modelo nuevo = new Modelo();
                ModeloNegocio negocio = new ModeloNegocio();
                Usuario usuarioActual = (Usuario)Session["UsuarioLogueado"];

                nuevo.Descripcion = txtModelo.Text;

                negocio.AgregarModelo(nuevo, usuarioActual);

                BloquearNotificacion();

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LimpiarFondo", "limpiarFondoModal();", true);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnAceptarNuevoRango_Click(object sender, EventArgs e)
        {

        }

        protected void lnkAgregarRango_Click(object sender, EventArgs e)
        {
            BloquearNotificacion();

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "AbrirModal", "abrirModalRango();", true);
        }

        protected void btnAceptarNuevaUnidad_Click(object sender, EventArgs e)
        {

        }

        protected void lnkAgregarUnidadMedida_Click(object sender, EventArgs e)
        {
            BloquearNotificacion();

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "AbrirModal", "abrirModalUnidad();", true);
        }

        // + Realizar
        // Logica evento botón Aceptar
    }
}