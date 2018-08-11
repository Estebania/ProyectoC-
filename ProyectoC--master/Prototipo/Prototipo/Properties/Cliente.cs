using System;
using System.Collections.Generic;
namespace Prototipo
{
	public class Cliente
	{
		public Cliente ()
		{
		}
		public int CodigoCliente {get;	set;}
		public string NombreCliente {get;	set;}
		public int CantidadDeFacturas {get;	set;}

		public void VerCliente(){}

		public static List<Cliente> PreCargarClientes(){
            //cuando lo enlacemos con la base de datos 
            //se cargaran lo clientes de la tabla cliente con la consulta
            //select codigoCliente, nombreCliente, cantidadCliente from clientes
			// en este link encontraran mas informacio https://www.youtube.com/watch?v=_4D6_j_JGes
			//https://www.youtube.com/watch?v=hglfksF7NnM


			List<Cliente> clientes = new List<Cliente>(){

				new Cliente(){
					CodigoCliente = 1,
					NombreCliente = "Juan",
                    CantidadDeFacturas = 2


				},
				new Cliente(){
                    CodigoCliente = 2,
                    NombreCliente = "Maria",
                    CantidadDeFacturas = 2


                },
				new Cliente(){
                    CodigoCliente = 3,
                    NombreCliente = "Pedro",
                    CantidadDeFacturas = 2


                },
			};

			return clientes;
		}
		public static List<Cliente> NuevoCliente(){
			//Cuando se conecte a la base de datos debe insertar las repuestas

			List<Cliente> clientes = Cliente.PreCargarClientes();
            Console.WriteLine("Nombre del Cliente");
            string nombre = Console.ReadLine();
            int CodigoCli = clientes.Count + 1;
            int CantidadFacturas = 0;

            Cliente unNuevoCliente = new Cliente()
            {
                NombreCliente = nombre,
                CodigoCliente = CodigoCli,
                CantidadDeFacturas = CantidadFacturas

            };

            clientes.Add(unNuevoCliente);
			return clientes;
		}

	}
}

