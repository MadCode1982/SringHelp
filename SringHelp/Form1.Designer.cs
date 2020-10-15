namespace SringHelp
{
    partial class MainFrom
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Button_Create = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ComboBox_ExamList = new System.Windows.Forms.ComboBox();
            this.Label_Org = new System.Windows.Forms.Label();
            this.ComboBox_OrgList = new System.Windows.Forms.ComboBox();
            this.Button_SearchOrg = new System.Windows.Forms.Button();
            this.TextBox_OrgSearch = new System.Windows.Forms.TextBox();
            this.TextBox_SearchExam = new System.Windows.Forms.TextBox();
            this.Button_SearchExam = new System.Windows.Forms.Button();
            this.Button_SignUser = new System.Windows.Forms.Button();
            this.Button_ExcelSign = new System.Windows.Forms.Button();
            this.Button_UserSelect = new System.Windows.Forms.Button();
            this.StatusStrip_Main = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar_Default = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel_Default = new System.Windows.Forms.ToolStripStatusLabel();
            this.checkBox_OrgUser = new System.Windows.Forms.CheckBox();
            this.button_UpdateToDataBase = new System.Windows.Forms.Button();
            this.button_LoadExamToCache = new System.Windows.Forms.Button();
            this.label_ConStr = new System.Windows.Forms.Label();
            this.label_MaxSignCount = new System.Windows.Forms.Label();
            this.label_MaxCachePageCount = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.numericUpDown_MaxSignPageCount = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_MaxCachePageCount = new System.Windows.Forms.NumericUpDown();
            this.button_BulkToolsTest = new System.Windows.Forms.Button();
            this.StatusStrip_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MaxSignPageCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MaxCachePageCount)).BeginInit();
            this.SuspendLayout();
            // 
            // Button_Create
            // 
            this.Button_Create.Location = new System.Drawing.Point(702, 30);
            this.Button_Create.Name = "Button_Create";
            this.Button_Create.Size = new System.Drawing.Size(75, 23);
            this.Button_Create.TabIndex = 0;
            this.Button_Create.Text = "创建";
            this.Button_Create.UseVisualStyleBackColor = true;
            this.Button_Create.Click += new System.EventHandler(this.Button_Create_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "考试：";
            // 
            // ComboBox_ExamList
            // 
            this.ComboBox_ExamList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_ExamList.FormattingEnabled = true;
            this.ComboBox_ExamList.Location = new System.Drawing.Point(64, 58);
            this.ComboBox_ExamList.Name = "ComboBox_ExamList";
            this.ComboBox_ExamList.Size = new System.Drawing.Size(219, 25);
            this.ComboBox_ExamList.TabIndex = 2;
            // 
            // Label_Org
            // 
            this.Label_Org.AutoSize = true;
            this.Label_Org.Location = new System.Drawing.Point(12, 9);
            this.Label_Org.Name = "Label_Org";
            this.Label_Org.Size = new System.Drawing.Size(44, 17);
            this.Label_Org.TabIndex = 3;
            this.Label_Org.Text = "机构：";
            // 
            // ComboBox_OrgList
            // 
            this.ComboBox_OrgList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_OrgList.FormattingEnabled = true;
            this.ComboBox_OrgList.Location = new System.Drawing.Point(64, 6);
            this.ComboBox_OrgList.Name = "ComboBox_OrgList";
            this.ComboBox_OrgList.Size = new System.Drawing.Size(219, 25);
            this.ComboBox_OrgList.TabIndex = 2;
            // 
            // Button_SearchOrg
            // 
            this.Button_SearchOrg.Location = new System.Drawing.Point(423, 6);
            this.Button_SearchOrg.Name = "Button_SearchOrg";
            this.Button_SearchOrg.Size = new System.Drawing.Size(75, 23);
            this.Button_SearchOrg.TabIndex = 4;
            this.Button_SearchOrg.Text = "查询";
            this.Button_SearchOrg.UseVisualStyleBackColor = true;
            this.Button_SearchOrg.Click += new System.EventHandler(this.Button_SearchOrg_Click);
            // 
            // TextBox_OrgSearch
            // 
            this.TextBox_OrgSearch.Location = new System.Drawing.Point(301, 6);
            this.TextBox_OrgSearch.Name = "TextBox_OrgSearch";
            this.TextBox_OrgSearch.Size = new System.Drawing.Size(100, 23);
            this.TextBox_OrgSearch.TabIndex = 5;
            // 
            // TextBox_SearchExam
            // 
            this.TextBox_SearchExam.Location = new System.Drawing.Point(301, 58);
            this.TextBox_SearchExam.Name = "TextBox_SearchExam";
            this.TextBox_SearchExam.Size = new System.Drawing.Size(100, 23);
            this.TextBox_SearchExam.TabIndex = 5;
            // 
            // Button_SearchExam
            // 
            this.Button_SearchExam.Location = new System.Drawing.Point(423, 58);
            this.Button_SearchExam.Name = "Button_SearchExam";
            this.Button_SearchExam.Size = new System.Drawing.Size(75, 23);
            this.Button_SearchExam.TabIndex = 4;
            this.Button_SearchExam.Text = "查询";
            this.Button_SearchExam.UseVisualStyleBackColor = true;
            this.Button_SearchExam.Click += new System.EventHandler(this.Button_SearchExam_Click);
            // 
            // Button_SignUser
            // 
            this.Button_SignUser.Location = new System.Drawing.Point(423, 109);
            this.Button_SignUser.Name = "Button_SignUser";
            this.Button_SignUser.Size = new System.Drawing.Size(75, 23);
            this.Button_SignUser.TabIndex = 6;
            this.Button_SignUser.Text = "报名全部";
            this.Button_SignUser.UseVisualStyleBackColor = true;
            this.Button_SignUser.Click += new System.EventHandler(this.Button_SignUser_Click);
            // 
            // Button_ExcelSign
            // 
            this.Button_ExcelSign.Location = new System.Drawing.Point(301, 109);
            this.Button_ExcelSign.Name = "Button_ExcelSign";
            this.Button_ExcelSign.Size = new System.Drawing.Size(100, 23);
            this.Button_ExcelSign.TabIndex = 7;
            this.Button_ExcelSign.Text = "Excel 报名";
            this.Button_ExcelSign.UseVisualStyleBackColor = true;
            this.Button_ExcelSign.Click += new System.EventHandler(this.Button_ExcelSign_Click);
            // 
            // Button_UserSelect
            // 
            this.Button_UserSelect.Location = new System.Drawing.Point(189, 109);
            this.Button_UserSelect.Name = "Button_UserSelect";
            this.Button_UserSelect.Size = new System.Drawing.Size(94, 23);
            this.Button_UserSelect.TabIndex = 8;
            this.Button_UserSelect.Text = "选择报名";
            this.Button_UserSelect.UseVisualStyleBackColor = true;
            this.Button_UserSelect.Click += new System.EventHandler(this.Button_UserSelect_Click);
            // 
            // StatusStrip_Main
            // 
            this.StatusStrip_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar_Default,
            this.toolStripStatusLabel_Default});
            this.StatusStrip_Main.Location = new System.Drawing.Point(0, 551);
            this.StatusStrip_Main.MinimumSize = new System.Drawing.Size(0, 30);
            this.StatusStrip_Main.Name = "StatusStrip_Main";
            this.StatusStrip_Main.Size = new System.Drawing.Size(1076, 30);
            this.StatusStrip_Main.TabIndex = 10;
            this.StatusStrip_Main.Text = "statusStrip1";
            // 
            // toolStripProgressBar_Default
            // 
            this.toolStripProgressBar_Default.Name = "toolStripProgressBar_Default";
            this.toolStripProgressBar_Default.Size = new System.Drawing.Size(100, 24);
            // 
            // toolStripStatusLabel_Default
            // 
            this.toolStripStatusLabel_Default.Name = "toolStripStatusLabel_Default";
            this.toolStripStatusLabel_Default.Size = new System.Drawing.Size(131, 25);
            this.toolStripStatusLabel_Default.Text = "toolStripStatusLabel1";
            // 
            // checkBox_OrgUser
            // 
            this.checkBox_OrgUser.AutoSize = true;
            this.checkBox_OrgUser.Location = new System.Drawing.Point(518, 111);
            this.checkBox_OrgUser.Name = "checkBox_OrgUser";
            this.checkBox_OrgUser.Size = new System.Drawing.Size(75, 21);
            this.checkBox_OrgUser.TabIndex = 12;
            this.checkBox_OrgUser.Text = "机构用户";
            this.checkBox_OrgUser.UseVisualStyleBackColor = true;
            // 
            // button_UpdateToDataBase
            // 
            this.button_UpdateToDataBase.Location = new System.Drawing.Point(702, 109);
            this.button_UpdateToDataBase.Name = "button_UpdateToDataBase";
            this.button_UpdateToDataBase.Size = new System.Drawing.Size(87, 52);
            this.button_UpdateToDataBase.TabIndex = 13;
            this.button_UpdateToDataBase.Text = "同步缓存数据到数据库";
            this.button_UpdateToDataBase.UseVisualStyleBackColor = true;
            this.button_UpdateToDataBase.Click += new System.EventHandler(this.button_UpdateToDataBase_Click);
            // 
            // button_LoadExamToCache
            // 
            this.button_LoadExamToCache.Location = new System.Drawing.Point(702, 192);
            this.button_LoadExamToCache.Name = "button_LoadExamToCache";
            this.button_LoadExamToCache.Size = new System.Drawing.Size(87, 43);
            this.button_LoadExamToCache.TabIndex = 14;
            this.button_LoadExamToCache.Text = "加载考试信息到缓存";
            this.button_LoadExamToCache.UseVisualStyleBackColor = true;
            this.button_LoadExamToCache.Click += new System.EventHandler(this.button_LoadExamToCache_Click);
            // 
            // label_ConStr
            // 
            this.label_ConStr.AutoSize = true;
            this.label_ConStr.Location = new System.Drawing.Point(0, 530);
            this.label_ConStr.Name = "label_ConStr";
            this.label_ConStr.Size = new System.Drawing.Size(116, 17);
            this.label_ConStr.TabIndex = 15;
            this.label_ConStr.Text = "数据库连接字符串：";
            // 
            // label_MaxSignCount
            // 
            this.label_MaxSignCount.AutoSize = true;
            this.label_MaxSignCount.Location = new System.Drawing.Point(0, 495);
            this.label_MaxSignCount.Name = "label_MaxSignCount";
            this.label_MaxSignCount.Size = new System.Drawing.Size(104, 17);
            this.label_MaxSignCount.TabIndex = 16;
            this.label_MaxSignCount.Text = "最大每次报名数：";
            // 
            // label_MaxCachePageCount
            // 
            this.label_MaxCachePageCount.AutoSize = true;
            this.label_MaxCachePageCount.Location = new System.Drawing.Point(221, 495);
            this.label_MaxCachePageCount.Name = "label_MaxCachePageCount";
            this.label_MaxCachePageCount.Size = new System.Drawing.Size(104, 17);
            this.label_MaxCachePageCount.TabIndex = 17;
            this.label_MaxCachePageCount.Text = "最大每次缓存数：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(122, 527);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(751, 23);
            this.textBox1.TabIndex = 18;
            this.textBox1.Text = "Server=DESKTOP-JDEFIP0\\MSSQLSERVER16;Initial Catalog=Edu;Integrated Security=True" +
    "";
            // 
            // numericUpDown_MaxSignPageCount
            // 
            this.numericUpDown_MaxSignPageCount.Location = new System.Drawing.Point(122, 493);
            this.numericUpDown_MaxSignPageCount.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown_MaxSignPageCount.Name = "numericUpDown_MaxSignPageCount";
            this.numericUpDown_MaxSignPageCount.Size = new System.Drawing.Size(77, 23);
            this.numericUpDown_MaxSignPageCount.TabIndex = 19;
            // 
            // numericUpDown_MaxCachePageCount
            // 
            this.numericUpDown_MaxCachePageCount.Location = new System.Drawing.Point(331, 493);
            this.numericUpDown_MaxCachePageCount.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown_MaxCachePageCount.Name = "numericUpDown_MaxCachePageCount";
            this.numericUpDown_MaxCachePageCount.Size = new System.Drawing.Size(70, 23);
            this.numericUpDown_MaxCachePageCount.TabIndex = 20;
            // 
            // button_BulkToolsTest
            // 
            this.button_BulkToolsTest.Location = new System.Drawing.Point(667, 342);
            this.button_BulkToolsTest.Name = "button_BulkToolsTest";
            this.button_BulkToolsTest.Size = new System.Drawing.Size(75, 46);
            this.button_BulkToolsTest.TabIndex = 21;
            this.button_BulkToolsTest.Text = "批量报名插件测试";
            this.button_BulkToolsTest.UseVisualStyleBackColor = true;
            this.button_BulkToolsTest.Click += new System.EventHandler(this.button_BulkToolsTest_Click);
            // 
            // MainFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1076, 581);
            this.Controls.Add(this.button_BulkToolsTest);
            this.Controls.Add(this.numericUpDown_MaxCachePageCount);
            this.Controls.Add(this.numericUpDown_MaxSignPageCount);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label_MaxCachePageCount);
            this.Controls.Add(this.label_MaxSignCount);
            this.Controls.Add(this.label_ConStr);
            this.Controls.Add(this.button_LoadExamToCache);
            this.Controls.Add(this.button_UpdateToDataBase);
            this.Controls.Add(this.checkBox_OrgUser);
            this.Controls.Add(this.StatusStrip_Main);
            this.Controls.Add(this.Button_UserSelect);
            this.Controls.Add(this.Button_ExcelSign);
            this.Controls.Add(this.Button_SignUser);
            this.Controls.Add(this.Button_SearchExam);
            this.Controls.Add(this.TextBox_SearchExam);
            this.Controls.Add(this.TextBox_OrgSearch);
            this.Controls.Add(this.Button_SearchOrg);
            this.Controls.Add(this.ComboBox_OrgList);
            this.Controls.Add(this.Label_Org);
            this.Controls.Add(this.ComboBox_ExamList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Button_Create);
            this.Name = "MainFrom";
            this.Text = "主窗口";
            this.Load += new System.EventHandler(this.MainFrom_Load);
            this.StatusStrip_Main.ResumeLayout(false);
            this.StatusStrip_Main.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MaxSignPageCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MaxCachePageCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Button_Create;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ComboBox_ExamList;
        private System.Windows.Forms.Label Label_Org;
        private System.Windows.Forms.ComboBox ComboBox_OrgList;
        private System.Windows.Forms.Button Button_SearchOrg;
        private System.Windows.Forms.TextBox TextBox_OrgSearch;
        private System.Windows.Forms.TextBox TextBox_SearchExam;
        private System.Windows.Forms.Button Button_SearchExam;
        private System.Windows.Forms.Button Button_SignUser;
        private System.Windows.Forms.Button Button_ExcelSign;
        private System.Windows.Forms.Button Button_UserSelect;
        private System.Windows.Forms.StatusStrip StatusStrip_Main;
        private System.Windows.Forms.CheckBox checkBox_OrgUser;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar_Default;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_Default;
        private System.Windows.Forms.Button button_UpdateToDataBase;
        private System.Windows.Forms.Button button_LoadExamToCache;
        private System.Windows.Forms.Label label_ConStr;
        private System.Windows.Forms.Label label_MaxSignCount;
        private System.Windows.Forms.Label label_MaxCachePageCount;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown_MaxSignPageCount;
        private System.Windows.Forms.NumericUpDown numericUpDown_MaxCachePageCount;
        private System.Windows.Forms.Button button_BulkToolsTest;
    }
}

