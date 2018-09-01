using System;

namespace Prototipo
{
	class Program
	{
		public static void Main (string[] args)
		{
			Console.BackgroundColor = ConsoleColor.Blue;
			Console.Title = "FacturaSystem";
			Usuario.IniciarSeccion();

			//Factura.GenerarReportesFactura();
			//Producto.insertaProductos();
			Console.ReadKey();
		}
	}
}
