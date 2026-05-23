using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_UTD.Dominio.Entidades
{
    internal class PuntoCalibracion
    {
        public int Id { get; set; }
        public decimal ValorInstrumento { get; set; }
        public decimal ValorReal { get; set; } // Patron
        public int Error { get; set; } // En ejecucion trasformar a %

        public int PlanillaCalibracionId { get; set; }
        public PlanillaCalibracion PlanillaCalibracion { get; set; }

    }
}
