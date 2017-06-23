namespace GOA
{
    partial class Results
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Results));
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RollButton = new System.Windows.Forms.Button();
            this.SizeButton = new System.Windows.Forms.Button();
            this.CrossButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dataGridView.Location = new System.Drawing.Point(26, 33);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(816, 162);
            this.dataGridView.TabIndex = 16;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            // 
            // RollButton
            // 
            this.RollButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RollButton.BackColor = System.Drawing.Color.Transparent;
            this.RollButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.RollButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RollButton.FlatAppearance.BorderSize = 0;
            this.RollButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RollButton.Image = global::GOA.Properties.Resources.roll;
            this.RollButton.Location = new System.Drawing.Point(800, 1);
            this.RollButton.Name = "RollButton";
            this.RollButton.Size = new System.Drawing.Size(20, 20);
            this.RollButton.TabIndex = 19;
            this.RollButton.UseVisualStyleBackColor = false;
            this.RollButton.Click += new System.EventHandler(this.RollButton_Click);
            // 
            // SizeButton
            // 
            this.SizeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SizeButton.BackColor = System.Drawing.Color.Transparent;
            this.SizeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.SizeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SizeButton.FlatAppearance.BorderSize = 0;
            this.SizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SizeButton.Image = global::GOA.Properties.Resources.size_max;
            this.SizeButton.Location = new System.Drawing.Point(822, 1);
            this.SizeButton.Name = "SizeButton";
            this.SizeButton.Size = new System.Drawing.Size(20, 20);
            this.SizeButton.TabIndex = 18;
            this.SizeButton.UseVisualStyleBackColor = false;
            this.SizeButton.Click += new System.EventHandler(this.SizeButton_Click);
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
            this.CrossButton.Location = new System.Drawing.Point(846, 1);
            this.CrossButton.Name = "CrossButton";
            this.CrossButton.Size = new System.Drawing.Size(20, 20);
            this.CrossButton.TabIndex = 17;
            this.CrossButton.UseVisualStyleBackColor = false;
            this.CrossButton.Click += new System.EventHandler(this.CrossButton_Click);
            // 
            // Results
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(869, 207);
            this.Controls.Add(this.RollButton);
            this.Controls.Add(this.SizeButton);
            this.Controls.Add(this.CrossButton);
            this.Controls.Add(this.dataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Results";
            this.Text = "Результаты анализа";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Results_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        public System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button RollButton;
        private System.Windows.Forms.Button SizeButton;
        private System.Windows.Forms.Button CrossButton;
    }
}