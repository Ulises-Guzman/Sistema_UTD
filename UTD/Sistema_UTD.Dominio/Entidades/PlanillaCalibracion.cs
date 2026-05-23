using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_UTD.Dominio.Entidades
{
    internal class PlanillaCalibracion
    {
        public int Id { get; set; }
        public int NumPlanilla { get; set; }
        public string Observacion { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public bool Habilitacion { get; set; }
        public Usuario RealizadoPor { get; set; }
        public Usuario InspeccionadoPor { get; set; }
        public Usuario HabilitadoPor { get; set; }
        // Propiedades por navegacion y Asociacion por roles, enlace fisico y objeto en memoria
        public Instrumento InstrumentoId { get; set; }
        public Instrumento Instrumento { get; set; }
        public Instrumento PatronId { get; set; }
        public Instrumento Patron { get; set; }
        public bool Activo { get; set; }


        // Coleccion de "Detalle" PuntoCalibracion
        public List<PuntoCalibracion> PuntosCalibracion { get; set; } = new List<PuntoCalibracion>(); // Iniciaizacion para evitar "NullReferenceException" cuando se quirea cargar puntos en una planilla recien generada

        // ProtoMetodo
        // CrearPuntoCalibracion()
        // CrearHistorial()
    }
}
