using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Utilidades;
using DominioClases;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;

namespace Presentasion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Text = "Administrador de Articulos";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cargar();
            cargarcombox();

        }

        public void cargarcombox()
        {
            cbxCategorias.Items.Add("Sin definir");
            cbxCategorias.Items.Add("Celulares");
            cbxCategorias.Items.Add("Media");
            cbxCategorias.Items.Add("Televisores");
            cbxCategorias.Items.Add("Audio");

            cbxMarcas.Items.Add("Sin definir");
            cbxMarcas.Items.Add("Apple");
            cbxMarcas.Items.Add("Motorola");
            cbxMarcas.Items.Add("Huawei");
            cbxMarcas.Items.Add("Sony");
            cbxMarcas.Items.Add("Samsung");

            cbxprecio.Items.Add("Sin definir");
            cbxprecio.Items.Add("Mayor a");
            cbxprecio.Items.Add("Menor a");
            cbxprecio.Items.Add("Igual a");



        }
        List<Articulo> listaarticulos = new List<Articulo>();
        private void cargar()
        {
            try
            {
                FuncionesArticulos articulo = new FuncionesArticulos();
                listaarticulos = articulo.listar();
                dvgarticulos.DataSource = listaarticulos;
                dvgarticulos.Columns["Imagen"].Visible = false;
                dvgarticulos.Columns["Id"].Visible = false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void dvgarticulos_SelectionChanged(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dvgarticulos.CurrentRow.DataBoundItem;
            cargarimagen(seleccionado.Imagen);
            lblNombre.Text = seleccionado.Nombre;
           
        }
        private void cargarimagen(string imagen)
        {
            try
            {
                if (imagen == null || imagen == "")
                {
                    ptbimagenlista.Load("https://img.freepik.com/vector-premium/icono-marco-fotos-foto-vacia-blanco-vector-sobre-fondo-transparente-aislado-eps-10_399089-1290.jpg");
                }
                else
                {
                    ptbimagenlista.Load(imagen);
                }
            }

            catch (System.Net.WebException)
            {
                ptbimagenlista.Load("https://img.freepik.com/vector-premium/icono-marco-fotos-foto-vacia-blanco-vector-sobre-fondo-transparente-aislado-eps-10_399089-1290.jpg");
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error al cargar la imagen: " + ex.Message);
                ptbimagenlista.Load("https://img.freepik.com/vector-premium/icono-marco-fotos-foto-vacia-blanco-vector-sobre-fondo-transparente-aislado-eps-10_399089-1290.jpg");
            }
        }

        private void btnagregar_Click(object sender, EventArgs e)
        {
            frmaltaarticulo alta=new frmaltaarticulo(); 
            alta.ShowDialog();
            cargar();
        }

        private void btnmodificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado = new Articulo();
            seleccionado=(Articulo)dvgarticulos.CurrentRow.DataBoundItem;
            frmaltaarticulo alta=new frmaltaarticulo(seleccionado); 
            alta.ShowDialog();
            cargar();
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            eliminar();
           
        }

        private void btnbajafisica_Click(object sender, EventArgs e)
        {
            eliminar(true);
        }

        public void eliminar(bool tipoeliminacion=false)
        {
            Articulo seleccionado = new Articulo();
            seleccionado = (Articulo)dvgarticulos.CurrentRow.DataBoundItem;
            if (tipoeliminacion)
            {
            DialogResult resultado = MessageBox.Show("Estas seguro de eliminar el Articulo" + seleccionado.Nombre, "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (resultado == DialogResult.Yes)
            {
                FuncionesArticulos funcion = new FuncionesArticulos();
                funcion.EliminarFisica(seleccionado);
            }
            cargar();

            }
            else
            {
            DialogResult resultado = MessageBox.Show("Desea sacar de stock el Articulo " + seleccionado.Nombre, "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (resultado == DialogResult.Yes)
            {
                FuncionesArticulos funcion = new FuncionesArticulos();
                funcion.Eliminar(seleccionado);
            }
            cargar();
            }
            
            
        }

        private void btnvermas_Click(object sender, EventArgs e)
        {
            if (dvgarticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dvgarticulos.CurrentRow.DataBoundItem;
                FormDetalles alta = new FormDetalles(seleccionado);
                alta.ShowDialog();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un artículo antes de continuar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ptbimagenlista_Click(object sender, EventArgs e)
        {

        }

        private void txtbuscar_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listafiltros = new List<Articulo>();
            if (txtbuscar.Text != "") {

                listafiltros = listaarticulos.FindAll(x => x.Nombre.ToUpper().Contains(txtbuscar.Text.ToUpper()));

            }
            else
            {
                listafiltros = listaarticulos;

            }
            dvgarticulos.DataSource= listafiltros;
        }

        private void btnfiltrar_Click(object sender, EventArgs e)
        {

            FuncionesArticulos funcion = new FuncionesArticulos();
            string Categoria = cbxCategorias.SelectedItem != null ? cbxCategorias.SelectedItem.ToString() : "Sin definir";
            string Marca = cbxMarcas.SelectedItem != null ? cbxMarcas.SelectedItem.ToString() : "Sin definir";
            string Precio = cbxprecio.SelectedItem != null ? cbxprecio.SelectedItem.ToString() : "Sin definir";
            string valor = txtvalor.Text != null ? txtvalor.Text.ToString() : "";
            if((Precio!="Sin definir" && valor == "")|| (Precio == "Sin definir" && valor != ""))
            {
                MessageBox.Show("Porfavor definir condicion y valor para filtrar por precio ");

            }else if(Categoria!="Sin definir" || Marca!="Sin definir"||Precio!="Sin definir")
            {
                dvgarticulos.DataSource=funcion.filtrar(Categoria,Marca,Precio,valor);

            }
            else
            {
                cargar();
            }
            

        }

    }
}
