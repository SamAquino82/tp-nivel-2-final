using DominioClases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilidades;
using System.Configuration;

namespace Presentasion
{
    public partial class frmaltaarticulo : Form
    {
        public Articulo articulo1 = null;
        private OpenFileDialog archivo =null;
        public frmaltaarticulo()
        {
            InitializeComponent();
            Text = "Agregar Articulo";

        }
        public frmaltaarticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo1 = articulo;
            Text = "Modificar Articulo";
            
        }

        private void btnaceptar_Click(object sender, EventArgs e)
        { 
                FuncionesArticulos funcion = new FuncionesArticulos();

            if (verificarespaciosvacios())
            {
                MessageBox.Show("Por favor, complete todos los campos antes de continuar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
             
            if (articulo1 == null)
                {
                    articulo1 = new Articulo();
                    articulo1.Codigo = txtcodigo.Text;
                    articulo1.Nombre = txtnombre.Text;
                    articulo1.Descripcion = txtdescripcion.Text;
                    articulo1.Imagen = txtimagen.Text;
                    articulo1.precio = int.Parse(txtprecio.Text);
                    articulo1.Categoria = (Descripcion)cbxcategoria.SelectedItem;
                    articulo1.Marca = (Marcas)cbxmarca.SelectedItem;
                    funcion.Agregar(articulo1);
                    MessageBox.Show("Articulo Agregado exitosamente", "informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    articulo1.Codigo = txtcodigo.Text;
                    articulo1.Nombre = txtnombre.Text;
                    articulo1.Descripcion = txtdescripcion.Text;
                    articulo1.Imagen = txtimagen.Text;
                    articulo1.precio = decimal.Parse(txtprecio.Text);
                    articulo1.Categoria = (Descripcion)cbxcategoria.SelectedItem;
                    articulo1.Marca = (Marcas)cbxmarca.SelectedItem;
    
                    funcion.Modificar(articulo1);
                    MessageBox.Show("Articulo" + articulo1.Nombre + " Modificado Exitosamente", "informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                } 
           
        }

        public bool verificarespaciosvacios()
        {
            bool camposVacios = false;

            
            if (txtcodigo.Text=="")
            {
                txtcodigo.BackColor = Color.Red;
                camposVacios = true;
            }
            if (txtnombre.Text=="")
            {
                txtnombre.BackColor = Color.Red;
                camposVacios = true;
            }
            if (txtdescripcion.Text == "")
            {
                txtdescripcion.BackColor = Color.Red;
                camposVacios = true;
            }
            if (txtimagen.Text == "")
            {
                txtimagen.BackColor = Color.Red;
                camposVacios = true;
            }
            if (txtprecio.Text == "")
            {
                txtprecio.BackColor = Color.Red;
                camposVacios = true;
            }
            

            return camposVacios;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmaltaarticulo_Load(object sender, EventArgs e)
        {
            try
            {
                FuncionesCategoria funcioncategoria = new FuncionesCategoria();
                funcionesmarcas funcionmarca = new funcionesmarcas();
                cbxcategoria.DataSource = funcioncategoria.Listar();
                cbxmarca.DataSource = funcionmarca.Listar();
                if(articulo1 != null)
                {
                    txtcodigo.Text = articulo1.Codigo;
                    txtnombre.Text = articulo1.Nombre;
                    txtdescripcion.Text = articulo1.Descripcion;
                    txtprecio.Text=articulo1.precio.ToString();
                    txtimagen.Text = articulo1.Imagen;
                    cargarimagen(articulo1.Imagen);
                    cbxcategoria.Text=articulo1.Categoria.ToString();
                    cbxmarca.Text=articulo1.Marca.ToString();
                }
            }
            catch (Exception)
            {

                throw;
            }

           
        }
        private void cargarimagen(string imagen)
        {
            try
            {
              if (imagen == null || imagen == "")
              {
                    pbxsub.Load("https://img.freepik.com/vector-premium/icono-marco-fotos-foto-vacia-blanco-vector-sobre-fondo-transparente-aislado-eps-10_399089-1290.jpg");
              }
              else
              {
                  pbxsub.Load(imagen);
              }
            }
                
            catch (System.Net.WebException)
            {
                pbxsub.Load("https://img.freepik.com/vector-premium/icono-marco-fotos-foto-vacia-blanco-vector-sobre-fondo-transparente-aislado-eps-10_399089-1290.jpg");
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error al cargar la imagen: " + ex.Message);
                pbxsub.Load("https://img.freepik.com/vector-premium/icono-marco-fotos-foto-vacia-blanco-vector-sobre-fondo-transparente-aislado-eps-10_399089-1290.jpg");
            }
        }

        private void txtimagen_Leave(object sender, EventArgs e)
        {
            Articulo articulo = new Articulo();
            articulo.Imagen=txtimagen.Text;
            cargarimagen(articulo.Imagen);
        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnimagenlocal_Click(object sender, EventArgs e)
        {
            archivo = new OpenFileDialog();
            archivo.Filter = "jpg|*.jpg";
            if (archivo.ShowDialog() == DialogResult.OK)
            {
               txtimagen.Text = archivo.FileName;
               cargarimagen(archivo.FileName);
            } 
            
        }

        private void txtprecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
