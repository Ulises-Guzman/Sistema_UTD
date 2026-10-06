using Sistema_UTD.Admin;
using Sistema_UTD.Dominio.Entidades;
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
    public partial class FormularioModelo : System.Web.UI.Page
    {
        // TODO: CodeBehind BM de modelos de componentes asociados
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<BreadcrumbItem> listaRutas = new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Titulo = "Inicio", Url = "~/Default.aspx", EsActivo = false },
                    new BreadcrumbItem { Titulo = "Gestión de componentes asociados", Url = "~/GestionInstrumentos/CompAsocModelos.aspx", EsActivo = false},
                    new BreadcrumbItem { Titulo = "Modificar Modelo", Url = "", EsActivo = true}
                };

                if (Master != null)
                {
                    Master.ActualizarBreadcrumb(listaRutas);
                }

                //// Cargar el gv con la lista de usuarios
                //ModeloNegocio modeloNegocio = new ModeloNegocio();
                //Session.Add("listaUsuario", modeloNegocio.ListarModelo());
                //gvModelos.DataSource = Session["listaUsuario"];
                //gvModelos.DataBind();
            }

        }

        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {

        }

        protected void lnkBtnActualizar_Click(object sender, EventArgs e)
        {

        }

        protected void gvModelos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvModelos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void btnAceptarModificarModelo_Click(object sender, EventArgs e)
        {

        }
    }
}