using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TripVisor
{
    public partial class EditUser : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=ACIONAR;Initial Catalog=TripVisor;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
        private int userId;
        public EditUser(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        private void EditUser_Load(object sender, EventArgs e)
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

                    // Load image if exists
                    if (reader["UserImg"] != DBNull.Value)
                    {
                        byte[] photoBytes = (byte[])reader["UserImg"];
                        using (MemoryStream ms = new MemoryStream(photoBytes))
                        {
                            picUserImg.Image = Image.FromStream(ms);
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                string query = "UPDATE UserInfo SET Name=@Name, Email=@Mail, Phone=@Phone, Username=@Username, " +
                                "UserImg=@UserImg WHERE UserId=@UserId";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@Mail", txtMail.Text.Trim());
                cmd.Parameters.AddWithValue("@Phone", txtPhone.Text.Trim());
                cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                cmd.Parameters.AddWithValue("@UserId", userId);


                // Handle photo update
                if (picUserImg.Image != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        picUserImg.Image.Save(ms, picUserImg.Image.RawFormat);
                        cmd.Parameters.Add("@UserImg", SqlDbType.VarBinary).Value = ms.ToArray();
                    }
                }
                else
                {
                    cmd.Parameters.Add("@UserImg", SqlDbType.VarBinary).Value = DBNull.Value;
                }

                cmd.ExecuteNonQuery();

                MessageBox.Show("User updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating user: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Select a Photo";
                ofd.Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg;*.jpeg;*.png";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    // Display image in PictureBox
                    picUserImg.Image = new Bitmap(ofd.FileName);

                    // Save file path temporarily (useful when saving to DB)
                    picUserImg.Tag = ofd.FileName;
                }
            }
        }
    }
}
