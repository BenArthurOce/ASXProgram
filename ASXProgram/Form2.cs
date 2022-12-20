using ASXProgram.TabUserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASXProgram
{
    public partial class TestingTabs : Form
    {
        public TestingTabs()
        {
            InitializeComponent();
            UC_Tab1 uc = new UC_Tab1();
            addUserControl(uc);
        }



        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void gBtn_tab1_Click(object sender, EventArgs e)
        {
            UC_Tab1 uc = new UC_Tab1();
            addUserControl(uc);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            UC_Tab2 uc = new UC_Tab2();
            addUserControl(uc);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            UC_Tab3 uc = new UC_Tab3();
            addUserControl(uc);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            UC_Tab4 uc = new UC_Tab4();
            addUserControl(uc);
        }
    }
}
