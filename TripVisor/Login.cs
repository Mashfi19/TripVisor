using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TripVisor
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=ACIONAR;Initial Catalog=TripVisor;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");


        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register reg = new Register();
            reg.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AdminLogin l2 = new AdminLogin();
            l2.Show();
        }

        private void chkShow_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = txtPassword.PasswordChar.Equals('*') ? '\0' : '*' ;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUsername.Text;
                string password = txtPassword.Text;

                con.Open();
                string query = "SELECT UserId, Name FROM UserInfo WHERE Username COLLATE SQL_Latin1_General_CP1_CS_AS = @username AND " +
                               "Password COLLATE SQL_Latin1_General_CP1_CS_AS = @password";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    int userId = Convert.ToInt32(reader["UserId"]);

                    string name = reader["Name"].ToString();
                    MessageBox.Show("Welcome " + name + "!", "Login Successful!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    
                    Main main = new Main(name, userId);
                    main.Show();
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password!");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            finally
            {
                con.Close();
            }
        }
    
    }
}
