using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sistema_UTD.Dominio.Entidades;
using Sistema_UTD.Negocio;

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

            Usuario usuarioValidado = negocio.ValidarLogin(txtUsuario.Text, txtContrasenia.Text);

            if (usuarioValidado != null)
            {
                Session["UsuarioLoguedo"] = usuarioValidado;

                if (usuarioValidado.Rol.Id == 1) // Administrador
                {
                    Response.Redirect("~/Default.aspx", false);
                }
                else if (usuarioValidado.Rol.Id == 2) // Técnico
                {
                    Response.Redirect("~/Default.aspx", false);
                }
                else if (usuarioValidado.Rol.Id == 3) // Cliente
                {
                    Response.Redirect("~/Default.aspx", false);
                }

            }
            else
            {
                // Notificación de usuario o contraseña incorrectos, o cuenta inactiva.
            }
        }
    }
}