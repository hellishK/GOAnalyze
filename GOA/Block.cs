using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;


namespace GOA
{
    public partial class Block : UserControl
    {
        List<Block> childs = new List<Block>();
        List<Block> branch = new List<Block>();
        public int lvl = 0;
        public int number = 0;
        public int margin = 30;
        public int newloc = 0;
        public Block first;
        public Block myParent;
        private string typeOfBlock;
        ContextMenu add_menu = new ContextMenu();
        ContextMenu node_menu = new ContextMenu();
        ContextMenu node_menu_add = new ContextMenu();
        ContextMenu node_menu_del = new ContextMenu();
        Tree myTreeView = new Tree();

        public string TypeOfBlock
        {
            get { return typeOfBlock; }  
            set { typeOfBlock = value;}
        }

        public List<Block> Branch
        {
            get { return branch; }
            set { branch = value; }
        }

        public List<Block> Childs
        {
            get {return childs; }
            set { childs = value; }
        }

        public Tree MyTreeView
        {
            get{return myTreeView;}
            set{myTreeView = value;}
        }

        public Block()
        {
            InitializeComponent();
            CreateAddMenu();
        }

        //public Block(Block other)
        //{
        //    InitializeComponent();
        //    CreateAddMenu();
        //    this.BlockData.Text = other.BlockData.Text;
        //    copyNodes(other.myTreeView, this.myTreeView);
        //}

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
            ((Block)(((Button)sender).Parent.Parent)).MyTreeView.treeView.Nodes[0].Text = ((Block)(((Button)sender).Parent.Parent)).BlockData.Text;
            ((Block)(((Button)sender).Parent.Parent)).MyTreeView.Text = ((Block)(((Button)sender).Parent.Parent)).BlockData.Text;
            ((Block)(((Button)sender).Parent.Parent)).MyTreeView.treeView.ExpandAll();
            ((Block)(((Button)sender).Parent.Parent)).MyTreeView.Show();

        }

        public void copyNodes(Tree source, Tree target)
        {
            TreeNode newTn = new TreeNode();

            MenuItem add_node1 = new MenuItem { Name = "Добавить ниже", Text = "Добавить ниже" };
            MenuItem del_node1 = new MenuItem { Name = "Удалить", Text = "Удалить" };
            add_node1.Click += target.AddNode_Click;
            del_node1.Click += (s, ee) => { source.treeView.SelectedNode.Remove(); };

            MenuItem add_node2 = new MenuItem { Name = "Добавить ниже", Text = "Добавить ниже" };
            MenuItem del_node2 = new MenuItem { Name = "Удалить", Text = "Удалить" };
            add_node2.Click += target.AddNode_Click;
            del_node2.Click += (s, ee) => { source.treeView.SelectedNode.Remove(); };

            node_menu.MenuItems.AddRange(new[] { add_node1, del_node1 });
            node_menu_add.MenuItems.Add(add_node2);
            node_menu_del.MenuItems.Add(del_node2);

            target.treeView.Nodes.Clear();

            foreach (TreeNode tn in source.treeView.Nodes)
            {
                copyChildNodes(tn, newTn);
                target.treeView.Nodes.Add(newTn);
            }
            target.treeView.NodeMouseClick += MyTreeView.treeView_NodeMouseClick;
        }

        private void copyChildNodes(TreeNode tn, TreeNode newTn)
        {

            newTn.Text = tn.Text;

            if (tn.Level == 0)
                newTn.ContextMenu = node_menu_add;

            else if (tn.Level == 1)
                newTn.ContextMenu = node_menu;

            else if (tn.Level == 2)
                newTn.ContextMenu = node_menu;

            else if (tn.Level == 3)
                newTn.ContextMenu = node_menu_del;

            foreach (TreeNode tn_child in tn.Nodes)
            {
                newTn.Text = tn.Text;
                TreeNode newTn_child = new TreeNode();
                newTn_child.Text = tn_child.Text;
                copyChildNodes(tn_child, newTn_child); 
                newTn.Nodes.Add(newTn_child);
            }
        }

        public void AddPosition_Click(object sender, EventArgs e)
        {
            Add(sender, 0, null, "position");
        }
        public void AddDepartment_Click(object sender, EventArgs e)
        {
            Add(sender, 0, null, "department");
        }

        public void AddChild_Click(object sender, EventArgs e)
        {
            add_menu.Show((Button)sender, new Point(0, ((Button)sender).Height));
        }

        public void AddType(object obj)
        {
            //MessageBox.Show(((Block)((((obj as MenuItem).GetContextMenu() as ContextMenu).SourceControl).Parent)).ToString());
            if ((obj as Block).TypeOfBlock == "position")
            { 
                ((Block)obj).typePicture.BackgroundImage = new Bitmap(Properties.Resources.position_ico);
                ((Block)obj).Extend.Visible = false;
            }
            else if ((obj as Block).TypeOfBlock == "department")
            {
                ((Block)obj).typePicture.BackgroundImage = new Bitmap(Properties.Resources.department_ico);
                ((Block)obj).Extend.Visible = true;
                ((Block)obj).Extend.Click += Extend_Click;
            }
            //MessageBox.Show((obj as Block).TypeOfBlock);
        }

