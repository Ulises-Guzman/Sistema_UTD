using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_UTD.Datos;
using Sistema_UTD.Dominio.Entidades;
using Sistema_UTD.Dominio.DTOs;
using BCrypt.Net;
using System.Security.Cryptography;


namespace Sistema_UTD.Negocio
{
    public class UsuarioNegocio
    {
        public void AgregarUsuario(UsuarioCreacionDTO UsuarioDTO, int usuarioLogueadoId)
        {
            
            AccesoDatos datos = new AccesoDatos();
            //string hashContrasenia = BCrypt.Net.BCrypt.HashPassword(nuevoUsuarioDTO.Contrasenia);

            // 1. Mapeo de DTO a Entidad de Dominio (POCO)
            Usuario nuevoUsuario = new Usuario();
            nuevoUsuario.NombUsuario = UsuarioDTO.NombUsuario;
            nuevoUsuario.Contrasenia = BCrypt.Net.BCrypt.HashPassword(UsuarioDTO.Contrasenia);
            nuevoUsuario.Rol = new Rol();
            nuevoUsuario.Rol.Id = UsuarioDTO.RolId;
            nuevoUsuario.Apellido = UsuarioDTO.Apellido;
            nuevoUsuario.Nombre = UsuarioDTO.Nombre;
            nuevoUsuario.Activo = UsuarioDTO.Activo;

            try
            {
                // Validación si ya existe el usuario
                // 1. Verificación de existencia
                datos.SetearConsulta("SELECT COUNT(1) FROM Usuarios WHERE Usuario = @usuarioCheck");
                datos.SetearParametro("@usuarioCheck", nuevoUsuario.NombUsuario);

                int existe = (int)datos.EjecutarScalar();
                if (existe > 0)
                {
                    // Corta la ejecución y lanza el error hacia el CodeBehind
                    throw new Exception("El nombre de usuario ya se encuentra registrado y/o se bloqueó multiples intentos.");
                }

                // 2. Si no existe, limpieza de parámetros y se procede con el INSERT
                datos.LimpiarParametros();
                datos.CerrarConexion();

                datos.SetearConsulta("INSERT INTO Usuarios (Usuario, Contrasenia, Apellido, Nombre, RolId, Activo, FechaCreacion, CreacionUsuarioId) " +
                                     "VALUES (@usuario, @contrasenia, @apellido, @nombre, @rolId, @activo, SYSDATETIME(), @creacionUsuarioId)");
                datos.SetearParametro("@usuario", nuevoUsuario.NombUsuario);
                datos.SetearParametro("@contrasenia", nuevoUsuario.Contrasenia);
                datos.SetearParametro("@apellido", nuevoUsuario.Apellido);
                datos.SetearParametro("@nombre", nuevoUsuario.Nombre);
                datos.SetearParametro("@rolId", nuevoUsuario.Rol.Id);
                datos.SetearParametro("@activo", nuevoUsuario.Activo);
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
        } // DONE: Utilizar DTO

        public void ModificarUsuario(bool cambioContrasenia, UsuarioEdicionDTO usuarioDTO, int usuarioLogueadoId)
        {
            AccesoDatos datos = new AccesoDatos();

            // 1. Mapeo de DTO a Entidad de Dominio (POCO)
            Usuario modificadoUsuario = new Usuario();
            modificadoUsuario.Id = usuarioDTO.Id;
            modificadoUsuario.NombUsuario = usuarioDTO.NombUsuario;
            modificadoUsuario.Contrasenia = BCrypt.Net.BCrypt.HashPassword(usuarioDTO.Contrasenia);
            modificadoUsuario.Rol = new Rol();
            modificadoUsuario.Rol.Id = usuarioDTO.RolId;
            modificadoUsuario.Apellido = usuarioDTO.Apellido;
            modificadoUsuario.Nombre = usuarioDTO.Nombre;
            modificadoUsuario.Activo = usuarioDTO.Activo;

            try
            {
                if (cambioContrasenia)
                {
                    // Lado por contraseña cambiada
                    datos.SetearConsulta("UPDATE Usuarios " +
                                         "SET Usuario = @usuario, Contrasenia = @contrasenia, Apellido = @apellido, Nombre = @nombre, RolId = @rolId, Activo = @activo, " +
                                             "FechaModificacion = SYSDATETIME(), ModificacionUsuarioId = @modificacionUsuarioId " +
                                         "WHERE UsuarioId = @id");
                    datos.SetearParametro("@usuario", modificadoUsuario.NombUsuario);
                    datos.SetearParametro("@contrasenia", modificadoUsuario.Contrasenia);
                    datos.SetearParametro("@apellido", modificadoUsuario.Apellido);
                    datos.SetearParametro("@nombre", modificadoUsuario.Nombre);
                    datos.SetearParametro("@rolId", modificadoUsuario.Rol.Id);
                    datos.SetearParametro("@activo", true);
                    datos.SetearParametro("@modificacionUsuarioId", usuarioLogueadoId);
                    datos.SetearParametro("@id", modificadoUsuario.Id);

                    datos.EjecutarConsulta();

                }
                else
                {
                    // Lado por solo datos modificados
                    datos.SetearConsulta("UPDATE Usuarios " +
                                         "SET Usuario = @usuario, Apellido = @apellido, Nombre = @nombre, RolId = @rolId, Activo = @activo, " +
                                             "FechaModificacion = SYSDATETIME(), ModificacionUsuarioId = @modificacionUsuarioId " +
                                         "WHERE UsuarioId = @id");
                    datos.SetearParametro("@usuario", modificadoUsuario.NombUsuario);
                    datos.SetearParametro("@apellido", modificadoUsuario.Apellido);
                    datos.SetearParametro("@nombre", modificadoUsuario.Nombre);
                    datos.SetearParametro("@rolId", modificadoUsuario.Rol.Id);
                    datos.SetearParametro("@activo", true);
                    datos.SetearParametro("@modificacionUsuarioId", usuarioLogueadoId);
                    datos.SetearParametro("@id", modificadoUsuario.Id);

                    datos.EjecutarConsulta();
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.CerrarConexion();
            }

        } // DONE: Ultilizar DTO

        public void DesactivarUsuario(int usuarioSeleccionadoId, int usuarioLogueadoId)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE Usuarios SET Activo = @activo, FechaModificacion = SYSDATETIME(), ModificacionUsuarioId = @modificacionUsuarioId " +
                                     "WHERE UsuarioId = @id");
                datos.SetearParametro("@activo", 0);
                datos.SetearParametro("@modificacionUsuarioId", usuarioLogueadoId);
                datos.SetearParametro("@id", usuarioSeleccionadoId);

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

        public List<UsuarioResumenDTO> ListarUsuario()
        {
            // List<Usuario> lista = new List<Usuario>();
            List<UsuarioResumenDTO> lista = new List<UsuarioResumenDTO>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT U.UsuarioId, U.Usuario, U.Apellido, U.Nombre, U.Activo, U.RolId, R.Descripcion " +
                                     "FROM Usuarios U " +
                                     "INNER JOIN Roles R ON U.RolId = R.RolId");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    //Usuario aux = new Usuario();
                    //aux.Id = (int)datos.Lector["UsuarioId"];
                    //aux.NombUsuario = (string)datos.Lector["Usuario"];
                    //aux.Apellido = (string)datos.Lector["Apellido"];
                    //aux.Nombre = (string)datos.Lector["Nombre"];

                    //aux.Rol = new Rol();
                    //aux.Rol.Id = (int)datos.Lector["RolId"];
                    //aux.Rol.Descripcion = (string)datos.Lector["Descripcion"];
                    //aux.Activo = (bool)datos.Lector["Activo"];

                    UsuarioResumenDTO usuarioDTO = new UsuarioResumenDTO();
                    usuarioDTO.Id = (int)datos.Lector["UsuarioId"];
                    usuarioDTO.NombUsuario = (string)datos.Lector["Usuario"];
                    usuarioDTO.Apellido = (string)datos.Lector["Apellido"];
                    usuarioDTO.Nombre = (string)datos.Lector["Nombre"];
                    usuarioDTO.RolId = (int)datos.Lector["RolId"];
                    usuarioDTO.RolDescripcion = (string)datos.Lector["Descripcion"];
                    usuarioDTO.Activo = (bool)datos.Lector["Activo"];

                    lista.Add(usuarioDTO);
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
        } // DONE: Utilizar DTO

