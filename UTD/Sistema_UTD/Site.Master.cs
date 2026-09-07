using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sistema_UTD.Helpers;
using Sistema_UTD.Dominio.Entidades;

namespace Sistema_UTD
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioLoguedo"] == null)
            {
                Response.Redirect("~/Login.aspx", false);

                // Detiene cualquier otro proceso que la página intentara hacer
                Context.ApplicationInstance.CompleteRequest();

            }
            else
            {   
                // Si es legítimo...
                if (!IsPostBack)
                {
                    Usuario usuarioActual = (Usuario)Session["UsuarioLogueado"];

                    // Cargar la Navbar con el usuario loguedo
                }
            }
        }

        public void ActualizarBreadcrumb(List<BreadcrumbItem> item)
        {
            rptBreadcrumbs.DataSource = item;
            rptBreadcrumbs.DataBind();
        }
    }
}