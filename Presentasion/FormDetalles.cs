using DominioClases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentasion
{
    public partial class FormDetalles : Form
    {
       private Articulo articulo=null;
        public FormDetalles(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Detalles de Articulos";
        }
        private void cargarimagen(string imagen)
        {
            try
            {
                if (imagen == null || imagen == "")
                {
                    ptxdetalles.Load("https://img.freepik.com/vector-premium/icono-marco-fotos-foto-vacia-blanco-vector-sobre-fondo-transparente-aislado-eps-10_399089-1290.jpg");
                }
                else
                {
                    ptxdetalles.Load(imagen);
                }
            }

            catch (System.Net.WebException)
            {
                ptxdetalles.Load("https://img.freepik.com/vector-premium/icono-marco-fotos-foto-vacia-blanco-vector-sobre-fondo-transparente-aislado-eps-10_399089-1290.jpg");
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error al cargar la imagen: " + ex.Message);
                ptxdetalles.Load("https://img.freepik.com/vector-premium/icono-marco-fotos-foto-vacia-blanco-vector-sobre-fondo-transparente-aislado-eps-10_399089-1290.jpg");
            }
        }

        private void FormDetalles_Load(object sender, EventArgs e)
        {
            if (articulo == null)
            {
                MessageBox.Show("No hay detalles sobre este Articulo");
            }
            else
            {
                lblarticulo.Text = articulo.Nombre;
                lblcodigo.Text = articulo.Codigo;
                lblcategoria.Text=articulo.Categoria.ToString();
                lblmarca.Text=articulo.Marca.ToString();
                lblprecio.Text=articulo.precio.ToString();
                lbldescripcion.Text = articulo.Descripcion;
                cargarimagen(articulo.Imagen);

            }
        }

        private void ptxdetalles_Click(object sender, EventArgs e)
        {

        }
    }
}
