using System;
using System.Collections.Generic;
namespace Prototipo
{
	public class Factura
	{
		public Factura()
		{
		}

		public Factura(List<Factura> facturas)
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
		public static void GenerarReportesFactura(Usuario user,List<Factura> fc)
		{
			Factura f = new Factura();
			f.facturas = fc;
			if(f.facturas.Count==0){
				Console.WriteLine("Aun no se a generado ninguna factura");
			}else{

				Console.WriteLine("Numero de factura | Usuario         | Cliente    | Monto | Fecha");
				foreach (var i in f.facturas)
				{
					Console.WriteLine(i.NumeroFactura+"\t\t"+i.UsuarioQueLaGenero+"\t\t"+i.NombreCliente+"\t\t"+i.Monto+"\t\t"+i.FechaDeCreacion.Date);
				};
			}
            Console.WriteLine();
			Usuario.Salir(user,fc);
		}
        
      
  //Metodo para generar la factura
		public static void GenerarFactura(Usuario user, List<Factura> fc)
		{
			//cargamos los datos 
			Factura f = new Factura();
			List<Cliente> clientes = Cliente.PreCargarClientes();
			List<Producto> productos = Producto.PreCargarProductos();
			List<Cliente> unCliente = new List<Cliente>();
			List<DetalleFactura> detalleFacturas = DetalleFactura.PreCargarDetalle();
			double monto = 0.0;
			f.facturas = fc;
			//El primer paso es buscar o registrar al cliente
			//Buscamos el cliente
			Console.WriteLine("Fecha de Hoy: ");
			DateTime date = DateTime.Parse(Console.ReadLine());
			int cont = 0;
			do
			{
				Console.WriteLine("ID del cliente: ");
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
					GenerarFactura(user,f.facturas);
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


			int numFactura = f.facturas.Count + 1;//numero de la factura


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

					}

				}


				Console.WriteLine("Otro producto: S/N");
				resp = char.Parse(Console.ReadLine());
			} while (resp == 'S' || resp =='s');


			Factura facturaGenerada = new Factura()
			{
				NumeroFactura = numFactura,
				NombreCliente = nombreCliente,
				Monto = monto,
				FechaDeCreacion = date.Date,
				UsuarioQueLaGenero = user.NombreUsuario
			
			};
			f.facturas.Add(facturaGenerada);
			DetalleFactura.GuardarDetalle(detalleFacturas);

			ImprimirFactura(detalleFacturas, f.facturas);//imprimimos la factura
            
			Usuario.Salir(user,f.facturas);
		}

		public static void ImprimirFactura(List<DetalleFactura> df, List<Factura> facturas)
		{
			Console.Clear();
            Console.WriteLine("******Factura*****");
			foreach (var i in facturas)
			{
                Console.WriteLine("Numero de factura      "+i.NumeroFactura);
				Console.WriteLine("Nombre del cliente     "+i.NombreCliente);
                foreach (var j in df)
				{
					Console.WriteLine(j.NombreProducto+"   "+j.Precio+"   "+j.CantidadDelProducto);
                    //la variable numero de factura en Detallefactura es solo para trabajar con la base de datos
				}
				Console.WriteLine("Monto total: " + i.Monto);
				Console.WriteLine("Lo atendio: "+ i.UsuarioQueLaGenero);
			}

		}
		public static void GuardarFactura(List<Factura> facturas){

            
		}



	}
	

}


