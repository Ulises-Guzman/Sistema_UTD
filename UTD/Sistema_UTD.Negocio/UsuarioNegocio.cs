using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_UTD.Datos;
using Sistema_UTD.Dominio.Entidades;
using BCrypt.Net;
using System.Security.Cryptography;


namespace Sistema_UTD.Negocio
{
    public class UsuarioNegocio
    {
        public void AgregarUsuario(Usuario nuevo)
        {
            string hashContrasenia = BCrypt.Net.BCrypt.HashPassword(nuevo.Contrasenia);
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("INSERT INTO Usuarios (Usuario, Contrasenia, Apellido, Nombre, RolId, Activo) " +
                                     "VALUES (@usuario, @contrasenia, @apellido, @nombre, @rolId, @activo)");
                datos.SetearParametro("@usuario", nuevo.NombUsuario);
                datos.SetearParametro("@contrasenia", hashContrasenia);
                datos.SetearParametro("@apellido", nuevo.Apellido);
                datos.SetearParametro("@nombre", nuevo.Nombre);
                datos.SetearParametro("@rolId", nuevo.Rol.Id);
                datos.SetearParametro("@activo", nuevo.Activo);

                datos.EjecutarConsulta();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void ModificarUsuario(bool cambioContrasenia, Usuario modificado)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                if (cambioContrasenia)
                {
                    // lado por contraseña cambiada, llamar a logica de datos
                    string hashContrasenia = BCrypt.Net.BCrypt.HashPassword(modificado.Contrasenia);
                    datos.SetearConsulta("UPDATE Usuarios " +
                                         "SET Usuario = @usuario, Contrasenia = @contrasenia, Apellido = @apellido, Nombre = @nombre, RolId = @rolId " +
                                         "WHERE UsuarioId = @id");
                    datos.SetearParametro("@usuario", modificado.NombUsuario);
                    datos.SetearParametro("@contrasenia", hashContrasenia);
                    datos.SetearParametro("@apellido", modificado.Apellido);
                    datos.SetearParametro("@nombre", modificado.Nombre);
                    datos.SetearParametro("@rolId", modificado.Rol.Id);
                    datos.SetearParametro("@id", modificado.Id);

                    datos.EjecutarConsulta();

                }
                else
                {
                    // lado por solo datos modificados, llamar a logica de datos
                    datos.SetearConsulta("UPDATE Usuarios " +
                                         "SET Usuario = @usuario, Apellido = @apellido, Nombre = @nombre, RolId = @rolId " +
                                         "WHERE UsuarioId = @id");
                    datos.SetearParametro("@usuario", modificado.NombUsuario);
                    datos.SetearParametro("@apellido", modificado.Apellido);
                    datos.SetearParametro("@nombre", modificado.Nombre);
                    datos.SetearParametro("@rolId", modificado.Rol.Id);
                    datos.SetearParametro("@id", modificado.Id);

                    datos.EjecutarConsulta();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }

        }

        public void DesactivarUsuario(int usuarioId)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE Usuarios SET Activo = @activo WHERE UsuarioId = @id");
                datos.SetearParametro("@activo", 0);
                datos.SetearParametro("@id", usuarioId);

                datos.EjecutarConsulta();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void ActivarUsuario(int usuarioId)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE Usuarios SET Activo = @activo WHERE UsuarioId = @id");
                datos.SetearParametro("@activo", 1);
                datos.SetearParametro("@id", usuarioId);

                datos.EjecutarConsulta();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally 
            {
                datos.CerrarConexion();
            }
        }

        public List<Usuario> ListarUsuario()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT U.UsuarioId, U.Usuario, U.Apellido, U.Nombre, U.Activo, U.RolId, R.Descripcion " +
                                     "FROM Usuarios U " +
                                     "INNER JOIN Roles R ON U.RolId = R.RolId");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();
                    aux.Id = (int)datos.Lector["UsuarioId"];
                    aux.NombUsuario = (string)datos.Lector["Usuario"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.Nombre = (string)datos.Lector["Nombre"];

                    aux.Rol = new Rol();
                    aux.Rol.Id = (int)datos.Lector["RolId"];
                    aux.Rol.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Activo = (bool)datos.Lector["Activo"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public List<Usuario> BuscarAvanzado(string campo, string palabraClave, string rol = "", string estado = "")
        {
            List<Usuario> lista = new List<Usuario>();
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
                    Usuario aux = new Usuario();
                    aux.Id = (int)datos.Lector["UsuarioId"];
                    aux.NombUsuario = (string)datos.Lector["Usuario"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.Nombre = (string)datos.Lector["Nombre"];

                    aux.Rol = new Rol();
                    aux.Rol.Id = (int)datos.Lector["RolId"];
                    aux.Rol.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Activo = (bool)datos.Lector["Activo"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}
