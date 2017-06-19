using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

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
        OleDbCommand command;
        ContextMenu menu = new ContextMenu();
        ContextMenu delPage = new ContextMenu();
        Results result = new Results();
        MenuItem mi_copy, mi_copy_block, mi_copy_branch, mi_paste, mi_paste_block_down, mi_paste_branch_down, mi_paste_block_excange, mi_paste_branch_excange, mi_delete, typeItem, tp_delete;
        bool isCopyBlock = false, isCopyBranch = false;
        int id_org, id_str, id_par, id_block, id_node_par, currentTab;

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
                currentTab = getNumber(tabControl.TabPages[tabControl.TabPages.Count - 2].Text)+1;
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
                tabControl.TabPages[tabControl.TabPages.Count - 1].Text = "Оргструктура" + currentTab;
                tabControl.TabPages[tabControl.TabPages.Count - 1].Name = "tabPage" + currentTab;
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
            {
                currentTab = getNumber(tabControl.SelectedTab.Text);
                ((Block)((TabControl)sender).SelectedTab.Controls.Find("block" + currentTab + "_1", false).FirstOrDefault()).drawLines();
            }
        }

        public void Page_DrawLines(object sender, EventArgs e)
        {
            try
            {
                currentTab = getNumber(tabControl.SelectedTab.Text);
                ((Block)((TabPage)sender).Controls.Find("block" + currentTab + "_1", false).FirstOrDefault()).drawLines();
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

            tp_delete = new MenuItem { Name = "Удалить структуру", Text = "Удалить структуру" };
            tp_delete.Click += (sen,ee) => { tabControl.TabPages.Remove(tabControl.TabPages[(int)delPage.Tag]); } ;
            delPage.MenuItems.Add(tp_delete);
        }

        private void tabControl_MouseUp(object sender, MouseEventArgs e)
        {
            // проверяем что нажата была правая кнопка
            if (e.Button == MouseButtons.Right)
            {
                // проходим циклом по всем табам для поиска на котором был клик
                for (int i = 0; i < tabControl.TabCount-1; i++)
                {
                    // получаем область таба и проверяем входит ли курсор в него или нет
                    Rectangle r = tabControl.GetTabRect(i);
                    if (r.Contains(e.Location))
                    {
                        // показываем контекстое меню и сохраняем номер таба                       
                        delPage.Tag = i; // сохраняем номер таба
                        tabControl.TabPages[i].Show();
                        delPage.Show((sender as TabControl).TabPages[i], e.Location);
                    }
                }
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            tabControl.Size = new Size(Size.Width - 30, Size.Height - 140);
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

        int getNumber(string s)
        {
            Regex regex = new Regex(@"\D");
            s = regex.Replace(s, "");            
            return Convert.ToInt16(s);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] lvls = new int[15];
            int cur_i = -1;
            //forms2.Add(result);

            currentTab = getNumber(tabControl.SelectedTab.Text);
            Block first_here = (Block)tabControl.SelectedTab.Controls.Find("block" + currentTab + "_1", false).FirstOrDefault();

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

        private void OpenStruc_Click(object sender, EventArgs e)
        {
            Organizations orgDialog = new Organizations();
            orgDialog.OrgList.Items.Clear();

            OleDbCommand command_org = new OleDbCommand();
            command_org.Connection = oleDbConnection1;
            command_org.CommandText = "SELECT DISTINCT (Наименование) FROM Организации";

            oleDbConnection1.Open();

            OleDbDataReader reader_org = command_org.ExecuteReader();

            if (reader_org.HasRows)
            {
                orgDialog.OrgList.Enabled = true;

                while (reader_org.Read())
                {
                    orgDialog.OrgList.Items.Add(reader_org["Наименование"]);
                }
                reader_org.Close();
            }
            else
            {
                orgDialog.OrgList.Text = "Ранее добавленные организации отсутсвуют";
                orgDialog.OrgList.Enabled = false;
            }

            orgDialog.ShowDialog();


            if (orgDialog.DialogResult == DialogResult.OK)
            {
                orgDialog.Hide();
                tabControl.Selected -= addPage_Selected;

                foreach (TabPage tp in tabControl.TabPages)
                {
                    tabControl.TabPages.Remove(tp);
                    tabControl.TabPages.Clear();
                }

                command_org.CommandText = "SELECT (Код) FROM Организации WHERE (Наименование='" + orgDialog.OrgList.SelectedItem + "')";
                reader_org = command_org.ExecuteReader();
                reader_org.Read();
                id_org = (int)reader_org["Код"];
                reader_org.Close();

                OleDbCommand command_str = new OleDbCommand();
                command_str.Connection = oleDbConnection1;
                OleDbDataReader reader_str;

                command_str.CommandText = "SELECT * FROM Структуры WHERE (Организация=" + id_org + ")";
                reader_str = command_str.ExecuteReader();
     
                if (reader_str.HasRows)
                {
                    while (reader_str.Read())
                    {
                        id_str = (int)reader_str["Код"];
                        MessageBox.Show(id_str.ToString());
                        currentTab = getNumber(tabControl.TabPages[tabControl.TabPages.Count - 1].Text) + 1;
                        TabPage new_tp = new TabPage("tabPage" + currentTab);
                        new_tp.Text = "Оргструктура " + currentTab;
                        new_tp.Paint += Page_DrawLines;
                        new_tp.AutoScroll = true;
                        new_tp.BackColor = SystemColors.Control;
                        tabControl.TabPages.Add(new_tp);
                        tabControl.SelectedTab = new_tp;

                        OleDbCommand command_block = new OleDbCommand();
                        command_block.Connection = oleDbConnection1;
                        OleDbDataReader reader_block;

                        command_block.CommandText = "SELECT * FROM Блоки WHERE (Структура=" + id_str + ") AND (Уровень=1)";
                        
                        reader_block = command_block.ExecuteReader();

                        if (reader_block.HasRows)
                        {
                            reader_block.Read();
                            id_block = (int)reader_block["Код"];
                            currentTab = getNumber(tabControl.TabPages[tabControl.TabPages.Count - 1].Text) + 1;
                            Block first_here = new Block();
                            first_here.lvl = (int)reader_block["Уровень"];
                            first_here.number = (int)reader_block["Номер на уровне"];
                            first_here.BlockData.Text = (string)reader_block["Имя"];
                            first_here.TypeOfBlock = "position";
                            first_here.ContextMenu = menu;
                            first_here.BlockData.ContextMenu = menu;
                            first_here.Extend.Visible = false;
                            first_here.Name = "block" + currentTab + "_1";
                            first_here.first = first_here;
                            first_here.Branch.Add(first_here);
                            first_here.Location = new Point(tabControl.SelectedTab.Width / 2 - first_here.Width / 2, 15);
                            new_tp.Controls.Add(first_here);
                            reader_block.Close();

                            command_block.CommandText = "SELECT * FROM (Блоки AS b1 LEFT JOIN Блоки AS b2 ON b1.Родитель=b2.Код) LEFT JOIN Блоки AS b3 ON b2.Родитель=b3.Код WHERE (b1.Структура=" + id_str + ") AND (b1.Родитель IS NOT NULL) ORDER BY b1.Родитель";
                            reader_block = command_block.ExecuteReader();

                            if (reader_block.HasRows)
                            {
                                while (reader_block.Read())
                                {
                                    Block nb = new Block();
                                    nb.lvl = (int)reader_block["b1.Уровень"];
                                    nb.number = (int)reader_block["b1.Номер на уровне"];
                                    nb.BlockData.Text = (string)reader_block["b1.Имя"];
                                    id_block = (int)reader_block["b1.Код"];

                                    if ((string)reader_block["b1.Тип"] == "department")
                                    {
                                        nb.MyTreeView.treeView.Nodes.Clear();

                                        OleDbCommand command_func = new OleDbCommand();
                                        command_func.Connection = oleDbConnection1;
                                        OleDbDataReader reader_func;

                                        command_func.CommandText = "SELECT * FROM (Функции AS t1 LEFT JOIN Функции AS t2 ON t1.Родитель=t2.Код) WHERE (t1.Блок=" + id_block + ") ORDER BY t1.Родитель";
                                        reader_func = command_func.ExecuteReader();

                                        if (reader_func.HasRows)
                                        {
                                            while (reader_func.Read())
                                            {
                                                if (reader_func["t1.Родитель"] == DBNull.Value)
                                                {
                                                    TreeNode tn = new TreeNode((string)reader_func["t1.Наименование"]);
                                                    tn.Name = tn.Text;
                                                    nb.MyTreeView.treeView.Nodes.Add(tn);
                                                }
                                                else
                                                {
                                                    TreeNode[] tns = nb.MyTreeView.treeView.Nodes.Find((string)reader_func["t2.Наименование"], true);
                                                    TreeNode tn = new TreeNode((string)reader_func["t1.Наименование"]);
                                                    tn.Name = tn.Text;
                                                    tns[0].Nodes.Add(tn);
                                                }
                                            }
                                            reader_func.Close();
                                        }
                                    }

                                    Block par;
                                    currentTab = getNumber(tabControl.SelectedTab.Name);

                                    if (reader_block["b2.Родитель"] == DBNull.Value)
                                        par = first_here;
                                    else
                                        par = (Block)tabControl.SelectedTab.Controls.Find("block" + currentTab + "_" + ((int)reader_block["b2.Уровень"]).ToString() + "_" + ((int)reader_block["b3.Номер на уровне"]).ToString() + "(" + ((int)reader_block["b2.Номер на уровне"]).ToString() + ")", true).FirstOrDefault();
                                    par.Add(par, 1, nb, (string)reader_block["b1.Тип"]);
                                }
                                reader_block.Close();
                            }  
                        }
                    }
                    reader_str.Close();
                    TabPage add_tp = new TabPage("+");
                    add_tp.Name = "addPage";
                    add_tp.Text = "+";
                    add_tp.Paint += Page_DrawLines;
                    add_tp.AutoScroll = true;
                    add_tp.BackColor = SystemColors.Control;
                    tabControl.TabPages.Add(add_tp);
                    tabControl.Selected += addPage_Selected;
                }

                oleDbConnection1.Close();
            }

        }

        private void TabControl_Selected(object sender, TabControlEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            //OleDbConnection connection = new OleDbConnection();
            command = new OleDbCommand();
            OleDbDataReader reader;
            oleDbConnection1.Open();  
            command.Connection = oleDbConnection1;
            string query= "Select @@Identity";

            command.CommandText = "SELECT (Код) FROM Организации WHERE (Наименование='" + org_name.Text + "')";

            if (command.ExecuteScalar() != null)
            {
                reader = command.ExecuteReader();
                reader.Read();
                id_org = (int)reader["Код"];
                reader.Close();
                MessageBox.Show("найдена старая организация - " + id_org);
            }

            else
            {
                command.CommandText = "insert into Организации (наименование) values ('" + org_name.Text + "')";
                command.ExecuteNonQuery();
                command.CommandText = query;
                id_org = (int)command.ExecuteScalar();

                MessageBox.Show("добавлена новая организация - " + id_org);
            }

           
            foreach (TabPage tp in tabControl.TabPages)
            {
                if (tp.Text == "+")
                    break;
                else
                {

                    command.CommandText = "insert into Структуры (Организация) values (" + id_org + ")";
                    command.ExecuteNonQuery();
                    command.CommandText = query;
                    id_str = (int)command.ExecuteScalar();
                    //MessageBox.Show("Добавлена структура " + id_str + " для организации- " + id_org);
                    currentTab = getNumber(tp.Text);
                    Block first_here = (Block)tp.Controls.Find("block" + currentTab + "_1", false).FirstOrDefault();
                    foreach (Block b in first_here.Branch)
                    {
                       // MessageBox.Show("Сохраняем блок " + b.Name);
                        if (b.myParent != null)
                        {

                            command.CommandText = "SELECT (Код) FROM Блоки WHERE (Имя='" + b.myParent.BlockData.Text+ "') AND (Структура="+ id_str + ")";
                            command.ExecuteNonQuery();
                            reader = command.ExecuteReader();
                            reader.Read();
                            id_par = reader.GetInt32(0);
                            reader.Close();

                            command.CommandText = "insert into Блоки (Структура, Уровень, [Номер на уровне], Имя, Родитель, Тип) values (" + id_str + "," + b.lvl + "," + b.number + ",'" + b.BlockData.Text + "'," + id_par + ",'" + b.TypeOfBlock + "')";
                            command.ExecuteNonQuery();
                            command.CommandText = query;
                            id_block = (int)command.ExecuteScalar();
                            //MessageBox.Show("Добавлен блок " + id_block + " для структуры " + id_str + ", родитель " + id_par);
                        }
                        else
                        {
                            command.CommandText = "insert into Блоки (Структура, Уровень, [Номер на уровне], Имя, Тип) values (" + id_str + "," + b.lvl + "," + b.number + ",'" + b.BlockData.Text + "','" + b.TypeOfBlock + "')";
                            command.ExecuteNonQuery();
                            command.CommandText = query;
                            id_block = (int)command.ExecuteScalar();
                           // MessageBox.Show("Добавлен блок " + id_block + " для структуры " + id_str + ", без родителя ");
                        }

                        if (b.TypeOfBlock == "department")
                        {
                            foreach (TreeNode tn in b.MyTreeView.treeView.Nodes)
                            {
                                command.CommandText = "insert into Функции ( Наименование, Блок) values ('" + tn.Text + "'," + id_block + ")";
                                command.ExecuteNonQuery();
                                command.CommandText = query;
                                id_node_par = (int)command.ExecuteScalar();
                                //MessageBox.Show("Добавлена функция " + id_node_par + " для блока " + id_block );
                                SaveNodes(tn, id_node_par);
                            }
                        }


                    }

                    try
                    {
                        command.CommandText = "insert into Характеристики (Структура, [Информационная оценка], [Число состояний системы], [Коэффициент централизации], [Коэффициент децентрализации]) values (" +
                            id_str + ",'" + Convert.ToSingle(result.dataGridView.Rows[0].Cells[tp.Text].Value) + "','" + Convert.ToSingle(result.dataGridView.Rows[1].Cells[tp.Text].Value) + "','" +
                            Convert.ToSingle(result.dataGridView.Rows[2].Cells[tp.Text].Value) + "','" + Convert.ToSingle(result.dataGridView.Rows[3].Cells[tp.Text].Value) + "')";
                        command.ExecuteNonQuery(); ;
                    }
                    catch { }
                    //MessageBox.Show("Добавлена характеристика для структуры " + id_str);
                }
            }
            oleDbConnection1.Close();
        }

        public void SaveNodes(TreeNode tn, int id)
        {
            string query = "Select @@Identity";
            int func = id;

            foreach (TreeNode tnch in tn.Nodes)
            {
                command.CommandText = "insert into Функции (Родитель, Наименование, Блок) values (" + func + ",'" + tnch.Text + "'," + id_block + ")";
                command.ExecuteNonQuery();
                command.CommandText = query;
                //MessageBox.Show("Добавлена функция " + (int)command.ExecuteScalar() + " для блока " +id_block + ", родитель " + func);
                SaveNodes(tnch, (int)command.ExecuteScalar());
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
