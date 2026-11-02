using Sistema_UTD.Datos;
using Sistema_UTD.Dominio.DTOs;
using Sistema_UTD.Dominio.Entidades;
using Sistema_UTD.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_UTD.Negocio
{
    public class TiempoHabilitacionNegocio
    {
        public void AgregarTiempoHabilitacion(TiempoHabilitacionCreacionDTO tiempoDTO, int usuarioLogueadoId)
        {
            AccesoDatos datos = new AccesoDatos();

            TiempoHabilitacion nuevoTiempo = new TiempoHabilitacion();
            nuevoTiempo.CantidadMeses = tiempoDTO.CantidadMeses;

            try
            {
                datos.SetearConsulta("SELECT COUNT(1) FROM TiemposHabilitacion WHERE CantidadMeses = @tiempoCheck");
                datos.SetearParametro("@tiempoCheck", nuevoTiempo.CantidadMeses);

                int existe = (int)datos.EjecutarScalar();
                if (existe > 0)
                {
                    throw new Exception("El tiempo de habilitación ya se encuentra registrada y/o se bloqueó multiples intentos.");
                }
                datos.LimpiarParametros();
                datos.CerrarConexion();

                datos.SetearConsulta("INSERT INTO TiemposHabilitacion (CantidadMeses, Activo, FechaCreacion, CreacionUsuarioId) " +
                                     "VALUES (@cantidadMeses, @activo, SYSDATETIME(), @creacionUsuarioId)");
                datos.SetearParametro("@cantidadMeses", nuevoTiempo.CantidadMeses);
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

        public List<TiempoHabilitacionResumenDTO> ListarTiempo()
        {
            List<TiempoHabilitacionResumenDTO> lista = new List<TiempoHabilitacionResumenDTO>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT TiempoHabilitacionId, CantidadMeses, Activo FROM TiemposHabilitacion " +
                                     "ORDER BY CantidadMeses ASC");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    TiempoHabilitacionResumenDTO tiempoDTO = new TiempoHabilitacionResumenDTO();
                    tiempoDTO.Id = (int)datos.Lector["TiempoHabilitacionId"];
                    tiempoDTO.CantidadMeses = (int)datos.Lector["CantidadMeses"];
                    tiempoDTO.Activo = (bool)datos.Lector["Activo"];

                    lista.Add(tiempoDTO);
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

        public void DesactivarTiempo(int tiempoSeleccionadoId, int usuarioLogueadoId)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE TiemposHabilitacion SET Activo = @activo, FechaModificacion = SYSDATETIME(), ModificacionUsuarioId = @modificacionUsuarioId " +
                                     "WHERE TiempoHabilitacionId = @id");
                datos.SetearParametro("@activo", 0);
                datos.SetearParametro("@modificacionUsuarioId", usuarioLogueadoId);
                datos.SetearParametro("@id", tiempoSeleccionadoId);

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

        public void ModificarTiempo(TiempoHabilitacionEdicionDTO tiempoDTO, int usuarioLoguedoId)
        {
            AccesoDatos datos = new AccesoDatos();

            TiempoHabilitacion modificadoTiempo = new TiempoHabilitacion();
            modificadoTiempo.Id = tiempoDTO.Id;
            modificadoTiempo.CantidadMeses = tiempoDTO.CantidadMeses;
            modificadoTiempo.Activo = tiempoDTO.Activo;

            try
            {
                datos.SetearConsulta("UPDATE TiemposHabilitacion " +
                                     "SET CantidadMeses = @cantidadMeses, Activo = @activo, " +
                                         "FechaModificacion = SYSDATETIME(), ModificacionUsuarioId = @modificacionUsuarioId " +
                                     "WHERE TiempoHabilitacionId = @id");
                datos.SetearParametro("@descripcion", modificadoTiempo.CantidadMeses);
                datos.SetearParametro("@activo", modificadoTiempo.Activo);
                datos.SetearParametro("@modificacionUsuarioId", usuarioLoguedoId);
                datos.SetearParametro("@id", modificadoTiempo.Id);

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
