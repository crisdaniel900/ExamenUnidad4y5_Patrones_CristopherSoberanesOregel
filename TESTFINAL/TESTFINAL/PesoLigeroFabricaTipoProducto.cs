using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESTFINAL
{
    public class FabricaFlyweightProducto
    {
        private Dictionary<string, PesoLigeroTipoProducto> tiposProducto = new Dictionary<string, PesoLigeroTipoProducto>();
        private static FabricaFlyweightProducto instancia;

        private FabricaFlyweightProducto()
        {
            
            tiposProducto["Pepino"] = new PesoLigeroTipoProducto("Pepino", "Pepino fresco", 0.3);
            tiposProducto["Jicama"] = new PesoLigeroTipoProducto("Jicama", "Jicama fresca", 0.8);
            tiposProducto["Mango"] = new PesoLigeroTipoProducto("Mango", "Mango fresco", 0.4);
            tiposProducto["Fresa"] = new PesoLigeroTipoProducto("Fresa", "Fresa fresca", 0.15);
        }

        public static FabricaFlyweightProducto Instancia
        {
            get
            {
                if (instancia == null)
                    instancia = new FabricaFlyweightProducto();
                return instancia;
            }
        }

        public PesoLigeroTipoProducto ObtenerTipoProducto(string categoria)
        {
            if (tiposProducto.ContainsKey(categoria))
                return tiposProducto[categoria];

            
            var nuevoTipo = new PesoLigeroTipoProducto(categoria, $"Categoría {categoria}", 5.0);
            tiposProducto[categoria] = nuevoTipo;
            return nuevoTipo;
        }

        public int ObtenerTotalTipos()
        {
            return tiposProducto.Count;
        }
    }
}
