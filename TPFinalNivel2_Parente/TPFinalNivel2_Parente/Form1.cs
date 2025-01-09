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

namespace TPFinalNivel2_Parente
{
    public partial class frmIndex : Form
    {
        private AccesoDatos accDatos;
        public frmIndex()
        {
            InitializeComponent();
            accDatos = new AccesoDatos();
        }

        private void frmIndex_Load(object sender, EventArgs e)
        {
            MessageBox.Show(accDatos.probando());
        }
    }
}
