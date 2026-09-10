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
            if (Session["UsuarioLogueado"] == null)
            {
                Response.Redirect("~/Account/Login.aspx", false);

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
                    //lblBienvenidaUsuario.Text = $"Hola, {usuarioActual.Nombre}...";
                    lnkLblAvatar.InnerText += $"{usuarioActual.Nombre} {usuarioActual.Apellido}";
                    
                }
            }
        }

        public void ActualizarBreadcrumb(List<BreadcrumbItem> item)
        {
            rptBreadcrumbs.DataSource = item;
            rptBreadcrumbs.DataBind();
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            System.Web.Security.FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Account/Login.aspx", false);
        }
    }
}