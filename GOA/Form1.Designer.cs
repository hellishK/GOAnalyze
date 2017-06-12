namespace GOA
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.org_name = new System.Windows.Forms.TextBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.addPage = new System.Windows.Forms.TabPage();
            this.block1_1 = new GOA.Block();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // org_name
            // 
            this.org_name.Location = new System.Drawing.Point(254, 12);
            this.org_name.Multiline = true;
            this.org_name.Name = "org_name";
            this.org_name.Size = new System.Drawing.Size(631, 36);
            this.org_name.TabIndex = 1;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.addPage);
            this.tabControl.Location = new System.Drawing.Point(12, 64);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1129, 554);
            this.tabControl.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.block1_1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1121, 528);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Оргструктура1";
            // 
            // addPage
            // 
            this.addPage.BackColor = System.Drawing.SystemColors.Control;
            this.addPage.Location = new System.Drawing.Point(4, 22);
            this.addPage.Name = "addPage";
            this.addPage.Size = new System.Drawing.Size(1121, 528);
            this.addPage.TabIndex = 1;
            this.addPage.Text = "+";
            // 
            // block1_1
            // 
            this.block1_1.BackColor = System.Drawing.SystemColors.Control;
            this.block1_1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.block1_1.Location = new System.Drawing.Point(447, 20);
            this.block1_1.Name = "block1_1";
            this.block1_1.Size = new System.Drawing.Size(190, 74);
            this.block1_1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1154, 631);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.org_name);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox org_name;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage addPage;
        private Block block1_1;
    }
}

