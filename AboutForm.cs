using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SysInfo
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
         }

        private void __AboutBox_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
