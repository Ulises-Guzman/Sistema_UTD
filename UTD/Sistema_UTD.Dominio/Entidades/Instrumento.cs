using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_UTD.Dominio.Entidades
{
    internal class Instrumento : ModeloInstrumento
    {
        public new int Id { get; set; }
        public string NumSerie { get; set; }
        public bool PatronPrimario { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public Destino Destino { get; set; }
        public Seccion Seccion { get; set; }
        public Estado Estado { get; set; }
        public bool Habilitacion { get; set; }
        public DateTime FechaHabilitacion { get; set; }
        public TiempoHabilitacion TiempoHabilitacion { get; set; }
        public new bool Activo { get; set; }

        // ProtoMetodo
        // CrearHistorial()
        // ListarHistorial()
        // CrearPlanillaCalibracion()
        // ListarPlanillaCalibracion()

    }
}
