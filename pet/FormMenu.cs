using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pet
{
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
        }

        private void pbFuncionario_Click(object sender, EventArgs e)
        {
            var func = new FormFuncionario();
            func.MdiParent = this.MdiParent;
            func.Show();
        }

        private void pbCliente_Click(object sender, EventArgs e)
        {
            var cli = new FormCliente();
            cli.MdiParent = this.MdiParent;
            cli.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var ani = new FormAnimais();
            ani.MdiParent = this.MdiParent;
            ani.Show();
        }
    }
}
