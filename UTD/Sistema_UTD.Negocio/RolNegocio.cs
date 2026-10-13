using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_UTD.Dominio;
using Sistema_UTD.Dominio.Entidades;
using Sistema_UTD.Dominio.DTOs;
using Sistema_UTD.Datos;

namespace Sistema_UTD.Negocio
{
    public class RolNegocio
    {
        public List<RolResumenDTO> Listar()
        {
            List<RolResumenDTO> lista = new List<RolResumenDTO>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT RolId, Descripcion FROM Roles");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    RolResumenDTO rolDTO = new RolResumenDTO();
                    rolDTO.Id = (int)datos.Lector["RolId"];
                    rolDTO.Descripcion = (string)datos.Lector["Descripcion"];

                    lista.Add(rolDTO);
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
        } // DONE: Pasar a DTO
    }
}
