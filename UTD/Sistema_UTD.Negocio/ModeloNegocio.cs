using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_UTD.Datos;
using Sistema_UTD.Dominio;
using Sistema_UTD.Dominio.DTOs;
using Sistema_UTD.Dominio.Entidades;

namespace Sistema_UTD.Negocio
{
    public class ModeloNegocio
    {
        public void AgregarModelo(ModeloCreacionDTO modeloDTO, int usuarioLogueadoId)
        {
            AccesoDatos datos = new AccesoDatos();

			Modelo nuevoModelo = new Modelo();
			nuevoModelo.Descripcion = modeloDTO.Descripcion;

			try
			{
                datos.SetearConsulta("SELECT COUNT(1) FROM Modelos WHERE Descripcion = @modeloCheck");
                datos.SetearParametro("@modeloCheck", nuevoModelo.Descripcion);

                int existe = (int)datos.EjecutarScalar();
                if (existe > 0)
                {
                    throw new Exception("El nombre de usuario ya se encuentra registrado y/o se bloqueó multiples intentos.");
                }
				datos.LimpiarParametros();
				datos.CerrarConexion();

                datos.SetearConsulta("INSERT INTO Modelos (Descripcion, Activo, FechaCreacion, CreacionUsuarioId) " +
									 "VALUES (@descripcion, @activo, SYSDATETIME(), @creacionUsuarioId)");
				datos.SetearParametro("@descripcion", nuevoModelo.Descripcion);
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
