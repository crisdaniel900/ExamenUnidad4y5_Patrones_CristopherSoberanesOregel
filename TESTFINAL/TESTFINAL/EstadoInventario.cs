using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESTFINAL
{
    public abstract class EstadoInventario
    {
        public abstract EstadoInventario Manejar(EstadoItemInventario contexto);
        public abstract string ObtenerNombreEstado();
        public abstract ConsoleColor ObtenerColor();
    }
}
