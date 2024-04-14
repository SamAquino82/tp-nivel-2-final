using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DominioClases;

namespace Utilidades
{
    public class AccesoDatos
    {
        private SqlCommand comando = new SqlCommand();
        private SqlConnection conexion = new SqlConnection();
        private SqlDataReader Lector;

        public SqlDataReader lector
        {
            get { return Lector; }
        }

        public AccesoDatos()
        {
            conexion = new SqlConnection("server=.\\SQLEXPRESS; database=CATALOGO_DB; integrated security=true");
        }

        public void setconsulta(string consulta)
        {
            comando.CommandType=System.Data.CommandType.Text;
            comando.CommandText=consulta;
        }
        public void ejecutaraccion()
        {
            comando.Connection=conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ejecutarlectura()
        {
            comando.Connection=conexion;
            try
            {
                conexion.Open();
                Lector=comando.ExecuteReader();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void cerrarconexion()
        {
            if (lector != null)
                lector.Close();
            conexion.Close();

        }

        public void setdatos(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }
    }
}
