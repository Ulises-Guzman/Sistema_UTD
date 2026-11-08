using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_UTD.Dominio.DTOs
{
    public class ModeloInstrumentoCreacionDTO
    {
        public string NumParte { get; set; }
        public string Nomenclatura { get; set; }
        public string Marca { get; set; }
        public int ModeloId { get; set; }
        public int RangoId { get; set; }
        public int UnidadMedidaId { get; set; }
        public bool Activo { get; set; }
    }
}
