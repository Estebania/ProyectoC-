using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
namespace Prototipo
{
	public class Usuario
	{
		public Usuario ()
		{
		}
       
		public string ClaveUsuario {get;	set;}
		public int CodigoUsuario {get;	set;}
		public string NombreUsuario {get;	set;}

		public static void IniciarSeccion(){


			Console.BackgroundColor = ConsoleColor.Black;
			Console.WriteLine("CodigoUsuario");
            int codUsuario = int.Parse(Console.ReadLine());
            Console.WriteLine("Clave Usuario");
			string claveUsuario = null;

			while (true){
				var key = Console.ReadKey(true);
                Console.Write("*");
                if (key.Key== ConsoleKey.Enter)
				{
					break;
				}
				claveUsuario += key.KeyChar;
			}


			List<Usuario> usuarios = Usuario.PreCargarUsuarios();

			var users = usuarios.FindAll(u => u.ClaveUsuario == claveUsuario && u.CodigoUsuario == codUsuario);

			if(users.Count!=0){

				Usuario user = users[0];
				Menu(user);
               

			}
            else
			{
				Console.Clear();
				IniciarSeccion();
			}
		}

		public static List<Usuario> PreCargarUsuarios(){
			List<Usuario> usuarios = new List<Usuario>();
			MySqlCommand selecionar = new MySqlCommand("select * from Usuarios;", connection.conectar());


            MySqlDataReader leer = selecionar.ExecuteReader();

            while (leer.Read())
            {
				Usuario ur = new Usuario();
                
				ur.CodigoUsuario = leer.GetInt32(0);
				ur.NombreUsuario = leer.GetString(2);
				ur.ClaveUsuario = leer.GetString(1);

				usuarios.Add(ur);

            }

			return usuarios;
            

		}

		public static void Menu(Usuario user){
			Console.Clear();
			Console.BackgroundColor = ConsoleColor.DarkGray;
			Console.WriteLine("\n\t1-Facturar\n\n\t2-Imprimir reporte de facturas\n\n\t3-Agregar cliente\n\n\t4-Salir");
			Console.Write("\n Inserte el numero de la opcion: ");
            int resp = int.Parse(Console.ReadLine());


            if (resp == 1)
            {
                Factura.GenerarFactura(user);
            }
            else if (resp == 2)
            {
				Factura.GenerarReportesFactura();
				Salir(user);
            }else if (resp == 3)
			{
				Cliente.AgregarCliente();
				Salir(user);

			}
			else if(resp == 4){
				Console.Clear();
				Salir(user);
			}
            
		}
		public static void Salir(Usuario user){
			Console.BackgroundColor = ConsoleColor.DarkRed;
			Console.WriteLine("\n1-Volver al menu \n2-Salir");
			int resp = int.Parse(Console.ReadLine());

            
            if (resp == 1)
            {
				Console.BackgroundColor = ConsoleColor.Blue;
				Usuario.Menu(user);
            }
            else if (resp == 2)
            {
				Environment.Exit(0);
            }

		}
	}
}

