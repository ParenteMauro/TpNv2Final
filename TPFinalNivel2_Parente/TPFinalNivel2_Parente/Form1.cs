using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using negocio;
using dominio;

namespace TPFinalNivel2_Parente
{
    public partial class frmIndex : Form
    {
        private AccesoDatos accDatos;
        private ArticulosNegocio articulosNegocio;
        private List<Articulo> listaArticulos;
        public frmIndex()
        {
            InitializeComponent();
            accDatos = new AccesoDatos();
            articulosNegocio = new ArticulosNegocio();
        }

        private void frmIndex_Load(object sender, EventArgs e)
        {
            cargarArticulos();
            dgvArticulos.Columns["UrlImagen"].Visible = false;
            cargarImagen(listaArticulos[0].UrlImagen);

        }

        private void cargarImagen(string url)
        {
            pbxImagen.Load(url);
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                Articulo actual = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                cargarImagen(actual.UrlImagen);




            }
            catch (Exception ex)
            {
                cargarImagen("https://static.thenounproject.com/png/4595376-200.png");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FrmAltaArticulo frmAltaArticulo = new FrmAltaArticulo();
            frmAltaArticulo.ShowDialog();
            cargarArticulos();
        }

        private void cargarArticulos()
        {
            listaArticulos = articulosNegocio.listar();
            dgvArticulos.DataSource = listaArticulos;
        }
    }
}
