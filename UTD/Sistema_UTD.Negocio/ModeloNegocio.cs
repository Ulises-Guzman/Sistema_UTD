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
        public void AgregarModelo(Modelo modelo)
        {
            AccesoDatos datos = new AccesoDatos();

			try
			{
				datos.SetearConsulta("INSERT INTO Modelos (Descripcion, Activo, FechaCreacion, CreacionUsuarioId) " +
									 "VALUES (@descripcion, @activo, @FechaCreacion, @CreacionUsuarioId)");
				datos.SetearParametro("@descripcion", modelo.Descripcion);
				datos.SetearParametro("@activo", true);
				//datos.SetearParametro("@FechaCreacion", );
				// Primero debo modificar el tipo de dato datatime a datatime2(3)
			}
			catch (Exception ex)
			{

				throw ex;
			}
        }
    }
}
