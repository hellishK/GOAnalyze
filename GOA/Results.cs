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
    public partial class Results : Form
    {
        public Results()
        {
            InitializeComponent();
            BackColor = Color.FromArgb(30, 111, 166);
            dataGridView.BackgroundColor = Color.FromArgb(30, 111, 166);
            this.MouseDown += Form_MouseDown;
            this.MouseMove += Form_MouseMove;
            this.MouseUp += Form_MouseUp;
        }

        private bool isDragging = false;
        private Point lastCursor;
        private Point lastForm;
        int pos_x, pos_y;

        private void Results_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

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

        private void RollButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void SizeButton_Click(object sender, EventArgs e)
        {
            if (Size.Width < 1000)
            {
                pos_x = Location.X;
                pos_y = Location.Y;
                Width = SystemInformation.VirtualScreen.Width;
                Height = SystemInformation.VirtualScreen.Height;
                Location = new Point(0, 0);
                SizeButton.Image = Properties.Resources.size_min;
            }
            else
            {

                Size = new Size(869, 207);
                Location = new Point(pos_x, pos_y);
                SizeButton.Image = Properties.Resources.size_max;
            }
        }
    }
}
