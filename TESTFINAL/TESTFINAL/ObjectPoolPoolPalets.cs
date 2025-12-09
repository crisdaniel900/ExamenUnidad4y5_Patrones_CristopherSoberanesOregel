using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESTFINAL
{
    public class ObjectPoolPoolPalets
    {
        private List<CapasDatosPalet> paletsDisponibles = new List<CapasDatosPalet>();
        private List<CapasDatosPalet> paletsEnUso = new List<CapasDatosPalet>();
        private int siguienteId = 1;
        private readonly double capacidadPorDefecto;
        private const int MAXIMO_PALETS = 15; 

        public ObjectPoolPoolPalets(double capacidadPorDefecto = 1000.0)
        {
            this.capacidadPorDefecto = capacidadPorDefecto;
        }

        
        public CapasDatosPalet AdquirirPalet()
        {
            CapasDatosPalet palet;

            if (paletsDisponibles.Count > 0)
            {
               
                palet = paletsDisponibles[0];
                paletsDisponibles.RemoveAt(0);
                Console.WriteLine($"[POOL] Reutilizando palet {palet.Id}");
            }
            else
            {
               
                if (siguienteId > MAXIMO_PALETS)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[POOL] ¡LÍMITE ALCANZADO! No se pueden crear más de {MAXIMO_PALETS} palets.");
                    Console.ResetColor();
                    return null;
                }

             
                palet = new CapasDatosPalet($"PAL-{siguienteId++}", capacidadPorDefecto);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"[POOL] Creando nuevo palet {palet.Id} ({siguienteId - 1}/{MAXIMO_PALETS})");
                Console.ResetColor();
            }

            palet.EstaDisponible = false;
            paletsEnUso.Add(palet);
            return palet;
        }

       
        public void LiberarPalet(CapasDatosPalet palet)
        {
            if (paletsEnUso.Contains(palet))
            {
                paletsEnUso.Remove(palet);
                palet.Reiniciar();
                paletsDisponibles.Add(palet);
                Console.WriteLine($"[POOL] Palet {palet.Id} devuelto al pool");
            }
        }

        public void MostrarEstadisticasPool()
        {
            Console.WriteLine($"\n=== Estadísticas del Pool de Palets ===");
            Console.WriteLine($"Palets disponibles: {paletsDisponibles.Count}");
            Console.WriteLine($"Palets en uso: {paletsEnUso.Count}");
            Console.WriteLine($"Total creados: {siguienteId - 1}/{MAXIMO_PALETS}");
            Console.ForegroundColor = (siguienteId - 1) >= MAXIMO_PALETS ? ConsoleColor.Red : ConsoleColor.Green;
            Console.WriteLine($"Estado: {(siguienteId - 1 >= MAXIMO_PALETS ? "LÍMITE ALCANZADO" : "Disponible para crear más")}");
            Console.ResetColor();
        }

        public List<CapasDatosPalet> ObtenerTodosPalets()
        {
            var todosPalets = new List<CapasDatosPalet>();
            todosPalets.AddRange(paletsDisponibles);
            todosPalets.AddRange(paletsEnUso);
            return todosPalets;
        }

        public int ObtenerPaletsRestantes()
        {
            return MAXIMO_PALETS - (siguienteId - 1);
        }
    }
}
