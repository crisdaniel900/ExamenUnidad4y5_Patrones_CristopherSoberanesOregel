using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESTFINAL
{
    public class CapasDatosProducto
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public double Peso { get; set; }
        public string Categoria { get; set; }
        public DateTime FechaCreacion { get; set; }

        public CapasDatosProducto(string id, string nombre, double peso, string categoria)
        {
            Id = id;
            Nombre = nombre;
            Peso = peso;
            Categoria = categoria;
            FechaCreacion = DateTime.Now;
        }

        public override string ToString()
        {
            return $"[{Id}] {Nombre} ({Categoria}) - {Peso}kg";
        }
    }
}
