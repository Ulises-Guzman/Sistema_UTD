using Sistema_UTD.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
            lblTituloModalModelo.Text = "Crear Modelo";
            lblDescripcionModalModelo.InnerText = "Cree un modelo de para clasificar instrumentos";

            BloquearNotificacion();

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "AbrirModal", "abrirModalModelo();", true);
        }

        protected void BloquearNotificacion()
        {
            // Para eliminar la carga fantasma del modal y la ejecución del update panel
            alertaSatisfactoria.Style["display"] = "none";
        }

        // + Realizar
        // Logica evento botón Aceptar
        // Función para cargar la tarjeta de cantidad de modelos
    }
}