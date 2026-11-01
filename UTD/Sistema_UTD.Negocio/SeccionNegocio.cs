using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_UTD.Datos;
using Sistema_UTD.Dominio.DTOs;
using Sistema_UTD.Dominio.Entidades;

namespace Sistema_UTD.Negocio
{
    public class SeccionNegocio
    {
        public void AgregarSeccion(SeccionCreacionDTO seccionDTO, int usuarioLogueadoId)
        {
            AccesoDatos datos = new AccesoDatos();

            Seccion nuevaSeccion = new Seccion();
            nuevaSeccion.Descripcion = seccionDTO.Descripcion;

            try
            {
                datos.SetearConsulta("SELECT COUNT(1) FROM Secciones WHERE Descripcion = @seccionCheck");
                datos.SetearParametro("@seccionCheck", nuevaSeccion.Descripcion);

                int existe = (int)datos.EjecutarScalar();
                if (existe > 0)
                {
                    throw new Exception("La sección ya se encuentra registrada y/o se bloqueó multiples intentos.");
                }
                datos.LimpiarParametros();
                datos.CerrarConexion();

                datos.SetearConsulta("INSERT INTO Secciones (Descripcion, Activo, FechaCreacion, CreacionUsuarioId) " +
                                     "VALUES (@descripcion, @activo, SYSDATETIME(), @creacionUsuarioId)");
                datos.SetearParametro("@descripcion", nuevaSeccion.Descripcion);
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

        public List<SeccionResumenDTO> ListarSeccion()
        {
            List<SeccionResumenDTO> lista = new List<SeccionResumenDTO>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT SeccionId, Descripcion, Activo FROM Secciones " +
                                     "ORDER BY Descripcion ASC");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    SeccionResumenDTO seccionDTO = new SeccionResumenDTO();
                    seccionDTO.Id = (int)datos.Lector["SeccionId"];
                    seccionDTO.Descripcion = (string)datos.Lector["Descripcion"];
                    seccionDTO.Activo = (bool)datos.Lector["Activo"];

                    lista.Add(seccionDTO);
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

        public void DesactivarSeccion(int seccionSeleccionadaId, int usuarioLogueadoId)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE Secciones SET Activo = @activo, FechaModificacion = SYSDATETIME(), ModificacionUsuarioId = @modificacionUsuarioId " +
                                     "WHERE SeccionId = @id");
                datos.SetearParametro("@activo", 0);
                datos.SetearParametro("@modificacionUsuarioId", usuarioLogueadoId);
                datos.SetearParametro("@id", seccionSeleccionadaId);

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

        public void ModificarSeccion(SeccionEdicionDTO seccionDTO, int usuarioLoguedoId)
        {
            AccesoDatos datos = new AccesoDatos();

            Seccion modificadaSeccion = new Seccion();
            modificadaSeccion.Descripcion = seccionDTO.Descripcion;
            modificadaSeccion.Activo = seccionDTO.Activo;
            modificadaSeccion.Id = seccionDTO.Id;

            try
            {
                datos.SetearConsulta("UPDATE Secciones " +
                                     "SET Descripcion = @descripcion, Activo = @activo, " +
                                         "FechaModificacion = SYSDATETIME(), ModificacionUsuarioId = @modificacionUsuarioId " +
                                     "WHERE SeccionId = @id");
                datos.SetearParametro("@descripcion", modificadaSeccion.Descripcion);
                datos.SetearParametro("@activo", modificadaSeccion.Activo);
                datos.SetearParametro("@modificacionUsuarioId", usuarioLoguedoId);
                datos.SetearParametro("@id", modificadaSeccion.Id);

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
