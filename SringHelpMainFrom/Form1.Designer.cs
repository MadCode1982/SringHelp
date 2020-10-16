namespace SringHelpMainFrom
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button_Sign = new System.Windows.Forms.Button();
            this.numericUpDown_PageCount = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_TotalCount = new System.Windows.Forms.NumericUpDown();
            this.textBox_ExamId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_PageCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_TotalCount)).BeginInit();
            this.SuspendLayout();
            // 
            // button_Sign
            // 
            this.button_Sign.Location = new System.Drawing.Point(499, 175);
            this.button_Sign.Name = "button_Sign";
            this.button_Sign.Size = new System.Drawing.Size(75, 23);
            this.button_Sign.TabIndex = 0;
            this.button_Sign.Text = "报名";
            this.button_Sign.UseVisualStyleBackColor = true;
            this.button_Sign.Click += new System.EventHandler(this.button_Sign_Click);
            // 
            // numericUpDown_PageCount
            // 
            this.numericUpDown_PageCount.Location = new System.Drawing.Point(83, 35);
            this.numericUpDown_PageCount.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown_PageCount.Name = "numericUpDown_PageCount";
            this.numericUpDown_PageCount.Size = new System.Drawing.Size(120, 21);
            this.numericUpDown_PageCount.TabIndex = 1;
            // 
            // numericUpDown_TotalCount
            // 
            this.numericUpDown_TotalCount.Location = new System.Drawing.Point(83, 69);
            this.numericUpDown_TotalCount.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown_TotalCount.Name = "numericUpDown_TotalCount";
            this.numericUpDown_TotalCount.Size = new System.Drawing.Size(120, 21);
            this.numericUpDown_TotalCount.TabIndex = 2;
            // 
            // textBox_ExamId
            // 
            this.textBox_ExamId.Location = new System.Drawing.Point(83, 6);
            this.textBox_ExamId.Name = "textBox_ExamId";
            this.textBox_ExamId.Size = new System.Drawing.Size(276, 21);
            this.textBox_ExamId.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "考试主键：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "每页数量：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "报名人数：";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_ExamId);
            this.Controls.Add(this.numericUpDown_TotalCount);
            this.Controls.Add(this.numericUpDown_PageCount);
            this.Controls.Add(this.button_Sign);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_PageCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_TotalCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Sign;
        private System.Windows.Forms.NumericUpDown numericUpDown_PageCount;
        private System.Windows.Forms.NumericUpDown numericUpDown_TotalCount;
        private System.Windows.Forms.TextBox textBox_ExamId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

