namespace Game_Enhancer
{
    partial class Form2
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
            this.components = new System.ComponentModel.Container();
            this.guna2AnimateWindow1 = new Guna.UI2.WinForms.Guna2AnimateWindow(this.components);
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.BypassBtn = new Guna.UI2.WinForms.Guna2Button();
            this.Sta = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.closeBp = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.AutoSize = false;
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Old English Text MT", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(12, 12);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(396, 62);
            this.guna2HtmlLabel1.TabIndex = 0;
            this.guna2HtmlLabel1.Text = "BYPASS";
            // 
            // BypassBtn
            // 
            this.BypassBtn.BorderRadius = 5;
            this.BypassBtn.BorderThickness = 2;
            this.BypassBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.BypassBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.BypassBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.BypassBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.BypassBtn.FillColor = System.Drawing.Color.Transparent;
            this.BypassBtn.Font = new System.Drawing.Font("Lucida Console", 7.8F, System.Drawing.FontStyle.Bold);
            this.BypassBtn.ForeColor = System.Drawing.Color.Black;
            this.BypassBtn.Location = new System.Drawing.Point(91, 240);
            this.BypassBtn.Name = "BypassBtn";
            this.BypassBtn.Size = new System.Drawing.Size(221, 59);
            this.BypassBtn.TabIndex = 3;
            this.BypassBtn.Text = "ACTIVATE";
            this.BypassBtn.Click += new System.EventHandler(this.BypassBtn_Click);
            // 
            // Sta
            // 
            this.Sta.BackColor = System.Drawing.Color.Transparent;
            this.Sta.Font = new System.Drawing.Font("Palatino Linotype", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Sta.ForeColor = System.Drawing.Color.Black;
            this.Sta.Location = new System.Drawing.Point(12, 464);
            this.Sta.Name = "Sta";
            this.Sta.Size = new System.Drawing.Size(40, 20);
            this.Sta.TabIndex = 5;
            this.Sta.Text = "Status";
            this.Sta.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // closeBp
            // 
            this.closeBp.BorderRadius = 5;
            this.closeBp.BorderThickness = 2;
            this.closeBp.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.closeBp.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.closeBp.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.closeBp.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.closeBp.FillColor = System.Drawing.Color.Transparent;
            this.closeBp.Font = new System.Drawing.Font("Lucida Console", 7.8F, System.Drawing.FontStyle.Bold);
            this.closeBp.ForeColor = System.Drawing.Color.Black;
            this.closeBp.Location = new System.Drawing.Point(320, 448);
            this.closeBp.Name = "closeBp";
            this.closeBp.Size = new System.Drawing.Size(88, 36);
            this.closeBp.TabIndex = 6;
            this.closeBp.Text = "close";
            this.closeBp.Click += new System.EventHandler(this.closeBp_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(420, 496);
            this.ControlBox = false;
            this.Controls.Add(this.closeBp);
            this.Controls.Add(this.Sta);
            this.Controls.Add(this.BypassBtn);
            this.Controls.Add(this.guna2HtmlLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2AnimateWindow guna2AnimateWindow1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2Button BypassBtn;
        private Guna.UI2.WinForms.Guna2HtmlLabel Sta;
        private Guna.UI2.WinForms.Guna2Button closeBp;
    }
}