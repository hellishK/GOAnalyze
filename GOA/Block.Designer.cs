namespace GOA
{
    partial class Block
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.BlockData = new System.Windows.Forms.TextBox();
            this.AddChild = new System.Windows.Forms.Button();
            this.Extend = new System.Windows.Forms.Button();
            this.cross = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BlockData
            // 
            this.BlockData.BackColor = System.Drawing.Color.White;
            this.BlockData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.BlockData.Location = new System.Drawing.Point(0, 0);
            this.BlockData.Multiline = true;
            this.BlockData.Name = "BlockData";
            this.BlockData.Size = new System.Drawing.Size(164, 50);
            this.BlockData.TabIndex = 0;
            // 
            // AddChild
            // 
            this.AddChild.FlatAppearance.BorderSize = 0;
            this.AddChild.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AddChild.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddChild.Location = new System.Drawing.Point(141, 50);
            this.AddChild.Name = "AddChild";
            this.AddChild.Size = new System.Drawing.Size(19, 18);
            this.AddChild.TabIndex = 1;
            this.AddChild.Text = "✚";
            this.AddChild.UseVisualStyleBackColor = true;
            this.AddChild.Click += new System.EventHandler(this.AddChild_Click);
            // 
            // Extend
            // 
            this.Extend.FlatAppearance.BorderSize = 0;
            this.Extend.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Extend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Extend.Location = new System.Drawing.Point(120, 50);
            this.Extend.Name = "Extend";
            this.Extend.Size = new System.Drawing.Size(19, 18);
            this.Extend.TabIndex = 2;
            this.Extend.Text = "▼";
            this.Extend.UseVisualStyleBackColor = true;
            // 
            // cross
            // 
            this.cross.BackColor = System.Drawing.Color.Transparent;
            this.cross.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cross.FlatAppearance.BorderSize = 0;
            this.cross.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cross.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cross.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cross.Location = new System.Drawing.Point(146, -2);
            this.cross.Name = "cross";
            this.cross.Size = new System.Drawing.Size(18, 18);
            this.cross.TabIndex = 3;
            this.cross.Text = "×";
            this.cross.UseVisualStyleBackColor = false;
            // 
            // Block
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cross);
            this.Controls.Add(this.Extend);
            this.Controls.Add(this.AddChild);
            this.Controls.Add(this.BlockData);
            this.Name = "Block";
            this.Size = new System.Drawing.Size(162, 70);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox BlockData;
        public System.Windows.Forms.Button AddChild;
        public System.Windows.Forms.Button Extend;
        public System.Windows.Forms.Button cross;
    }
}
