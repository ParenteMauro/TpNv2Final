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
using System.Diagnostics;

namespace TPFinalNivel2_Parente
{
    public partial class frmIndex : Form
    {
        private AccesoDatos accDatos;
        private ArticulosNegocio articulosNegocio;
        private List<Articulo> listaArticulos;
        private MarcaNegocio marcas = new MarcaNegocio();
        private List<Marca> listaMarcas;
        private List<Categoria> listaCategoria;
        private CategoriaNegocio categorias = new CategoriaNegocio();
        public frmIndex()
        {
            InitializeComponent();
            accDatos = new AccesoDatos();
            articulosNegocio = new ArticulosNegocio();
            cargarFiltro(cbxPropiedad, "Nombre", "Descripcion", "Codigo", "Marca","Categoria");
            configuracionesIniciales();
            filtroTimer = new Timer();
            filtroTimer.Interval = 300;
            filtroTimer.Tick += FiltroTimer_Tick;

        }

        private void frmIndex_Load(object sender, EventArgs e)
        {
            cargarArticulos();

            dgvArticulos.Columns["UrlImagen"].Visible = false;
            dgvArticulos.Columns["Id"].Visible = false;

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
                cargarImagen("https://media.istockphoto.com/id/1354776457/vector/default-image-icon-vector-missing-picture-page-for-website-design-or-mobile-app-no-photo.jpg?s=612x612&w=0&k=20&c=w3OW0wX3LyiFRuDHo9A32Q0IUMtD4yjXEvQlqyYk9O4=");
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
           
            {
                try
                {
                    if(!(dgvArticulos.CurrentRow is null)) { 
                        Articulo articuloBorrar = new Articulo();
                        articuloBorrar = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

                        DialogResult respuesta = MessageBox.Show("¿Estás seguro que quieres eliminar " + articuloBorrar.Nombre + " de la Base de Datos?", "¿Deseas Borrar el Registro?", MessageBoxButtons.YesNo);
                        if (respuesta == DialogResult.Yes)
                        {
                            articulosNegocio.eliminar(articuloBorrar.Id);
                            cargarArticulos();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontró Articulo para eliminar, compruebe selección");
                    }
                    
                }
               
                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }

        private Timer filtroTimer = new Timer();

        private void FiltroTimer_Tick(object sender, EventArgs e)
        {
            
            filtroTimer.Stop();
            
            
            List<Articulo> listaFiltrada = new List<Articulo>();

            dgvArticulos.SuspendLayout();
            if (txtFiltroRap.Text.Length >= 3)
            {
                string busqueda = txtFiltroRap.Text;

                listaFiltrada = listaArticulos.FindAll(articulo =>
                    articulo.Nombre.ToUpper().Contains(busqueda.ToUpper()) ||
                    articulo.Descripcion.ToUpper().Contains(busqueda.ToUpper()));

                Task.Run(() =>
                {
                    Invoke(new Action(() => dgvArticulos.DataSource = listaFiltrada));
                });


            }
            else
            {

                dgvArticulos.DataSource = listaArticulos;
            }
            dgvArticulos.ResumeLayout();
        }

        private void txtFiltroRap_TextChanged(object sender, EventArgs e)
        {
            filtroTimer.Stop();

            filtroTimer.Start();
        }

        private void cargarFiltro(ComboBox cbx, string opc1, string opc2, string opc3)
        {
            cbx.Items.Add(opc1);
            cbx.Items.Add(opc2);
            cbx.Items.Add(opc3);
        }
        private void cargarFiltro(ComboBox cbx, string opc1, string opc2, string opc3, string opc4, string opc5)
        {
            cbx.Items.Add(opc1);
            cbx.Items.Add(opc2);
            cbx.Items.Add(opc3);
            cbx.Items.Add(opc4);
            cbx.Items.Add(opc5);

        }
       
        private void cargarFiltroDescrpcion()
        {
            string eleccion = (string)cbxPropiedad.SelectedItem;
            
            switch (eleccion)
            {
                case "Nombre":
                    {
                        cbxCriterio.Items.Clear();
                        cargarFiltro(cbxCriterio, "Empieza con", "Termina con", "Contiene");
                        txtFiltroAvanzado.BackColor = default;
                        validarBusquedaAvanzada();
                        btnFiltroAvanzado.Enabled = false;
                        cbxCriterio.Enabled = true;
                        txtFiltroAvanzado.Text = "";
                        break;
                    }
                case "Descripcion":
                    {
                        cbxCriterio.Items.Clear();
                        cargarFiltro(cbxCriterio, "Empieza con", "Termina con", "Contiene");
                        txtFiltroAvanzado.BackColor = default;
                        cbxCriterio.Enabled = true;
                        btnFiltroAvanzado.Enabled = false;
                        txtFiltroAvanzado.Text = "";
                        validarBusquedaAvanzada();
                        break;
                    }
                case "Codigo":
                    {
                        cbxCriterio.Items.Clear();
                        cbxCriterio.Items.Add("Escribe el Código:");
                        cbxCriterio.SelectedIndex = 0;
                        txtFiltroAvanzado.Text = "";
                        validarBusquedaAvanzada();
                        cbxCriterio.Enabled = false;
                        
                        break;

                    }
                case "Marca":
                    {
                        
                        cbxCriterio.Items.Clear();
                        listaMarcas = marcas.listar();
                        listaMarcas.ForEach(marca => cbxCriterio.Items.Add(marca.ToString()));
                        cbxCriterio.Enabled = true;
                        txtFiltroAvanzado.Text = "";
                        txtFiltroAvanzado.Enabled = false;
                        btnFiltroAvanzado .Enabled = false;
                        break;
                    }
                case "Categoria":
                    {
                       
                        cbxCriterio.Items.Clear();
                        listaCategoria = categorias.listar();
                        listaCategoria.ForEach(categoria => cbxCriterio.Items.Add(categoria.ToString()));
                        cbxCriterio.Enabled = true;
                        txtFiltroAvanzado.Enabled = false;
                        btnFiltroAvanzado.Enabled = false;
                        txtFiltroAvanzado.Text = "";
                        break;
                    }
            }
            

        }
        private void validarBusquedaAvanzada()
        {
            if(cbxCriterio.SelectedItem != null  && cbxPropiedad.SelectedItem != null)
            {
                txtFiltroAvanzado.Enabled = true;
            }
            else
            {
                txtFiltroAvanzado.Enabled = false;
            }
        }
        private void cbxPropiedad_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarFiltroDescrpcion();
        }

        private void cbxCriterio_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnFiltroAvanzado.Enabled = true;
            if (cbxPropiedad.Text == "Marca" || cbxPropiedad.Text == "Categoria")
            {
                txtFiltroAvanzado.Text = "Buscar Articulos que sean: ''" + cbxCriterio.Text + "''";
                txtFiltroAvanzado.Enabled = false;
            }
            else
            { 
            validarBusquedaAvanzada();
            }
        }

        private void btnFiltroAvanzado_Click(object sender, EventArgs e)
        {
            
            try
            {
                string propiedad = cbxPropiedad.Text;
                string criterio = cbxCriterio.Text;
                string filtroAvanzado = txtFiltroAvanzado.Text;
                
                dgvArticulos.DataSource = articulosNegocio.listar(propiedad,criterio,filtroAvanzado);

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
        private void configuracionesIniciales()
        {
            txtFiltroAvanzado.Enabled = false;
            btnFiltroAvanzado.Enabled = false;
            dgvArticulos.MultiSelect = false;
            dgvArticulos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvArticulos.ReadOnly = true;   
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvArticulos.CurrentRow != null)
                {
                    Articulo articuloModificar = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    FrmAltaArticulo frmAltaArticulo = new FrmAltaArticulo(articuloModificar);
                    frmAltaArticulo.ShowDialog();
                    cargarArticulos();
                }
                else
                {
                    MessageBox.Show("No se encontró objeto para Modificar");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

