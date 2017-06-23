using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GOA
{
    public partial class Organizations : Form
    {
        public Organizations()
        {
            InitializeComponent();
            BackColor = Color.FromArgb(25, 107, 164);
            this.MouseDown += Form_MouseDown;
            this.MouseMove += Form_MouseMove;
            this.MouseUp += Form_MouseUp;
        }

        private bool isDragging = false;
        private Point lastCursor;
        private Point lastForm;
        int pos_x, pos_y;

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;

            lastCursor = Cursor.Position;
            lastForm = this.Location;
        }

        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                this.Location = Point.Add(lastForm, new Size(Point.Subtract(Cursor.Position, new Size(lastCursor))));
            }
        }

        private void Form_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void CrossButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
