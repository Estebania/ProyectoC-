using System;
using System.Data;
using MySql.Data.MySqlClient;



namespace Prototipo
{
    public class connection
    {



        public connection()
        {
        }

        public static MySqlConnection conectar()
        {
           

			MySqlConnection cn = new MySqlConnection("server=127.0.0.1;database=FacturaSystem;Uid=root;pwd=mypass123;SslMode=none;");
            cn.Open();

            return cn;


        }




    }
}
