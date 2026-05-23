using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_UTD.Dominio.Entidades
{
    internal class Usuario
    {
        public int Id { get; set; }
        public string NombUsuario { get; set; }
        public string Contrasenia { get; set; }
        public string Apellido { get; set; }
        public string Nombre{ get; set; }
        public Rol Rol { get; set; }
        public bool Activo { get; set; }
    }
}
