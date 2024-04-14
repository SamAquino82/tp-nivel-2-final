using DominioClases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilidades
{
    public class FuncionesCategoria
    {
        
            public List<Descripcion> Listar()
            {
                List<Descripcion> lista = new List<Descripcion>();
                AccesoDatos datos = new AccesoDatos();
                try
                {
                    datos.setconsulta("select id,descripcion from CATEGORIAS");
                    datos.ejecutarlectura();
                    while (datos.lector.Read())
                    {
                        Descripcion aux = new Descripcion();
                        aux.TipoArticulo = (string)datos.lector["descripcion"];
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
