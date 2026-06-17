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
    public partial class AddNew : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=ACIONAR;Initial Catalog=TripVisor;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");

        private string currentView;
        public AddNew(string View)
        {
            InitializeComponent();
            currentView = View;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text == "" || txtMail.Text == "" || txtPhone.Text == "" ||
                    txtUsername.Text == "" || txtPassword.Text == "" || txtIdNum.Text == "")
                {
                    MessageBox.Show("Please Fill All Fields!");
                    return;
                }

                int Id = 0;

                if (currentView == "Manager")
                    Id = 2;
                else if (currentView == "Employee")
                    Id = 3;

                byte[] photoBytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    picUserImg.Image.Save(ms, picUserImg.Image.RawFormat);
                    photoBytes = ms.ToArray();
                }

                string idType = "";
                if (rbtnNid.Checked) idType = "NID";
                else if (rbtnBC.Checked) idType = "Birth Certificate";
                else if (rbtnPassport.Checked) idType = "Passport";

                con.Open();

                string checkUser = "SELECT COUNT(*) FROM UserInfo WHERE Username=@username";
                SqlCommand cmdCheckUser = new SqlCommand(checkUser, con);
                cmdCheckUser.Parameters.AddWithValue("@username", txtUsername.Text);
                int userExists = (int)cmdCheckUser.ExecuteScalar();
                if (userExists > 0)
                {
                    MessageBox.Show("Username already exists!");
                    return;
                }

                string checkIdNum = "SELECT COUNT(*) FROM UserInfo WHERE IdNum=@idnum";
                SqlCommand cmdCheckId = new SqlCommand(checkIdNum, con);
                cmdCheckId.Parameters.AddWithValue("@idnum", txtIdNum.Text);
                int idExists = (int)cmdCheckId.ExecuteScalar();
                if (idExists > 0)
                {
                    MessageBox.Show("ID Number already exists! Please use a different one.");
                    return;
                }

                string query = "INSERT INTO UserInfo (Name, Email, Phone, IdType, IdNum, Username, Password, Date, Id, UserImg) " +
                               "VALUES (@name, @mail, @phone, @idtype, @idnum, @username, @password, @date, @id, @userImg)";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@mail", txtMail.Text);
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@idtype", idType);
                cmd.Parameters.AddWithValue("@idnum", txtIdNum.Text);
                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                cmd.Parameters.AddWithValue("@date", DateTime.Now);
                cmd.Parameters.AddWithValue("@id", Id);
                cmd.Parameters.Add("@userImg", SqlDbType.VarBinary).Value = photoBytes;


                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("Add successful!");
                }
                else
                {
                    MessageBox.Show("Add failed!");
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddNew_Load(object sender, EventArgs e)
        {
            lblAddNew.Text = "Add New " + currentView;
        }
    }
}
