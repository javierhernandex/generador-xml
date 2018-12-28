using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows;
using System.IO;

using System.Data.SqlClient;


namespace xml
{
    class coneccion
    {
            
       private SqlConnection m_cnn;
       
        // 1. Crear una instancia de la conexión
        String conn = ("Data Source=fcfdi.cjsgnbio4rra.us-east-1.rds.amazonaws.com;Persist Security Info=True;User ID=hermes;Password=12345678");

        public SqlConnection conectarbd = new SqlConnection();

        public coneccion()
        {

            m_cnn = new SqlConnection(conectarbd.ConnectionString = conn);

        }
      
     
        public void abrir ()
        {
            try
            {
                conectarbd.Open();
                Console.WriteLine("concecion abierta");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("error al abrir"+ ex.Message);
            }
        }

        public void cerrar()
        {
            conectarbd.Close();
            //Console.WriteLine("concecion cerrada");
        }
    }
}





    