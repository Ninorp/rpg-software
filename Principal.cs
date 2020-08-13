using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RpG_Software
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormImporta f = new FormImporta();
            f.ShowDialog();
        }

        private void gruposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormGrupos f = new FormGrupos();
            f.ShowDialog();         
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void facilitadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormFacilitador f = new FormFacilitador();
            f.ShowDialog();
        }
    }
}
