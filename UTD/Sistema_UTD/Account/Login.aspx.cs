using Sistema_UTD.Dominio.DTOs;
using Sistema_UTD.Dominio.Entidades;
using Sistema_UTD.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistema_UTD.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();

            UsuarioResumenDTO usuarioValidado = negocio.ValidarLogin(txtUsuario.Text.Trim(), txtContrasenia.Text.Trim());

            if (usuarioValidado != null)
            {
                Session["UsuarioLogueado"] = usuarioValidado;

                if (usuarioValidado.RolId == 1) // Administrador
                {
                    Response.Redirect("~/Default.aspx", false);
                }
                else if (usuarioValidado.RolId == 2) // Técnico
                {
                    Response.Redirect("~/Default.aspx", false);
                }
                else if (usuarioValidado.RolId == 3) // Cliente
                {
                    Response.Redirect("~/Default.aspx", false);
                }

            }
            else
            {
                // Notificación de usuario o contraseña incorrectos, o cuenta inactiva.
                alertaNoSatisfactoria.Style["display"] = "block";
            }
        } // DONE: Pasar a DTO
    }
}