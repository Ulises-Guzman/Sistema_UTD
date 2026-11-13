using Sistema_UTD.Datos;
using Sistema_UTD.Dominio;
using Sistema_UTD.Dominio.DTOs;
using Sistema_UTD.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_UTD.Negocio
{
    public class DestinoNegocio
    {
        public void AgregarDestino(DestinoCreacionDTO destinoDTO, int usuarioLogueadoId)
        {
            AccesoDatos datos = new AccesoDatos();

            Destino nuevoDestino = new Destino();
            nuevoDestino.Cuatrigrama = destinoDTO.Cuatrigrama;
            nuevoDestino.Descripcion = destinoDTO.Descripcion;

            try
            {
                datos.SetearConsulta("SELECT COUNT(1) FROM Destinos WHERE Descripcion = @destinoCheck");
                datos.SetearParametro("@destinoCheck", nuevoDestino.Descripcion);

                int existe = (int)datos.EjecutarScalar();
                if (existe > 0)
                {
                    throw new Exception("El nombre de destino ya se encuentra registrado y/o se bloqueó multiples intentos.");
                }
                datos.LimpiarParametros();
                datos.CerrarConexion();

                datos.SetearConsulta("INSERT INTO Destinos (Cuatrigrama, Descripcion, Activo, FechaCreacion, CreacionUsuarioId) " +
                                     "VALUES (@cuatrigrama, @descripcion, @activo, SYSDATETIME(), @creacionUsuarioId)");
                datos.SetearParametro("@cuatrigrama", nuevoDestino.Cuatrigrama);
                datos.SetearParametro("@descripcion", nuevoDestino.Descripcion);
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

        public List<DestinoResumenDTO> ListarDestino()
        {
            List<DestinoResumenDTO> lista = new List<DestinoResumenDTO>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT DestinoId, Cuatrigrama, Descripcion, Activo FROM Destinos " +
                                     "WHERE Activo = 1 " +
                                     "ORDER BY Descripcion ASC");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    DestinoResumenDTO destinoDTO = new DestinoResumenDTO();
                    destinoDTO.Id = (int)datos.Lector["DestinoId"];
                    destinoDTO.Cuatrigrama = (string)datos.Lector["Cuatrigrama"];
                    destinoDTO.Descripcion = (string)datos.Lector["Descripcion"];
                    destinoDTO.Activo = (bool)datos.Lector["Activo"];

                    lista.Add(destinoDTO);
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

        public void DesactivarDestino(int destinoSeleccionadoId, int usuarioLogueadoId)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE Destinos SET Activo = @activo, FechaModificacion = SYSDATETIME(), ModificacionUsuarioId = @modificacionUsuarioId " +
                                     "WHERE DestinoId = @id");
                datos.SetearParametro("@activo", 0);
                datos.SetearParametro("@modificacionUsuarioId", usuarioLogueadoId);
                datos.SetearParametro("@id", destinoSeleccionadoId);

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

        public void ModificarDestino(DestinoEdicionDTO destinoDTO, int usuarioLoguedoId)
        {
            AccesoDatos datos = new AccesoDatos();

            Destino modificadoDestino = new Destino();
            modificadoDestino.Id = destinoDTO.Id;
            modificadoDestino.Cuatrigrama = destinoDTO.Cuatrigrama;
            modificadoDestino.Descripcion = destinoDTO.Descripcion;
            modificadoDestino.Activo = destinoDTO.Activo;

            try
            {
                datos.SetearConsulta("UPDATE Destinos " +
                                     "SET Cuatrigrama = @cuatrigrama, Descripcion = @descripcion, Activo = @activo, " +
                                         "FechaModificacion = SYSDATETIME(), ModificacionUsuarioId = @modificacionUsuarioId " +
                                     "WHERE DestinoId = @id");
                datos.SetearParametro("@cuatrigrama", modificadoDestino.Cuatrigrama);
                datos.SetearParametro("@descripcion", modificadoDestino.Descripcion);
                datos.SetearParametro("@activo", modificadoDestino.Activo);
                datos.SetearParametro("@modificacionUsuarioId", usuarioLoguedoId);
                datos.SetearParametro("@id", modificadoDestino.Id);

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
