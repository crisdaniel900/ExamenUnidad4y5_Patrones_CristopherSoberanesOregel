using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESTFINAL
{
    public class PesoLigeroTipoProducto
    {
        public string Categoria { get; private set; }
        public string Descripcion { get; private set; }
        public double PesoEstandar { get; private set; }

        public PesoLigeroTipoProducto(string categoria, string descripcion, double pesoEstandar)
        {
            Categoria = categoria;
            Descripcion = descripcion;
            PesoEstandar = pesoEstandar;
        }
    }
}
