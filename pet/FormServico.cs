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
    public partial class FormServico : Form
    {
        SqlConnection con = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=C:\\Programas\\pet\\pet\\DbPetshop1.mdf;Integrated Security = True");
        public FormServico()
        {
            InitializeComponent();
        }

        public void CarregaDgv()
        {
            String sql = "SELECT * FROM Servicos";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable teste = new DataTable();
            da.Fill(teste);
            dgvServico.DataSource = teste;
            con.Close();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void CarregaCbxCliente()
        {
            string cli = "SELECT Id, nome FROM Cliente";
            SqlCommand cmd = new SqlCommand(cli, con);
            con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cli, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "cliente");
            cbxCliente.ValueMember = "Id";
            cbxCliente.DisplayMember = "nome";
            cbxCliente.DataSource = ds.Tables["cliente"];
            con.Close();
        }

        public void CarregaCbxAnimal()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            string ani = "SELECT Id, nome FROM Animais";
            SqlCommand cmd = new SqlCommand(ani, con);
            con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(ani, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "animais");
            cbxAnimal.ValueMember = "Id";
            cbxAnimal.DisplayMember = "nome";
            cbxAnimal.DataSource = ds.Tables["animais"];
            con.Close();
        }

        private void FormServico_Load(object sender, EventArgs e)
        {
            CarregaCbxCliente();
            CarregaCbxAnimal();
            CarregaDgv();
        }

        private void btnNovoServico_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("InserirServico", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@servico", SqlDbType.NChar).Value = cbxServico.Text;
                cmd.Parameters.AddWithValue("@preco", SqlDbType.Decimal).Value = Convert.ToDecimal(txtValor.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Serviço inserido com sucesso!", "Inserir", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                CarregaDgv();
                txtIdServico.Text = "";
                txtValor.Text = "";
                cbxAnimal.Text = "";
                cbxCliente.Text = "";
                cbxServico.Text = "";
                cbxSituacao.Text = "";
                

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
                SqlCommand cmd = new SqlCommand("LocalizarServico", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = Convert.ToInt32(txtIdServico.Text.Trim());
                cmd.ExecuteNonQuery();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    cbxServico.Text = dr["servico"].ToString();
                    txtValor.Text = dr["preco"].ToString();
                    
                    


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

        private void btnExcluirServico_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ApagarServico", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = Convert.ToInt32(txtIdServico.Text.Trim());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Serviço apagado com sucesso!", "Apagar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                CarregaDgv();
                txtIdServico.Text = "";
                txtValor.Text = "";
                cbxAnimal.Text = "";
                cbxCliente.Text = "";
                cbxServico.Text = "";
                cbxSituacao.Text = "";
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnEditarServico_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("AtualizarServico", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NChar).Value = Convert.ToInt32(txtIdServico.Text);
                cmd.Parameters.AddWithValue("@servico", SqlDbType.NChar).Value = cbxServico.Text;
                cmd.Parameters.AddWithValue("@preco", SqlDbType.Decimal).Value = Convert.ToDecimal(txtValor.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Serviço atualizado com sucesso!", "Atualizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                CarregaDgv();
                txtIdServico.Text = "";
                txtValor.Text = "";
                cbxAnimal.Text = "";
                cbxCliente.Text = "";
                cbxServico.Text = "";
                cbxSituacao.Text = "";
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }
    }
}
