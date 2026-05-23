using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_UTD.Dominio.Entidades
{
    internal class EquipoCalibracion
    {
        // public int Id { get; set; } // Verificar
        public Instrumento NumSerie { get; set; }
        public Instrumento Marca { get; set; }
        public Instrumento Modelo { get; set; }
        public Instrumento Rango { get; set; }
        public Instrumento UnidadMedida { get; set; }

        // Sobre escribir lo que devuelve, verificar si asi se hace o no, y que devuelvo para este caso ¿Id?
    }
}
