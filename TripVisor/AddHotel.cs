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
    public partial class AddHotel : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=ACIONAR;Initial Catalog=TripVisor;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");

        string mode;
        int hotelId = 0;


        public AddHotel(string mode, int hotelId = 0)
        {
            InitializeComponent();
            this.mode = mode;
            this.hotelId = hotelId;

            btnAdd.Text = mode; // change button text

            if (mode == "Update")
            {
                LoadHotelData(hotelId); // load package info into textboxes
            }
            lblAddEdit.Text = mode + " Hotel";
        }

        private void AddHotelData()
        {
            if (cbClass.SelectedItem == null || cbRoom.SelectedItem == null)
            {
                MessageBox.Show("Please select a package name first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            byte[] photoBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                picHotel.Image.Save(ms, picHotel.Image.RawFormat);
                photoBytes = ms.ToArray();
            }

            string selectedClass = cbClass.SelectedItem.ToString();
            string selectedRoom = cbRoom.SelectedItem.ToString();

            try
            {
                con.Open();
                string query = "INSERT INTO Hotel (HotelName, Class, Room, Price, HotelImg) " +
                               "VALUES (@hotelName, @class, @room, @price, @hotelImg)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@hotelName", txtName.Text);
                cmd.Parameters.AddWithValue("@price", txtPrice.Text);
                cmd.Parameters.AddWithValue("@class", selectedClass);
                cmd.Parameters.AddWithValue("@room", selectedRoom);
                cmd.Parameters.Add("@hotelImg", SqlDbType.VarBinary).Value = photoBytes;


                cmd.ExecuteNonQuery();

                MessageBox.Show("Hotel added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding hotel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void LoadHotelData(int id)
        {
            {
                string query = "SELECT * FROM Hotel WHERE HotelId = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtName.Text = reader["HotelName"].ToString();
                        cbClass.Text = reader["Class"].ToString();
                        txtPrice.Text = reader["Price"].ToString();
                        cbRoom.Text = reader["Room"].ToString();

                        if (reader["HotelImg"] != DBNull.Value)
                        {
                            byte[] photoBytes = (byte[])reader["HotelImg"];
                            using (MemoryStream ms = new MemoryStream(photoBytes))
                            {
                                picHotel.Image = Image.FromStream(ms);
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

        private void UpdateHotelData()
        {
            try
            {
                con.Open();

                string query = "UPDATE Hotel SET HotelName=@hotelName, Price=@price, Class=@class, Room=@room, HotelImg=@hotelImg " +
                                "WHERE HotelId=@hotelId";

                byte[] photoBytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    picHotel.Image.Save(ms, picHotel.Image.RawFormat);
                    photoBytes = ms.ToArray();
                }

                string selectedClass = cbClass.SelectedItem.ToString();
                string selectedRoom = cbRoom.SelectedItem.ToString();

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@hotelName", txtName.Text);
                cmd.Parameters.AddWithValue("@price", txtPrice.Text);
                cmd.Parameters.AddWithValue("@class", selectedClass);
                cmd.Parameters.AddWithValue("@room", selectedRoom);
                cmd.Parameters.Add("@hotelImg", SqlDbType.VarBinary).Value = photoBytes;
                cmd.Parameters.AddWithValue("@hotelId", hotelId);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Hotel updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating hotel: " + ex.Message);
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (mode == "Add")
            {
                AddHotelData();
            }

            else if (mode == "Update")
            {
                UpdateHotelData();
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
                    picHotel.Image = new Bitmap(ofd.FileName);

                    picHotel.Tag = ofd.FileName;
                }
            }
        }
    }
}

