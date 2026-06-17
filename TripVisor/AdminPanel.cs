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
    public partial class AdminPanel : Form
    {
        private int userId;
        private int Id;
        private string name;
        private string role;

        public AdminPanel(int id, string Name, string Role, int userId)
        {
            InitializeComponent();
            Id = id;
            name = Name;
            role = Role;
            this.userId = userId;
        }

        private void AdminPanel_Load(object sender, EventArgs e)
        {
            lblName.Text = name + "  [ " + role + " ]";
            ShadowFormAdminPanel.SetShadowForm(this);
            btnUserInfo.PerformClick();
        }

        private void container2(object form)
        {
            if (pnlContainer2.Controls.Count > 0)
            {
                pnlContainer2.Controls.Clear();
            }

            Form fm = form as Form;
            fm.TopLevel = false;
            fm.FormBorderStyle = FormBorderStyle.None;
            fm.Dock = DockStyle.Fill;
            pnlContainer2.Controls.Add(fm);
            pnlContainer2.Tag = fm;
            fm.Show();
        }

        private void btnUserInfo_Click(object sender, EventArgs e)
        {
            container2(new UserInfo(Id));

        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            container2(new Inventory());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            AdminLogin adminLogin = new AdminLogin();
            adminLogin.Show();
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            container2(new Profile(userId));
        }
    }
}
