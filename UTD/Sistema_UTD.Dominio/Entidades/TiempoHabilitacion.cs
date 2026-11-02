using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_UTD.Dominio.Entidades
{
    public class TiempoHabilitacion
    {
        public int Id { get; set; }
        public int CantidadMeses { get; set; }
        public bool Activo { get; set; }

        public byte[] VersionRow { get; set; }
    }
}
