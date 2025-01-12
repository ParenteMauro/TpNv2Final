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
           
            cbxMarca.DataSource = listaMarcas.listar();
            cbxMarca.ValueMember = "Id";
            cbxMarca.DisplayMember = "Descripcion";
            

            cbxCategoria.DataSource = listaCategorias.listar();
            cbxCategoria.ValueMember = "Id";
            cbxCategoria.DisplayMember = "Descripcion";
            

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
                    if (validarCarga() == true)
                    {
                        articulo = new Articulo();
                        articulo.Codigo = (string)txtCodigo.Text;
                        articulo.Nombre = (string)txtNombre.Text;
                        articulo.Descripcion =(string)txtDescripcion.Text;

                        if (txtImagen.Text != "") 
                        {
                            articulo.UrlImagen =(string)txtImagen.Text; 
                        }

                        articulo.Marca = (Marca)cbxMarca.SelectedItem;
                        articulo.Categoria = (Categoria)cbxCategoria.SelectedItem;
                        articulo.Precio = Convert.ToDecimal(txtPrecio.Text);

                        articuloNegocio.agregar(articulo);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Tiene Campos Vacíos");
                    }
                }
                else
                {
                    if (validarCarga() == true)
                    {
                        
                        articulo.Codigo =(string)txtCodigo.Text;
                        articulo.Nombre =(string)txtNombre.Text;
                        articulo.Descripcion = (string)txtDescripcion.Text;
                        articulo.UrlImagen =(string)txtImagen.Text;
                        articulo.Marca = (Marca)cbxMarca.SelectedItem;
                        articulo.Categoria = (Categoria)cbxCategoria.SelectedItem;
                        articulo.Precio = Convert.ToDecimal(txtPrecio.Text);
                        articuloNegocio.modificar(articulo);
                        MessageBox.Show("Modificado Exitosamente");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Tiene Campos Vacíos");
                    }
                    
                }
            }
            catch(NullReferenceException ex)
            {
                MessageBox.Show("Campos vacíos detectados");
            }
            catch (FormatException ex) 
            {
                MessageBox.Show("Comprueba si tienes campos vacíos");
            }
            
        }
        private bool validarCarga()
        {
            if (txtCodigo.Text != "" & txtNombre.Text
                != "" && txtDescripcion.Text != "" && cbxMarca.SelectedItem != null && cbxCategoria.SelectedItem != null && txtPrecio.Text != "")
            {
                return true; 
            }
            else
            {
                return false; 
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
               cargarImagen(txtImagen.Text);
            }
            catch(Exception ex)
            {
                cargarImagen("https://media.istockphoto.com/id/1354776457/vector/default-image-icon-vector-missing-picture-page-for-website-design-or-mobile-app-no-photo.jpg?s=612x612&w=0&k=20&c=w3OW0wX3LyiFRuDHo9A32Q0IUMtD4yjXEvQlqyYk9O4=");
            }
        }
        private void cargarImagen(string url) 
        {
            pbxImagen.Load(url);
        }
        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar ) && e.KeyChar != '.' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnImagen_Click(object sender, EventArgs e)
        {
            string directorioApp = Application.StartupPath;

            // Esto lo que va a hacer es crear una carpeta en la dirección de la App para poder subirla localmente
            string carpetaImagenes = Path.Combine(directorioApp, "Imagenes");
            if (!Directory.Exists(carpetaImagenes))
            {
                Directory.CreateDirectory(carpetaImagenes);
            }

            OpenFileDialog archivo = new OpenFileDialog();
            archivo.Filter = "*.jpg;*.png|*.jpg;*.png";

            


            
            if (archivo.ShowDialog() == DialogResult.OK)
            {
                
                string rutaDestino = Path.Combine(carpetaImagenes, Path.GetFileName(archivo.FileName));

                try
                {
                    File.Copy(archivo.FileName, rutaDestino);
                }
                catch(IOException ex) 
                {
                    MessageBox.Show("La imagen que quieres Agregar ya existe en tu carpeta local");
                }
                txtImagen.Text = rutaDestino;

                cargarImagen(rutaDestino);
        
                
            }
        }
    }
}
