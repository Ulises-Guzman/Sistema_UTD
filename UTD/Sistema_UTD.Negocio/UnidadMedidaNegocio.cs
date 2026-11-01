using Sistema_UTD.Datos;
using Sistema_UTD.Dominio;
using Sistema_UTD.Dominio.Entidades;
using Sistema_UTD.Dominio.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_UTD.Negocio
{
    public class UnidadMedidaNegocio
    {
        public void AgregarUnidadMedida(UnidadMedidaCreacionDTO unidadMedidaDTO, int usuarioLogueadoId)
        {
            AccesoDatos datos = new AccesoDatos();

            UnidadMedida nuevoUnidadMedida = new UnidadMedida();
            nuevoUnidadMedida.Descripcion = unidadMedidaDTO.Descripcion;

            try
            {
                datos.SetearConsulta("SELECT COUNT(1) FROM UnidadesMedida WHERE Descripcion = @modeloCheck");
                datos.SetearParametro("@modeloCheck", nuevoUnidadMedida.Descripcion);

                int existe = (int)datos.EjecutarScalar();
                if (existe > 0)
                {
                    throw new Exception("La unidad de medida ya se encuentra registrada y/o se bloqueó multiples intentos.");
                }
                datos.LimpiarParametros();
                datos.CerrarConexion();

                datos.SetearConsulta("INSERT INTO UnidadesMedida (Descripcion, Activo, FechaCreacion, CreacionUsuarioId) " +
                                     "VALUES (@descripcion, @activo, SYSDATETIME(), @creacionUsuarioId)");
                datos.SetearParametro("@descripcion", nuevoUnidadMedida.Descripcion);
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

        public List<UnidadMedidaResumenDTO> ListarUnidadMedida()
        {
            List<UnidadMedidaResumenDTO> lista = new List<UnidadMedidaResumenDTO>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT UnidadMedidaId, Descripcion, Activo FROM UnidadesMedida " +
                                     "ORDER BY Descripcion ASC");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    UnidadMedidaResumenDTO  unidadMedidaDTO = new UnidadMedidaResumenDTO();
                    unidadMedidaDTO.Id = (int)datos.Lector["UnidadMedidaId"];
                    unidadMedidaDTO.Descripcion = (string)datos.Lector["Descripcion"];
                    unidadMedidaDTO.Activo = (bool)datos.Lector["Activo"];

                    lista.Add(unidadMedidaDTO);
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

        public void DesactivarUnidadMedida(int unidadMedidaSeleccionadaId, int usuarioLogueadoId)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE UnidadesMedida SET Activo = @activo, FechaModificacion = SYSDATETIME(), ModificacionUsuarioId = @modificacionUsuarioId " +
                                     "WHERE UnidadMedidaId = @id");
                datos.SetearParametro("@activo", 0);
                datos.SetearParametro("@modificacionUsuarioId", usuarioLogueadoId);
                datos.SetearParametro("@id", unidadMedidaSeleccionadaId);

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

        public void ModificarUnidadMedida(UnidadMedidaEdicionDTO unidadMedidaDTO, int usuarioLoguedoId)
        {
            AccesoDatos datos = new AccesoDatos();

            UnidadMedida modificadaUnidadMedida = new UnidadMedida();
            modificadaUnidadMedida.Id = unidadMedidaDTO.Id;
            modificadaUnidadMedida.Descripcion = unidadMedidaDTO.Descripcion;
            modificadaUnidadMedida.Activo = unidadMedidaDTO.Activo;

            try
            {
                datos.SetearConsulta("UPDATE UnidadesMedida " +
                                     "SET Descripcion = @descripcion, Activo = @activo, " +
                                         "FechaModificacion = SYSDATETIME(), ModificacionUsuarioId = @modificacionUsuarioId " +
                                     "WHERE UnidadMedidaId = @id");
                datos.SetearParametro("@descripcion", modificadaUnidadMedida.Descripcion);
                datos.SetearParametro("@activo", modificadaUnidadMedida.Activo);
                datos.SetearParametro("@modificacionUsuarioId", usuarioLoguedoId);
                datos.SetearParametro("@id", modificadaUnidadMedida.Id);

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
