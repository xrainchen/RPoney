namespace RPoney.HttpTools
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtRequestUrl = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbRequestMethod = new System.Windows.Forms.ComboBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbContentType = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtRequestData = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtReponstData = new System.Windows.Forms.TextBox();
            this.cbCharset = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtRequestUrl);
            this.groupBox1.Location = new System.Drawing.Point(93, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(594, 47);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "请求地址";
            // 
            // txtRequestUrl
            // 
            this.txtRequestUrl.Location = new System.Drawing.Point(6, 20);
            this.txtRequestUrl.Name = "txtRequestUrl";
            this.txtRequestUrl.Size = new System.Drawing.Size(572, 21);
            this.txtRequestUrl.TabIndex = 0;
            this.txtRequestUrl.Text = "http://";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbRequestMethod);
            this.groupBox2.Location = new System.Drawing.Point(5, 11);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(82, 48);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "请求方式";
            // 
            // cbRequestMethod
            // 
            this.cbRequestMethod.FormattingEnabled = true;
            this.cbRequestMethod.Items.AddRange(new object[] {
            "GET",
            "POST"});
            this.cbRequestMethod.Location = new System.Drawing.Point(7, 20);
            this.cbRequestMethod.Name = "cbRequestMethod";
            this.cbRequestMethod.Size = new System.Drawing.Size(69, 20);
            this.cbRequestMethod.TabIndex = 3;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(693, 18);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(46, 41);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Go";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbContentType);
            this.groupBox3.Location = new System.Drawing.Point(5, 66);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(300, 45);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Content-Type";
            // 
            // cbContentType
            // 
            this.cbContentType.FormattingEnabled = true;
            this.cbContentType.Items.AddRange(new object[] {
            "application/x-www-form-urlencoded",
            "multipart/form-data",
            "application/json",
            "text/xml"});
            this.cbContentType.Location = new System.Drawing.Point(6, 19);
            this.cbContentType.Name = "cbContentType";
            this.cbContentType.Size = new System.Drawing.Size(248, 20);
            this.cbContentType.TabIndex = 4;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbCharset);
            this.groupBox4.Location = new System.Drawing.Point(328, 66);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(155, 45);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Charset";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtRequestData);
            this.groupBox5.Location = new System.Drawing.Point(13, 118);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(726, 100);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "请求数据";
            // 
            // txtRequestData
            // 
            this.txtRequestData.Location = new System.Drawing.Point(7, 21);
            this.txtRequestData.Multiline = true;
            this.txtRequestData.Name = "txtRequestData";
            this.txtRequestData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRequestData.Size = new System.Drawing.Size(713, 73);
            this.txtRequestData.TabIndex = 0;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtReponstData);
            this.groupBox6.Location = new System.Drawing.Point(12, 225);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(727, 217);
            this.groupBox6.TabIndex = 6;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "返回数据";
            // 
            // txtReponstData
            // 
            this.txtReponstData.Location = new System.Drawing.Point(8, 21);
            this.txtReponstData.Multiline = true;
            this.txtReponstData.Name = "txtReponstData";
            this.txtReponstData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtReponstData.Size = new System.Drawing.Size(713, 179);
            this.txtReponstData.TabIndex = 0;
            // 
            // cbCharset
            // 
            this.cbCharset.FormattingEnabled = true;
            this.cbCharset.Items.AddRange(new object[] {
            "UTF-8",
            "GB2312",
            "GBK"});
            this.cbCharset.Location = new System.Drawing.Point(6, 19);
            this.cbCharset.Name = "cbCharset";
            this.cbCharset.Size = new System.Drawing.Size(143, 20);
            this.cbCharset.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 454);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtRequestUrl;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbRequestMethod;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cbContentType;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtRequestData;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txtReponstData;
        private System.Windows.Forms.ComboBox cbCharset;
    }
}

