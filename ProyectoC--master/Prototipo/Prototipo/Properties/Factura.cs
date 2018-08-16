using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
namespace Prototipo
{
	public class Factura
	{
		public Factura()
		{
		}



		public long NumeroFactura { get; set; }
		public string UsuarioQueLaGenero { get; set; }
		public string NombreCliente { get; set; }
		public double Monto { get; set; }
		public DateTime FechaDeCreacion { get; set; }
		List<Factura> facturas = new List<Factura>();



		public void BuscarFactura()
		{
		}
		public static void GenerarReportesFactura()
		{
			Console.Clear();

			MySqlCommand selecionar = new MySqlCommand("select * from Facturas;", connection.conectar());
			List<Factura> facturas = new List<Factura>();

            MySqlDataReader leer = selecionar.ExecuteReader();

            while (leer.Read())
            {
				Factura ft = new Factura();
				ft.NumeroFactura = leer.GetInt32(0);
				ft.UsuarioQueLaGenero = leer.GetString(1);
				ft.NombreCliente = leer.GetString(2);
				ft.FechaDeCreacion = leer.GetDateTime(3);
				ft.Monto = leer.GetDouble(4);

                
                
				facturas.Add(ft);

            }
            





            foreach (var i in facturas)
            {
				Console.WriteLine(i.NumeroFactura + " " + i.UsuarioQueLaGenero + " " + i.NombreCliente + " " + i.FechaDeCreacion+ " "+i.Monto);

            }
			
		}
        
      
        //Metodo para generar la factura
		public static void GenerarFactura(Usuario user)
		{

			Console.Clear();

			//cargamos los datos 
			Factura f = new Factura();
			List<Cliente> clientes = Cliente.PreCargarClientes();
			List<Producto> productos = Producto.PreCargarProductos();
			List<Factura> facturas = Factura.PrecargarFacturas();


			List<DetalleFactura> detalleFacturas = new List<DetalleFactura>();
			double monto = 0.0;

			List<Cliente> unCliente = new List<Cliente>();
			int cont = 0;
			do
			{
				Console.Write("ID del cliente: ");
				int idCliente = int.Parse(Console.ReadLine());

				unCliente = clientes.FindAll(c => c.CodigoCliente == idCliente);
				cont++;
			} while (unCliente.Count == 0 && cont < 3);
			//Si no existe el cliente se crea uno nuevo o se regresara a la opcion generarFactura

			string nombreCliente = "";

			if (unCliente.Count == 0)
			{
				Console.WriteLine("Agregar Cliente: S/N");
				var respuesta = Console.ReadKey().Key;

				if (respuesta == ConsoleKey.S)
				{//El metodo NuevoCliente permite agregar al nuevo cliente
					clientes = Cliente.NuevoCliente();
					unCliente = unCliente = clientes.FindAll(c => c.CodigoCliente == (clientes.Count));
				}
				else if (respuesta == ConsoleKey.N)
				{
					//GenerarFactura(user,f.facturas);
				}


				foreach (var i in unCliente)
				{
					nombreCliente = i.NombreCliente;//Pasamos el nombre del cliente
				}


			}

			foreach (var i in unCliente)
            {
                nombreCliente = i.NombreCliente;//Pasamos el nombre del cliente
            }


			int numFactura = facturas.Count + 1;//numero de la factura


			char resp;

			do
			{
				Console.Clear();
				Console.WriteLine("Codigo del producto");//solicitar el codigo del producto
				int codigoProducto = int.Parse(Console.ReadLine());
				var cp = productos.FindAll(pc => pc.CodigoDeProducto == codigoProducto);//buscar el producto
				if (cp.Count != 0)
				{
					Console.WriteLine("Cantidad del Producto");//luego de buscar el producto solicitamos la
															   //cantidad del producto que se va a solicitar
					int cant = int.Parse(Console.ReadLine());

					int cantidadDisponible = 0;

					foreach (var i in cp)
					{
						cantidadDisponible = i.CantidadDisponible;
					}

					if (cant <= cantidadDisponible)// verficamos que tengamos la cantidad suficiente
					{
						DetalleFactura df = new DetalleFactura();
						foreach (var i in cp)
						{

							df = new DetalleFactura()
							{
								ID = detalleFacturas.Count,
								NombreProducto = i.NombreProducto,
								NumeroFactura = numFactura,
								CantidadDelProducto = cant,
								Precio = i.Precio
                                


							};
							monto = (i.Precio*cant) + monto;
                            
						}
						detalleFacturas.Add(df);
						cp[0].CantidadDisponible = cp[0].CantidadDisponible - cant;
					}

				}


				Console.WriteLine("Otro producto: S/N");
				resp = char.Parse(Console.ReadLine());

				Producto.RestarCantidad(cp[0].CodigoDeProducto,cp[0].CantidadDisponible);

			} while (resp == 'S' || resp =='s');


			Factura facturaGenerada = new Factura()
			{
				
				NombreCliente = nombreCliente,
				Monto = monto,
				UsuarioQueLaGenero = user.NombreUsuario
			
			};

			DetalleFactura.GuardarDetalle(detalleFacturas);
			GuardarFactura(facturaGenerada);
			int codigo = 0, cantfc = 0;
			foreach (var i in unCliente)
            {
				codigo = i.CodigoCliente;
				cantfc = i.CantidadDeFacturas + 1;
            }

			Cliente.SumarFactura(codigo,cantfc);

			ImprimirFactura(detalleFacturas, facturaGenerada,numFactura);//imprimimos la factura
            
			Usuario.Salir(user);
		}
        




		public static void ImprimirFactura(List<DetalleFactura> df,Factura f, int numfact)
		{
			Console.Clear();
            Console.WriteLine("******Factura*****");

                Console.WriteLine("Numero de factura      "+numfact);
				Console.WriteLine("Nombre del cliente     "+f.NombreCliente);
                foreach (var j in df)
				{
					Console.WriteLine(j.NombreProducto+"   "+j.Precio+"   "+j.CantidadDelProducto);
                    //la variable numero de factura en Detallefactura es solo para trabajar con la base de datos
				}
				Console.WriteLine("Monto total: " + f.Monto);
				Console.WriteLine("Lo atendio: "+ f.UsuarioQueLaGenero);
            

		}
		public static void GuardarFactura(Factura facturas){

			string user = facturas.UsuarioQueLaGenero;
			string cliente = facturas.NombreCliente;
			double monto = facturas.Monto;
			MySqlCommand insertar = new MySqlCommand(string.Format("insert into Facturas (usuariQueLaGenero,cliente,Monto) values('{0}','{1}',{2}); ", user, cliente,monto), connection.conectar());

            insertar.BeginExecuteNonQuery();
            
		}







		public static List<Factura> PrecargarFacturas(){
			
			List<Factura> facturas = new List<Factura>();

            MySqlCommand selecionar = new MySqlCommand("select * from Facturas;", connection.conectar());

           
            MySqlDataReader leer = selecionar.ExecuteReader();

            while (leer.Read())
            {
				Factura f = new Factura();
				f.NumeroFactura = leer.GetInt32(0);
				f.UsuarioQueLaGenero = leer.GetString(1);
				f.NombreCliente = leer.GetString(2);             

				facturas.Add(f);
            }


			return facturas;
		}

        

	}


}


