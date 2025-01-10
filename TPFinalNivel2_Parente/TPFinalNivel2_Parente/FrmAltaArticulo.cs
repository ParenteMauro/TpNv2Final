using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                articulo = new Articulo();
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
            catch (Exception ex) 
            {
                throw ex;
            }
            
        }

        private void FrmAltaArticulo_Load(object sender, EventArgs e)
        {
            cbxMarca.DataSource = listaMarcas.listar();
            cbxMarca.DisplayMember = "Descripcion";
            cbxMarca.ValueMember = "Id";
            cbxCategoria.DataSource = listaCategorias.listar();
            cbxCategoria.DisplayMember = "Descripcion";
            cbxCategoria.ValueMember = "Id";
            cbxMarca.SelectedIndex = -1;
            cbxCategoria.SelectedIndex = -1;
        }

        private void txtImagen_TextChanged(object sender, EventArgs e)
        {
            pbxImagen.Load(txtImagen.Text);
        }
    }
}
