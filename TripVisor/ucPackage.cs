using Microsoft.VisualBasic.ApplicationServices;
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
    public partial class ucPackage : UserControl
    {
        SqlConnection con = new SqlConnection(@"Data Source=ACIONAR;Initial Catalog=TripVisor;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
        private int userId;

        public ucPackage(int userId)
        {
            InitializeComponent();
            this.userId = userId;

        }

        public int PackId { get; set; }
        public string PackName 
        {
            get { return lblName.Text; }
            set { lblName.Text = value;  } 
        }

        public string Type
        {
            get { return lblType.Text; }
            set { lblType.Text = value; }
        }

        public string Price
        {
            get { return lblPrice.Text; }
            set { lblPrice.Text = value; }
        }

        public Image PackImg
        {
            get { return picPack.Image; }
            set { picPack.Image = value; }
        }


        private void btnDetails_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                        string query = "SELECT P.PackId, P.Type, P.PackName, D.Places, D.DestImg, H.HotelName, H.Class, " +
                               "P.Price, P.Status, P.TimePeriod FROM Package P " +
                               "INNER JOIN Destination D ON P.DestId = D.DestId " +
                               "INNER JOIN Hotel H ON P.HotelId = H.HotelId " +
                               "WHERE P.PackName = @PackName"; // শুধুমাত্র বর্তমান প্যাকেজের তথ্য লোড করার জন্য WHERE ক্লজ যোগ করা হলো।

                        // (নোট: আপনার কোয়েরি শুধু একটি রো আনার জন্য WHERE ক্লজ ছাড়া সঠিক নয়। 
                        // আমি এখানে ধরে নিয়েছি আপনার ইউজার কন্ট্রোলে প্যাকেজের নাম (lblPackName.Text) আছে এবং এটি ব্যবহার করা হয়েছে।)

                        SqlCommand command = new SqlCommand(query, con);
                        command.Parameters.AddWithValue("@PackName", PackName);

                SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            string packName = reader["PackName"].ToString();
                            //string imagePath = reader["DestImg"].ToString();
                            string place = reader["Places"].ToString(); // --- ২. নতুন কলাম যোগ করা ---
                            string price = reader["Price"].ToString();
                            string hotelName = reader["HotelName"].ToString();
                            string time = reader["TimePeriod"].ToString();
                            string status = reader["Status"].ToString();


                            // প্যাকেজের বিস্তারিত তথ্য দেখানোর জন্য ফর্ম খোলা
                            // --- ৩. প্যারামিটার সংখ্যা মিলানো ---
                            PackDetail detailsForm = new PackDetail(packName, place, hotelName, time, status, price, userId); // 7টি প্যারামিটার পাঠানো হলো

                            detailsForm.TopLevel = false;
                            detailsForm.FormBorderStyle = FormBorderStyle.None;
                            detailsForm.Dock = DockStyle.Fill;

                    // মেইন ফর্মের কন্টেইনার প্যানেল অ্যাক্সেস করা:
                    // ধরে নেওয়া হয়েছে UserControl এর Parent হলো MainForm এবং তাতে pnlContainer নামের Panel আছে
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
                        // ডায়াগনস্টিক বার্তা: কোন ফর্মের ধরন পাওয়া গেছে, তা দেখাবে
                        MessageBox.Show("Main container panel not found. নিশ্চিত করুন যে Main ফর্মটি ওপেন আছে এবং তাতে একটি public 'ContainerPanel' প্রপার্টি আছে।", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                        reader.Close();
            }

            catch (Exception ex)
            {
                // যেকোনো ডাটাবেস বা ফাইল লোডিং ত্রুটি হলে এখানে বার্তা দেখানো হবে
                MessageBox.Show("প্যাকেজ লোড করতে গিয়ে একটি ত্রুটি হয়েছে:\n" + ex.Message, "ডাটাবেস ত্রুটি", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // যদি রিডার খোলা থাকে, তবে তা নিরাপদে বন্ধ করা হলো 
           
                    con.Close();
                
            }
        }
    }
}
