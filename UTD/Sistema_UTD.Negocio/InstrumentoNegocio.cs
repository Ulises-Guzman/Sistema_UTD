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
    public class InstrumentoNegocio
    {
        public void AgregarInstrumento(InstrumentoCreacionDTO instrumentoDTO, int usuarioLogueadoId)
        {

            AccesoDatos datos = new AccesoDatos();

            Instrumento nuevoInstrumento = new Instrumento
            {
                Modelo = new Modelo { Id = instrumentoDTO.ModeloInstrumentoId },
                NumSerie = instrumentoDTO.NumSerie,
                PatronPrimario = instrumentoDTO.PatronPrimario,
                FechaRecepcion = instrumentoDTO.FechaRecepcion,
                Destino = new Destino { Id = instrumentoDTO.DestinoId },
                Seccion = new Seccion { Id = instrumentoDTO.SeccionId },
                Usuario = new Usuario { Id = instrumentoDTO.UsuarioId },
                Estado = new Estado { Id = instrumentoDTO.EstadoId },
                //Habilitacion = instrumentoDTO.Habilitacion,
                //FechaHabilitacion = instrumentoDTO.FechaHabilitacion,
                //TiempoHabilitacion = new TiempoHabilitacion { Id = instrumentoDTO.TiempoHabilitacionId },
                Activo = instrumentoDTO.Activo
            };

            try
            {
                // Validación si ya existe el usuario
                // 1. Verificación de existencia
                datos.SetearConsulta("SELECT COUNT(1) FROM Instrumentos WHERE NumSerie = @numSerieCheck");
                datos.SetearParametro("@numSerieCheck", nuevoInstrumento.NumSerie);

                int existe = (int)datos.EjecutarScalar();
                if (existe > 0)
                {
                    // Corta la ejecución y lanza el error hacia el CodeBehind
                    throw new Exception("El número de serie ya se encuentra registrado y/o se bloqueó multiples intentos.");
                }

                // 2. Si no existe, limpieza de parámetros y se procede con el INSERT
                datos.LimpiarParametros();
                datos.CerrarConexion();

                datos.SetearConsulta("INSERT INTO Instrumentos (ModeloInstrumentoId, NumSerie, PatronPrimario, FechaRecepcion, DestinoId, SeccionId, UsuarioId, EstadoId, Habilitacion, Activo, FechaCreacion, CreacionUsuarioId) " +
                                     "VALUES (@ModeloInstrumentoId, @NumSerie, @PatronPrimario, @FechaRecepcion, @DestinoId, @SeccionId, @UsuarioId, @EstadoId, @Habilitacion, @Activo, SYSDATETIME(), @CreacionUsuarioId)");
                datos.SetearParametro("@ModeloInstrumentoId", nuevoInstrumento.Modelo.Id);
                datos.SetearParametro("@NumSerie", nuevoInstrumento.NumSerie);
                datos.SetearParametro("@PatronPrimario", nuevoInstrumento.PatronPrimario);
                datos.SetearParametro("@FechaRecepcion", nuevoInstrumento.FechaRecepcion);
                datos.SetearParametro("@DestinoId", nuevoInstrumento.Destino.Id);
                datos.SetearParametro("@SeccionId", nuevoInstrumento.Seccion.Id);
                datos.SetearParametro("@UsuarioId", nuevoInstrumento.Usuario.Id);
                datos.SetearParametro("@EstadoId", nuevoInstrumento.Estado.Id);
                datos.SetearParametro("@Habilitacion", 0);
                //FechaHabilitacion, @TiempoHabilitacionId,
                //@FechaHabilitacion, @TiempoHabilitacionId,
                //datos.SetearParametro("@FechaHabilitacion", nuevoInstrumento.FechaHabilitacion);
                //datos.SetearParametro("@TiempoHabilitacionId", nuevoInstrumento.TiempoHabilitacion);
                datos.SetearParametro("@Activo", true);
                datos.SetearParametro("@CreacionUsuarioId", usuarioLogueadoId);

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

        public void ModificarInstrumento(InstrumentoEdicionDTO instrumentoDTO, int usuarioLogueadoId)
        {
            AccesoDatos datos = new AccesoDatos();

            Instrumento modificadoInstrumento = new Instrumento
            {
                NumSerie = instrumentoDTO.NumSerie,
                PatronPrimario = instrumentoDTO.PatronPrimario,
                FechaRecepcion = instrumentoDTO.FechaRecepcion,
                Destino = new Destino { Id = instrumentoDTO.DestinoId },
                Seccion = new Seccion { Id = instrumentoDTO.SeccionId },
                Usuario = new Usuario { Id = instrumentoDTO.UsuarioId },
                Estado = new Estado { Id = instrumentoDTO.EstadoId },
                Habilitacion = instrumentoDTO.Habilitacion,
                FechaHabilitacion = instrumentoDTO.FechaHabilitacion,
                TiempoHabilitacion = new TiempoHabilitacion { Id = instrumentoDTO.TiempoHabilitacionId },
                Activo = instrumentoDTO.Activo,
                Id = instrumentoDTO.Id
            };

            try
            {
                datos.SetearConsulta("UPDATE Instrumentos " +
                                     "SET NumSerie = @NumSerie, PatronPrimario = @PatronPrimario, FechaRecepcion = @FechaRecepcion, DestinoId = @DestinoId, SeccionId = @SeccionId, UsuarioId = @UsuarioId, EstadoId = @EstadoId, Habilitacion = @Habilitacion, Activo = @Activo, " +
                                         "FechaModificacion = SYSDATETIME(), ModificacionUsuarioId = @ModificacionUsuarioId " +
                                     "WHERE InstrumentoId = @id");
                datos.SetearParametro("@NumSerie", modificadoInstrumento.NumSerie);
                datos.SetearParametro("@PatronPrimario", modificadoInstrumento.PatronPrimario);
                datos.SetearParametro("@FechaRecepcion", modificadoInstrumento.FechaRecepcion);
                datos.SetearParametro("@DestinoId", modificadoInstrumento.Destino.Id);
                datos.SetearParametro("@SeccionId", modificadoInstrumento.Seccion.Id);
                datos.SetearParametro("@UsuarioId", modificadoInstrumento.Usuario.Id);
                datos.SetearParametro("@EstadoId", modificadoInstrumento.Estado.Id);
                datos.SetearParametro("@Habilitacion", 0);
                //FechaHabilitacion = @FechaHabilitacion, TiempoHabilitacionId = @TiempoHabilitacionId,
                //datos.SetearParametro("@FechaHabilitacion", modificadoInstrumento.FechaHabilitacion);
                //datos.SetearParametro("@TiempoHabilitacionId", modificadoInstrumento.TiempoHabilitacion);
                datos.SetearParametro("@Activo", modificadoInstrumento.Activo);
                datos.SetearParametro("@ModificacionUsuarioId", usuarioLogueadoId);
                datos.SetearParametro("@id", modificadoInstrumento.Id);

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

        public void DesactivarInstrumento(int instrumentoSeleccionadoId, int usuarioLogueadoId)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE Instrumentos SET Activo = @Activo, FechaModificacion = SYSDATETIME(), ModificacionUsuarioId = @ModificacionUsuarioId " +
                                     "WHERE InstrumentoId = @id");
                datos.SetearParametro("@Activo", 0);
                datos.SetearParametro("@ModificacionUsuarioId", usuarioLogueadoId);
                datos.SetearParametro("@id", instrumentoSeleccionadoId);

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

        public List<InstrumentoResumenDTO> ListarInstrumento()
        {
            List<InstrumentoResumenDTO> lista = new List<InstrumentoResumenDTO>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT " +
                                        "I.InstrumentoId, " +
                                        "I.ModeloInstrumentoId, " +
                                        "I.NumSerie, " +
                                        "I.PatronPrimario, " +
                                        "I.FechaRecepcion, " +
                                        "I.DestinoId, " +
                                        "I.SeccionId, " +
                                        "I.UsuarioId, " +
                                        "I.EstadoId, " +
                                        "I.Habilitacion, " +
                                        "I.FechaHabilitacion, " +
                                        "I.TiempoHabilitacionId, " +
                                        "I.Activo, " +

                                        "MI.Nomenclatura, " +
                                        "MI.Marca, " +
                                        "D.Cuatrigrama, " +
                                        "D.Descripcion AS DestinoDescripcion, " +
                                        "S.Descripcion AS SeccionDescripcion, " +
                                        "U.Apellido, " +
                                        "U.Nombre, " +
                                        "E.Descripcion AS EstadoDescripcion, " +
                                        "T.CantidadMeses " +

                                    "FROM Instrumentos I " +
                                    "INNER JOIN ModelosInstrumento MI ON I.ModeloInstrumentoId = MI.ModeloInstrumentoId " +
                                    "INNER JOIN Destinos D ON I.DestinoId = D.DestinoId " +
                                    "INNER JOIN Secciones S ON I.SeccionId = S.SeccionId " +
                                    "INNER JOIN Usuarios U ON I.UsuarioId = U.UsuarioId " +
                                    "INNER JOIN Estados E ON I.EstadoId = E.EstadoId " +
                                    "LEFT JOIN TiemposHabilitacion T ON I.TiempoHabilitacionId = T.TiempoHabilitacionId " +
                                    "WHERE I.Activo = 1 " +
                                    "ORDER BY MI.Nomenclatura ASC");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    InstrumentoResumenDTO instrumentoDTO = new InstrumentoResumenDTO
                    {
                        Id = (int)datos.Lector["InstrumentoId"],
                        ModeloInstrumentoId = (int)datos.Lector["ModeloInstrumentoId"],
                        Nomenclatura = (string)datos.Lector["Nomenclatura"],
                        Marca = (string)datos.Lector["Marca"],
                        NumSerie = (string)datos.Lector["NumSerie"],
                        PatronPrimario = (bool)datos.Lector["PatronPrimario"],
                        FechaRecepcion = (DateTime)datos.Lector["FechaRecepcion"],
                        DestinoId = (int)datos.Lector["DestinoId"],
                        DestinoDescripcion = (string)datos.Lector["DestinoDescripcion"],
                        Cuatrigrama = (string)datos.Lector["Cuatrigrama"],
                        SeccionId = (int)datos.Lector["SeccionId"],
                        SeccionDescripcion = (string)datos.Lector["SeccionDescripcion"],
                        UsuarioId = (int)datos.Lector["UsuarioId"],
                        Apellido = (string)datos.Lector["Apellido"],
                        Nombre = (string)datos.Lector["Nombre"],
                        EstadoId = (int)datos.Lector["EstadoId"],
                        EstadoDescripcion = (string)datos.Lector["EstadoDescripcion"],
                        Habilitacion = (bool)datos.Lector["Habilitacion"],
                        FechaHabilitacion = datos.Lector["FechaHabilitacion"] != DBNull.Value
                                            ? (DateTime)datos.Lector["FechaHabilitacion"]
                                            : (DateTime?)null,
                        TiempoHabilitacionId = datos.Lector["TiempoHabilitacionId"] != DBNull.Value
                                               ? (int)datos.Lector["TiempoHabilitacionId"]
                                               : (int?)null,
                        CantidadMeses = datos.Lector["CantidadMeses"] != DBNull.Value
                                        ? (int)datos.Lector["CantidadMeses"]
                                        : (int?)null,
                        Activo = (bool)datos.Lector["Activo"]
                    };

                    lista.Add(instrumentoDTO);
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

        public List<InstrumentoResumenDTO> ListarNoHabilitados()
        {
            List<InstrumentoResumenDTO> lista = new List<InstrumentoResumenDTO>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT " +
                                        "I.InstrumentoId, " +
                                        "I.ModeloInstrumentoId, " +
                                        "I.NumSerie, " +
                                        "I.PatronPrimario, " +
                                        "I.FechaRecepcion, " +
                                        "I.DestinoId, " +
                                        "I.SeccionId, " +
                                        "I.UsuarioId, " +
                                        "I.EstadoId, " +
                                        "I.Habilitacion, " +
                                        "I.FechaHabilitacion, " +
                                        "I.TiempoHabilitacionId, " +
                                        "I.Activo, " +

                                        "MI.Nomenclatura, " +
                                        "MI.Marca, " +
                                        "D.Cuatrigrama, " +
                                        "D.Descripcion AS DestinoDescripcion, " +
                                        "S.Descripcion AS SeccionDescripcion, " +
                                        "U.Apellido, " +
                                        "U.Nombre, " +
                                        "E.Descripcion AS EstadoDescripcion, " +
                                        "T.CantidadMeses " +

                                    "FROM Instrumentos I " +
                                    "INNER JOIN ModelosInstrumento MI ON I.ModeloInstrumentoId = MI.ModeloInstrumentoId " +
                                    "INNER JOIN Destinos D ON I.DestinoId = D.DestinoId " +
                                    "INNER JOIN Secciones S ON I.SeccionId = S.SeccionId " +
                                    "INNER JOIN Usuarios U ON I.UsuarioId = U.UsuarioId " +
                                    "INNER JOIN Estados E ON I.EstadoId = E.EstadoId " +
                                    "LEFT JOIN TiemposHabilitacion T ON I.TiempoHabilitacionId = T.TiempoHabilitacionId " +
                                    "WHERE I.Activo = 1 AND I.Habilitacion = 0" +
                                    "ORDER BY MI.Nomenclatura ASC");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    InstrumentoResumenDTO instrumentoDTO = new InstrumentoResumenDTO
                    {
                        Id = (int)datos.Lector["InstrumentoId"],
                        ModeloInstrumentoId = (int)datos.Lector["ModeloInstrumentoId"],
                        Nomenclatura = (string)datos.Lector["Nomenclatura"],
                        Marca = (string)datos.Lector["Marca"],
                        NumSerie = (string)datos.Lector["NumSerie"],
                        PatronPrimario = (bool)datos.Lector["PatronPrimario"],
                        FechaRecepcion = (DateTime)datos.Lector["FechaRecepcion"],
                        DestinoId = (int)datos.Lector["DestinoId"],
                        DestinoDescripcion = (string)datos.Lector["DestinoDescripcion"],
                        Cuatrigrama = (string)datos.Lector["Cuatrigrama"],
                        SeccionId = (int)datos.Lector["SeccionId"],
                        SeccionDescripcion = (string)datos.Lector["SeccionDescripcion"],
                        UsuarioId = (int)datos.Lector["UsuarioId"],
                        Apellido = (string)datos.Lector["Apellido"],
                        Nombre = (string)datos.Lector["Nombre"],
                        EstadoId = (int)datos.Lector["EstadoId"],
                        EstadoDescripcion = (string)datos.Lector["EstadoDescripcion"],
                        Habilitacion = (bool)datos.Lector["Habilitacion"],
                        FechaHabilitacion = datos.Lector["FechaHabilitacion"] != DBNull.Value
                                            ? (DateTime)datos.Lector["FechaHabilitacion"]
                                            : (DateTime?)null,
                        TiempoHabilitacionId = datos.Lector["TiempoHabilitacionId"] != DBNull.Value
                                               ? (int)datos.Lector["TiempoHabilitacionId"]
                                               : (int?)null,
                        CantidadMeses = datos.Lector["CantidadMeses"] != DBNull.Value
                                        ? (int)datos.Lector["CantidadMeses"]
                                        : (int?)null,
                        Activo = (bool)datos.Lector["Activo"]
                    };

                    lista.Add(instrumentoDTO);
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

        public List<InstrumentoResumenDTO> ListarHabilitados()
        {
            List<InstrumentoResumenDTO> lista = new List<InstrumentoResumenDTO>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT " +
                                        "I.InstrumentoId, " +
                                        "I.ModeloInstrumentoId, " +
                                        "I.NumSerie, " +
                                        "I.PatronPrimario, " +
                                        "I.FechaRecepcion, " +
                                        "I.DestinoId, " +
                                        "I.SeccionId, " +
                                        "I.UsuarioId, " +
                                        "I.EstadoId, " +
                                        "I.Habilitacion, " +
                                        "I.FechaHabilitacion, " +
                                        "I.TiempoHabilitacionId, " +
                                        "I.Activo, " +

                                        "MI.Nomenclatura, " +
                                        "MI.Marca, " +
                                        "D.Cuatrigrama, " +
                                        "D.Descripcion AS DestinoDescripcion, " +
                                        "S.Descripcion AS SeccionDescripcion, " +
                                        "U.Apellido, " +
                                        "U.Nombre, " +
                                        "E.Descripcion AS EstadoDescripcion, " +
                                        "T.CantidadMeses " +

                                    "FROM Instrumentos I " +
                                    "INNER JOIN ModelosInstrumento MI ON I.ModeloInstrumentoId = MI.ModeloInstrumentoId " +
                                    "INNER JOIN Destinos D ON I.DestinoId = D.DestinoId " +
                                    "INNER JOIN Secciones S ON I.SeccionId = S.SeccionId " +
                                    "INNER JOIN Usuarios U ON I.UsuarioId = U.UsuarioId " +
                                    "INNER JOIN Estados E ON I.EstadoId = E.EstadoId " +
                                    "LEFT JOIN TiemposHabilitacion T ON I.TiempoHabilitacionId = T.TiempoHabilitacionId " +
                                    "WHERE I.Activo = 1 AND I.Habilitacion = 1" +
                                    "ORDER BY MI.Nomenclatura ASC");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    InstrumentoResumenDTO instrumentoDTO = new InstrumentoResumenDTO
                    {
                        Id = (int)datos.Lector["InstrumentoId"],
                        ModeloInstrumentoId = (int)datos.Lector["ModeloInstrumentoId"],
                        Nomenclatura = (string)datos.Lector["Nomenclatura"],
                        Marca = (string)datos.Lector["Marca"],
                        NumSerie = (string)datos.Lector["NumSerie"],
                        PatronPrimario = (bool)datos.Lector["PatronPrimario"],
                        FechaRecepcion = (DateTime)datos.Lector["FechaRecepcion"],
                        DestinoId = (int)datos.Lector["DestinoId"],
                        DestinoDescripcion = (string)datos.Lector["DestinoDescripcion"],
                        Cuatrigrama = (string)datos.Lector["Cuatrigrama"],
                        SeccionId = (int)datos.Lector["SeccionId"],
                        SeccionDescripcion = (string)datos.Lector["SeccionDescripcion"],
                        UsuarioId = (int)datos.Lector["UsuarioId"],
                        Apellido = (string)datos.Lector["Apellido"],
                        Nombre = (string)datos.Lector["Nombre"],
                        EstadoId = (int)datos.Lector["EstadoId"],
                        EstadoDescripcion = (string)datos.Lector["EstadoDescripcion"],
                        Habilitacion = (bool)datos.Lector["Habilitacion"],
                        FechaHabilitacion = datos.Lector["FechaHabilitacion"] != DBNull.Value
                                            ? (DateTime)datos.Lector["FechaHabilitacion"]
                                            : (DateTime?)null,
                        TiempoHabilitacionId = datos.Lector["TiempoHabilitacionId"] != DBNull.Value
                                               ? (int)datos.Lector["TiempoHabilitacionId"]
                                               : (int?)null,
                        CantidadMeses = datos.Lector["CantidadMeses"] != DBNull.Value
                                        ? (int)datos.Lector["CantidadMeses"]
                                        : (int?)null,
                        Activo = (bool)datos.Lector["Activo"]
                    };

                    lista.Add(instrumentoDTO);
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
    }
}
