using System;
using System.Collections.Generic;
namespace Prototipo
{
	public class Factura
	{
		public Factura ()
		{
		}

		public long NumeroFactura {	get;set;}
		public Usuario UsuarioQueLaGenero {get;	set;}
		public Cliente Cliente {get;	set;}
		public List<Producto> Productos{get;set;}
		public double Monto{get;set;}
		public DateTime FechaDeCreacion {get;set;}

		public void BuscarFactura(){
		}
		public void GenerarReportesFactura(){
		}
		public void GenerarFactura(){
		}
		public void ImprimirFactura(){
		}

	}
}

