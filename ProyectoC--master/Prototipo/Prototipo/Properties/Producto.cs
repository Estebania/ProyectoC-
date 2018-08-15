using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
namespace Prototipo
{
	public class Producto
	{
		public Producto()
		{
		}
		public int CodigoDeProducto { get; set; }
		public double Precio { get; set; }
		public int CantidadDisponible { get; set; }
		public string Descriocion { get; set; }
		public string NombreProducto { get; set; }

		public void VerProducto() { }
		public void BuscarProducto(int codigoProducto) { }

		public static List<Producto> PreCargarProductos()
		{

			List<Producto> productos = new List<Producto>();
			MySqlCommand selecionar = new MySqlCommand("select * from Productos;", connection.conectar());


			MySqlDataReader leer = selecionar.ExecuteReader();

			while (leer.Read())
			{
				Producto p = new Producto();

				p.CodigoDeProducto = leer.GetInt32(0);
				p.NombreProducto = leer.GetString(1);
				p.Precio = leer.GetDouble(2);
				p.CantidadDisponible = leer.GetInt32(3);
				p.Descriocion = leer.GetString(4);

				productos.Add(p);
			}


			return productos;
		}
		public static void RestarCantidad(int codigo, int cantpro)
        {

			MySqlCommand restar = new MySqlCommand(string.Format("update  Productos set cantidadDisponible ={0} where codigoProductos= {1};", cantpro, codigo), connection.conectar());

            restar.BeginExecuteNonQuery();
        }


	}
}