        public void Add(object par, int k, Block copy, string type)
        {
            Block newBlock = new Block();       
            Block papa;
            Block deda;
            Block pra;
            first = Form.ActiveForm.Controls.Find("block1", true).FirstOrDefault() as Block;
            Block left = first;
            Block right = first;

            newBlock.MyTreeView.Hide();
            newBlock.TypeOfBlock = type;
            AddType(newBlock);
            

            if (k == 0)
            {
                papa = ((Block)((par as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent.Parent);
                //MessageBox.Show(((Block)((obj as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent.Parent).ToString());
                //papa = (obj as Control).Parent as Block;

            }
            else
            {
                papa = par as Block;
                copyNodes(copy.MyTreeView, newBlock.MyTreeView);
                newBlock.BlockData.Text = copy.BlockData.Text;
            }

            if(papa.myParent!=null)
                deda = papa.myParent;
  
            newBlock.myParent = papa;
            newBlock.lvl = papa.lvl + 1;
            newBlock.ContextMenu = Form.ActiveForm.Controls.Find("block1", true).FirstOrDefault().ContextMenu;
            newBlock.BlockData.ContextMenu = Form.ActiveForm.Controls.Find("block1", true).FirstOrDefault().ContextMenu;
            newBlock.number = papa.childs.Count;
            //int index = 1;

            //if (papa != null && papa.myParent != null)
            //{
            //    foreach (Block ch in papa.myParent.childs)
            //    {
            //        if (papa.Name == ch.Name) break;
            //        index++;
            //    }
            //}
            newBlock.Name = "block" + newBlock.lvl + "-" + papa.number + "(" + (papa.childs.Count + 1) + ")";
            //index = 0;
           newBlock.BlockData.Text = newBlock.Name;
            // первый блок распологается непосредственно под родительским
            if (papa.childs.Count == 0)
            {
                newBlock.Location = new Point(papa.Location.X, papa.Location.Y + papa.Height + margin);
                papa.childs.Add(newBlock);

                pra = newBlock;
                do
                {
                    pra.Branch.Add(newBlock);
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
                    pra.Branch.Add(newBlock);
                    pra = pra.myParent;
                }
                while (pra != null);


                //если добавление ОТ 1 уровня (на второй )
                if (papa.lvl == 1)
                {
                    ////элементы размещаются по середине новое положение = середина_формы/2 - (колво_эл_2ур*ширина + отступ*(колво_эл_2ур - 2 (по одному отступу у первого и последнего)))/2
                    //newloc = Form.ActiveForm.Size.Width / 2 - ((papa.Childs.Count) * (papa.Width) + margin * (papa.Childs.Count - 1)) / 2;
                    //foreach (Block ch in papa.Childs)
                    //{
                    //    ch.Location = new Point(newloc, ch.Location.Y);
                    //    newloc += papa.Width + margin;
                    //}

                    right = papa.childs[papa.childs.Count - 2];
                    while (right.childs.Count > 0)
                    {
                        right = right.childs[right.childs.Count - 1];
                    }

                    newBlock.Location = new Point(right.Location.X + right.Width + margin, papa.childs[papa.childs.Count - 2].Location.Y);
                }

                //на последующие уровни - необходимо перемещать всех родителей и их родителей, находящихся справа...
                else
                {
                    //находим индекс родителя
                    pra = papa;
                    while (pra.myParent != null)
                    {
                        pra.Location = new Point(pra.childs[0].Location.X + (pra.childs[pra.childs.Count - 1].Location.X - pra.childs[0].Location.X) / 2, pra.Location.Y);
                        //foreach (Block ch in pra.myParent.childs)
                        //{
                        //    index++;
                        //    if (pra.Name == ch.Name)
                        //    {
                        //        break;
                        //    }
                        //}

                        //берем всех правых братьев родителя
                        for (int idx = pra.number+1; idx < pra.myParent.childs.Count; idx++)
                        {
                            //и двигаем ВСЮ ИХ ВЕТКУ вправа

                            foreach (Block ch in pra.myParent.childs[idx].Branch)
                            {
                                ch.Location = new Point(ch.Location.X + pra.Width + margin, ch.Location.Y);
                            }
                        }

                        //index = 0;
                        pra = pra.myParent;
                    }

               }
            }

            //вычисляем координаты для блока1 (середину)
            first.Location = new Point(first.childs[0].Location.X + (first.childs[first.childs.Count-1].Location.X - first.childs[0].Location.X) / 2, first.Location.Y);      
            Form.ActiveForm.Controls.Find("panel1", false).FirstOrDefault().Controls.Add(newBlock);

            drawLines();



        }


        //отрисовка соединительных линий
        public void drawLines()
        {
            Form.ActiveForm.Controls.Find("panel1", false).FirstOrDefault().Refresh();
            Pen pen = new Pen(Color.Black, 3);
            Graphics formGraphics = Form.ActiveForm.Controls.Find("panel1", false).FirstOrDefault().CreateGraphics();
            formGraphics.SmoothingMode = SmoothingMode.HighQuality;
            int x1, y1, x2, y2, x3, y3, x4, y4;

            foreach (Block b in first.Branch)
            {
                if (b.myParent != null)
                {
                    x1 = 15 + b.myParent.Location.X + b.Width / 2;
                    y1 = b.myParent.Location.Y - 2 + b.Height;
                    x4 = 15 + b.Location.X + b.Width / 2;
                    y4 = b.Location.Y + 2;

                    if (b.Location.X == b.myParent.Location.X)
                    {
                        formGraphics.DrawLine(pen, x1, y1, x4, y4);
                    }
                    else
                    {
                        x2 = x1;
                        y2 = b.myParent.Location.Y - 2 + b.Height + 10;
                        x3 = 15 + b.Location.X + b.Width / 2;
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

