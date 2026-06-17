using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TripVisor
{
    public partial class Profile : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=ACIONAR;Initial Catalog=TripVisor;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");

        private int userId;
        public Profile(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        private void Profile_Load(object sender, EventArgs e)
        {
            LoadUserData();
        }

        private void LoadUserData()
        {
            try
            {
                con.Open();
                string query = "SELECT * FROM UserInfo WHERE UserId = @UserId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserId", userId);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtName.Text = reader["Name"].ToString();
                    txtMail.Text = reader["Email"].ToString();
                    txtPhone.Text = reader["Phone"].ToString();
                    txtUsername.Text = reader["Username"].ToString();
                    txtPassword.Text = reader["Password"].ToString();

                    // Load image if exists
                    if (reader["UserImg"] != DBNull.Value)
                    {
                        byte[] photoBytes = (byte[])reader["UserImg"];
                        using (MemoryStream ms = new MemoryStream(photoBytes))
                        {
                            picUser.Image = Image.FromStream(ms);
                        }
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading user data: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
