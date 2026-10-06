using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_UTD.Datos;
using Sistema_UTD.Dominio;
using Sistema_UTD.Dominio.Entidades;

namespace Sistema_UTD.Negocio
{
    public class ModeloNegocio
    {
        public void AgregarModelo(Modelo modelo, int usuarioLogueadoId)
        {
            AccesoDatos datos = new AccesoDatos();

			try
			{
				datos.SetearConsulta("INSERT INTO Modelos (Descripcion, Activo, FechaCreacion, CreacionUsuarioId) " +
									 "VALUES (@descripcion, @activo, SYSDATETIME(), @creacionUsuarioId)");
				datos.SetearParametro("@descripcion", modelo.Descripcion);
				datos.SetearParametro("@activo", true);
				datos.SetearParametro("@creacionUsuarioId", usuarioLogueadoId);

				datos.EjecutarConsulta();
			}
			catch (Exception)
			{

				throw;
			}
			finally
			{
				datos.CerrarConexion();
			}
        }
    }
}
