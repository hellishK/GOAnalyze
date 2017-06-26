namespace GOA
{
    partial class Organizations
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Organizations));
            this.OrgList = new System.Windows.Forms.ComboBox();
            this.OK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.CrossButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OrgList
            // 
            this.OrgList.FormattingEnabled = true;
            this.OrgList.Location = new System.Drawing.Point(14, 50);
            this.OrgList.Name = "OrgList";
            this.OrgList.Size = new System.Drawing.Size(355, 21);
            this.OrgList.TabIndex = 0;
            // 
            // OK
            // 
            this.OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OK.FlatAppearance.BorderSize = 0;
            this.OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OK.Image = global::GOA.Properties.Resources.open_button;
            this.OK.Location = new System.Drawing.Point(382, 50);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 23);
            this.OK.TabIndex = 1;
            this.OK.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(18, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Выберите организацию:";
            // 
            // CrossButton
            // 
            this.CrossButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CrossButton.BackColor = System.Drawing.Color.Transparent;
            this.CrossButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CrossButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CrossButton.FlatAppearance.BorderSize = 0;
            this.CrossButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CrossButton.Image = ((System.Drawing.Image)(resources.GetObject("CrossButton.Image")));
            this.CrossButton.Location = new System.Drawing.Point(453, 1);
            this.CrossButton.Name = "CrossButton";
            this.CrossButton.Size = new System.Drawing.Size(20, 20);
            this.CrossButton.TabIndex = 14;
            this.CrossButton.UseVisualStyleBackColor = false;
            this.CrossButton.Click += new System.EventHandler(this.CrossButton_Click);
            // 
            // Organizations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 107);
            this.Controls.Add(this.CrossButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.OrgList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Organizations";
            this.Text = "Открытие оргструктур";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox OrgList;
        private System.Windows.Forms.Button CrossButton;
    }
}