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

namespace pet
{
    public partial class FormFuncionario : Form
    {
        SqlConnection con = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=C:\\Programas\\pet\\pet\\DbPetshop1.mdf;Integrated Security = True");
        public FormFuncionario()
        {
            InitializeComponent();
        }

        public void CarregaDgv()
        {
            String sql = "SELECT * FROM Funcionarios";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable teste = new DataTable();
            da.Fill(teste);
            dgvFuncionario.DataSource = teste;
            con.Close();
        }

        private void FormFuncionario_Load(object sender, EventArgs e)
        {
            CarregaDgv();
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("InserirFuncionario", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nome", SqlDbType.NChar).Value = txtNome.Text;
                cmd.Parameters.AddWithValue("@login", SqlDbType.NChar).Value = txtLogin.Text;
                cmd.Parameters.AddWithValue("@senha", SqlDbType.NChar).Value = txtSenha.Text.Trim();
                cmd.Parameters.AddWithValue("@celular", SqlDbType.NChar).Value = txtCelular.Text.Trim();
                cmd.Parameters.AddWithValue("@data_admissao", SqlDbType.Date).Value = Convert.ToDateTime(dtDataAdmissao.Value);
                cmd.Parameters.AddWithValue("@genero", SqlDbType.NChar).Value = (cbxGenero.Text.Trim());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Funcionário inserido com sucesso!", "Inserir", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                CarregaDgv();
                txtId.Text = "";
                txtNome.Text = "";
                txtCelular.Text = "";
                txtLogin.Text = "";
                txtSenha.Text = "";
                cbxGenero.Text = "";
                
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
