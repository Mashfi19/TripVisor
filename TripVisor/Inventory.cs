using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace TripVisor
{
    public partial class Inventory : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=ACIONAR;Initial Catalog=TripVisor;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");

        private string currentView = "";
        private string query = "";
        public Inventory()
        {
            InitializeComponent();
        }

        private void Inventory_Load(object sender, EventArgs e)
        {
            btnPackage.PerformClick();
            if (currentView == "Package")
            {

            }

            else if (currentView == "Destination")
            {

            }

            else if (currentView == "Hotel")
            {

            }

        }

        private void LoadInventory()
        {
            try
            {
                con.Open();
                if (currentView == "Package")
                {
                    query = "SELECT P.PackId, P.Type, P.PackName AS [Pack Name], D.DestName AS [Destination], H.HotelName AS [Hotel Name], H.Class AS [Hotel Class], " +
                        "P.Price, P.Status, P.TimePeriod AS [Time Period] FROM Package P " +
                        "INNER JOIN Destination D ON P.DestId = D.DestId " +
                        "INNER JOIN Hotel H ON P.HotelId = H.HotelId";
                }
                else if (currentView == "Destination")
                {
                    query = "SELECT DestId, DestName AS [Destination], Places FROM Destination";
                }

                else if (currentView == "Hotel")
                {
                    query = "SELECT HotelId, HotelName AS [Hotel Name], Class, Room, Price FROM Hotel";
                }


                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvInventory.DataSource = dt;
                dgvInventory.ClearSelection();
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

        private void btnPackage_Click(object sender, EventArgs e)
        {
            currentView = "Package";
            LoadInventory();

        }

        private void btnDestination_Click(object sender, EventArgs e)
        {
            currentView = "Destination";
            LoadInventory();

        }

        private void btnHotel_Click(object sender, EventArgs e)
        {
            currentView = "Hotel";
            LoadInventory();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (currentView == "Package")
            {
                AddPack add = new AddPack("Add");
                add.Show();
            }

            else if (currentView == "Destination")
            {
                AddDest add = new AddDest("Add");
                add.Show();
            }

            else if (currentView == "Hotel")
            {
                AddHotel add = new AddHotel("Add");
                add.Show();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadInventory();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvInventory.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a full row to delete.");
                    return;
                }

                DataGridViewRow selectedRow = dgvInventory.SelectedRows[0];
                int id = Convert.ToInt32(selectedRow.Cells[0].Value);

                DialogResult confirm = MessageBox.Show(
                    "Are you sure you want to delete this item?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (confirm != DialogResult.Yes)
                    return;

                {
                    con.Open();
                    if (currentView == "Package")
                    {
                        query = "DELETE FROM Package WHERE PackId = @Id";
                    }
                    else if (currentView == "Destination")
                    {
                        query = "DELETE FROM Destination WHERE DestId = @Id";
                    }

                    else if (currentView == "Hotel")
                    {
                        query = "DELETE FROM Hotel WHERE HotelId = @Id";
                    }
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }

                dgvInventory.Rows.Remove(selectedRow);
                MessageBox.Show("Item deleted successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting item: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvInventory.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a item to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (currentView == "Package")
            {
                try
                {
                    // Get the selected package ID from the first cell (PackId)
                    int packId = Convert.ToInt32(dgvInventory.SelectedRows[0].Cells["PackId"].Value);

                    // Open AddPack form in Update mode and pass the selected ID
                    AddPack editPack = new AddPack("Update", packId);
                    editPack.ShowDialog();

                    // Refresh data after editing
                    LoadInventory();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while opening edit form: " + ex.Message);
                }
            }

            else if (currentView == "Destination")
            {
                try
                {
                    // Get the selected package ID from the first cell (PackId)
                    int destId = Convert.ToInt32(dgvInventory.SelectedRows[0].Cells["DestId"].Value);

                    // Open AddPack form in Update mode and pass the selected ID
                    AddDest edit = new AddDest("Update", destId);
                    edit.ShowDialog();

                    // Refresh data after editing
                    LoadInventory();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while opening edit form: " + ex.Message);
                }
            }

            else if (currentView == "Hotel")
            {
                try
                {
                    // Get the selected package ID from the first cell (PackId)
                    int hotelId = Convert.ToInt32(dgvInventory.SelectedRows[0].Cells["HotelId"].Value);

                    // Open AddPack form in Update mode and pass the selected ID
                    AddHotel editHotel = new AddHotel("Update", hotelId);
                    editHotel.ShowDialog();

                    // Refresh data after editing
                    LoadInventory();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while opening edit form: " + ex.Message);
                }
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchName = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchName))
            {
                MessageBox.Show("Please enter a name to search!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                con.Open();
                string query = "";

                if (currentView == "Package")
                {
                    query = "SELECT * FROM Package WHERE PackName LIKE @name";
                }

                else if (currentView == "Destination")
                {
                    query = "SELECT * FROM Destination WHERE DestName LIKE @name";
                }

                else if (currentView == "Hotel")
                {
                    query = "SELECT * FROM Hotel WHERE HotelName LIKE @name";
                }

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@name", "%" + searchName + "%");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    dgvInventory.DataSource = dt;
                }
                else
                {
                    dgvInventory.DataSource = null;
                    MessageBox.Show("No item found with this name!", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while searching: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
