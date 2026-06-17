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
    public partial class Main : Form
    {
        private string name;
        private int userId;

        public Panel ContainerPanel
        {
            get { return pnlContainer; } 
        }

        public Main(string Name, int UserId)
        {
            InitializeComponent();
            name = Name;
            userId = UserId;
        }



        private void Main_Load(object sender, EventArgs e)
        {
            lblName.Text = "HI, " + name + "...!";
            ShadowFormMain.SetShadowForm(this);
            btnTour.PerformClick();
        }

        private void btnTour_Click(object sender, EventArgs e)
        {
            container(new Tour(userId));
        }

        private void container(object _form)
        {
            if (pnlContainer.Controls.Count > 0)
            {
                pnlContainer.Controls.Clear();
            }

            Form fm = _form as Form;
            fm.TopLevel = false;
            fm.FormBorderStyle = FormBorderStyle.None;
            fm.Dock = DockStyle.Fill;
            pnlContainer.Controls.Add(fm);
            pnlContainer.Tag = fm;
            fm.Show();
        }

        private void btnHotel_Click(object sender, EventArgs e)
        {
            container(new Hotel(userId));
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            container(new Profile(userId));
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
        }
    }
}
