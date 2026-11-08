using Sistema_UTD.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_UTD.Dominio.DTOs
{
    public class ModeloInstrumentoResumenDTO
    {
        public int Id { get; set; }
        public string NumParte { get; set; }
        public string Nomenclatura { get; set; }
        public string Marca { get; set; }
        public int ModeloId { get; set; }
        public string ModeloDescripcion { get; set; }
        public int RangoId { get; set; }
        public string RangoDescripcion { get; set; }
        public int UnidadMedidaId { get; set; }
        public string UnidadMedidaDescripcion { get; set; }
        public bool Activo { get; set; }
    }
}
