using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESTFINAL
{
    public class EstadoItemInventario
    {
        public CapasDatosProducto Producto { get; set; }
        public int Cantidad { get; set; }
        public int StockMinimo { get; set; }
        public EstadoInventario Estado { get; set; }
        public string IdPaletAsignado { get; set; }
        public Bodega BodegaAsignada { get; set; } 

        public EstadoItemInventario(CapasDatosProducto producto, int cantidad, int stockMinimo = 10)
        {
            Producto = producto;
            Cantidad = cantidad;
            StockMinimo = stockMinimo;
            Estado = new EstadoDisponible();
            IdPaletAsignado = null;
            BodegaAsignada = Bodega.SinAsignar;
            ActualizarEstado();
        }

        public void AgregarStock(int cantidad)
        {
            Cantidad += cantidad;
            ActualizarEstado();
        }

        public bool RetirarStock(int cantidad)
        {
            if (Cantidad >= cantidad)
            {
                Cantidad -= cantidad;
                ActualizarEstado();
                return true;
            }
            return false;
        }

        private void ActualizarEstado()
        {
            Estado = Estado.Manejar(this);
        }

        public override string ToString()
        {
            string infoPalet = IdPaletAsignado != null ? $" [Palet: {IdPaletAsignado}]" : "";
            string infoBodega = BodegaAsignada != Bodega.SinAsignar ? $" [Bodega: {(int)BodegaAsignada}]" : ""; 
            return $"{Producto} - Stock: {Cantidad} - Estado: {Estado.ObtenerNombreEstado()}{infoPalet}{infoBodega}";
        }
    }
}