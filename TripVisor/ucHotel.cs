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
    public partial class ucHotel : UserControl
    {
        SqlConnection con = new SqlConnection(@"Data Source=ACIONAR;Initial Catalog=TripVisor;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
        private int userId;
        public ucHotel(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        public int HotelId { get; set; }
        public string HotelName
        {
            get { return lblName.Text; }
            set { lblName.Text = value; }
        }

        public string Class
        {
            get { return lblType.Text; }
            set { lblType.Text = value; }
        }

        public string Price
        {
            get { return lblPrice.Text; }
            set { lblPrice.Text = value; }
        }

        public Image HotelImg
        {
            get { return picHotel.Image; }
            set { picHotel.Image = value; }
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            string hotelName = this.HotelName; // ইউজার কন্ট্রোল থেকে নাম নেওয়া

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // HotelId এবং Room লোড করার ক্যোয়ারী। আমরা HotelName ব্যবহার করে সঠিক রো খুঁজে আনছি।
                string query = "SELECT HotelId, Room FROM Hotel WHERE HotelName = @HotelName";

                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@HotelName", hotelName);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // ডাটাবেস থেকে HotelId এবং Room সংখ্যা নেওয়া
                            int fetchedHotelId = reader.GetInt32(reader.GetOrdinal("HotelId"));
                            string roomCount = reader["Room"].ToString();

                            // প্যাকেজের বিস্তারিত তথ্য দেখানোর জন্য ফর্ম খোলা
                            // HotelDetails (hotelClass, hotelName, room, price, userId)
                            HotelDetails detailsForm = new HotelDetails(
                                this.Class,     // Class (lblType.Text থেকে)
                                hotelName,      // HotelName (lblName.Text থেকে)
                                roomCount,      // Room (ডাটাবেস থেকে)
                                this.Price,     // Price (lblPrice.Text থেকে)
                                fetchedHotelId, // HotelId (ডাটাবেস থেকে) - কনস্ট্রাকটরে যোগ করা হলো
                                userId     // UserId (কনস্ট্রাকটর থেকে)
                            );

                            detailsForm.TopLevel = false;
                            detailsForm.FormBorderStyle = FormBorderStyle.None;
                            detailsForm.Dock = DockStyle.Fill;

                            // মেইন ফর্মের কন্টেইনার প্যানেল অ্যাক্সেস করা:
                            Main mainForm = Application.OpenForms.OfType<Main>().FirstOrDefault();

                            if (mainForm != null && mainForm.ContainerPanel != null)
                            {
                                // মেইন ফর্মের কন্টেইনার প্যানেল অ্যাক্সেস করা হলো
                                mainForm.ContainerPanel.Controls.Clear();
                                mainForm.ContainerPanel.Controls.Add(detailsForm);
                                detailsForm.Show();
                            }
                            else
                            {
                                MessageBox.Show("Main container panel not found. নিশ্চিত করুন যে Main ফর্মটি ওপেন আছে এবং তাতে একটি public 'ContainerPanel' প্রপার্টি আছে।", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show($"ডাটাবেসে '{hotelName}' নামের কোনো হোটেল পাওয়া যায়নি।", "ডাটা মিসিং", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // যেকোনো ডাটাবেস বা ফাইল লোডিং ত্রুটি হলে এখানে বার্তা দেখানো হবে
                MessageBox.Show("হোটেল বিস্তারিত লোড করতে গিয়ে একটি ত্রুটি হয়েছে:\n" + ex.Message, "ডাটাবেস ত্রুটি", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        //public Panel pnlContainer
        //{
        //    get { return pnlContainer; }
        //}
    }
}
