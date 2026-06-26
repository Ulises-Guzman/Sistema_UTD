using Sistema_UTD.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistema_UTD.Admin
{
    public partial class FormularioUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<BreadcrumbItem> listaRutas = new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Titulo = "Inicio", Url = "~/Default.aspx", EsActivo = false },
                    new BreadcrumbItem { Titulo = "Usuarios", Url = "~/Admin/Usuarios.aspx",EsActivo = false },
                    new BreadcrumbItem { Titulo = "Modificar", Url = "", EsActivo = true}
                };

                if (Master != null)
                {
                    Master.ActualizarBreadcrumb(listaRutas);
                }

                if (Request.QueryString["accion"] != null)
                {

                    string tituloAccion = "";
                    switch (Request.QueryString["accion"])
                    {
                        case "Modificar":
                            tituloAccion = Request.QueryString["accion"].ToString();
                            tituloFormulario.Text = tituloAccion + tituloFormulario.Text;
                            lblDescripcion.InnerText = "Modifique datos del usuario, activaciones y reestablesca contraseñas";
                            break;
                        default:
                            break;
                    }


                }
            }
        }

    }
}