using System;
using System.Collections.Generic;
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
		public string NombreProducto { get; set; }

		public void VerProducto(){}
		public void BuscarProducto(int codigoProducto){}

		public static List<Producto> PreCargarProductos(){

			List<Producto> productos = new List<Producto>()
			{
				new Producto(){
					CodigoDeProducto = 1,
					Precio = 1499.99,
					CantidadDisponible = 10,
					NombreProducto = "Mochila totto",
                    Descriocion = "Utiles escolares"


				},
				new Producto(){
					CodigoDeProducto = 2,
                    Precio = 199.99,
                    CantidadDisponible = 15,
                    NombreProducto = "Cartera totto",
                    Descriocion = "Utiles escolares"


                },
				new Producto(){
					CodigoDeProducto = 3,
                    Precio = 500.00,
                    CantidadDisponible = 10,
                    NombreProducto = "Camisa Azul Lacoste",
                    Descriocion = "Prenda de bestir"


                },
				new Producto(){
					CodigoDeProducto = 4,
                    Precio = 1499.99,
                    CantidadDisponible = 10,
                    NombreProducto = "Cangurera totto",
                    Descriocion = "Utiles escolares"


                },
				new Producto(){
					CodigoDeProducto = 5,
                    Precio = 59.99,
                    CantidadDisponible = 10,
                    NombreProducto = "DVD-R MAXELL",
                    Descriocion = "Articulos de almacenamiento"


                },
				new Producto(){
					CodigoDeProducto = 6,
                    Precio = 9499.99,
                    CantidadDisponible = 10,
                    NombreProducto = "DELL LATITUDE",
                    Descriocion = "Dispositivos Informaticos"
                        

                },


			};
			return productos;
		}

	}
}

