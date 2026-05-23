using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_UTD.Dominio.Entidades
{
    internal class ModeloInstrumento
    {
        public int Id { get; set; }
        public string Nomenclatura { get; set; }
        public Modelo Modelo { get; set; }
        public Rango Rango { get; set; }
        public UnidadMedida UnidadMedida { get; set; }
        public bool Activo { get; set; }
    }
}