        public List<UsuarioResumenDTO> BuscarAvanzado(string campo, string palabraClave, string rol = "", string estado = "")
        {
            //List<Usuario> lista = new List<Usuario>();
            List<UsuarioResumenDTO> lista = new List<UsuarioResumenDTO>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT U.UsuarioId, U.Usuario, U.Apellido, U.Nombre, U.Activo, U.RolId, R.Descripcion " +
                                  "FROM Usuarios U " +
                                  "INNER JOIN Roles R ON U.RolId = R.RolId WHERE 1=1";

                if (!string.IsNullOrEmpty(palabraClave))
                {
                    if (campo == "Apellido")
                    {
                        consulta += " AND U.Apellido LIKE @palabraClave";
                        datos.SetearParametro("@palabraClave", "%" + palabraClave + "%");
                    }
                    else if (campo == "Nombre")
                    {
                        consulta += " AND U.Nombre LIKE @palabraClave";
                        datos.SetearParametro("@palabraClave", "%" + palabraClave + "%");
                    }
                }

                if (rol == "Administrador" || rol == "Técnico" || rol == "Cliente")
                {
                    consulta += " AND R.Descripcion LIKE @rol";
                    datos.SetearParametro("@rol", "%" + rol + "%");
                }

                if (estado == "Activo")
                {
                    consulta += " AND U.Activo = @activo";
                    datos.SetearParametro("@activo", 1);
                }
                else if (estado == "Inactivo")
                {
                    consulta += " AND U.Activo = @activo";
                    datos.SetearParametro("@activo", 0);
                }

                datos.SetearConsulta(consulta);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    //Usuario aux = new Usuario();
                    //aux.Id = (int)datos.Lector["UsuarioId"];
                    //aux.NombUsuario = (string)datos.Lector["Usuario"];
                    //aux.Apellido = (string)datos.Lector["Apellido"];
                    //aux.Nombre = (string)datos.Lector["Nombre"];

                    //aux.Rol = new Rol();
                    //aux.Rol.Id = (int)datos.Lector["RolId"];
                    //aux.Rol.Descripcion = (string)datos.Lector["Descripcion"];
                    //aux.Activo = (bool)datos.Lector["Activo"];

                    UsuarioResumenDTO usuarioDTO = new UsuarioResumenDTO();
                    usuarioDTO.Id = (int)datos.Lector["UsuarioId"];
                    usuarioDTO.NombUsuario = (string)datos.Lector["Usuario"];
                    usuarioDTO.Apellido = (string)datos.Lector["Apellido"];
                    usuarioDTO.Nombre = (string)datos.Lector["Nombre"];
                    usuarioDTO.RolId = (int)datos.Lector["RolId"];
                    usuarioDTO.RolDescripcion = (string)datos.Lector["Descripcion"];
                    usuarioDTO.Activo = (bool)datos.Lector["Activo"];

                    lista.Add(usuarioDTO);
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
        } // DONE: Utilizar DTO

