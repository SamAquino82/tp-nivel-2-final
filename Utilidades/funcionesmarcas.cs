using DominioClases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilidades
{
    public class funcionesmarcas
    {
        public List<Marcas> Listar()
        {
            List<Marcas> lista = new List<Marcas>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setconsulta("select id,descripcion from Marcas");
                datos.ejecutarlectura();
                while (datos.lector.Read())
                {
                    Marcas aux = new Marcas();
                    aux.Descripcion = (string)datos.lector["descripcion"];
                    aux.Id = (int)datos.lector["id"];
                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
