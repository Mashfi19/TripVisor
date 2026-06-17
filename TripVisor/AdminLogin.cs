using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TripVisor
{
    public partial class AdminLogin : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=ACIONAR;Initial Catalog=TripVisor;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");

        public AdminLogin()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login log = new Login();
            log.Show();
        }

        private void chkShow_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = txtPassword.PasswordChar.Equals('*') ? '\0' : '*';
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();

                con.Open();

                // JOIN query to get user name and role (Type)
                string query = @"SELECT U.UserID, U.Id, U.Name, T.Title
                                 FROM UserInfo U
                                 INNER JOIN UserType T ON U.Id = T.Id
                                 WHERE U.Username COLLATE SQL_Latin1_General_CP1_CS_AS = @username 
                                 AND U.Password COLLATE SQL_Latin1_General_CP1_CS_AS = @password";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    int userId = Convert.ToInt32(reader["UserId"]);

                    int Id = Convert.ToInt32(reader["Id"]);
                    string name = reader["Name"].ToString();
                    string role = reader["Title"].ToString();

                    if (Id ==1 || Id==2 || Id == 3)
                    {
                        MessageBox.Show($"Welcome {name}!\n{role}",
                                        "Official Login Successful",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);

              
                        AdminPanel ap = new AdminPanel(Id, name, role, userId);
                        ap.Show();
                    }
                    else
                    {
                        MessageBox.Show("You are not an official person!",
                                        "Access Denied",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password!",
                                    "Login Failed",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                reader.Close();
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
