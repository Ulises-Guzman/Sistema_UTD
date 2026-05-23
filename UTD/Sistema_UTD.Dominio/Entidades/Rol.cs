using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_UTD.Dominio.Entidades
{
    internal class Rol
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }

        // Sobreescribir lo que devuelve Descripcion
    }
}
