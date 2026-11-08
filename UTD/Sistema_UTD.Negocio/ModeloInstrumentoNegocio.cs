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
    public class ModeloInstrumentoNegocio
    {
        public void AgregarModeloInstrumento(ModeloInstrumentoCreacionDTO modeloInstrumentoDTO, int usuarioLogueadoId)
        {

            AccesoDatos datos = new AccesoDatos();

            ModeloInstrumento nuevoModeloInstrumento = new ModeloInstrumento
            {
                NumParte = modeloInstrumentoDTO.NumParte,
                Nomenclatura = modeloInstrumentoDTO.Nomenclatura,
                Marca = modeloInstrumentoDTO.Marca,
                Modelo = new Modelo { Id = modeloInstrumentoDTO.ModeloId },
                Rango = new Rango() { Id = modeloInstrumentoDTO.RangoId },
                UnidadMedida = new UnidadMedida { Id = modeloInstrumentoDTO.UnidadMedidaId },
            };

            try
            {
                // Validación si ya existe el usuario
                // 1. Verificación de existencia
                datos.SetearConsulta("SELECT COUNT(1) FROM ModelosInstrumento WHERE NumParte = @numParteCheck");
                datos.SetearParametro("@numParteCheck", nuevoModeloInstrumento.NumParte);

                int existe = (int)datos.EjecutarScalar();
                if (existe > 0)
                {
                    // Corta la ejecución y lanza el error hacia el CodeBehind
                    throw new Exception("El número de parte ya se encuentra registrado y/o se bloqueó multiples intentos.");
                }

                // 2. Si no existe, limpieza de parámetros y se procede con el INSERT
                datos.LimpiarParametros();
                datos.CerrarConexion();

                datos.SetearConsulta("INSERT INTO ModelosInstrumento (NumParte, Nomenclatura, Marca, ModeloId, RangoId, UnidadMedidaId, Activo, FechaCreacion, CreacionUsuarioId) " +
                                     "VALUES (@NumParte, @Nomenclatura, @Marca, @ModeloId, @RangoId, @UnidadMedidaId, @Activo, SYSDATETIME(), @CreacionUsuarioId)");
                datos.SetearParametro("@NumParte", nuevoModeloInstrumento.NumParte);
                datos.SetearParametro("@Nomenclatura", nuevoModeloInstrumento.Nomenclatura);
                datos.SetearParametro("@Marca", nuevoModeloInstrumento.Marca);
                datos.SetearParametro("@ModeloId", nuevoModeloInstrumento.Modelo.Id);
                datos.SetearParametro("@RangoId", nuevoModeloInstrumento.Rango.Id);
                datos.SetearParametro("@UnidadMedidaId", nuevoModeloInstrumento.UnidadMedida.Id);
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

        public void ModificarModeloInstrumento(ModeloInstrumentoEdicionDTO modeloInstrumentoDTO, int usuarioLoguedoId)
        {
            AccesoDatos datos = new AccesoDatos();

            ModeloInstrumento modificadoModeloInstrumento = new ModeloInstrumento
            {
                Id = modeloInstrumentoDTO.Id,
                NumParte = modeloInstrumentoDTO.NumParte,
                Nomenclatura = modeloInstrumentoDTO.Nomenclatura,
                Marca = modeloInstrumentoDTO.Marca,
                Modelo = new Modelo(),
                Rango = new Rango(),
                UnidadMedida = new UnidadMedida()
            };
            modificadoModeloInstrumento.Modelo.Id = modeloInstrumentoDTO.ModeloId;
            modificadoModeloInstrumento.Rango.Id = modeloInstrumentoDTO.RangoId;
            modificadoModeloInstrumento.UnidadMedida.Id = modeloInstrumentoDTO.UnidadMedidaId;

            try
            {
                datos.SetearConsulta("UPDATE ModelosInstrumento " +
                                     "SET NumParte = @NumParte, Nomenclatura = @Nomenclatura, Marca = @Marca, ModeloId = @ModeloId, RangoId = @RangoId, UnidadMedidaId = @UnidadMedidaId, Activo = @Activo, " +
                                         "FechaModificacion = SYSDATETIME(), ModificacionUsuarioId = @ModificacionUsuarioId " +
                                     "WHERE ModeloInstrumentoId = @id");
                datos.SetearParametro("@NumParte", modificadoModeloInstrumento.NumParte);
                datos.SetearParametro("@Nomenclatura", modificadoModeloInstrumento.Nomenclatura);
                datos.SetearParametro("@Marca", modificadoModeloInstrumento.Marca);
                datos.SetearParametro("@ModeloId", modificadoModeloInstrumento.Modelo.Id);
                datos.SetearParametro("@RangoId", modificadoModeloInstrumento.Rango.Id);
                datos.SetearParametro("@UnidadMedidaId", modificadoModeloInstrumento.UnidadMedida.Id);
                datos.SetearParametro("@Activo", true);
                datos.SetearParametro("@ModificacionUsuarioId", usuarioLoguedoId);
                datos.SetearParametro("@id", modificadoModeloInstrumento.Id);

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

        public void DesactivarModeloInstrumento(int modeloInstrumentoSeleccionadoId, int usuarioLogueadoId)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE ModelosInstrumento SET Activo = @Activo, FechaModificacion = SYSDATETIME(), ModificacionUsuarioId = @ModificacionUsuarioId " +
                                     "WHERE ModeloInstrumentoId = @id");
                datos.SetearParametro("@Activo", 0);
                datos.SetearParametro("@ModificacionUsuarioId", usuarioLogueadoId);
                datos.SetearParametro("@id", modeloInstrumentoSeleccionadoId);

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

        public List<ModeloInstrumentoResumenDTO> ListarModeloInstrumento()
        {
            List<ModeloInstrumentoResumenDTO> lista = new List<ModeloInstrumentoResumenDTO>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT " +
                                        "MI.ModeloInstrumentoId, " +
                                        "MI.NumParte, " +
                                        "MI.Nomenclatura, " +
                                        "MI.ModeloId, " +
                                        "MI.RangoId, " +
                                        "MI.UnidadMedidaId, " +
                                        "MI.Marca, " +
                                        "MI.Activo, " +
                                        "M.Descripcion AS ModeloDescripcion, " +
                                        "R.Descripcion AS RangoDescripcion, " +
                                        "U.Descripcion AS UnidadMedidaDescripcion " +
                                    "FROM ModelosInstrumento MI " +
                                    "INNER JOIN Modelos M ON MI.ModeloId = M.ModeloId " +
                                    "INNER JOIN Rangos R ON MI.RangoId = R.RangoId " +
                                    "INNER JOIN UnidadesMedida U ON MI.UnidadMedidaId = U.UnidadMedidaId " +
                                    "ORDER BY MI.Nomenclatura ASC");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    ModeloInstrumentoResumenDTO modeloInstrumentoDTO = new ModeloInstrumentoResumenDTO
                    {
                        Id = (int)datos.Lector["ModeloInstrumentoId"],
                        NumParte = (string)datos.Lector["NumParte"],
                        Nomenclatura = (string)datos.Lector["Nomenclatura"],
                        Marca = (string)datos.Lector["Marca"],
                        ModeloId = (int)datos.Lector["ModeloId"],
                        ModeloDescripcion = (string)datos.Lector["ModeloDescripcion"],
                        RangoId = (int)datos.Lector["RangoId"],
                        RangoDescripcion = (string)datos.Lector["RangoDescripcion"],
                        UnidadMedidaId = (int)datos.Lector["UnidadMedidaId"],
                        UnidadMedidaDescripcion = (string)datos.Lector["UnidadMedidaDescripcion"],
                        Activo = (bool)datos.Lector["Activo"]
                    };

                    lista.Add(modeloInstrumentoDTO);
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
