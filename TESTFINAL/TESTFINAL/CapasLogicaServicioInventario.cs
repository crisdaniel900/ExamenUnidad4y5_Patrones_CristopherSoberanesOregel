using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESTFINAL
{
    public class CapasLogicaServicioInventario
    {
        private Dictionary<string, EstadoItemInventario> inventario = new Dictionary<string, EstadoItemInventario>();
        private ObjectPoolPoolPalets poolPalets = new ObjectPoolPoolPalets();
        private int siguienteIdProducto = 1;

    
        public void AgregarProducto(string categoria, string nombre, double peso, int cantidad)
        {
            var tipoProducto = FabricaFlyweightProducto.Instancia.ObtenerTipoProducto(categoria);
            var claveProducto = $"{categoria}-{nombre}";

            if (inventario.ContainsKey(claveProducto))
            {
                inventario[claveProducto].AgregarStock(cantidad);
                Console.WriteLine($"[INVENTARIO] Agregado stock a {nombre}: +{cantidad} unidades");
            }
            else
            {
                var producto = new CapasDatosProducto(
                    $"FRUTA-{siguienteIdProducto++}",
                    nombre,
                    peso > 0 ? peso : tipoProducto.PesoEstandar,
                    categoria
                );
                var item = new EstadoItemInventario(producto, cantidad);
                inventario[claveProducto] = item;
                Console.WriteLine($"[INVENTARIO] Nueva fruta agregada: {producto.Nombre} ({categoria})");
            }
        }

        public bool RetirarProducto(string claveProducto, int cantidad)
        {
            if (inventario.ContainsKey(claveProducto))
            {
                if (inventario[claveProducto].RetirarStock(cantidad))
                {
                    Console.WriteLine($"[INVENTARIO] Retirado: {cantidad} unidades");
                    return true;
                }
                else
                {
                    Console.WriteLine($"[INVENTARIO] Stock insuficiente");
                    return false;
                }
            }
            Console.WriteLine($"[INVENTARIO] Producto no encontrado");
            return false;
        }

        public bool AsignarPalet(string claveProducto)
        {
            if (inventario.ContainsKey(claveProducto))
            {
                var item = inventario[claveProducto];
                if (item.IdPaletAsignado == null)
                {
                    var palet = poolPalets.AdquirirPalet();
                    if (palet == null)
                    {
                        Console.WriteLine($"[LOGÍSTICA] No se pudo asignar palet: límite alcanzado");
                        return false;
                    }

                    if (palet.AgregarProducto(item.Producto))
                    {
                        item.IdPaletAsignado = palet.Id;
                        Console.WriteLine($"[LOGÍSTICA] Palet {palet.Id} asignado a {item.Producto.Nombre}");
                        return true;
                    }
                }
                else
                {
                    Console.WriteLine($"[LOGÍSTICA] El producto ya tiene un palet asignado");
                }
            }
            return false;
        }

        public bool LiberarPalet(string claveProducto)
        {
            if (inventario.ContainsKey(claveProducto))
            {
                var item = inventario[claveProducto];
                if (item.IdPaletAsignado != null)
                {
                    var palet = poolPalets.ObtenerTodosPalets().Find(p => p.Id == item.IdPaletAsignado);
                    if (palet != null)
                    {
                        poolPalets.LiberarPalet(palet);
                        item.IdPaletAsignado = null;
                        Console.WriteLine($"[LOGÍSTICA] Palet liberado de {item.Producto.Nombre}");
                        return true;
                    }
                }
            }
            return false;
        }


        public bool AsignarBodega(string claveProducto, Bodega bodega)
        {
            if (inventario.ContainsKey(claveProducto))
            {
                var item = inventario[claveProducto];
                item.BodegaAsignada = bodega;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"[BODEGA] {item.Producto.Nombre} asignado a Bodega {(int)bodega}");
                Console.ResetColor();
                return true;
            }
            Console.WriteLine($"[BODEGA] Producto no encontrado");
            return false;
        }

        public List<EstadoItemInventario> ObtenerProductosPorBodega(Bodega bodega)
        {
            return inventario.Values.Where(item => item.BodegaAsignada == bodega).ToList();
        }

        public void MostrarEstadisticasBodegas()
        {
            Console.WriteLine("\n=================================================================");
            Console.WriteLine("              ESTADÍSTICAS POR BODEGA                           ");
            Console.WriteLine("===================================================================");

            for (int i = 1; i <= 3; i++)
            {
                var bodega = (Bodega)i;
                var productos = ObtenerProductosPorBodega(bodega);
                var totalStock = productos.Sum(p => p.Cantidad);

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\nBodega {i}:");
                Console.ResetColor();
                Console.WriteLine($"  Variedades: {productos.Count}");
                Console.WriteLine($"  Stock total: {totalStock} unidades");

                if (productos.Count > 0)
                {
                    Console.WriteLine("  Productos:");
                    foreach (var item in productos)
                    {
                        Console.WriteLine($"    • {item.Producto.Nombre} ({item.Cantidad} unidades)");
                    }
                }
            }

            var sinAsignar = ObtenerProductosPorBodega(Bodega.SinAsignar);
            if (sinAsignar.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nSin asignar a bodega: {sinAsignar.Count} variedades");
                Console.ResetColor();
            }
        }

        // *** FIN NUEVOS MÉTODOS ***

        public List<EstadoItemInventario> ObtenerInventario()
        {
            return inventario.Values.ToList();
        }

        public List<CapasDatosPalet> ObtenerPalets()
        {
            return poolPalets.ObtenerTodosPalets();
        }

        public void MostrarEstadisticas()
        {
            poolPalets.MostrarEstadisticasPool();
            Console.WriteLine($"\n=== Estadísticas de Flyweight ===");
            Console.WriteLine($"Tipos de frutas en caché: {FabricaFlyweightProducto.Instancia.ObtenerTotalTipos()}");
            Console.WriteLine($"Variedades únicas en inventario: {inventario.Count}");
        }

        public int ObtenerPaletsRestantes()
        {
            return poolPalets.ObtenerPaletsRestantes();
        }
    }
}