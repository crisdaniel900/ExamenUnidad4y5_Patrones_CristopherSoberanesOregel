using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESTFINAL
{
    public class EstadoStockBajo : EstadoInventario
    {
        public override EstadoInventario Manejar(EstadoItemInventario contexto)
        {
            if (contexto.Cantidad == 0)
                return new EstadoSinStock();
            else if (contexto.Cantidad > contexto.StockMinimo)
                return new EstadoDisponible();
            return this;
        }

        public override string ObtenerNombreEstado() => "STOCK BAJO";
        public override ConsoleColor ObtenerColor() => ConsoleColor.Yellow;
    }
}
