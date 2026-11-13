using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_UTD.Dominio.DTOs
{
    public class DestinoResumenDTO
    {
        public int Id { get; set; }
        public string Cuatrigrama { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }

        public string CuatrigramaYDescripcion
        {
            get { return $"{Cuatrigrama} - {Descripcion}"; }
        }
    }
}
