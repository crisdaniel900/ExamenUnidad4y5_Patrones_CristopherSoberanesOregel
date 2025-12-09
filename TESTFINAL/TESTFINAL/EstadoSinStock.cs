using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESTFINAL
{
    public class EstadoSinStock : EstadoInventario
    {
        public override EstadoInventario Manejar(EstadoItemInventario contexto)
        {
            if (contexto.Cantidad > 0)
                return new EstadoStockBajo();
            return this;
        }

        public override string ObtenerNombreEstado() => "SIN STOCK";
        public override ConsoleColor ObtenerColor() => ConsoleColor.Red;
    }
}
