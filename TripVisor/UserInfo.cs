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
    public partial class UserInfo : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=ACIONAR;Initial Catalog=TripVisor;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
        
        private string currentView = "";
        private int id;
        
        public UserInfo(int id)
        {
            InitializeComponent();
            this.id = id;
        }


        private void UserInfo_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'tripVisorDataSet1.UserInfo' table. You can move, or remove it, as needed.
            this.userInfoTableAdapter.Fill(this.tripVisorDataSet1.UserInfo);
            // TODO: This line of code loads data into the 'tripVisorDataSet1.UserInfo' table. You can move, or remove it, as needed.
            this.userInfoTableAdapter.Fill(this.tripVisorDataSet1.UserInfo);
            if (id == 1)
            {
                btnOwner.PerformClick();
            }
            else if (id == 2)
            {
                btnOwner.Enabled = false;
                btnManager.PerformClick();
            }

            else if (id == 3)
            { 
                btnOwner.Enabled = false;
                btnManager.Enabled = false;
                btnEmployee.PerformClick();
            }

        }

        private void LoadUserData(int titleid)
        {
            try
            {
                con.Open();
                string query = "SELECT * FROM UserInfo WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", titleid);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvUserInfo.DataSource = dt;
                dgvUserInfo.ClearSelection();
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

        private void btnOwner_Click(object sender, EventArgs e)
        {
            LoadUserData(1);
            btnAdd.Visible = true;
            btnEdit.Visible = true;
            btnAdd.Enabled = false;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            currentView = "Owner";

        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            if (id == 1 || id == 2 || id == 3)
            {
                btnDelete.Enabled = true;
            }
            LoadUserData(4);
            btnAdd.Visible = false;
            btnEdit.Visible = false;
            currentView = "Customer";
        }

        private void btnManager_Click(object sender, EventArgs e)
        {
            if (id == 1)
            {
                btnAdd.Enabled = true;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
            }
            else
            {
                btnAdd.Enabled = false;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
            LoadUserData(2);
            btnAdd.Visible = true;
            btnEdit.Visible = true;
            currentView = "Manager";

        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            if (id == 1 || id == 2)
            {
                btnAdd.Enabled = true;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
            }
            else
            {
                btnAdd.Enabled = false;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
            LoadUserData(3);
            btnAdd.Visible = true;
            btnEdit.Visible = true;
            currentView = "Employee";


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

                if (currentView == "Customer")
                {
                    query = "SELECT * FROM UserInfo WHERE Name LIKE @name AND Id NOT IN (1,2,3)";
                }

                else if (currentView == "Employee")
                {
                    query = "SELECT * FROM UserInfo WHERE Name LIKE @name AND Id NOT IN (1,2,4)";
                }

                else if (currentView == "Manager")
                {
                    query = "SELECT * FROM UserInfo WHERE Name LIKE @name AND Id NOT IN (1,3,4)";
                }

                else if (currentView == "Owner")
                {
                    query = "SELECT * FROM UserInfo WHERE Name LIKE @name AND Id NOT IN (2,3,4)";
                }

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@name", "%" + searchName + "%");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    dgvUserInfo.DataSource = dt;
                }
                else
                {
                    dgvUserInfo.DataSource = null;
                    MessageBox.Show("No user found with this name!", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddNew addForm = new AddNew(currentView);
            addForm.ShowDialog();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            int id = 0;
            if (currentView == "Owner") id = 1;
            else if (currentView == "Manager") id = 2;
            else if (currentView == "Employee") id = 3;
            else if (currentView == "Customer") id = 4;

            LoadUserData(id);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvUserInfo.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a full row to delete.");
                    return;
                }

                DataGridViewRow selectedRow = dgvUserInfo.SelectedRows[0];
                int userId = Convert.ToInt32(selectedRow.Cells[0].Value);

                DialogResult confirm = MessageBox.Show(
                    "Are you sure you want to delete this user?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (confirm != DialogResult.Yes)
                    return;

                {
                    con.Open();
                    string query = "DELETE FROM UserInfo WHERE UserId = @UserId";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.ExecuteNonQuery();
                }

                dgvUserInfo.Rows.Remove(selectedRow);
                MessageBox.Show("User deleted successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting user: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvUserInfo.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int userId = Convert.ToInt32(dgvUserInfo.SelectedRows[0].Cells[0].Value);
            EditUser editForm = new EditUser(userId);
            editForm.ShowDialog();

        }
    }
}
