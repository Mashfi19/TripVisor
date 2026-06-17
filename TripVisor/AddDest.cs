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
    public partial class AddDest : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=ACIONAR;Initial Catalog=TripVisor;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
        string mode;
        int destId = 0;
        public AddDest(string mode, int destId = 0)
        {
            InitializeComponent();
            this.mode = mode;
            this.destId = destId;

            btnAdd.Text = mode; // change button text

            if (mode == "Update")
            {
                LoadDestData(destId); // load package info into textboxes
            }
            lblAddEdit.Text = mode + " Destination";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddDestData()
        {
            byte[] photoBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                picDest.Image.Save(ms, picDest.Image.RawFormat);
                photoBytes = ms.ToArray();
            }

            try
            {
                con.Open();

                string query = "INSERT INTO Destination (DestName, Places, DestImg) " +
                               "VALUES (@destName, @places, @destImg)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@destName", txtName.Text);
                cmd.Parameters.AddWithValue("@places", txtPlace.Text);
                cmd.Parameters.Add("@destImg", SqlDbType.VarBinary).Value = photoBytes;


                cmd.ExecuteNonQuery();

                MessageBox.Show("Destination added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding destination: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void LoadDestData(int id)
        {
            {
                string query = "SELECT * FROM Destination WHERE DestId = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtName.Text = reader["DestName"].ToString();
                        txtPlace.Text = reader["Places"].ToString();

                        if (reader["DestImg"] != DBNull.Value)
                        {
                            byte[] photoBytes = (byte[])reader["DestImg"];
                            using (MemoryStream ms = new MemoryStream(photoBytes))
                            {
                                picDest.Image = Image.FromStream(ms);
                            }
                        }
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading package: " + ex.Message);
                }
                //finally
                //{
                //    con.Close();
                //}
            }
        }

        private void UpdateDestData()
        {
            try
            {
                con.Open();

                string query = "UPDATE Destination SET DestName=@destName, Places=@places, DestImg=@destImg " +
                                "WHERE DestId=@destId";

                byte[] photoBytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    picDest.Image.Save(ms, picDest.Image.RawFormat);
                    photoBytes = ms.ToArray();
                }

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@destName", txtName.Text);
                cmd.Parameters.AddWithValue("@places", txtPlace.Text);
                cmd.Parameters.Add("@destImg", SqlDbType.VarBinary).Value = photoBytes;
                cmd.Parameters.AddWithValue("@destId", destId);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Destination updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating destination: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (mode == "Add")
            {
                AddDestData();
            }

            else if (mode == "Update")
            {
                UpdateDestData();
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
                    picDest.Image = new Bitmap(ofd.FileName);

                    picDest.Tag = ofd.FileName;
                }
            }
        }
    }
}
