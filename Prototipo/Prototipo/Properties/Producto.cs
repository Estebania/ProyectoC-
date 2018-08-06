using System;

namespace Prototipo
{
	public class Producto
	{
		public Producto ()
		{
		}
		public int CodigoDeProducto{get;	set;}
		public double Precio {get;	set;}
		public int CantidadDisponible {get;	set;}
		public string Descriocion {get;	set;}

		public void VerProducto(){}
		public void BuscarProducto(int codigoProducto){}

	}
}

