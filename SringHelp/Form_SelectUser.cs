using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SringHelp
{
    public partial class Form_SelectUser : Form
    {
        public Form_SelectUser()
        {
            InitializeComponent();
        }

        private void Form_SelectUser_Load(object sender, EventArgs e)
        {
            label_Status.Text = "";
        }
    }
}
