using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESTFINAL
{
    public class CapasDisenoInterfazLogistica
    {
        private CapasLogicaServicioInventario servicioInventario = new CapasLogicaServicioInventario();

        public void Ejecutar()
        {
            Console.WriteLine("==================================================================");
            Console.WriteLine("   SISTEMA DE GESTIÓN LOGÍSTICA DE FRUTAS                       ");
            Console.WriteLine("   Frutas disponibles: Pepino, Jicama, Mango, Fresa            ");
            Console.WriteLine("   Bodegas disponibles: Bodega 1, 2, 3                         ");
            Console.WriteLine("   Patrones Implementados:                                      ");
            Console.WriteLine("   • Object Pool (Creacional) - PoolPalets [MÁXIMO: 15]        ");
            Console.WriteLine("   • Flyweight (Estructural) - FabricaFlyweightProducto        ");
            Console.WriteLine("   • State (Comportamiento) - EstadoInventario                  ");
            Console.WriteLine("   • Capas (Arquitectura) - 3 capas separadas                   ");
            Console.WriteLine("===================================================================\n");

            bool salir = false;
            while (!salir)
            {
                MostrarMenu();
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        MenuAgregarProducto();
                        break;
                    case "2":
                        MenuRetirarProducto();
                        break;
                    case "3":
                        MostrarInventario();
                        break;
                    case "4":
                        MenuAsignarPalet();
                        break;
                    case "5":
                        MenuLiberarPalet();
                        break;
                    case "6":
                        MostrarPalets();
                        break;
                    case "7":
                        servicioInventario.MostrarEstadisticas();
                        break;
                    case "8": 
                        MenuAsignarBodega();
                        break;
                    case "9": 
                        MenuVerPorBodega();
                        break;
                    case "0":
                        salir = true;
                        Console.WriteLine("\n¡Hasta luego!");
                        break;
                    default:
                        Console.WriteLine("Opción no válida");
                        break;
                }

                if (!salir)
                {
                    Console.WriteLine("\nPresione Enter para continuar...");
                    Console.ReadLine();
                }
            }
        }

        private void MostrarMenu()
        {
            Console.Clear();
            int paletsRestantes = servicioInventario.ObtenerPaletsRestantes();
            Console.WriteLine("\n==========================================");
            Console.WriteLine("         MENÚ PRINCIPAL                ");
            Console.WriteLine("=========================================");
            Console.WriteLine(" 1. Agregar fruta                      ");
            Console.WriteLine(" 2. Retirar fruta                      ");
            Console.WriteLine(" 3. Ver inventario de frutas           ");
            Console.WriteLine(" 4. Asignar palet a fruta              ");
            Console.WriteLine(" 5. Liberar palet de fruta             ");
            Console.WriteLine(" 6. Ver palets                         ");
            Console.WriteLine(" 7. Ver estadísticas                   ");
            Console.WriteLine(" 8. Asignar fruta a bodega             "); 
            Console.WriteLine(" 9. Ver productos por bodega           "); 
            Console.WriteLine(" 0. Salir                              ");
            Console.WriteLine("=========================================");
            Console.ForegroundColor = paletsRestantes > 0 ? ConsoleColor.Green : ConsoleColor.Red;
            Console.ResetColor();
            Console.Write("\nSeleccione una opción: ");
        }

        private void MenuAgregarProducto()
        {
            Console.WriteLine("\n=== AGREGAR FRUTA ===");
            Console.WriteLine("Tipos de fruta: Pepino, Jicama, Mango, Fresa");
            Console.Write("Tipo de fruta: ");
            string categoria = Console.ReadLine();

            Console.Write("Nombre/Variedad: ");
            string nombre = Console.ReadLine();

            Console.Write("Peso por unidad (kg, 0 para peso estándar): ");
            double peso = double.Parse(Console.ReadLine() ?? "0");

            Console.Write("Cantidad de unidades: ");
            int cantidad = int.Parse(Console.ReadLine() ?? "1");

            servicioInventario.AgregarProducto(categoria, nombre, peso, cantidad);
        }

        private void MenuRetirarProducto()
        {
            MostrarInventario();
            Console.Write("\nIngrese el número de fruta: ");
            int indice = int.Parse(Console.ReadLine() ?? "0") - 1;

            var inventario = servicioInventario.ObtenerInventario();
            if (indice >= 0 && indice < inventario.Count)
            {
                Console.Write("Cantidad de unidades a retirar: ");
                int cantidad = int.Parse(Console.ReadLine() ?? "1");

                var claveProducto = $"{inventario[indice].Producto.Categoria}-{inventario[indice].Producto.Nombre}";
                servicioInventario.RetirarProducto(claveProducto, cantidad);
            }
        }

        private void MostrarInventario()
        {
            Console.WriteLine("\n=================================================================");
            Console.WriteLine("                   INVENTARIO GLOBAL DE FRUTAS                   ");
            Console.WriteLine("===================================================================");

            var inventario = servicioInventario.ObtenerInventario();
            if (inventario.Count == 0)
            {
                Console.WriteLine("El inventario de frutas está vacío.");
                return;
            }

            for (int i = 0; i < inventario.Count; i++)
            {
                var item = inventario[i];
                Console.ForegroundColor = item.Estado.ObtenerColor();
                Console.WriteLine($"{i + 1}. {item}");
                Console.ResetColor();
            }
        }

        private void MenuAsignarPalet()
        {
            MostrarInventario();
            Console.Write("\nIngrese el número de fruta: ");
            int indice = int.Parse(Console.ReadLine() ?? "0") - 1;

            var inventario = servicioInventario.ObtenerInventario();
            if (indice >= 0 && indice < inventario.Count)
            {
                var claveProducto = $"{inventario[indice].Producto.Categoria}-{inventario[indice].Producto.Nombre}";
                servicioInventario.AsignarPalet(claveProducto);
            }
        }

        private void MenuLiberarPalet()
        {
            MostrarInventario();
            Console.Write("\nIngrese el número de fruta: ");
            int indice = int.Parse(Console.ReadLine() ?? "0") - 1;

            var inventario = servicioInventario.ObtenerInventario();
            if (indice >= 0 && indice < inventario.Count)
            {
                var claveProducto = $"{inventario[indice].Producto.Categoria}-{inventario[indice].Producto.Nombre}";
                servicioInventario.LiberarPalet(claveProducto);
            }
        }

        private void MostrarPalets()
        {
            Console.WriteLine("\n================================================================");
            Console.WriteLine("         PALETS REUTILIZABLES (Object Pool - Máx: 15)          ");
            Console.WriteLine("===================================================================");

            var palets = servicioInventario.ObtenerPalets();
            if (palets.Count == 0)
            {
                Console.WriteLine("No hay palets creados aún.");
                return;
            }

            foreach (var palet in palets)
            {
                Console.ForegroundColor = palet.EstaDisponible ? ConsoleColor.Green : ConsoleColor.Cyan;
                Console.WriteLine(palet);
                Console.ResetColor();

                if (palet.Productos.Count > 0)
                {
                    Console.WriteLine("  Productos en el palet:");
                    foreach (var producto in palet.Productos)
                    {
                        Console.WriteLine($"    • {producto}");
                    }
                }
            }
        }

       
        private void MenuAsignarBodega()
        {
            MostrarInventario();
            Console.Write("\nIngrese el número de fruta: ");
            int indice = int.Parse(Console.ReadLine() ?? "0") - 1;

            var inventario = servicioInventario.ObtenerInventario();
            if (indice >= 0 && indice < inventario.Count)
            {
                Console.WriteLine("\n=== BODEGAS DISPONIBLES ===");
                Console.WriteLine("1. Bodega 1");
                Console.WriteLine("2. Bodega 2");
                Console.WriteLine("3. Bodega 3");
                Console.Write("\nSeleccione bodega: ");
                int numBodega = int.Parse(Console.ReadLine() ?? "0");

                if (numBodega >= 1 && numBodega <= 3)
                {
                    var claveProducto = $"{inventario[indice].Producto.Categoria}-{inventario[indice].Producto.Nombre}";
                    servicioInventario.AsignarBodega(claveProducto, (Bodega)numBodega);
                }
                else
                {
                    Console.WriteLine("Bodega no válida");
                }
            }
        }

        private void MenuVerPorBodega()
        {
            Console.WriteLine("\n=== SELECCIONAR BODEGA ===");
            Console.WriteLine("1. Bodega 1");
            Console.WriteLine("2. Bodega 2");
            Console.WriteLine("3. Bodega 3");
            Console.WriteLine("4. Ver todas las bodegas (estadísticas)");
            Console.Write("\nSeleccione opción: ");
            int opcion = int.Parse(Console.ReadLine() ?? "0");

            if (opcion >= 1 && opcion <= 3)
            {
                var bodega = (Bodega)opcion;
                var productos = servicioInventario.ObtenerProductosPorBodega(bodega);

                Console.WriteLine($"\n=================================================================");
                Console.WriteLine($"              PRODUCTOS EN BODEGA {opcion}                            ");
                Console.WriteLine("===================================================================");

                if (productos.Count == 0)
                {
                    Console.WriteLine("No hay productos en esta bodega.");
                    return;
                }

                for (int i = 0; i < productos.Count; i++)
                {
                    var item = productos[i];
                    Console.ForegroundColor = item.Estado.ObtenerColor();
                    Console.WriteLine($"{i + 1}. {item}");
                    Console.ResetColor();
                }
            }
            else if (opcion == 4)
            {
                servicioInventario.MostrarEstadisticasBodegas();
            }
            else
            {
                Console.WriteLine("Opción no válida");
            }
        }
    }
}