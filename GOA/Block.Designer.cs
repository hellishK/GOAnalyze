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
            this.AddChild = new System.Windows.Forms.Button();
            this.Extend = new System.Windows.Forms.Button();
            this.BlockData = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.typePicture = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.typePicture)).BeginInit();
            this.SuspendLayout();
            // 
            // AddChild
            // 
            this.AddChild.FlatAppearance.BorderSize = 0;
            this.AddChild.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AddChild.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddChild.Location = new System.Drawing.Point(144, 50);
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
            this.Extend.Location = new System.Drawing.Point(125, 50);
            this.Extend.Name = "Extend";
            this.Extend.Size = new System.Drawing.Size(19, 18);
            this.Extend.TabIndex = 2;
            this.Extend.Text = "▼";
            this.Extend.UseVisualStyleBackColor = true;
            // 
            // BlockData
            // 
            this.BlockData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.BlockData.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BlockData.Location = new System.Drawing.Point(27, 5);
            this.BlockData.MaxLength = 66;
            this.BlockData.Multiline = true;
            this.BlockData.Name = "BlockData";
            this.BlockData.Size = new System.Drawing.Size(157, 46);
            this.BlockData.TabIndex = 5;
            this.BlockData.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.AddChild);
            this.panel1.Controls.Add(this.Extend);
            this.panel1.Location = new System.Drawing.Point(22, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(166, 70);
            this.panel1.TabIndex = 6;
            // 
            // typePicture
            // 
            this.typePicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.typePicture.Location = new System.Drawing.Point(0, 5);
            this.typePicture.Name = "typePicture";
            this.typePicture.Size = new System.Drawing.Size(21, 21);
            this.typePicture.TabIndex = 4;
            this.typePicture.TabStop = false;
            // 
            // Block
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.typePicture);
            this.Controls.Add(this.BlockData);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Name = "Block";
            this.Size = new System.Drawing.Size(190, 74);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.typePicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Button AddChild;
        public System.Windows.Forms.Button Extend;
        private System.Windows.Forms.PictureBox typePicture;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.TextBox BlockData;
    }
}
