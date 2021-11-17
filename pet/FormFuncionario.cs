using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace pet
{
    
    public partial class FormFuncionario : Form
    {
        //private Cripto b;
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
                //string senha = b.Base64Encode(txtSenha.Text);
                SqlCommand cmd = new SqlCommand("InserirFuncionario", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nome", SqlDbType.NChar).Value = txtNome.Text;
                cmd.Parameters.AddWithValue("@login", SqlDbType.NChar).Value = txtLogin.Text;
                cmd.Parameters.AddWithValue("@senha", SqlDbType.NChar).Value = txtSenha.Text;
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

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("AtualizarFuncionario", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NChar).Value = Convert.ToInt32(txtId.Text);
                cmd.Parameters.AddWithValue("@nome", SqlDbType.NChar).Value = txtNome.Text;
                cmd.Parameters.AddWithValue("@login", SqlDbType.NChar).Value = txtLogin.Text;
                cmd.Parameters.AddWithValue("@senha", SqlDbType.NChar).Value = txtSenha.Text;
                cmd.Parameters.AddWithValue("@celular", SqlDbType.NChar).Value = txtCelular.Text.Trim();
                cmd.Parameters.AddWithValue("@data_admissao", SqlDbType.Date).Value = Convert.ToDateTime(dtDataAdmissao.Value);
                cmd.Parameters.AddWithValue("@genero", SqlDbType.NChar).Value = (cbxGenero.Text.Trim());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Funcionário atualizado com sucesso!", "Atualizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnLocalizar_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("LocalizarFuncionario", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = Convert.ToInt32(txtId.Text.Trim());
                cmd.ExecuteNonQuery();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtNome.Text = dr["nome"].ToString();
                    txtCelular.Text = dr["celular"].ToString();
                    txtLogin.Text = dr["login"].ToString();
                    txtSenha.Text = dr["senha"].ToString();
                    cbxGenero.Text = dr["genero"].ToString();
                    

                    dr.Close();
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Nenhum funcionário localizado com este ID?", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                dr.Close();
                con.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ApagarFuncionario", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = Convert.ToInt32(txtId.Text.Trim());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Funcionário apagado com sucesso!", "Apagar", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