        public string CargarTarjetaAdministradores()
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT COUNT(*) FROM Usuarios WHERE RolId = @rolId AND Activo = @activo");
                datos.SetearParametro("@rolId", 1);
                datos.SetearParametro("@activo", 1);

                // EjecutarScalar para obtener un solo valor y pasar a string
                object resultado = datos.EjecutarScalar();
                return resultado != null ? resultado.ToString() : "0";
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

        public string CargarTarjetaTecnicos()
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT COUNT(*) FROM Usuarios WHERE RolId = @rolId AND Activo = @activo");
                datos.SetearParametro("@rolId", 2);
                datos.SetearParametro("@activo", 1);

                object resultado = datos.EjecutarScalar();
                return resultado != null ? resultado.ToString() : "0";
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string CargarTrajetaClientes()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT COUNT(*) FROM Usuarios WHERE RolId = @rolId AND Activo = @activo");
                datos.SetearParametro("@rolId", 3);
                datos.SetearParametro("@activo", 1);

                object resultado = datos.EjecutarScalar();
                return resultado != null ? resultado.ToString() : "0";
            }
            catch (Exception)
            {

                throw;
            }
        }

        public UsuarioResumenDTO ValidarLogin(string nombUsuario, string contraseniaTextoPlano)
        {
            AccesoDatos datos = new AccesoDatos();
            UsuarioResumenDTO usuarioLogueado = null;

            try
            {
                string consulta = "SELECT U.UsuarioId, U.Usuario, U.Contrasenia, U.Apellido, U.Nombre, U.Activo, U.RolId, R.Descripcion " +
                                  "FROM Usuarios U " +
                                  "INNER JOIN Roles R ON U.RolId = R.RolId " +
                                  "WHERE U.Usuario = @usuario AND U.Activo = 1";

                datos.SetearConsulta(consulta);
                datos.SetearParametro("@usuario", nombUsuario);
                datos.EjecutarLectura();

                // Si el usuario existe
                if (datos.Lector.Read())
                {
                    string contraseniaHash = (string)datos.Lector["Contrasenia"];

                    bool loginValido = BCrypt.Net.BCrypt.Verify(contraseniaTextoPlano, contraseniaHash);

                    if (loginValido)
                    {
                        // Instancio y mapeo el objeto completo
                        //usuarioLogueado = new Usuario();
                        //usuarioLogueado.Id = (int)datos.Lector["UsuarioId"];
                        //usuarioLogueado.NombUsuario = (string)datos.Lector["Usuario"];
                        //usuarioLogueado.Apellido = (string)datos.Lector["Apellido"];
                        //usuarioLogueado.Nombre = (string)datos.Lector["Nombre"];
                        //usuarioLogueado.Activo = (bool)datos.Lector["Activo"];

                        //usuarioLogueado.Rol = new Rol();
                        //usuarioLogueado.Rol.Id = (int)datos.Lector["RolId"];
                        //usuarioLogueado.Rol.Descripcion = (string)datos.Lector["Descripcion"];
                        usuarioLogueado = new UsuarioResumenDTO();
                        usuarioLogueado.Id = (int)datos.Lector["UsuarioId"];
                        usuarioLogueado.NombUsuario = (string)datos.Lector["Usuario"];
                        usuarioLogueado.Apellido = (string)datos.Lector["Apellido"];
                        usuarioLogueado.Nombre = (string)datos.Lector["Nombre"];
                        usuarioLogueado.RolId = (int)datos.Lector["RolId"];
                        usuarioLogueado.RolDescripcion = (string)datos.Lector["Descripcion"];
                        usuarioLogueado.Activo = (bool)datos.Lector["Activo"];
                    }
                }

                // Retornará null si el usuario no existe o si la contraseña es incorrecta
                return usuarioLogueado;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.CerrarConexion();
            }
        } // DONE: Utilizar DTO
    }
}
