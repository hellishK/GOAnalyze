namespace GOA
{
    partial class Tree
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
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Подразделение");
            this.treeView = new System.Windows.Forms.TreeView();
            this.RollButton = new System.Windows.Forms.Button();
            this.CrossButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.BackColor = System.Drawing.SystemColors.Control;
            this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.treeView.ForeColor = System.Drawing.Color.White;
            this.treeView.LabelEdit = true;
            this.treeView.Location = new System.Drawing.Point(71, 38);
            this.treeView.Name = "treeView";
            treeNode7.Name = "Подразделение";
            treeNode7.Text = "Подразделение";
            this.treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode7});
            this.treeView.Size = new System.Drawing.Size(430, 454);
            this.treeView.TabIndex = 0;
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
            this.RollButton.Location = new System.Drawing.Point(510, 2);
            this.RollButton.Name = "RollButton";
            this.RollButton.Size = new System.Drawing.Size(20, 20);
            this.RollButton.TabIndex = 16;
            this.RollButton.UseVisualStyleBackColor = false;
            this.RollButton.Click += new System.EventHandler(this.RollButton_Click);
            // 
            // CrossButton
            // 
            this.CrossButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CrossButton.BackColor = System.Drawing.Color.Transparent;
            this.CrossButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CrossButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CrossButton.FlatAppearance.BorderSize = 0;
            this.CrossButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CrossButton.Image = global::GOA.Properties.Resources.cross;
            this.CrossButton.Location = new System.Drawing.Point(534, 2);
            this.CrossButton.Name = "CrossButton";
            this.CrossButton.Size = new System.Drawing.Size(20, 20);
            this.CrossButton.TabIndex = 14;
            this.CrossButton.UseVisualStyleBackColor = false;
            this.CrossButton.Click += new System.EventHandler(this.CrossButton_Click);
            // 
            // Tree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 529);
            this.Controls.Add(this.RollButton);
            this.Controls.Add(this.CrossButton);
            this.Controls.Add(this.treeView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Tree";
            this.Text = "Tree";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Tree_FormClosing);
            this.Load += new System.EventHandler(this.Tree_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.Button RollButton;
        private System.Windows.Forms.Button CrossButton;
    }
}