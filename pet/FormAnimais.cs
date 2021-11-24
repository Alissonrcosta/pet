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
    public partial class FormAnimais : Form
    {
        SqlConnection con = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=C:\\Programas\\pet\\pet\\DbPetshop1.mdf;Integrated Security = True");
        public FormAnimais()
        {
            InitializeComponent();
        }

        public void CarregaDgv()
        {
            String sql = "SELECT * FROM Animais";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable teste = new DataTable();
            da.Fill(teste);
            dgvAnimal.DataSource = teste;
            con.Close();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                //string senha = b.Base64Encode(txtSenha.Text);
                SqlCommand cmd = new SqlCommand("InserirAnimal", con);
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("@id_proprietario", SqlDbType.NChar).Value = txtidCliente.Text;
                cmd.Parameters.AddWithValue("@nome", SqlDbType.NChar).Value = txtNome.Text;
                cmd.Parameters.AddWithValue("@raca", SqlDbType.NChar).Value = txtRaca.Text;
                cmd.Parameters.AddWithValue("@data_nascimento", SqlDbType.Date).Value = Convert.ToDateTime(dtDataNascimento.Value);
                cmd.Parameters.AddWithValue("@especie", SqlDbType.NChar).Value = txtEspecie.Text;
                cmd.Parameters.AddWithValue("@pelagem", SqlDbType.NChar).Value = (txtPelagem.Text.Trim());
                cmd.Parameters.AddWithValue("@peso", SqlDbType.Decimal).Value = Convert.ToDecimal(txtPeso.Text);
                cmd.Parameters.AddWithValue("@porte", SqlDbType.NChar).Value = (cbxPorte.Text.Trim());
                cmd.Parameters.AddWithValue("@sexo", SqlDbType.NChar).Value = (cbxSexo.Text.Trim());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Animal inserido com sucesso!", "Inserir", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                CarregaDgv();
                txtId.Text = "";
                txtidCliente.Text = "";
                txtNome.Text = "";
                txtRaca.Text = "";
                txtEspecie.Text = "";
                txtPelagem.Text = "";
                txtPeso.Text = "";
                cbxPorte.Text = "";
                cbxSexo.Text = "";

            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void FormAnimais_Load(object sender, EventArgs e)
        {
            CarregaDgv();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("AtualizarAnimal", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NChar).Value = Convert.ToInt32(txtId.Text);
                cmd.Parameters.AddWithValue("@id_proprietario", SqlDbType.NChar).Value = txtidCliente.Text;
                cmd.Parameters.AddWithValue("@nome", SqlDbType.NChar).Value = txtNome.Text;
                cmd.Parameters.AddWithValue("@raca", SqlDbType.NChar).Value = txtRaca.Text;
                cmd.Parameters.AddWithValue("@data_nascimento", SqlDbType.Date).Value = Convert.ToDateTime(dtDataNascimento.Value);
                cmd.Parameters.AddWithValue("@especie", SqlDbType.NChar).Value = txtEspecie.Text;
                cmd.Parameters.AddWithValue("@pelagem", SqlDbType.NChar).Value = (txtPelagem.Text.Trim());
                cmd.Parameters.AddWithValue("@peso", SqlDbType.Decimal).Value = Convert.ToDecimal(txtPeso.Text);
                cmd.Parameters.AddWithValue("@porte", SqlDbType.NChar).Value = (cbxPorte.Text.Trim());
                cmd.Parameters.AddWithValue("@sexo", SqlDbType.NChar).Value = (cbxSexo.Text.Trim());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Animal atualizado com sucesso!", "Atualizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                CarregaDgv();
                txtId.Text = "";
                txtidCliente.Text = "";
                txtNome.Text = "";
                txtRaca.Text = "";
                txtEspecie.Text = "";
                txtPelagem.Text = "";
                txtPeso.Text = "";
                cbxPorte.Text = "";
                cbxSexo.Text = "";
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
                SqlCommand cmd = new SqlCommand("LocalizarAnimal", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = Convert.ToInt32(txtId.Text.Trim());
                cmd.ExecuteNonQuery();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    txtidCliente.Text = dr["id_proprietario"].ToString();
                    txtNome.Text = dr["nome"].ToString();
                    txtRaca.Text = dr["raca"].ToString();
                    dtDataNascimento.Text = dr["data_nascimento"].ToString();
                    txtEspecie.Text = dr["especie"].ToString();
                    txtPelagem.Text = dr["pelagem"].ToString();
                    txtPeso.Text = dr["peso"].ToString();
                    cbxPorte.Text = dr["porte"].ToString();
                    cbxSexo.Text = dr["sexo"].ToString();
                    


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
                SqlCommand cmd = new SqlCommand("ApagarAnimal", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = Convert.ToInt32(txtId.Text.Trim());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Animal apagado com sucesso!", "Apagar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                CarregaDgv();
                txtId.Text = "";
                txtidCliente.Text = "";
                txtNome.Text = "";
                txtRaca.Text = "";
                txtEspecie.Text = "";
                txtPelagem.Text = "";
                txtPeso.Text = "";
                cbxPorte.Text = "";
                cbxSexo.Text = "";
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }
    }
}
