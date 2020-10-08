using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SringHelp
{
    public partial class MainFrom : Form
    {
        public MainFrom()
        {
            InitializeComponent();
        }

        private void Button_Create_Click(object sender, EventArgs e)
        {
            var userIds = ExamDataHelper.GetUserIds().ToArray();
            var rst = ExamDataHelper.SignUserToExam(new Guid("A36C9AF2-F522-4EB7-BB98-945BEBD51C21"), userIds);
            MessageBox.Show(rst);
        }

        private void Button_SearchOrg_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel_Default.Text = "加载机构数据";
            var orgs = ExamDataHelper.GetOrgIdNamesTable(TextBox_OrgSearch.Text);
            ComboBox_OrgList.DisplayMember = "FullName";
            ComboBox_OrgList.ValueMember = "OrganizeId";
            ComboBox_OrgList.DataSource = orgs;
            toolStripStatusLabel_Default.Text = "机构数据加载完成";
        }

        private void Button_SearchExam_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel_Default.Text = "加载考试数据";
            var orgSelect = (DataRowView)ComboBox_OrgList.SelectedItem;
            var exams = ExamDataHelper.GetExamTable(orgSelect?[0].ToString());
            ComboBox_ExamList.DisplayMember = "ExamName";
            ComboBox_ExamList.ValueMember = "ExamId";
            ComboBox_ExamList.DataSource = exams;
            toolStripStatusLabel_Default.Text = "考试数据加载完成";
        }

        private void Button_SignUser_Click(object sender, EventArgs e)
        {
            var orgSelect = (DataRowView)ComboBox_OrgList.SelectedItem;
            var selectExam = (DataRowView)ComboBox_ExamList.SelectedItem;

            if (selectExam == null)
            {
                MessageBox.Show("请选择考试！");
                return;
            }
            Guid? orgId = null;
            if (checkBox_OrgUser.Checked)
            {
                if (orgSelect == null)
                {
                    MessageBox.Show("请选择机构！");
                    return;
                }
                orgId = (Guid)orgSelect[0];
            }
            toolStripStatusLabel_Default.Text = "开始报名";
            var rst = ExamDataHelper.SignOrgUser((Guid)selectExam[0], orgId);
            toolStripStatusLabel_Default.Text = "报名结束";
            MessageBox.Show(rst);
        }

        private void Button_ExcelSign_Click(object sender, EventArgs e)
        {
            var selectExam = (DataRowView)ComboBox_ExamList.SelectedItem;
            if (selectExam == null)
            {
                MessageBox.Show("请选择考试！");
                return;
            }

            toolStripStatusLabel_Default.Text = "选择报名文件";
            OpenFileDialog excelFileDialog = new OpenFileDialog();
            excelFileDialog.Title = "请选择报名文件";
            excelFileDialog.Filter = "Excel文件|*.xls;*.xlsx"; //设置要选择的文件的类型

            if (excelFileDialog.ShowDialog() == DialogResult.OK)
            {
                toolStripStatusLabel_Default.Text = "开始报名";
                var rst = ExamDataHelper.SignExcelUser((Guid)selectExam[0], excelFileDialog.FileName);
                MessageBox.Show(rst);
            }
            toolStripStatusLabel_Default.Text = "报名结束";
        }

        private void MainFrom_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel_Default.Text = "";
        }


        public void SignExcelUser(Guid examId, string fileName)
        {
            var userAccounts = ExcelHelper.GetWorkBookFromFile(fileName).GetUserAccounts().ToArray();
            var userIds = ExamDataHelper.GetUserIdsByAccount(userAccounts).ToArray();
            var rst = Parallel.For(0, userIds.Length / 5000, d =>
            {
                ExamDataHelper.SignUserToExam(examId, userIds.Skip(d * 5000).Take(5000).ToArray());
            });
            while(!rst.IsCompleted)
            {

            }
        }

        delegate void SetLabelStatusText(string text);

        //第三步：定义更新UI控件的方法
        /// <summary>
        /// 更新文本框内容的方法
        /// </summary>
        /// <param name="text"></param>
        private void SetText(string text)
        {
            toolStripStatusLabel_Default.Text = text;
            //if (this.Label_Status.InvokeRequired)//如果调用控件的线程和创建创建控件的线程不是同一个则为True
            //{
            //    while (!this.Label_Status.IsHandleCreated)
            //    {
            //        //解决窗体关闭时出现“访问已释放句柄“的异常
            //        if (this.Label_Status.Disposing || this.Label_Status.IsDisposed)
            //            return;
            //    }
            //    SetLabelStatusText d = new SetLabelStatusText(SetText);
            //    this.toolStripStatusLabel_Default.Invoke(d, text);

            //}
            //else
            //{
            //    this.Label_Status.Text = text;
            //}
        }
        private void Button_UserSelect_Click(object sender, EventArgs e)
        {
            var up = int.Parse(TextBox_OrgSearch.Text);
            var down = int.Parse(TextBox_SearchExam.Text);

            toolStripStatusLabel_Default.Text = (up / down).ToString();

        }
    }
}