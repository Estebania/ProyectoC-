using System;
using System.Collections.Generic;
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
			Console.WriteLine("CodigoUsuario");
            int codUsuario = int.Parse(Console.ReadLine());
            Console.WriteLine("Clave Usuario");
			string claveUsuario = Console.ReadLine();

			List<Usuario> usuarios = Usuario.PreCargarUsuarios();
			List<Factura> facturas = new List<Factura>();//cuando se conecte a la base de datos se 
            //se creara un metodo que carge las facturas ya generadas
			var users = usuarios.FindAll(u => u.ClaveUsuario == claveUsuario && u.CodigoUsuario == codUsuario);

			if(users.Count!=0){

				Usuario user = users[0];
				Menu(user,facturas);
               

			}
		}

		public static List<Usuario> PreCargarUsuarios(){
			List<Usuario> usuarios = new List<Usuario>(){

				new Usuario (){
					CodigoUsuario = 1000,
                    NombreUsuario = "Betania Jimenez",
                    ClaveUsuario = "PrimerUsuario"
				},
				new Usuario (){
                    CodigoUsuario = 2000,
                    NombreUsuario = "Catherine Santana",
                    ClaveUsuario = "SegundoUsuario"
                },
				new Usuario (){
                    CodigoUsuario = 3000,
                    NombreUsuario = "Starling Contreras",
                    ClaveUsuario = "TercerUsuario"
                },
				new Usuario (){
                    CodigoUsuario = 4000,
                    NombreUsuario = "Manuel Mateo",
                    ClaveUsuario = "CuartoUsuario"
                },
				new Usuario (){
                    CodigoUsuario = 5000,
                    NombreUsuario = "Kevin Visioso",
                    ClaveUsuario = "QuintoUsuario"
                },
			};
			return usuarios;
            
		}

		public static void Menu(Usuario user, List<Factura> f){
			Console.Clear();
			Console.WriteLine("1-Facturar\n2-Imprimir reporte de facturas\n3-Salir");
            int resp = int.Parse(Console.ReadLine());


            if (resp == 1)
            {
                Factura.GenerarFactura(user,f);
            }
            else if (resp == 2)
            {
                Factura.GenerarReportesFactura(user,f);
            }else if (resp == 3)
			{
				Salir(user,f);
			}
            
		}
		public static void Salir(Usuario user,List<Factura> f){

            Console.WriteLine("\n\n1-Volver al menu \n2-Salir");
			int resp = int.Parse(Console.ReadLine());


            if (resp == 1)
            {
				Usuario.Menu(user,f);
            }
            else if (resp == 2)
            {
				Environment.Exit(0);
            }

		}
	}
}

