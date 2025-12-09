using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESTFINAL
{
    public class CapasDatosPalet
    {
        public string Id { get; set; }
        public double CapacidadMaxima { get; set; }
        public double PesoActual { get; set; }
        public List<CapasDatosProducto> Productos { get; set; }
        public bool EstaDisponible { get; set; }

        public CapasDatosPalet(string id, double capacidadMaxima)
        {
            Id = id;
            CapacidadMaxima = capacidadMaxima;
            PesoActual = 0;
            Productos = new List<CapasDatosProducto>();
            EstaDisponible = true;
        }

        public bool AgregarProducto(CapasDatosProducto producto)
        {
            if (PesoActual + producto.Peso <= CapacidadMaxima)
            {
                Productos.Add(producto);
                PesoActual += producto.Peso;
                return true;
            }
            return false;
        }

        public void Reiniciar()
        {
            Productos.Clear();
            PesoActual = 0;
            EstaDisponible = true;
        }

        public override string ToString()
        {
            return $"{Id} - {PesoActual}/{CapacidadMaxima}kg - {Productos.Count} productos - {(EstaDisponible ? "Disponible" : "En uso")}";
        }
    }
}
