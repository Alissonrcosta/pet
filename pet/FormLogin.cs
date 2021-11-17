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
    public partial class FormLogin : Form
    {
        private Cripto b;
        SqlConnection con = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=C:\\Programas\\pet\\pet\\DbPetshop1.mdf;Integrated Security = True");

        public FormLogin()
        {
            InitializeComponent();
            FormSplashScreen formSplash = new FormSplashScreen();
            formSplash.Show();
            Thread.Sleep(3000);
            formSplash.Close();
            b = new Cripto();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //string senha = b.Base64Encode(txtSenha.Text);
            string usu = "SELECT login, senha FROM Funcionarios WHERE login = @login AND senha = @senha";
            SqlCommand cmd = new SqlCommand(usu, con);
            cmd.Parameters.AddWithValue("@login", SqlDbType.NChar).Value = txtLogin.Text.Trim();
            cmd.Parameters.AddWithValue("@senha", SqlDbType.NChar).Value = txtSenha.Text.Trim();
            con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader usuario = cmd.ExecuteReader();
            if (usuario.HasRows)
            {
                this.Hide();
                FormPrincipal pri = new FormPrincipal();
                pri.Show();
                con.Close();
            }
            else
            {
                MessageBox.Show("Login ou senha inválidos! Tente novamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLogin.Text = "";
                txtSenha.Text = "";
                txtLogin.Focus();
                con.Close();
            }
        }
    }
}