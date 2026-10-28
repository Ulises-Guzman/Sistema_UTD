using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public List<ModeloResumenDTO> ListarModelo()
        {
            List<ModeloResumenDTO> lista = new List<ModeloResumenDTO>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT ModeloId, Descripcion, Activo FROM Modelos");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    ModeloResumenDTO modeloDTO = new ModeloResumenDTO();
                    modeloDTO.Id = (int)datos.Lector["ModeloId"];
                    modeloDTO.Descripcion = (string)datos.Lector["Descripcion"];
                    modeloDTO.Activo = (bool)datos.Lector["Activo"];

                    lista.Add(modeloDTO);
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
        } // DONE: Completar la lógica para listar modelos en el grid

        public void DesactivarModelo(int modeloSeleccionadoId, int usuarioLogueadoId)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE Modelos SET Activo = @activo, FechaModificacion = SYSDATETIME(), ModificacionUsuarioId = @modificacionUsuarioId " +
                                     "WHERE ModeloId = @id");
                datos.SetearParametro("@activo", 0);
                datos.SetearParametro("@modificacionUsuarioId", usuarioLogueadoId);
                datos.SetearParametro("@id", modeloSeleccionadoId);

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

        public void ModificarModelo(ModeloEdicionDTO modeloDTO, int usuarioLoguedoId)
        {
            AccesoDatos datos = new AccesoDatos();

            Modelo modificadoModelo = new Modelo();
            modificadoModelo.Id = modeloDTO.Id;
            modificadoModelo.Descripcion = modeloDTO.Descripcion;
            modificadoModelo.Activo = modeloDTO.Activo;

            try
            {
                datos.SetearConsulta("UPDATE Modelos " +
                                     "SET Descripcion = @descripcion, Activo = @activo, " +
                                         "FechaModificacion = SYSDATETIME(), ModificacionUsuarioId = @modificacionUsuarioId " +
                                     "WHERE ModeloId = @id");
                datos.SetearParametro("@descripcion", modificadoModelo.Descripcion);
                datos.SetearParametro("@activo", modificadoModelo.Activo);
                datos.SetearParametro("@modificacionUsuarioId", usuarioLoguedoId);
                datos.SetearParametro("@id", modificadoModelo.Id);

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
