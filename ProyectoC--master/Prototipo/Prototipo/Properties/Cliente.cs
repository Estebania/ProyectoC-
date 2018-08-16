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
                ct.CantidadDeFacturas = leer.GetInt32(2);

                clientes.Add(ct);

            }


			return clientes;
		}
		public static List<Cliente> NuevoCliente(){
			//Cuando se conecte a la base de datos debe insertar las repuestas

			List<Cliente> clientes = Cliente.PreCargarClientes();
            Console.WriteLine("Nombre del Cliente");
            string nombre = Console.ReadLine();
            
            int CantidadFacturas = 0;

            Cliente unNuevoCliente = new Cliente()
            {
                NombreCliente = nombre,
                CantidadDeFacturas = CantidadFacturas

            };

            clientes.Add(unNuevoCliente);

			MySqlCommand insertar = new MySqlCommand(string.Format("insert into Clientes (nombreCliente,cantidadFacuras) values ('{0}',{1})",nombre,CantidadFacturas),connection.conectar());
   
			insertar.BeginExecuteNonQuery();
            return clientes;
		}

		public static void AgregarCliente(){

            try
			{
				Console.WriteLine("Nombre del Cliente");
                string nombre = Console.ReadLine();

                int CantidadFacturas = 0;
				MySqlCommand insertar = new MySqlCommand(string.Format("insert into Clientes (nombreCliente,cantidadFacuras) values ('{0}',{1})", nombre, CantidadFacturas), connection.conectar());

                insertar.BeginExecuteNonQuery();


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

	}
}

