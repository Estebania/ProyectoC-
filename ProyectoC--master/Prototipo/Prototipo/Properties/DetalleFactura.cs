using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
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


            foreach (var i in df)
			{
				MySqlCommand insertar = new MySqlCommand(string.Format("insert into DetalleFactura (numeroFactura,nombreProducto,cantidadVendida,precioProducto) values('{0}','{1}',{2},{3}); ", i.NumeroFactura,i.NombreProducto,i.CantidadDelProducto,i.Precio), connection.conectar());

                insertar.BeginExecuteNonQuery();

			}

		}



    }
}
