using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sistema_UTD.Helpers;

namespace Sistema_UTD.GestionInstrumentos
{
    public partial class Modelos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<BreadcrumbItem> listaRutas = new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Titulo = "Inicio", Url = "~/Default.aspx", EsActivo = false },
                    new BreadcrumbItem { Titulo = "Gestión de componentes asociados", Url = "~/GestionInstrumentos/CompAsocModelos.aspx", EsActivo = false },
                    new BreadcrumbItem { Titulo = "Modificar", Url = "", EsActivo = true }
                };

                if (Master != null)
                {
                    Master.ActualizarBreadcrumb(listaRutas);
                }
            }
        }

        // + Realizar
        // Búsqueda rápida de modelo
        // Lógica de  consulta para la grilla
        // Lógica para la modificación del modelo
    }
}