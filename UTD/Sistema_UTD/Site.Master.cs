using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sistema_UTD.Helpers;

namespace Sistema_UTD
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void ActualizarBreadcrumb(List<BreadcrumbItem> item)
        {
            rptBreadcrumbs.DataSource = item;
            rptBreadcrumbs.DataBind();
        }
    }
}