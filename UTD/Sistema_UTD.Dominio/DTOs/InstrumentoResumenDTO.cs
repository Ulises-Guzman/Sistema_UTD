using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_UTD.Dominio.DTOs
{
    public class InstrumentoResumenDTO
    {
        public int Id { get; set; }
        public string NombUsuario { get; set; }
        public string Contrasenia { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public int RolId { get; set; }
        public string RolDescripcion { get; set; }
        public bool Activo { get; set; }
    }
}
