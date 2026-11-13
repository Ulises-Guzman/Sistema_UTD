using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_UTD.Dominio.DTOs
{
    public class InstrumentoEdicionDTO
    {
        public int Id { get; set; }
        public string NumSerie { get; set; }
        public bool PatronPrimario { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public int DestinoId { get; set; }
        public string Cuatrigrama { get; set; }
        public string DestinoDescripcion { get; set; }
        public int SeccionId { get; set; }
        public string SeccionDescripcion { get; set; }
        public int UsuarioId { get; set; }
        public string ApellidoYNombre { get; set; }
        public int EstadoId { get; set; }
        public string EstadoDescripcion { get; set; }
        public bool Habilitacion { get; set; }
        public DateTime FechaHabilitacion { get; set; }
        public int TiempoHabilitacionId { get; set; }
        public string TiempoHabilitacionDescripcion { get; set; }
        public bool Activo { get; set; }
    }
}
