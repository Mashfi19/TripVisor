using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace TripVisor
{
    public partial class Register : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=ACIONAR;Initial Catalog=TripVisor;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");

        public Register()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login log = new Login();
            log.Show();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text == "" || txtMail.Text == "" || txtPhone.Text == "" ||
                    txtUsername.Text == "" || txtPassword.Text == "" || txtIdNum.Text == "")
                {
                    MessageBox.Show("Please Fill All Fields!");
                    return;
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

                string query = "INSERT INTO UserInfo (Name, Email, Phone, IdType, IdNum, Username, Password, Date, Id) " +
                               "VALUES (@name, @mail, @phone, @idtype, @idnum, @username, @password, @date, @id)";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@mail", txtMail.Text);
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@idtype", idType);
                cmd.Parameters.AddWithValue("@idnum", txtIdNum.Text);
                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                cmd.Parameters.AddWithValue("@date", DateTime.Now);
                cmd.Parameters.AddWithValue("@id", 4);

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("Registration successful!");
                }
                else
                {
                    MessageBox.Show("Registration failed!");
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
