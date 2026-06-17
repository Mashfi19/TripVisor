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
    public partial class Tour : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=ACIONAR;Initial Catalog=TripVisor;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
        private int userId;

        public Tour(int userId)
        {
            InitializeComponent();
            this.userId = userId;

        }

        private void Tour_Load(object sender, EventArgs e)
        {
            try
            {

                 con.Open();
                string query = "SELECT PackName, PackImg, Price, Type FROM Package";

                // 2. SqlCommand-এর জন্য 'using' ব্লক ব্যবহার করা হয়েছে
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    // 3. SqlDataReader-এর জন্য 'using' ব্লক ব্যবহার করা হয়েছে
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // আগের প্যাকেজ প্যানেলগুলো মুছে ফেলা (যদি ফর্মটি আবার লোড হয়)
                        flpPack.Controls.Clear();

                        while (reader.Read())
                        {
                            ucPackage panel = new ucPackage(userId);

                            panel.PackName = reader["PackName"].ToString();
                            panel.Price = reader["Price"].ToString();
                            panel.Type = reader["Type"].ToString();


                            // 4. ইমেজ লোড করার সময় অতিরিক্ত সুরক্ষার জন্য try-catch ব্যবহার করুন
                            try
                            {
                                // মনে রাখবেন, PackImg-এ ডাটাবেসে ফাইলের পাথ বা URL থাকতে হবে
                                if (!reader.IsDBNull(reader.GetOrdinal("PackImg")))
                                {
                                    byte[] imageData = (byte[])reader["PackImg"];
                                    using (MemoryStream ms = new MemoryStream(imageData))
                                    {
                                        panel.PackImg = Image.FromStream(ms);
                                    }
                                }
                            }
                            catch (FileNotFoundException ex)
                            {
                                // যদি ইমেজ ফাইলটি খুঁজে না পাওয়া যায়, তাহলে একটি ডিফল্ট ইমেজ ব্যবহার করুন
                                // panel.PackImg = Properties.Resources.DefaultImage; 
                                Console.WriteLine("Image file not found: " + reader["PackImg"].ToString());
                            }
                            catch (Exception imgEx)
                            {
                                Console.WriteLine("Error loading image: " + imgEx.Message);
                            }


                            // প্যানেলটি ফ্লো লেআউট প্যানেলে যোগ করা
                            flpPack.Controls.Add(panel);
                        }
                        // reader 'using' ব্লকের শেষে স্বয়ংক্রিয়ভাবে Close() হয়ে যাবে।
                    }
                }
            }
            catch (SqlException sqlex)
            {
                // SQL-এর সাথে সম্পর্কিত কোনো ত্রুটি হলে তা দেখা যাবে
                MessageBox.Show("Error: " + sqlex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // অন্য কোনো সাধারণ ত্রুটি (যেমন: কানেকশন স্ট্রিং ভুল) হলে
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // 5. নিশ্চিত করা যে ফর্ম লোড শেষ হলে কানেকশন বন্ধ হবে, যদিও 'using' ব্লক এটি করে দেয়
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        private void flpPack_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
