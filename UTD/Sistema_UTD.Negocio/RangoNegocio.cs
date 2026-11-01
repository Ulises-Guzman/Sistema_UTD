using Sistema_UTD.Datos;
using Sistema_UTD.Dominio.DTOs;
using Sistema_UTD.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_UTD.Negocio
{
    public class RangoNegocio
    {
        public void AgregarRango(RangoCreacionDTO rangoDTO, int usuarioLogueadoId)
        {
            AccesoDatos datos = new AccesoDatos();

            Rango nuevoRango = new Rango();
            nuevoRango.Descripcion = rangoDTO.Descripcion;

            try
            {
                datos.SetearConsulta("SELECT COUNT(1) FROM Rangos WHERE Descripcion = @modeloCheck");
                datos.SetearParametro("@modeloCheck", nuevoRango.Descripcion);

                int existe = (int)datos.EjecutarScalar();
                if (existe > 0)
                {
                    throw new Exception("El rango ya se encuentra registrada y/o se bloqueó multiples intentos.");
                }
                datos.LimpiarParametros();
                datos.CerrarConexion();

                datos.SetearConsulta("INSERT INTO Rangos (Descripcion, Activo, FechaCreacion, CreacionUsuarioId) " +
                                     "VALUES (@descripcion, @activo, SYSDATETIME(), @creacionUsuarioId)");
                datos.SetearParametro("@descripcion", nuevoRango.Descripcion);
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

        public List<RangoResumenDTO> ListarRango()
        {
            List<RangoResumenDTO> lista = new List<RangoResumenDTO>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT RangoId, Descripcion, Activo FROM Rangos " +
                                     "ORDER BY Descripcion ASC");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    RangoResumenDTO rangoDTO = new RangoResumenDTO();
                    rangoDTO.Id = (int)datos.Lector["RangoId"];
                    rangoDTO.Descripcion = (string)datos.Lector["Descripcion"];
                    rangoDTO.Activo = (bool)datos.Lector["Activo"];

                    lista.Add(rangoDTO);
                }

                return lista;

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

        public void DesactivarRango(int rangoSeleccionadoId, int usuarioLogueadoId)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE Rangos SET Activo = @activo, FechaModificacion = SYSDATETIME(), ModificacionUsuarioId = @modificacionUsuarioId " +
                                     "WHERE RangoId = @id");
                datos.SetearParametro("@activo", 0);
                datos.SetearParametro("@modificacionUsuarioId", usuarioLogueadoId);
                datos.SetearParametro("@id", rangoSeleccionadoId);

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

        public void ModificarRango(RangoEdicionDTO rangoDTO, int usuarioLoguedoId)
        {
            AccesoDatos datos = new AccesoDatos();

            Rango modificadoRango = new Rango();
            modificadoRango.Id = rangoDTO.Id;
            modificadoRango.Descripcion = rangoDTO.Descripcion;
            modificadoRango.Activo = rangoDTO.Activo;

            try
            {
                datos.SetearConsulta("UPDATE Rangos " +
                                     "SET Descripcion = @descripcion, Activo = @activo, " +
                                         "FechaModificacion = SYSDATETIME(), ModificacionUsuarioId = @modificacionUsuarioId " +
                                     "WHERE RangoId = @id");
                datos.SetearParametro("@descripcion", modificadoRango.Descripcion);
                datos.SetearParametro("@activo", modificadoRango.Activo);
                datos.SetearParametro("@modificacionUsuarioId", usuarioLoguedoId);
                datos.SetearParametro("@id", modificadoRango.Id);

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
