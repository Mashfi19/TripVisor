using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TripVisor
{
    public partial class Payment : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=ACIONAR;Initial Catalog=TripVisor;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
        private int BookingId;
        private decimal totalAmount; // বুকিং এর মোট মূল্য (যা Payment ফর্মে পাস করা হয়েছে)
        private int currentUserId;

        public Payment(int BookingId, decimal totalPrice, int userId)
        {
            
            InitializeComponent();
            this.BookingId = BookingId;
            this.totalAmount = totalPrice;
            currentUserId = userId;

            this.Load += Payment_Load;

            // মোট Due Amount ইনিশিয়ালি দেখানোর জন্য
            if (lblAmount != null)
            {
                // lblTotalAmount হলো সেই লেবেল যা সর্বমোট বুকিং মূল্য দেখায় (যেমন: TotalPrice)
                lblAmount.Text = totalAmount.ToString("N2") + " Tk";
            }

            if (lblDue != null)
            {
                // ইনিশিয়ালি Due Amount হবে Total Amount এর সমান
                lblDue.Text = totalAmount.ToString("N2") + " Tk";
            }

            // ⭐️ নতুন: txtPaidAmount এর TextChanged ইভেন্ট হ্যান্ডলার যোগ করা হলো 
            if (txtAmount != null)
            {
                txtAmount.TextChanged += txtPaidAmount_TextChanged;
            }
        }

        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            CalculateDueAmount();
        }

        private void CalculateDueAmount()
        {
            if (txtAmount == null || lblDue == null) return;

            decimal paidAmount = 0;
            // ইনপুট থেকে paidAmount নেওয়ার চেষ্টা
            if (decimal.TryParse(txtAmount.Text, NumberStyles.Currency | NumberStyles.Number, CultureInfo.CurrentCulture, out paidAmount))
            {
                decimal due = totalAmount - paidAmount;

                // Due Amount লেবেলে দেখানো
                lblDue.Text = due.ToString("N2") + " Tk";

                // Pay বাটন এনাবল/ডিসেবল করা: যদি Due Amount 0 বা 0 এর কম হয়, ধরে নিচ্ছি পেমেন্ট সম্পন্ন।
                // যদি Due Amount 0 হয় বা Paid Amount Total Amount এর চেয়ে বেশি হয় (যদি পুরো পেমেন্ট রিকোয়ারমেন্ট থাকে)
                btnPay.Enabled = (due <= 0);

                // যদি আপনি শুধুমাত্র সম্পূর্ণ পেমেন্ট allow করতে চান
                if (due == 0)
                {
                    btnPay.Enabled = true;
                }
                else
                {
                    // যদি মোট টাকার চেয়ে কম পেমেন্ট করে, তবে Pay বাটন disable থাকবে (পার্শিয়াল পেমেন্ট allowed নয়)
                    btnPay.Enabled = false;
                }

            }
            else
            {
                // ইনপুট ভ্যালিড না হলে বা খালি হলে, Due Amount পুরো Total Amount থাকবে।
                lblDue.Text = totalAmount.ToString("N2") + " Tk";
                btnPay.Enabled = false; // ইনপুট ভুল হলে Pay বাটন বন্ধ থাকবে
            }
        }



        private void btnBack_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to cancel the payment? Your booking status will remain 'Pending'.", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            this.Close();
        }

        private void Payment_Load(object sender, EventArgs e)
        {
            //if (cbMethod != null)
            //{
            //    cbMethod.Items.Add("Bkash");
            //    cbMethod.Items.Add("Nagad");
            //    cbMethod.Items.Add("Rocket");
            //    cbMethod.Items.Add("Card (Master/Visa)");
            //    cbMethod.SelectedIndex = -1;
            //}
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter the Account Holder Name.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cbMethod.SelectedItem == null)
            {
                MessageBox.Show("Please select a Payment Method.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtNumber.Text))
            {
                MessageBox.Show("Please enter the Account/Card Number.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Paid Amount যাচাই
            if (!decimal.TryParse(txtAmount.Text, NumberStyles.Currency | NumberStyles.Number, CultureInfo.CurrentCulture, out decimal paidAmount) || paidAmount <= 0)
            {
                MessageBox.Show("Please enter a valid payment amount.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // সম্পূর্ণ পেমেন্ট নিশ্চিত করা
            if (paidAmount < totalAmount)
            {
                MessageBox.Show("Partial payments are not allowed. Please pay the full amount: " + totalAmount.ToString("N2") + " Tk.", "Payment Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string holderName = txtName.Text.Trim();
            string method = cbMethod.SelectedItem.ToString();
            // Due Amount এই ক্ষেত্রে 0 বা 0 এর কম হবে

            try
            {
                if (con.State == ConnectionState.Closed) con.Open();

                // 2. dbo.Payment টেবিলে ডেটা ঢোকানো (Amount এবং DueAmount কলামে Paid Amount ও Due 0 সেভ করা)
                string query = @"
                    INSERT INTO Payment (UserId, BookingId, Amount, DueAmount, Method, Date, HolderName)
                    VALUES (@UserId, @BookingId, @Amount, @DueAmount, @Method, @Date, @HolderName);";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", currentUserId);
                    cmd.Parameters.AddWithValue("@BookingId", BookingId);
                    cmd.Parameters.AddWithValue("@Amount", paidAmount);
                    cmd.Parameters.AddWithValue("@DueAmount", 0); // যেহেতু পুরোটা পেমেন্ট করা হয়েছে, Due 0
                    cmd.Parameters.AddWithValue("@Method", method);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@HolderName", holderName);

                    int rowsInserted = cmd.ExecuteNonQuery();

                    if (rowsInserted > 0)
                    {
                        // 3. dbo.Booking টেবিলে PaymentStatus আপডেট করা
                        string updateQuery = "UPDATE Booking SET PaymentStatus = @Status WHERE BookingId = @BookingId";

                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, con))
                        {
                            updateCommand.Parameters.AddWithValue("@Status", "Paid");
                            updateCommand.Parameters.AddWithValue("@BookingId", BookingId);
                            updateCommand.ExecuteNonQuery();
                        }

                        MessageBox.Show("Payment successful! Your booking is confirmed. Total Paid: " + paidAmount.ToString("N2") + " Tk.", "Payment Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // সফলতার পর Tour ফর্মে ফিরে যাওয়া
                        //NavigateToTour();
                    }
                    else
                    {
                        MessageBox.Show("Payment record insertion failed.", "Payment Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (SqlException sqlex)
            {
                MessageBox.Show("Database Error during Payment: " + sqlex.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }
    }
}
