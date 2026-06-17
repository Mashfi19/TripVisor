using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TripVisor
{
    public partial class HotelDetails : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=ACIONAR;Initial Catalog=TripVisor;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");

        private decimal basePrice = 0;

        private int userId;
        private int BookingId = 0;

        // কনস্ট্রাকটরে আসা প্যাকেজ এবং হোটেলের নাম ধরে রাখার জন্য
        private string HotelName; // HotelName সেভ করার জন্য

        // Booking-এর জন্য প্রয়োজনীয় আইডি (ডাটাবেস থেকে পরে লোড করা হবে)
        private int HotelId = 0;
        public HotelDetails(string roomCount, string hotelName, string Class, string Price, int userId, int fetchedHotelId)
        {
            InitializeComponent();
            HotelName = hotelName;
            this.userId = userId;
            if (decimal.TryParse(Price, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal parsedPrice))
            {
                basePrice = parsedPrice;
                lblPrice.Text = basePrice.ToString();
            }
            else
            {
                lblPrice.Text = "Price Error";
            }

            txtDays.TextChanged += TxtInputChanged;
            txtMember.TextChanged += TxtInputChanged;


            CalculateTotal();

            lblHotelName.Text = hotelName;
            lblRoom.Text = roomCount;
            lblClass.Text = Class;
            lblPrice.Text = Price + " Tk";
        }

        private void TxtInputChanged(object sender, EventArgs e)
        {
            CalculateTotal();
        }
        private void CalculateTotal()
        {
            // সদস্য সংখ্যা এবং দিনের সংখ্যা 1 ধরা হলো যদি ইনপুট না থাকে
            int memberCount = 1;
            int dayCount = 1;

            if (!int.TryParse(txtMember.Text, out memberCount) || memberCount <= 0)
            {
                memberCount = 1;
            }
            if (!int.TryParse(txtDays.Text, out dayCount) || dayCount <= 0)
            {
                dayCount = 1;
            }

            // ফাইনাল গণনা: সদস্য * দিন * বেস প্রাইস
            decimal finalPrice = memberCount * dayCount * basePrice;

            if (finalPrice > 0)
            {
                lblPrice.Text = finalPrice.ToString(); // মোট মূল্য Currency ফরম্যাটে দেখানো হলো
            }
            else
            {
                // যদি basePrice 0 হয় বা ইনপুট 0 হয়, তবে শুধু বেস প্রাইস দেখাবে
                lblPrice.Text = basePrice.ToString();
            }
        }
        private void HotelDetails_Load(object sender, EventArgs e)
        {

        }

        private void btnBookNow_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMember.Text, out int personCount) || personCount <= 0)
            {
                MessageBox.Show("Please enter a valid number of persons (Guests).", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txtDays.Text, out int dayCount) || dayCount <= 0)
            {
                MessageBox.Show("Please enter a valid number of days.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ফাইনাল মোট মূল্য গণনা
            decimal totalPrice = personCount * dayCount * basePrice;

            if (totalPrice <= 0)
            {
                MessageBox.Show("Booking price cannot be zero or negative. Please check hotel price.", "Calculation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DateTime bookingDate = dtpBookingDate.Value.Date;
            if (bookingDate.Date < DateTime.Now.Date)
            {
                MessageBox.Show("Booking date cannot be in the past. Please select a future date.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. PackId এবং HotelId খোঁজা (যদি আগে লোড করা না হয়ে থাকে)
            if (HotelId == 0)
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    // PackId এবং HotelId খোঁজার কোয়েরি
                    string idQuery = @"SELECT HotelId FROM Hotel WHERE HotelName = @HotelName";

                    using (SqlCommand idCommand = new SqlCommand(idQuery, con))
                    {
                        idCommand.Parameters.AddWithValue("@HotelName", HotelName);

                        using (SqlDataReader reader = idCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                HotelId = reader.GetInt32(reader.GetOrdinal("HotelId"));
                            }
                            else
                            {
                                MessageBox.Show("Error: Could not find Package or Hotel ID in database.", "Lookup Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                }
                catch (SqlException sqlex)
                {
                    MessageBox.Show("Database Error during ID lookup: " + sqlex.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }


            // 3. Booking ডেটাবেসে ডেটা ঢোকানো
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // Booking টেবিলে ডেটা ঢোকানোর জন্য INSERT কোয়েরি (আপডেটেড কলাম নাম সহ)
                // Columns: UserId, PackId, HotelId, Date, Person, TotalPrice
                string query = "INSERT INTO Booking (UserId, HotelId, Date, Person, StartDate, Day, TotalPrice, PaymentStatus) " +
                               "VALUES (@UserId, @HotelId, @Date, @Person, @StartDate, @Day, @TotalPrice, @PaymentStatus) ";
                ;

                using (SqlCommand command = new SqlCommand(query, con))
                {
                    // প্যারামিটারাইজড কোয়েরি: SQL Injection প্রতিরোধ করে
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@HotelId", HotelId);
                    command.Parameters.AddWithValue("@StartDate", bookingDate); // নতুন প্যারামিটার                                                                                  // Booking Date

                    command.Parameters.AddWithValue("@Date", DateTime.Now); // Booking Date
                    command.Parameters.AddWithValue("@Person", personCount); // Person (Quantity)
                    command.Parameters.AddWithValue("@Day", dayCount); // Person (Quantity)
                    command.Parameters.AddWithValue("@TotalPrice", totalPrice);
                    command.Parameters.AddWithValue("@PaymentStatus", "Pending");
                    command.Parameters.AddWithValue("@BookingId", BookingId);


                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Booking successful! Total amount: " + totalPrice.ToString(), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Payment payment = new Payment(BookingId, totalPrice, userId); 
                         payment.Show();
                        
                  
                    }
                    else
                    {
                        MessageBox.Show("Booking failed. No rows were inserted.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred during booking: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Hotel hotel = new Hotel(userId); // 7টি প্যারামিটার পাঠানো হলো

            hotel.TopLevel = false;
            hotel.FormBorderStyle = FormBorderStyle.None;
            hotel.Dock = DockStyle.Fill;

            Main mainForm = Application.OpenForms.OfType<Main>().FirstOrDefault();
            mainForm.ContainerPanel.Controls.Clear();
            mainForm.ContainerPanel.Controls.Add(hotel);
            hotel.Show();
        }
    }
}
