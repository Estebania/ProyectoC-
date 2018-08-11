using System;
using System.Collections.Generic;
namespace Prototipo
{
    public class DetalleFactura
    {
        public DetalleFactura()
        {
        }
        public int ID {get;set;}
		public string NombreProducto { get; set; }
		public int NumeroFactura { get; set; }
		public double Precio { get; set; }
		public int CantidadDelProducto{ get; set; }

		List<DetalleFactura> detalleFacturas = new List<DetalleFactura>();

		public static void GuardarDetalle(List<DetalleFactura> df){
			DetalleFactura detalle = new DetalleFactura();
			detalle.detalleFacturas = df;
		}

		public static List<DetalleFactura> PreCargarDetalle(){

			DetalleFactura detalle = new DetalleFactura();
			return detalle.detalleFacturas;
		}

    }
}
