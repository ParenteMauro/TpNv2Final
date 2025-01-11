using dominio;
using negocio;
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

namespace TPFinalNivel2_Parente
{
    public partial class FrmAltaArticulo : Form
    {
        private Articulo articulo;
        private List<Marca> marcasLista;
        private MarcaNegocio listaMarcas = new MarcaNegocio();
        private CategoriaNegocio listaCategorias = new CategoriaNegocio();
        private ArticulosNegocio articuloNegocio = new ArticulosNegocio();
        public FrmAltaArticulo()
        {
            InitializeComponent();
            

            listaMarcas = new MarcaNegocio();
            marcasLista = listaMarcas.listar();
            
           
        }
        public  FrmAltaArticulo (Articulo articulo)
        {
            InitializeComponent();


            listaMarcas = new MarcaNegocio();
            marcasLista = listaMarcas.listar();
           
            this.articulo = articulo;
            cargarDatos();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnAgregar.Text != "Modificar")
                {
                    articulo.Codigo = txtCodigo.Text;
                    articulo.Nombre = txtNombre.Text;
                    articulo.Descripcion = txtDescripcion.Text;
                    articulo.UrlImagen = txtImagen.Text;
                    articulo.Marca = (Marca)cbxMarca.SelectedItem;
                    articulo.Categoria = (Categoria)cbxCategoria.SelectedItem;
                    articulo.Precio = Convert.ToDecimal(txtPrecio.Text);
                    articuloNegocio.agregar(articulo);
                    MessageBox.Show("Agregado Exitosamente");
                }
                else
                {
                    articulo.Codigo = txtCodigo.Text;
                    articulo.Nombre = txtNombre.Text;
                    articulo.Descripcion = txtDescripcion.Text;
                    articulo.UrlImagen = txtImagen.Text;
                    articulo.Marca = (Marca)cbxMarca.SelectedItem;
                    articulo.Categoria = (Categoria)cbxCategoria.SelectedItem;
                    articulo.Precio = Convert.ToDecimal(txtPrecio.Text);
                    articuloNegocio.modificar(articulo);
                    MessageBox.Show("Modificado Exitosamente");
                }
            }
            catch (FormatException ex) 
            {
                MessageBox.Show("Comprueba si tienes campos vacíos");
            }
            finally
            {
                this.Close();
            }
        }

        private void cargarDatos()
        {
            try
            {
                txtCodigo.Text = articulo.Codigo;
                txtNombre.Text = articulo.Nombre;
                txtDescripcion.Text = articulo.Descripcion;
                txtImagen.Text = articulo.UrlImagen;
                cbxMarca.DataSource = listaMarcas.listar();
                cbxMarca.ValueMember = "Id";
                cbxMarca.DisplayMember = "Descripcion";
                cbxMarca.SelectedIndex = articulo.Marca.Id - 1;
                cbxMarca.DisplayMember = articulo.Marca.Descripcion;
                cbxCategoria.DataSource = listaCategorias.listar();
                cbxCategoria.ValueMember = "Id";
                cbxCategoria.DisplayMember = "Descripcion";
                cbxCategoria.SelectedIndex = articulo.Categoria.Id -1;
                txtPrecio.Text = articulo.Precio.ToString();
                btnAgregar.Text = "Modificar";
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }
        private void FrmAltaArticulo_Load(object sender, EventArgs e)
        {
            if (cbxMarca is null)
            {
                cbxMarca.DataSource = listaMarcas.listar();
                cbxMarca.DisplayMember = "Descripcion";
                cbxMarca.ValueMember = "Id";
                cbxMarca.SelectedIndex = -1;
            }
            if (cbxCategoria is null)
            {
                cbxCategoria.DataSource = listaCategorias.listar();
                cbxCategoria.DisplayMember = "Descripcion";
                cbxCategoria.ValueMember = "Id";
                cbxCategoria.SelectedIndex = -1;
            }
        }

        private void txtImagen_TextChanged(object sender, EventArgs e)
        {
            try
            {
                pbxImagen.Load(txtImagen.Text);
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("Imagen no encontrada");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
