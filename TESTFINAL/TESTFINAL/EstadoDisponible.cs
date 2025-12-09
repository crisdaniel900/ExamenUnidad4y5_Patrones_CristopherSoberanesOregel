using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESTFINAL
{
    public class EstadoDisponible : EstadoInventario
    {
        public override EstadoInventario Manejar(EstadoItemInventario contexto)
        {
            if (contexto.Cantidad == 0)
                return new EstadoSinStock();
            else if (contexto.Cantidad <= contexto.StockMinimo)
                return new EstadoStockBajo();
            return this;
        }

        public override string ObtenerNombreEstado() => "DISPONIBLE";
        public override ConsoleColor ObtenerColor() => ConsoleColor.Green;
    }
}
