using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TripVisor
{
    public partial class AddPack : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=ACIONAR;Initial Catalog=TripVisor;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
        string mode;
        int packId = 0;

        public AddPack(string mode, int packId = 0)
        {
            InitializeComponent();
            this.mode = mode;
            this.packId = packId;

            btnAdd.Text = mode; // change button text

            if (mode == "Update")
            {
                LoadPackData(packId); // load package info into textboxes
            }
            lblAddEdit.Text = mode + " Package";
        }

        private void AddPackData()
        {
            try
            {
                string type = cbType.SelectedItem?.ToString();
                string packName = txtName.Text.Trim();
                int destId = Convert.ToInt32(cbDest.SelectedValue);
                int hotelId = Convert.ToInt32(cbHotel.SelectedValue);
                decimal price = Convert.ToDecimal(txtPrice.Text.Trim());
                string status = cbStatus.SelectedItem?.ToString();
                string timePeriod = txtTime.Text.Trim();

                if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(packName) || string.IsNullOrEmpty(status) || string.IsNullOrEmpty(timePeriod))
                {
                    MessageBox.Show("Please fill all fields before adding a package!");
                    return;
                }

                byte[] photoBytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    picPack.Image.Save(ms, picPack.Image.RawFormat);
                    photoBytes = ms.ToArray();
                }

                con.Open();
                string query = "INSERT INTO Package (Type, PackName, DestId, HotelId, Price, Status, TimePeriod, PackImg) " +
                               "VALUES (@type, @packName, @destId, @hotelId, @price, @status, @timePeriod, @packImg)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@type", type);
                cmd.Parameters.AddWithValue("@packName", packName);
                cmd.Parameters.AddWithValue("@destId", destId);
                cmd.Parameters.AddWithValue("@hotelId", hotelId);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@timePeriod", timePeriod);
                cmd.Parameters.Add("@packImg", SqlDbType.VarBinary).Value = photoBytes;

                cmd.ExecuteNonQuery();

                MessageBox.Show("Package added successfully!");
                //LoadInventory(); // refresh the grid
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding package: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void LoadPackData(int id)
        {
            {
                string query = "SELECT * FROM Package WHERE PackId = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtName.Text = reader["PackName"].ToString();
                        cbType.Text = reader["Type"].ToString();
                        txtPrice.Text = reader["Price"].ToString();
                        cbStatus.Text = reader["Status"].ToString();
                        txtTime.Text = reader["TimePeriod"].ToString();
                        cbDest.Text = reader["DestId"].ToString();

                        if (reader["PackImg"] != DBNull.Value)
                        {
                            byte[] photoBytes = (byte[])reader["PackImg"];
                            using (MemoryStream ms = new MemoryStream(photoBytes))
                            {
                                picPack.Image = Image.FromStream(ms);
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

        private void ClearAllFields(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Clear();
                else if (c is ComboBox)
                    ((ComboBox)c).SelectedIndex = -1;
                else
                    ClearAllFields(c); // for nested containers
            }
        }

        private void AddPack_Load(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                // Load Destination
                SqlDataAdapter daDest = new SqlDataAdapter("SELECT DestId, DestName FROM Destination", con);
                DataTable dtDest = new DataTable();
                daDest.Fill(dtDest);
                cbDest.DataSource = dtDest;
                cbDest.DisplayMember = "DestName";
                cbDest.ValueMember = "DestId";

                // Load Hotel
                SqlDataAdapter daHotel = new SqlDataAdapter("SELECT HotelId, HotelName FROM Hotel", con);
                DataTable dtHotel = new DataTable();
                daHotel.Fill(dtHotel);
                cbHotel.DataSource = dtHotel;
                cbHotel.DisplayMember = "HotelName";
                cbHotel.ValueMember = "HotelId";
                ClearAllFields(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
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
                AddPackData();
            }

            else if (mode == "Update")
            {
                // UPDATE query
                //UpdatePack();
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
                    picPack.Image = new Bitmap(ofd.FileName);

                    picPack.Tag = ofd.FileName;
                }
            }
        }
    }
}
