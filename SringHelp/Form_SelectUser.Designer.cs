namespace SringHelp
{
    partial class Form_SelectUser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DataGridView_User = new System.Windows.Forms.DataGridView();
            this.ToolStrip_Main = new System.Windows.Forms.ToolStrip();
            this.Label_RowCount = new System.Windows.Forms.Label();
            this.NumericUpDown_RowCount = new System.Windows.Forms.NumericUpDown();
            this.NumericUpDown_ = new System.Windows.Forms.NumericUpDown();
            this.Label_Page = new System.Windows.Forms.Label();
            this.Button_SearchUser = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.label_Status = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_User)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_RowCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_)).BeginInit();
            this.SuspendLayout();
            // 
            // DataGridView_User
            // 
            this.DataGridView_User.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView_User.Location = new System.Drawing.Point(0, 28);
            this.DataGridView_User.Name = "DataGridView_User";
            this.DataGridView_User.Size = new System.Drawing.Size(800, 341);
            this.DataGridView_User.TabIndex = 0;
            this.DataGridView_User.Text = "dataGridView1";
            // 
            // ToolStrip_Main
            // 
            this.ToolStrip_Main.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip_Main.Name = "ToolStrip_Main";
            this.ToolStrip_Main.Size = new System.Drawing.Size(842, 25);
            this.ToolStrip_Main.TabIndex = 1;
            this.ToolStrip_Main.Text = "toolStrip1";
            // 
            // Label_RowCount
            // 
            this.Label_RowCount.AutoSize = true;
            this.Label_RowCount.Location = new System.Drawing.Point(12, 5);
            this.Label_RowCount.Name = "Label_RowCount";
            this.Label_RowCount.Size = new System.Drawing.Size(68, 17);
            this.Label_RowCount.TabIndex = 2;
            this.Label_RowCount.Text = "每页数量：";
            // 
            // NumericUpDown_RowCount
            // 
            this.NumericUpDown_RowCount.Location = new System.Drawing.Point(86, 2);
            this.NumericUpDown_RowCount.Name = "NumericUpDown_RowCount";
            this.NumericUpDown_RowCount.Size = new System.Drawing.Size(120, 23);
            this.NumericUpDown_RowCount.TabIndex = 3;
            this.NumericUpDown_RowCount.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // NumericUpDown_
            // 
            this.NumericUpDown_.Location = new System.Drawing.Point(250, 3);
            this.NumericUpDown_.Name = "NumericUpDown_";
            this.NumericUpDown_.Size = new System.Drawing.Size(120, 23);
            this.NumericUpDown_.TabIndex = 4;
            this.NumericUpDown_.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Label_Page
            // 
            this.Label_Page.AutoSize = true;
            this.Label_Page.Location = new System.Drawing.Point(212, 4);
            this.Label_Page.Name = "Label_Page";
            this.Label_Page.Size = new System.Drawing.Size(32, 17);
            this.Label_Page.TabIndex = 5;
            this.Label_Page.Text = "页：";
            // 
            // Button_SearchUser
            // 
            this.Button_SearchUser.Location = new System.Drawing.Point(376, 3);
            this.Button_SearchUser.Name = "Button_SearchUser";
            this.Button_SearchUser.Size = new System.Drawing.Size(75, 23);
            this.Button_SearchUser.TabIndex = 6;
            this.Button_SearchUser.Text = "查询";
            this.Button_SearchUser.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 452);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(842, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // label_Status
            // 
            this.label_Status.AutoSize = true;
            this.label_Status.Location = new System.Drawing.Point(1, 457);
            this.label_Status.Name = "label_Status";
            this.label_Status.Size = new System.Drawing.Size(79, 17);
            this.label_Status.TabIndex = 8;
            this.label_Status.Text = "Lable_Status";
            // 
            // Form_SelectUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 474);
            this.Controls.Add(this.label_Status);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.Button_SearchUser);
            this.Controls.Add(this.Label_Page);
            this.Controls.Add(this.NumericUpDown_);
            this.Controls.Add(this.NumericUpDown_RowCount);
            this.Controls.Add(this.Label_RowCount);
            this.Controls.Add(this.ToolStrip_Main);
            this.Controls.Add(this.DataGridView_User);
            this.Name = "Form_SelectUser";
            this.Text = "选择报名用户";
            this.Load += new System.EventHandler(this.Form_SelectUser_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_User)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_RowCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DataGridView_User;
        private System.Windows.Forms.ToolStrip ToolStrip_Main;
        private System.Windows.Forms.Label Label_RowCount;
        private System.Windows.Forms.NumericUpDown NumericUpDown_RowCount;
        private System.Windows.Forms.NumericUpDown NumericUpDown_;
        private System.Windows.Forms.Label Label_Page;
        private System.Windows.Forms.Button Button_SearchUser;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label label_Status;
    }
}