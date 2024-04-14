using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DominioClases;


namespace Utilidades
{
    public class FuncionesArticulos
    {
        public List<Articulo> listar()
        {
            
            try
            {
                List<Articulo> lista = new List<Articulo>();
                AccesoDatos datos = new AccesoDatos();
                datos.setconsulta("select a.Id,Codigo,a.Nombre,a.Descripcion descrip,ImagenUrl,Precio,c.Descripcion Categoria,m.Descripcion Marca from ARTICULOS a, CATEGORIAS c, MARCAS m where a.IdMarca=m.Id and a.IdCategoria=c.Id and Precio>0");
                datos.ejecutarlectura();
                while (datos.lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Codigo = (string)datos.lector["Codigo"];
                    aux.Nombre = (string)datos.lector["Nombre"];
                    aux.Descripcion = (string)datos.lector["descrip"];
                    aux.Imagen = (string)datos.lector["ImagenUrl"];
                    aux.precio = Convert.ToDecimal(datos.lector["Precio"]);
                    aux.Categoria = new Descripcion();
                    aux.Categoria.TipoArticulo = (string)datos.lector["Categoria"];
                    aux.Marca = new Marcas();
                    aux.Marca.Descripcion = (string)datos.lector["Marca"];
                    aux.Id = (int)datos.lector["Id"];
                    lista.Add(aux);
                }

                datos.cerrarconexion();
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                throw; 
            }
        }
        public void Agregar(Articulo aux)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setconsulta("insert into ARTICULOS (Codigo,Nombre,Descripcion,IdMarca,IdCategoria,ImagenUrl,Precio) values (@Codigo,@Nombre,@Descripcion, @IdMarca,@IdCategoria,@ImagenUrl,@Precio)");
                datos.setdatos("@Codigo", aux.Codigo);
                datos.setdatos("@Nombre", aux.Nombre);
                datos.setdatos("@Descripcion", aux.Descripcion);
                datos.setdatos("@IdMarca", aux.Marca.Id);
                datos.setdatos("@IdCategoria", aux.Categoria.Id);
                datos.setdatos("@ImagenUrl", aux.Imagen);
                datos.setdatos("@Precio", aux.precio);
                datos.ejecutaraccion();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.cerrarconexion();
            }

        }

        public void Modificar(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setconsulta("update ARTICULOS set Codigo=@Codigo,Nombre=@Nombre,Descripcion=@Descripcion,IdMarca=@IdMarca,IdCategoria=@IdCategoria,ImagenUrl=@ImagenUrl,Precio=@Precio where Id=@Id");
                datos.setdatos("@Codigo",articulo.Codigo);
                datos.setdatos("@Nombre",articulo.Nombre);
                datos.setdatos("@Descripcion",articulo.Descripcion);
                datos.setdatos("@IdMarca",articulo.Marca.Id);
                datos.setdatos("@IdCategoria", articulo.Categoria.Id);
                datos.setdatos("@ImagenUrl",articulo.Imagen);
                datos.setdatos("@Precio",articulo.precio);
                datos.setdatos("@Id",articulo.Id);
                datos.ejecutaraccion();
                datos.cerrarconexion();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Eliminar(Articulo articulo)
        {
            AccesoDatos datos=new AccesoDatos();
            try
            {
                datos.setconsulta("update Articulos set Precio=@Precio where Id=@Id");
                datos.setdatos("@Precio",( articulo.precio * -1));
                datos.setdatos("@Id",articulo.Id);
                datos.ejecutaraccion();
                datos.cerrarconexion();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void EliminarFisica(Articulo articulo)
        {
            AccesoDatos datos=new AccesoDatos();
            try
            {
                datos.setconsulta("delete ARTICULOS where Id=@Id");
                datos.setdatos("@Id",articulo.Id);
                datos.ejecutaraccion();
                datos.cerrarconexion();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<Articulo> filtrar(string categoria,string marca,string precio,string valor) {
            AccesoDatos datos = new AccesoDatos();
            List<Articulo> lista = new List<Articulo>();
            try
            {
                string consulta = "select a.Id,Codigo,a.Nombre,a.Descripcion descrip,ImagenUrl,Precio,c.Descripcion Categoria,m.Descripcion Marca from ARTICULOS a, CATEGORIAS c, MARCAS m where a.IdMarca=m.Id and a.IdCategoria=c.Id and Precio>0 and ";
                definirconsulta(datos, consulta, categoria, marca, precio, valor);
                datos.ejecutarlectura();
                while (datos.lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Codigo = (string)datos.lector["Codigo"];
                    aux.Nombre = (string)datos.lector["Nombre"];
                    aux.Descripcion = (string)datos.lector["descrip"];
                    aux.Imagen = (string)datos.lector["ImagenUrl"];
                    aux.precio = Convert.ToDecimal(datos.lector["Precio"]);
                    aux.Categoria = new Descripcion();
                    aux.Categoria.TipoArticulo = (string)datos.lector["Categoria"];
                    aux.Marca = new Marcas();
                    aux.Marca.Descripcion = (string)datos.lector["Marca"];
                    aux.Id = (int)datos.lector["Id"];
                    lista.Add(aux);
                }
                datos.cerrarconexion();
                return lista;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public void definirconsulta(AccesoDatos datos,string consulta,string categoria, string marca, string precio, string valor)
        {
            if (categoria != "Sin definir" && marca == "Sin definir" && precio == "Sin definir")
            {
                datos.setconsulta(consulta + "c.Descripcion=@Categoria");
                datos.setdatos("@Categoria", categoria);
            }
            if (categoria == "Sin definir" && marca != "Sin definir" && precio == "Sin definir")
            {
                datos.setconsulta(consulta + "m.Descripcion=@Marca");
                datos.setdatos("@Marca", marca);

            }
            if (categoria == "Sin definir" && marca == "Sin definir" && precio != "Sin definir" && valor != "")
            {
                if (precio == "Mayor a")
                {
                    datos.setconsulta(consulta + "Precio>@Valor");
                    datos.setdatos("@Valor", valor);
                }
                else if (precio == "Menor a")
                {
                    datos.setconsulta(consulta + "Precio<@Valor");
                    datos.setdatos("@Valor", valor);
                }
                else if (precio == "Igual a")
                {
                    datos.setconsulta(consulta + "Precio=@Valor");
                    datos.setdatos("@Valor", valor);
                }

            }
            ///////////////////////////////////////////////////////////////////////////////////////////
            
            if (categoria != "Sin definir" && marca != "Sin definir" && precio == "Sin definir")
            {
                datos.setconsulta(consulta + "c.Descripcion=@Categoria and m.Descripcion=@Marca");
                datos.setdatos("@Categoria", categoria);
                datos.setdatos("@Marca", marca);
            }
            if (categoria == "Sin definir" && marca != "Sin definir" && precio != "Sin definir" && valor != "")
            {
                if (precio == "Mayor a")
                {
                    datos.setconsulta(consulta + "Precio>@Valor and m.Descripcion=@Marca");
                    datos.setdatos("@Valor", valor);
                    datos.setdatos("@Marca", marca);
                }
                else if (precio == "Menor a")
                {
                    datos.setconsulta(consulta + "Precio<@Valor and m.Descripcion=@Marca");
                    datos.setdatos("@Valor", valor);
                    datos.setdatos("@Marca", marca);
                }
                else if (precio == "Igual a")
                {
                    datos.setconsulta(consulta + "Precio=@Valor and m.Descripcion=@Marca");
                    datos.setdatos("@Valor", valor);
                    datos.setdatos("@Marca", marca);
                }
            }
            if (categoria != "Sin definir" && marca == "Sin definir" && precio != "Sin definir" && valor != "")
            {
                if (precio == "Mayor a")
                {
                    datos.setconsulta(consulta + "Precio>@Valor and c.Descripcion=@Categoria");
                    datos.setdatos("@Valor", valor);
                    datos.setdatos("@Categoria", categoria);
                }
                else if (precio == "Menor a")
                {
                    datos.setconsulta(consulta + "Precio<@Valor and c.Descripcion=@Categoria");
                    datos.setdatos("@Valor", valor);
                    datos.setdatos("@Categoria", categoria);
                }
                else if (precio == "Igual a")
                {
                    datos.setconsulta(consulta + "Precio=@Valor and c.Descripcion=@Categoria");
                    datos.setdatos("@Valor", valor);
                    datos.setdatos("@Categoria", categoria);
                }
            }
        }
      
    }


}
