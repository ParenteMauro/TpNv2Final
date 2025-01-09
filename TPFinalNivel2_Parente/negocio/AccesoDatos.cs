using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        public AccesoDatos()
        {
            conexion = new SqlConnection(ConfigurationManager.AppSettings["bd-key"]);
            
        }
        public string probando()
        {
            try
            {
                conexion.Open();
                return "Estado: " + conexion.State;
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }
    }
}
