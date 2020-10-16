using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SringHelpMainFrom
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button_Sign_Click(object sender, EventArgs e)
        {
            var examId = new Guid(textBox_ExamId.Text);
            var user = new EduEntities().Base_User.Take(int.Parse(numericUpDown_TotalCount.Text));
            var rst = ExamData.BulkUserToExam(examId, int.Parse(numericUpDown_PageCount.Text),
                user.Select(d => d.UserId).ToArray());
            MessageBox.Show(rst);
        }
    }
}
