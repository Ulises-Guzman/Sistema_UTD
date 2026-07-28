using Sistema_UTD.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistema_UTD
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 1. Crear la estructura de navegación de la página actual
                List<BreadcrumbItem> listaRutas = new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Titulo = "Inicio", Url = "", EsActivo = true }
                };

                // 2. Enviar la lista a la Master Page
                if (Master != null)
                {
                    Master.ActualizarBreadcrumb(listaRutas);
                    //((Sistema_UTD.Site)Master).ActualizarBreadcrumb(listaRutas);
                }
            }

        }

        protected void lnkBtnGestionComponentes_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/GestionInstrumentos/GestionCompModInst.aspx");
        }

        protected void lnkGestionComponentesAsoc_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/GestionInstrumentos/GestionCompAsocInst.aspx");
        }

    }
}