using Sistema_UTD.Dominio.DTOs;
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
    public partial class FormularioSeccion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<BreadcrumbItem> listaRutas = new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Titulo = "Inicio", Url = "~/Default.aspx", EsActivo = false },
                    new BreadcrumbItem { Titulo = "Gestión de Componentes Asociados", Url = "~/GestionInstrumentos/CompAsocInstrumentos.aspx", EsActivo = false},
                    new BreadcrumbItem { Titulo = "Modificar Sección", Url = "", EsActivo = true}
                };

                if (Master != null)
                {
                    Master.ActualizarBreadcrumb(listaRutas);
                }

                // Cargar el gv con la lista de secciones
                SeccionNegocio seccionNegocio = new SeccionNegocio();
                Session.Add("listaSeccion", seccionNegocio.ListarSeccion());
                gvSecciones.DataSource = Session["listaSeccion"];
                gvSecciones.DataBind();
            }
        }

        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BloquearNotificacion();

            try
            {
                //// 1. INYECCIÓN DE PRUEBA: Fuerza el error inmediatamente
                //throw new Exception("Simulación de fallo crítico para probar la alerta roja.");

                List<SeccionResumenDTO> lista = (List<SeccionResumenDTO>)Session["listaSeccion"];
                List<SeccionResumenDTO> listaBusqueda = new List<SeccionResumenDTO>();
                listaBusqueda = lista.FindAll(x => x.Descripcion.ToLower().Contains(txtBuscar.Text.ToLower()));
                gvSecciones.DataSource = listaBusqueda;
                gvSecciones.DataBind();

            }
            catch (Exception ex)
            {
                alertaNoSatisfactoria.InnerText = "La sección no ha sido encontrado debido a un error: " + ex.Message;
                MostraNotificacionRoja();
            }
        }

        protected void lnkBtnActualizar_Click(object sender, EventArgs e)
        {
            BloquearNotificacion();
            ActualizarGrilla();
        }

        protected void gvSecciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //// 1. INYECCIÓN DE PRUEBA: Fuerza el error inmediatamente
                //throw new Exception("Simulación de fallo crítico para probar la alerta roja.");

                BloquearNotificacion();

                List<SeccionResumenDTO> lista = (List<SeccionResumenDTO>)Session["listaSeccion"];

                int id = int.Parse(gvSecciones.SelectedDataKey.Value.ToString());

                SeccionResumenDTO seccionSeleccionada = lista.Find(x => x.Id == id);

                if (seccionSeleccionada == null)
                {
                    throw new Exception("El registro ya no se encuentra en la memoria o la sesión expiró.");
                }

                Session.Add("seccionSeleccionada", seccionSeleccionada);

                // Cargar datos a los TextBox
                txtSeccion.Text = seccionSeleccionada.Descripcion;

                // Activar o Desactivar por asignación directa
                chkSeccionActivo.Checked = seccionSeleccionada.Activo;
                txtSeccion.Enabled = seccionSeleccionada.Activo;

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "AbrirModal", "abrirModalSeccion();", true);
            }
            catch (Exception ex)
            {
                alertaNoSatisfactoria.InnerText = "La sección no ha sido encontrado debido a un error: " + ex.Message;
                MostraNotificacionRoja();

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LimpiarFondo", "limpiarFondoModal();", true);
            }
        }

        protected void gvSecciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSecciones.PageIndex = e.NewPageIndex;
            EnlazarPaginaGridView(sender, e);
            gvSecciones.DataBind();

            BloquearNotificacion();
        }

        protected void EnlazarPaginaGridView(object sender, EventArgs e)
        {
            SeccionNegocio seccionNegocio = new SeccionNegocio();
            Session.Add("listaSeccion", seccionNegocio.ListarSeccion());
            gvSecciones.DataSource = Session["listaSeccion"];
            gvSecciones.DataBind();
        }

        protected void btnAceptarModificarSeccion_Click(object sender, EventArgs e)
        {
            // INYECCIÓN DE PRUEBA: Pausa el hilo del servidor por 2 segundos (2000 milisegundos)
            //System.Threading.Thread.Sleep(6000);

            //// 1. INYECCIÓN DE PRUEBA: Fuerza el error inmediatamente
            //throw new Exception("Simulación de fallo crítico para probar la alerta roja.");

            UsuarioResumenDTO usuarioLogueado = (UsuarioResumenDTO)Session["UsuarioLogueado"];

            SeccionResumenDTO seccionSeleccionada = (SeccionResumenDTO)Session["seccionSeleccionada"];

            SeccionNegocio seccionNegocio = new SeccionNegocio();

            if (!chkSeccionActivo.Checked)
            {
                seccionNegocio.DesactivarSeccion(seccionSeleccionada.Id, usuarioLogueado.Id);

                MostrarNotificacionVerde();

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LimpiarFondo", "limpiarFondoModal();", true);
            }
            else
            {
                SeccionEdicionDTO modificadaSeccionDTO = new SeccionEdicionDTO();
                modificadaSeccionDTO.Id = seccionSeleccionada.Id;
                modificadaSeccionDTO.Descripcion = txtSeccion.Text.Trim();
                modificadaSeccionDTO.Activo = chkSeccionActivo.Checked;

                seccionNegocio.ModificarSeccion(modificadaSeccionDTO, usuarioLogueado.Id);

                MostrarNotificacionVerde();

                LimpiarCampos();

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LimpiarFondo", "limpiarFondoModal();", true);
            }

            ActualizarGrilla();
        }

        protected void BloquearNotificacion()
        {
            alertaSatisfactoria.Style["display"] = "none";
            alertaNoSatisfactoria.Style["display"] = "none";
        }

        protected void MostrarNotificacionVerde()
        {
            alertaSatisfactoria.Style["display"] = "block";
            alertaNoSatisfactoria.Style["display"] = "none";
        }

        protected void MostraNotificacionRoja()
        {
            alertaSatisfactoria.Style["display"] = "none";
            alertaNoSatisfactoria.Style["display"] = "block";
        }

        protected void LimpiarCampos()
        {
            txtSeccion.Text = string.Empty;
        }

        protected void ActualizarGrilla()
        {
            SeccionNegocio seccionNegocio = new SeccionNegocio();
            Session.Add("listaSeccion", seccionNegocio.ListarSeccion());
            gvSecciones.DataSource = Session["listaSeccion"];
            gvSecciones.DataBind();
        }
    }
}