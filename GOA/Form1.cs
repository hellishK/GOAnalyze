using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Block copyBlock;
        Block currentBlock;
        ContextMenu menu = new ContextMenu();
        Results result = new Results();
        MenuItem mi_copy, mi_copy_block, mi_copy_branch, mi_paste, mi_paste_block_down, mi_paste_branch_down, mi_paste_block_excange, mi_paste_branch_excange, mi_delete, typeItem;
        bool isCopyBlock = false, isCopyBranch = false;
         int id_org, id_str, id_par, id_block, id_node_par;

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateMenu();
            block1_1.lvl = 1;
            block1_1.first = block1_1;
            block1_1.Branch.Add(block1_1);
            block1_1.ContextMenu = menu;
            block1_1.BlockData.ContextMenu = menu;
            block1_1.Extend.Visible = false;
            tabPage1.AutoScroll = true;
            tabPage1.HorizontalScroll.Visible = true;
            //block1_1.Location = new Point(1000, 15);
            //tabPage1.Size = new Size(2000, 1500);
            tabControl.Selected += addPage_Selected;
            tabControl.TabPages[0].Paint += Page_DrawLines;
            tabControl.TabPages[1].Paint += Page_DrawLines;

            result.MaximizeBox = false;
            result.dataGridView.Columns[0].HeaderText = "Показатель";
            result.dataGridView.Rows.Add(4);
            result.dataGridView.Rows[0].Cells[0].Value = "Информационная оценка";
            result.dataGridView.Rows[1].Cells[0].Value = "Число состояний системы";
            result.dataGridView.Rows[2].Cells[0].Value = "Коэффициент централизации";
            result.dataGridView.Rows[3].Cells[0].Value = "Коэффициент децентрализации";
        }

        public void addPage_Selected(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab.Text == "+")
            {
                Block nb = new Block();
                nb.lvl = 1;
                nb.ContextMenu = menu;
                nb.BlockData.ContextMenu = menu;
                nb.Extend.Visible = false;
                nb.Name = "block" + tabControl.TabPages.Count + "_1";
                nb.first = nb;
                nb.Branch.Add(nb);
                nb.Location = new Point(tabControl.SelectedTab.Width / 2 - nb.Width / 2, 15);
                TabPage new_tp = new TabPage("+");
                tabControl.TabPages[tabControl.TabPages.Count - 1].Text = "Оргструктура" + tabControl.TabPages.Count;
                tabControl.TabPages[tabControl.TabPages.Count - 1].Name = "tabPage" + tabControl.TabPages.Count;
                new_tp.Name = "addPage";
                new_tp.Text = "+";
                new_tp.Paint += Page_DrawLines;
                new_tp.AutoScroll = true;
                new_tp.BackColor = SystemColors.Control;
                tabControl.TabPages.Add(new_tp);
                tabControl.SelectedTab.Controls.Add(nb);
                nb.drawLines();
            }
            else
                ((Block)((TabControl)sender).SelectedTab.Controls.Find("block" + (tabControl.SelectedIndex + 1) + "_1", false).FirstOrDefault()).drawLines();
        }

        public void Page_DrawLines(object sender, EventArgs e)
        {
            try
            {
                ((Block)((TabPage)sender).Controls.Find("block" + (tabControl.SelectedIndex + 1) + "_1", false).FirstOrDefault()).drawLines();
            }
            catch { }
        }

        public void CreateMenu()
        {
            mi_copy = new MenuItem { Name = "Копировать", Text = "Копировать" };
            mi_copy_block = new MenuItem { Name = "Блок", Text = "Блок" };
            mi_copy_branch = new MenuItem { Name = "Ветвь", Text = "Ветвь" };
            mi_copy_block.Click += CopyMenuItem_Click;
            mi_copy_branch.Click += CopyMenuItem_Click;

            mi_paste = new MenuItem { Name = "Вставить", Text = "Вставить" };
            mi_paste_block_down = new MenuItem { Name = "Блок ниже", Text = "Блок ниже" };
            mi_paste_block_down.Click += menuPasteBlockDown_Click;
            mi_paste_branch_down = new MenuItem { Name = "Ветвь ниже", Text = "Ветвь ниже" };
            mi_paste_branch_down.Click += menuPasteBranchDown_Click;
            mi_paste_block_excange = new MenuItem { Name = "Блок с заменой", Text = "Блок с заменой текущего" };
            mi_paste_block_excange.Click += menuPasteBlockExcange_Click;
            mi_paste_branch_excange = new MenuItem { Name = "Ветвь с заменой", Text = "Ветвь с заменой текущей" };
            mi_paste_branch_excange.Click += menuPasteBranchExcange_Click;

            mi_delete = new MenuItem { Name = "Удалить", Text = "Удалить" };
            mi_delete.Click += menuDelete_Click;

            typeItem = new MenuItem { Name = "Изменить тип блока", Text = "Изменить тип блока" };
            typeItem.Click += ChangeType_Click;

            mi_paste.MenuItems.AddRange(new[] { mi_paste_block_down, mi_paste_branch_down, mi_paste_block_excange, mi_paste_branch_excange });
            mi_paste.Select += menuVisible;
            mi_copy.MenuItems.AddRange(new[] { mi_copy_block, mi_copy_branch });
            menu.MenuItems.AddRange(new[] { mi_copy, mi_paste, mi_delete, typeItem });
            menu.Popup += HideChangeType;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            tabControl.Size = new Size(Size.Width - 10, Size.Height - 140);
            org_name.Location = new Point(Size.Width / 2 - org_name.Width / 2, 12);

            //if (Size.Height > 670)
            //{
            //    foreach (TabPage tp in tabControl.TabPages)
            //    {
            //        //tabControl.TabPages[i].Controls.Find("block" + i + "_1", false).FirstOrDefault();
            //        foreach (Control c in tp.Controls)
            //        {

            //        }

            //    }
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] lvls = new int[15];
            int cur_i = -1;     
            //forms2.Add(result);
            
            Block first_here = (Block)tabControl.SelectedTab.Controls.Find("block" + (tabControl.SelectedIndex + 1) + "_1", false).FirstOrDefault();
            foreach (Block b in first_here.Branch)
            {
                lvls[b.lvl]++;
            }
            
            for (int i=0; i<result.dataGridView.Columns.Count; i++)
            {
                if (result.dataGridView.Columns[i].Name == tabControl.SelectedTab.Text)
                {
                    cur_i = i;
                    break;
                }     
            }

            if(cur_i==-1)
            {
                result.dataGridView.Columns.Add(tabControl.SelectedTab.Text, tabControl.SelectedTab.Text);
                cur_i = result.dataGridView.Columns.Count - 1;
            }

            result.dataGridView.ColumnHeadersVisible = true;
            MessageBox.Show(result.dataGridView.Columns.Count.ToString());
            result.dataGridView.Rows[0].Cells[cur_i].Value = Math.Round(Math.Log10(first_here.Branch.Count), 3).ToString();
            result.dataGridView.Rows[1].Cells[cur_i].Value = Math.Round(Math.Log(first_here.Branch.Count, 2), 3).ToString();
            result.dataGridView.Rows[2].Cells[cur_i].Value = Math.Round(((double)lvls[2] / first_here.Branch.Count), 3).ToString();
            //result.Width = Convert.ToInt32(result.inf_oc.Location.X) + result.inf_oc.Width + 100;
            result.dataGridView.Rows[3].Cells[cur_i].Value = Math.Round(((double)lvls.Length / first_here.Branch.Count), 3).ToString();

            result.Show();

            //tabl.dataGridView1.Width = tabl.Width;
            // tabl.Height = tabl.dataGridView1.Rows.Count * 20 + 79;
            //tabl.button1.Location = new Point(tabl.Width - tabl.button1.Width, tabl.Height - tabl.button1.Height);
            //tabl.Сохранить.Location = new Point(0, tabl.Height - tabl.Сохранить.Height);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            oleDbConnection1.Open(); //открыть соединение
                                     //заполнить таблицы в объекте DataSet
            FuncAdapter.Fill(dataSet1.Функции);
            OrgAdapter.Fill(dataSet1.Организации);
            BlockAdapter.Fill(dataSet1.Блоки);
            CharAdapter.Fill(dataSet1.Характеристики);
            StructAdapter.Fill(dataSet1.Структуры);


            string query = "Наименование='" + org_name.Text + "'";
            DataRow[] res = dataSet1.Организации.Select(query);

            if (res.Count() > 0)
                id_org = (int)res[0]["Код"];

            else
            {
                dataSet1.Организации.AddОрганизацииRow(org_name.Text);
                id_org = (int)dataSet1.Организации.Rows[dataSet1.Организации.Rows.Count - 1]["Код"];
                OrgAdapter.Update(dataSet1.Организации);
            }

            foreach (TabPage tp in tabControl.TabPages)
            {
                if (tp.Text == "+")
                    break;
                else
                {
                    dataSet1.Структуры.AddСтруктурыRow((DataSet1.ОрганизацииRow)dataSet1.Организации.Rows[id_org]["Код"]);
                    StructAdapter.Update(dataSet1.Структуры);
                    id_str = (DataSet1.СтруктурыRow)dataSet1.Структуры.Rows[dataSet1.Структуры.Rows.Count - 1];
                    //MessageBox.Show(tabControl.TabPages.IndexOf(tp).ToString());
                    Block first_here = (Block)tp.Controls.Find("block" + (tabControl.TabPages.IndexOf(tp) + 1) + "_1", false).FirstOrDefault();
                    foreach (Block b in first_here.Branch)
                    {

                        if (b.myParent != null)
                        {
                            query = "Имя='" + b.myParent.BlockData.Text + "'";
                            res = dataSet1.Блоки.Select(query);
                            id_par = (DataSet1.БлокиRow)res[0];
                            dataSet1.Блоки.AddБлокиRow((DataSet1.СтруктурыRow)id_str, b.lvl, b.number, b.BlockData.Text, (DataSet1.БлокиRow)id_par, b.TypeOfBlock);
                            BlockAdapter.Update(dataSet1.Блоки);
                        }
                        else dataSet1.Блоки.AddБлокиRow((DataSet1.СтруктурыRow)id_str, b.lvl, b.number, b.BlockData.Text, null, b.TypeOfBlock);
                        BlockAdapter.Update(dataSet1.Блоки);

                        if (b.TypeOfBlock == "department")
                        {
                            id_block = (DataSet1.БлокиRow)dataSet1.Блоки.Rows[dataSet1.Блоки.Rows.Count - 1];

                            foreach (TreeNode tn in b.MyTreeView.treeView.Nodes)
                            {
                                dataSet1.Функции.AddФункцииRow(null, tn.Text, (DataSet1.БлокиRow)id_block);
                                FuncAdapter.Update(dataSet1.Функции);
                                SaveNodes(tn, (DataSet1.ФункцииRow)dataSet1.Функции.Rows[dataSet1.Функции.Rows.Count - 1]);
                            }
                        }

                        try
                        {
                            dataSet1.Характеристики.AddХарактеристикиRow((DataSet1.СтруктурыRow)id_str,
                                Convert.ToSingle(result.dataGridView.Rows[0].Cells[tp.Text].Value), Convert.ToSingle(result.dataGridView.Rows[1].Cells[tp.Text].Value),
                                Convert.ToSingle(result.dataGridView.Rows[2].Cells[tp.Text].Value), Convert.ToSingle(result.dataGridView.Rows[3].Cells[tp.Text].Value));
                            CharAdapter.Update(dataSet1.Характеристики);
                        }
                        catch { }
                    }
                }
            }        
            oleDbConnection1.Close();
        }


        public void SaveNodes(TreeNode tn, DataSet1.ФункцииRow id)
        {
            id_node_par = (DataSet1.ФункцииRow)dataSet1.Функции.Rows[dataSet1.Функции.Rows.Count - 1];

            foreach (TreeNode tnch in tn.Nodes)
            {
                dataSet1.Функции.AddФункцииRow((DataSet1.ФункцииRow)id_node_par, tn.Text, (DataSet1.БлокиRow)id_block);
                FuncAdapter.Update(dataSet1.Функции);
                SaveNodes(tnch, (DataSet1.ФункцииRow)dataSet1.Функции.Rows[dataSet1.Функции.Rows.Count - 1]);
            }
        }


        public void HideChangeType(object sender, EventArgs e)
        {
            if (((ContextMenu)sender).SourceControl.Parent.GetType() == typeof(Block))
                currentBlock = (Block)((ContextMenu)sender).SourceControl.Parent;

            else if (((ContextMenu)sender).SourceControl.Parent.GetType() == typeof(TabPage))
                currentBlock = (Block)((ContextMenu)sender).SourceControl;

            if (currentBlock.myParent == null)
            {
                currentBlock.ContextMenu.MenuItems["Изменить тип блока"].Enabled = false;
                currentBlock.BlockData.ContextMenu.MenuItems["Изменить тип блока"].Enabled = false;
            }
            else
            {
                currentBlock.ContextMenu.MenuItems["Изменить тип блока"].Enabled = true;
                currentBlock.BlockData.ContextMenu.MenuItems["Изменить тип блока"].Enabled = true;
            }
        }


        public void ChangeType_Click(object sender, EventArgs e)
        {
            if (((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent.GetType() == typeof(Block))
                currentBlock = (Block)((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent;

            else if (((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent.GetType() == typeof(Panel))
                currentBlock = (Block)((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl;

            if (currentBlock.TypeOfBlock == "department")
            {
                currentBlock.TypeOfBlock = "position";
                currentBlock.AddType(currentBlock);
            }
            else if (currentBlock.TypeOfBlock == "position")
            {
                currentBlock.TypeOfBlock = "department";
                currentBlock.AddType(currentBlock);
            }
        }

        private void menuVisible(object sender, EventArgs e)
        {
            if (isCopyBlock == false)
            {
                mi_paste_block_down.Enabled = false;
                mi_paste_block_excange.Enabled = false;
            }
            else
            {
                mi_paste_block_down.Enabled = true;
                mi_paste_block_excange.Enabled = true;
            }

            if (isCopyBranch == false)
            {
                mi_paste_branch_down.Enabled = false;
                mi_paste_branch_excange.Enabled = false;
            }
            else
            {
                mi_paste_branch_down.Enabled = true;
                mi_paste_branch_excange.Enabled = true;
            }
        }

        private void menuDelete_Click(object sender, EventArgs e)
        {
            if (((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent.GetType() == typeof(Block))
                currentBlock = (Block)((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent;

            else if (((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent.GetType() == typeof(Panel))
                currentBlock = (Block)((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl;

            for (int i = currentBlock.Branch.Count - 1; i > -1; i--)
            {
                Block par = currentBlock;
                while (par.myParent != null)
                {
                    par = par.myParent;
                    par.Branch.Remove(currentBlock.Branch[i]);
                }

                tabControl.TabPages[tabControl.SelectedIndex].Controls.Remove(currentBlock.Branch[i]);
            }

            Block left_del = currentBlock;
            while (left_del.MyChilds.Count > 0)
                left_del = left_del.MyChilds[0];

            Block left_next = currentBlock.myParent.MyChilds[currentBlock.number + 1];
            while (left_next.MyChilds.Count > 0)
                left_next = left_next.MyChilds[0];

            int newpos = left_next.Location.X - left_del.Location.X;

            for (int i = currentBlock.number + 1; i < currentBlock.myParent.MyChilds.Count; i++)
            {
                foreach (Block b in currentBlock.myParent.MyChilds[i].Branch)
                    b.Location = new Point(b.Location.X - newpos, b.Location.Y);
            }



            currentBlock.first.Location = new Point(currentBlock.first.MyChilds[0].Location.X + (currentBlock.first.MyChilds[currentBlock.first.MyChilds.Count - 1].Location.X - currentBlock.first.MyChilds[0].Location.X) / 2, currentBlock.first.Location.Y);

            currentBlock.myParent.MyChilds.Remove(currentBlock);
            tabControl.TabPages[tabControl.SelectedIndex].Controls.Remove(currentBlock);
            currentBlock.Dispose();
            block1_1.drawLines();
        }


        private void menuPasteBranchExcange_Click(object sender, EventArgs e)
        {

            if (((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent.GetType() == typeof(Block))
                currentBlock = (Block)((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent;

            else if (((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent.GetType() == typeof(Panel))
                currentBlock = (Block)((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl;

            currentBlock.copyNodes(copyBlock.MyTreeView, currentBlock.MyTreeView);
            currentBlock.BlockData.Text = copyBlock.BlockData.Text;
            currentBlock.TypeOfBlock = copyBlock.TypeOfBlock;
            currentBlock.AddType(currentBlock);

            if (currentBlock.TypeOfBlock == "department")
                currentBlock.MyTreeView = copyBlock.MyTreeView;

            for (int i = currentBlock.Branch.Count - 1; i > -1; i--)
            {

                if (currentBlock.Branch[i].Name != currentBlock.Name)
                {
                    Block par = currentBlock;
                    while (par.myParent != null)
                    {
                        par = par.myParent;
                        par.Branch.Remove(currentBlock.Branch[i]);
                    }
                    tabControl.TabPages[tabControl.SelectedIndex].Controls.Remove(currentBlock.Branch[i]);
                    currentBlock.Branch.Remove(currentBlock.Branch[i]);
                }
            }
            currentBlock.MyChilds.Clear();

            pasteChilds(currentBlock, copyBlock);

            isCopyBranch = false;
        }


        private void menuPasteBranchDown_Click(object sender, EventArgs e)
        {

            if (((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent.GetType() == typeof(Block))
                currentBlock = (Block)((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent;

            else if (((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent.GetType() == typeof(Panel))
                currentBlock = (Block)((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl;

            foreach (Block c in copyBlock.Branch)
            {
                if (currentBlock.Name == c.Name)
                {
                    MessageBox.Show("Ошибка: Нельзя копировать ветвь саму в себя!");
                    return;
                }

            }

            currentBlock.Add(currentBlock, 1, copyBlock, copyBlock.TypeOfBlock);

            currentBlock = currentBlock.MyChilds[currentBlock.MyChilds.Count - 1];

            pasteChilds(currentBlock, copyBlock);

            isCopyBranch = false;
        }


        private void pasteChilds(Block parentBlock, Block copyBlock)
        {

            foreach (Block child in copyBlock.MyChilds)
            {
                currentBlock = parentBlock;
                currentBlock.Add(currentBlock, 1, child, child.TypeOfBlock);

                if (child.MyChilds.Count > 0)
                {
                    currentBlock = currentBlock.MyChilds[currentBlock.MyChilds.Count - 1];

                    pasteChilds(currentBlock, child);
                }
            }
        }


        private void CopyMenuItem_Click(object sender, EventArgs e)
        {
            if (((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent.GetType() == typeof(Block))
                copyBlock = (Block)((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent;

            else if (((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent.GetType() == typeof(Panel))
                copyBlock = (Block)((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl;

            if (((MenuItem)sender).Text == "Блок")
                isCopyBlock = true;
            else if (((MenuItem)sender).Text == "Ветвь")
                isCopyBranch = true;
        }


        private void menuPasteBlockExcange_Click(object sender, EventArgs e)
        {
            if (((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent.GetType() == typeof(Block))
                currentBlock = (Block)((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent;

            else if (((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent.GetType() == typeof(Panel))
                currentBlock = (Block)((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl;

            currentBlock.BlockData.Text = copyBlock.BlockData.Text;

            if (currentBlock.TypeOfBlock == "department")
                currentBlock.MyTreeView = copyBlock.MyTreeView;

            isCopyBlock = false;
        }


        private void menuPasteBlockDown_Click(object sender, EventArgs e)
        {
            if (((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent.GetType() == typeof(Block))
                currentBlock = (Block)((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent;

            else if (((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent.GetType() == typeof(Panel))
                currentBlock = (Block)((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl;

            currentBlock.Add(currentBlock, 1, copyBlock, copyBlock.TypeOfBlock);

            isCopyBlock = false;
        }

    }
}
