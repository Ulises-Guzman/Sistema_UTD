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
    public class EstadoNegocio
    {
        public void AgregarEstado(EstadoCreacionDTO estadoDTO, int usuarioLogueadoId)
        {
            AccesoDatos datos = new AccesoDatos();

            Estado nuevoEstado = new Estado();
            nuevoEstado.Descripcion = estadoDTO.Descripcion;

            try
            {
                datos.SetearConsulta("SELECT COUNT(1) FROM Estados WHERE Descripcion = @estadoCheck");
                datos.SetearParametro("@estadoCheck", nuevoEstado.Descripcion);

                int existe = (int)datos.EjecutarScalar();
                if (existe > 0)
                {
                    throw new Exception("El estado de instrumento ya se encuentra registrada y/o se bloqueó multiples intentos.");
                }
                datos.LimpiarParametros();
                datos.CerrarConexion();

                datos.SetearConsulta("INSERT INTO Estados (Descripcion, Activo, FechaCreacion, CreacionUsuarioId) " +
                                     "VALUES (@descripcion, @activo, SYSDATETIME(), @creacionUsuarioId)");
                datos.SetearParametro("@descripcion", nuevoEstado.Descripcion);
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

        public List<EstadoResumenDTO> ListarEstado()
        {
            List<EstadoResumenDTO> lista = new List<EstadoResumenDTO>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT EstadoId, Descripcion, Activo FROM Estados " +
                                     "ORDER BY Descripcion ASC");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    EstadoResumenDTO estadoDTO = new EstadoResumenDTO();
                    estadoDTO.Id = (int)datos.Lector["EstadoId"];
                    estadoDTO.Descripcion = (string)datos.Lector["Descripcion"];
                    estadoDTO.Activo = (bool)datos.Lector["Activo"];

                    lista.Add(estadoDTO);
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

        public void DesactivarEstado(int estadoSeleccionadoId, int usuarioLogueadoId)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE Estados SET Activo = @activo, FechaModificacion = SYSDATETIME(), ModificacionUsuarioId = @modificacionUsuarioId " +
                                     "WHERE EstadoId = @id");
                datos.SetearParametro("@activo", 0);
                datos.SetearParametro("@modificacionUsuarioId", usuarioLogueadoId);
                datos.SetearParametro("@id", estadoSeleccionadoId);

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

        public void ModificarEstado(EstadoEdicionDTO estadoDTO, int usuarioLoguedoId)
        {
            AccesoDatos datos = new AccesoDatos();

            Estado modificadoEstado = new Estado();
            modificadoEstado.Id = estadoDTO.Id;
            modificadoEstado.Descripcion = estadoDTO.Descripcion;
            modificadoEstado.Activo = estadoDTO.Activo;

            try
            {
                datos.SetearConsulta("UPDATE Estados " +
                                     "SET Descripcion = @descripcion, Activo = @activo, " +
                                         "FechaModificacion = SYSDATETIME(), ModificacionUsuarioId = @modificacionUsuarioId " +
                                     "WHERE EstadoId = @id");
                datos.SetearParametro("@descripcion", modificadoEstado.Descripcion);
                datos.SetearParametro("@activo", modificadoEstado.Activo);
                datos.SetearParametro("@modificacionUsuarioId", usuarioLoguedoId);
                datos.SetearParametro("@id", modificadoEstado.Id);

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
