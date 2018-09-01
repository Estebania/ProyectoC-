using System;
using System.Collections.Generic;

using System.Data;
using MySql.Data.MySqlClient;
namespace Prototipo
{
	public class Cliente
	{
		public Cliente ()
		{
		}
		public Cliente(int codigo){

			this.CodigoCliente = codigo;
		}

		public int CodigoCliente {get;	set;}
		public string NombreCliente {get;	set;}
		public string Direccion { get; set; }
		public string Correo { get; set; }
		public int CantidadDeFacturas {get;	set;}

		public void VerCliente(){}

		public static List<Cliente> PreCargarClientes(){
            


			List<Cliente> clientes = new List<Cliente>();


			MySqlCommand selecionar = new MySqlCommand("select * from Clientes;", connection.conectar());
            

            MySqlDataReader leer = selecionar.ExecuteReader();

            while (leer.Read())
            {
                Cliente ct = new Cliente();

                ct.CodigoCliente = leer.GetInt32(0);
                ct.NombreCliente = leer.GetString(1);
				ct.Direccion = leer.GetString(2);
				ct.Correo = leer.GetString(3);
                ct.CantidadDeFacturas = leer.GetInt32(4);
                
                clientes.Add(ct);

            }


			return clientes;
		}
		public static List<Cliente> NuevoCliente(){
			//Cuando se conecte a la base de datos debe insertar las repuestas

			List<Cliente> clientes = Cliente.PreCargarClientes();
            Console.WriteLine("Nombre del Cliente");
            string nombre = Console.ReadLine();
			Console.WriteLine("Direccion: ");
			string direccion = Console.ReadLine();
            Console.WriteLine("Correo");
			string coreo = Console.ReadLine();

            int CantidadFacturas = 0;

            Cliente unNuevoCliente = new Cliente()
            {
                NombreCliente = nombre,
                Direccion = direccion,
                Correo = coreo,
                CantidadDeFacturas = CantidadFacturas

            };

            clientes.Add(unNuevoCliente);

			MySqlCommand insertar = new MySqlCommand(string.Format("insert into Clientes (nombreCliente,direccion,correo,cantidadFacuras) values ('{0}','{1}','{2}',{3})",nombre,direccion,coreo,CantidadFacturas),connection.conectar());
   
			insertar.BeginExecuteNonQuery();
            return clientes;
		}

		public static void AgregarCliente(){
			Console.Clear();
            try
			{
				Console.WriteLine("Nombre del Cliente");
                string nombre = Console.ReadLine();
                Console.WriteLine("Direccion: ");
                string direccion = Console.ReadLine();
                Console.WriteLine("Correo");
                string coreo = Console.ReadLine();

                int CantidadFacturas = 0;


				MySqlCommand insertar = new MySqlCommand(string.Format("insert into Clientes (nombreCliente,direccion,correo,cantidadFacuras) values ('{0}','{1}','{2}',{3})", nombre, direccion, coreo, CantidadFacturas), connection.conectar());

                insertar.BeginExecuteNonQuery();
				Console.Clear();
                Console.WriteLine("Agregar otro cliente? \n 1-Si \n 2-No");
				int resp = int.Parse(Console.ReadLine());

                if (resp == 1)
				{
					Console.Clear();
					AgregarCliente();
				}

			}
			catch (Exception ex)
			{
                Console.WriteLine(ex.Message);
				AgregarCliente();
			}


		} 

		public static void BuscarTodoCliente(){

            MySqlCommand selecionar = new MySqlCommand("select * from Clientes;",connection.conectar());
			List<Cliente> clientes = new List<Cliente>();

                MySqlDataReader leer = selecionar.ExecuteReader();
                
                    while (leer.Read())
                    {
					Cliente ct = new Cliente();

					ct.CodigoCliente = leer.GetInt32(0);
					ct.NombreCliente = leer.GetString(1);
					ct.CantidadDeFacturas = leer.GetInt32(2);

					clientes.Add(ct);
                       
                    }
   

			foreach (var i in clientes)
            {
                Console.WriteLine(i.CodigoCliente + " " + i.NombreCliente + " " + i.CantidadDeFacturas);

            }

		}

		public static void SumarFactura(int codigo, int cantFac){

			MySqlCommand sumar= new MySqlCommand(string.Format("update  Clientes set cantidadFacuras ={0} where codigoCliente= {1};", cantFac, codigo), connection.conectar());

			sumar.BeginExecuteNonQuery();
		}

		public static string EnviarFactura(Cliente cliente, string mensaje){
			string estado = "";

			System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

			msg.To.Add(cliente.Correo);
			msg.Subject = "Aqui esta Su factura";
			msg.SubjectEncoding = System.Text.Encoding.UTF8;

			msg.Body = mensaje;
			msg.BodyEncoding = System.Text.Encoding.UTF8;
			msg.IsBodyHtml = true;
			msg.From = new System.Net.Mail.MailAddress("facturasystem001@gmail.com");


			System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();


			client.Credentials = new System.Net.NetworkCredential("facturasystem001@gmail.com", "FacSys001");

			client.Port = 587;
			client.EnableSsl = true;
			client.Host = "smtp.gmail.com";
            

            try
			{
				client.Send(msg);
				estado = "Se envio la factura";
			}
			catch (Exception ex)
			{

                Console.WriteLine(ex.Message);
				estado = "No se envio la factura";

			}

			return estado;

		}

	}
}

