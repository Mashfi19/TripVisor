using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TripVisor
{
    public partial class welcome : Form
    {
        public welcome()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
        }

        private void btnStart_Click_1(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
        }
    }
}
