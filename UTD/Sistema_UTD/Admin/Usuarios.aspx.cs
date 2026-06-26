using Sistema_UTD.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistema_UTD.Admin
{
    public partial class Usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<BreadcrumbItem> listaRutas = new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Titulo = "Inicio", Url = "~/Default.aspx", EsActivo = false },
                    new BreadcrumbItem { Titulo = "Usuarios", Url = "", EsActivo = true }
                };

                if (Master != null)
                {
                    Master.ActualizarBreadcrumb(listaRutas);
                }
            }
        }

        protected void lnkBtnCrearUsuario_Click(object sender, EventArgs e)
        {
            //string accion = "Crear";
            //Response.Redirect("~/Admin/FormularioUsuario.aspx?accion=" + accion, false);
            
            lblTituloModalABM.Text = "Crear Usuario";
            lblDescripcionModalABM.InnerText = "Cree un usuario para dar acceso el sistema";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "AbrirModal", "abrirModalABM();", true);
        }

        protected void lnkBtnModificar_Click(object sender, EventArgs e)
        {
            string accion = "Modificar";
            Response.Redirect("~/Admin/FormularioUsuario.aspx?accion=" + accion, false);
        }
    }
}