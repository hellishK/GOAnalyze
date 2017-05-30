using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GOA
{
    public partial class Block : UserControl
    {
        List<Block> childs = new List<Block>();
        List<Block> branch = new List<Block>();
        public int lvl = 0;
        public int margin = 20;
        public int newloc = 0;
        public Block myParent;
        public string typeOfBlock="";
        ContextMenu add_menu = new ContextMenu();
        public Tree myTreeView = new Tree();
        

        public Block()
        {
            InitializeComponent();
            CreateAddMenu();
        }

        public void CreateAddMenu()
        {
            MenuItem position = new MenuItem { Name = "Должность", Text = "Должность" };
            MenuItem department = new MenuItem { Name = "Подразделение", Text = "Подразделение" };
            position.Click += AddPosition_Click;
            department.Click += AddDepartment_Click;
            add_menu.MenuItems.AddRange(new[] { position, department });
            
        }

        public void Extend_Click(object sender, EventArgs e)
        {
            ((Block)(((Button)sender).Parent.Parent)).myTreeView.treeView.Nodes[0].Text = ((Block)(((Button)sender).Parent.Parent)).BlockData.Text;
            ((Block)(((Button)sender).Parent.Parent)).myTreeView.Text = ((Block)(((Button)sender).Parent.Parent)).BlockData.Text;
            ((Block)(((Button)sender).Parent.Parent)).myTreeView.Show();

        }

        public void AddPosition_Click(object sender, EventArgs e)
        {
            Add(sender, "", 0, "position");
        }
        public void AddDepartment_Click(object sender, EventArgs e)
        {
            Add(sender, "", 0, "department");
        }

        public void AddChild_Click(object sender, EventArgs e)
        {
            add_menu.Show((Button)sender, new Point(0, ((Button)sender).Height));
        }

        public void AddType(object obj, string type)
        {
            //MessageBox.Show(((Block)((((obj as MenuItem).GetContextMenu() as ContextMenu).SourceControl).Parent)).ToString());
            if (type == "position")
            {
                ((Block)obj).typePicture.BackgroundImage = global::GOA.Properties.Resources.position_ico;
                ((Block)obj).Extend.Visible = false;
            }
            else if (type == "department")
            {
                ((Block)obj).typePicture.BackgroundImage = global::GOA.Properties.Resources.department_ico;
                ((Block)obj).Extend.Visible = true;
                ((Block)obj).Extend.Click += Extend_Click;
            }
        }

        public void Add(object obj, string s, int k, string type)
        {
            Block newBlock = new Block();
            Block first = Form.ActiveForm.Controls.Find("block1", true).FirstOrDefault() as Block;
            Block papa;
            Block deda;
            Block pra;
            Block left = first;
            Block right = first;

            myTreeView.Hide();

            typeOfBlock = type;

            if (k == 0)
            {
                papa = ((Block)((obj as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent.Parent);
                //MessageBox.Show(((Block)((obj as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent.Parent).ToString());
                //papa = (obj as Control).Parent as Block;
            }
            else
                papa = obj as Block;

            if(papa.myParent!=null)
                deda = papa.myParent;
  
            newBlock.myParent = papa;
            newBlock.lvl = papa.lvl + 1;
            newBlock.ContextMenu = Form.ActiveForm.Controls.Find("block1", true).FirstOrDefault().ContextMenu;
            newBlock.BlockData.ContextMenu = Form.ActiveForm.Controls.Find("block1", true).FirstOrDefault().ContextMenu;
            int index = 1;

            if (papa != null)
            {
                foreach (Block ch in papa.childs)
                {
                    index++;
                    if (newBlock.Name == ch.Name) break;
                }
            }
            newBlock.Name = "block" + newBlock.lvl + "-" + index + "(" + (papa.childs.Count + 1) + ")";
            index = 0;
            // первый блок распологается непосредственно под родительским
            if (papa.childs.Count == 0)
            {
                newBlock.Location = new Point(papa.Location.X, papa.Location.Y + papa.Height + margin);
                papa.childs.Add(newBlock);

                pra = newBlock;
                do
                {
                    pra.branch.Add(newBlock);
                    pra = pra.myParent;
                    //MessageBox.Show("Добавили в ветку " + pra.Name);
                }
                while (pra != null);
            }

            //все последующие - добавляются справа за последним элементом данного уровня
            else
            {
                newBlock.Location = new Point(papa.childs[papa.childs.Count - 1].Location.X + papa.Width + margin, papa.childs[papa.childs.Count - 1].Location.Y);
                papa.childs.Add(newBlock);

                pra = newBlock;
                do
                {
                    pra.branch.Add(newBlock);
                    pra = pra.myParent;
                }
                while (pra != null);

                //если добавление ОТ 1 уровня (на второй )
                if (papa.lvl == 1)
                {
                    //элементы размещаются по середине новое положение = середина_формы/2 - (колво_эл_2ур*ширина + отступ*(колво_эл_2ур - 2 (по одному отступу у первого и последнего)))/2
                    newloc = Form.ActiveForm.Size.Width / 2 - ((papa.childs.Count) * (papa.Width) + margin * (papa.childs.Count - 2)) / 2;
                    foreach (Block ch in papa.childs)
                    {
                        ch.Location = new Point(newloc, ch.Location.Y);
                        newloc += papa.Width + margin;
                    }
                }

                //на последующие уровни - необходимо перемещать всех родителей и их родителей, находящихся справа...
                else
                {
                    //находим индекс родителя
                    pra = papa;
                    while (pra.myParent != null)
                    {
                        pra.Location = new Point(pra.childs[0].Location.X + (pra.childs[pra.childs.Count - 1].Location.X - pra.childs[0].Location.X) / 2, pra.Location.Y);
                        foreach (Block ch in pra.myParent.childs)
                        {
                            index++;
                            if (pra.Name == ch.Name)
                            {
                                break;
                            }
                        }

                        //берем всех правых братьев родителя
                        for (int idx = index; idx < pra.myParent.childs.Count; idx++)
                        {
                            //и двигаем ВСЮ ИХ ВЕТКУ вправа

                            foreach (Block ch in pra.myParent.childs[idx].branch)
                            {
                                ch.Location = new Point(ch.Location.X + pra.Width + margin, ch.Location.Y);
                            }
                        }

                        index = 0;
                        pra = pra.myParent;
                    }

                }
            }

            //находим сааамого нижнего левого ребенка
            while (left.childs.Count > 0)
            {
                left = left.childs[0];
            }

            //и самого нижнего правого ребенка
            while (right.childs.Count > 0)
            {
                right = right.childs[right.childs.Count - 1];
            }

            //вычисляем координаты для блока1 (середину)
            first.Location = new Point(left.Location.X + (right.Location.X - left.Location.X) / 2, first.Location.Y);
            newBlock.BlockData.Text = newBlock.Name;
            newBlock.BlockData.Text = s;
            AddType(newBlock, type);
            Form.ActiveForm.Controls.Find("panel1", false).FirstOrDefault().Controls.Add(newBlock);


            //отрисовка соединительных линий
            Form.ActiveForm.Controls.Find("panel1", false).FirstOrDefault().Refresh();
            Pen pen = new Pen(Color.Black, 3);
            Graphics formGraphics = Form.ActiveForm.Controls.Find("panel1", false).FirstOrDefault().CreateGraphics();
            formGraphics.SmoothingMode = SmoothingMode.HighQuality;
            int x1, y1, x2, y2, x3, y3, x4, y4;

            foreach (Block b in first.branch)
            {
                //MessageBox.Show(b.Name);
                if (b.myParent != null)
                {
                    x1 = b.myParent.Location.X + b.Width / 2;
                    y1 = b.myParent.Location.Y + b.Height;
                    x4 = b.Location.X + b.Width / 2;
                    y4 = b.Location.Y;

                    if (b.Location.X == b.myParent.Location.X)
                    {
                        formGraphics.DrawLine(pen, x1, y1, x4, y4);
                    }
                    else
                    {
                        x2 = x1;
                        y2 = b.myParent.Location.Y + b.Height + 10;
                        x3 = b.Location.X + b.Width / 2;
                        y3 = y2;
                        formGraphics.DrawLine(pen, x1, y1, x2, y2);
                        formGraphics.DrawLine(pen, x2, y2, x3, y3);
                        formGraphics.DrawLine(pen, x3, y3, x4, y4);
                    }
                }

            }
            pen.Dispose();
            formGraphics.Dispose();
            
        }
    }
}
