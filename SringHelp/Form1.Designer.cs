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
            this.Label_Status = new System.Windows.Forms.Label();
            this.checkBox_OrgUser = new System.Windows.Forms.CheckBox();
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
            // 
            // StatusStrip_Main
            // 
            this.StatusStrip_Main.Location = new System.Drawing.Point(0, 489);
            this.StatusStrip_Main.MinimumSize = new System.Drawing.Size(0, 30);
            this.StatusStrip_Main.Name = "StatusStrip_Main";
            this.StatusStrip_Main.Size = new System.Drawing.Size(800, 30);
            this.StatusStrip_Main.TabIndex = 10;
            this.StatusStrip_Main.Text = "statusStrip1";
            // 
            // Label_Status
            // 
            this.Label_Status.AutoSize = true;
            this.Label_Status.Location = new System.Drawing.Point(12, 495);
            this.Label_Status.Name = "Label_Status";
            this.Label_Status.Size = new System.Drawing.Size(79, 17);
            this.Label_Status.TabIndex = 11;
            this.Label_Status.Text = "Label_Status";
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
            // MainFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 519);
            this.Controls.Add(this.checkBox_OrgUser);
            this.Controls.Add(this.Label_Status);
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
        private System.Windows.Forms.Label Label_Status;
        private System.Windows.Forms.CheckBox checkBox_OrgUser;
    }
}

