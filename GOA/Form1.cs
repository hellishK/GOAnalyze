using System;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
        OleDbCommand command;
        ContextMenu menu = new ContextMenu();
        ContextMenu delPage = new ContextMenu();
        Results result = new Results();
        MenuItem mi_copy, mi_copy_block, mi_copy_branch, mi_paste, mi_paste_block_down, mi_paste_branch_down, mi_paste_block_excange, mi_paste_branch_excange, mi_delete, typeItem, tp_delete;
        bool isCopyBlock = false, isCopyBranch = false;
        int id_org, id_str, id_par, id_block, id_node_par, currentTab;
        int pos_x, pos_y;
        private bool isDragging = false;
        private Point lastCursor;
        private Point lastForm;

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
            tabPage1.AutoScrollMinSize = new System.Drawing.Size(3000, 3000);
            block1_1.Location = new Point(1500, 20);
            tabPage1.AutoScrollPosition = new Point(1500 - tabControl.Width / 2 + block1_1.Width / 2, 0);
            tabPage1.SetAutoScrollMargin((tabControl.Width - block1_1.Width) / 2, 0);
            tabControl.Selected += addPage_Selected;
            tabControl.TabPages[0].Paint += Page_DrawLines;
            tabControl.TabPages[1].Paint += Page_DrawLines;
            tabControl.TabPages[0].BackColor = Color.FromArgb(207, 231, 247);
            tabControl.TabPages[1].BackColor = Color.FromArgb(207, 231, 247);
            tabControl.TabPages[0].AutoScrollPosition = new Point(1500 - tabControl.Width / 2 + block1_1.Width / 2, 0);
            tabControl.TabPages[1].AutoScrollPosition = new Point(1500 - tabControl.Width / 2 + block1_1.Width / 2, 0);
            result.MaximizeBox = false;
            result.dataGridView.Columns[0].HeaderText = "Показатель";
            result.dataGridView.Rows.Add(4);
            result.dataGridView.Rows[0].Cells[0].Value = "Информационная оценка";
            result.dataGridView.Rows[1].Cells[0].Value = "Число состояний системы";
            result.dataGridView.Rows[2].Cells[0].Value = "Коэффициент централизации";
            result.dataGridView.Rows[3].Cells[0].Value = "Коэффициент децентрализации";
            this.BackColor = Color.FromArgb(30, 111, 166);
            org_name.BackColor = Color.FromArgb(30, 111, 166);
            this.MouseDown += Form_MouseDown;
            this.MouseMove += Form_MouseMove;
            this.MouseUp += Form_MouseUp;
            OpenStruc.FlatAppearance.BorderColor = Color.FromArgb(206, 230, 247);
            SaveStruc.FlatAppearance.BorderColor = Color.FromArgb(103, 150, 189);
            ImgStr.FlatAppearance.BorderColor = Color.FromArgb(206, 230, 247);
            AnalyzeStr.FlatAppearance.BorderColor = Color.FromArgb(206, 230, 247);
            CreateReport.FlatAppearance.BorderColor = Color.FromArgb(138, 179, 211);
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

        public void addPage_Selected(object sender, EventArgs e)
        {

            if (tabControl.SelectedTab.Text == "+")
            {
                tabControl.SelectedTab.AutoScrollMinSize = new System.Drawing.Size(3000, 3000);
                currentTab = getNumber(tabControl.TabPages[tabControl.TabPages.Count - 2].Text) + 1;
                Block nb = new Block();
                nb.lvl = 1;
                nb.ContextMenu = menu;
                nb.BlockData.ContextMenu = menu;
                nb.Extend.Visible = false;
                nb.Name = "block" + currentTab + "_1";
                nb.first = nb;
                nb.Location = new Point(1500, 20);
                nb.Branch.Add(nb);
                TabPage new_tp = new TabPage("+");
                tabControl.TabPages[tabControl.TabPages.Count - 1].Text = "Оргструктура" + currentTab;
                tabControl.TabPages[tabControl.TabPages.Count - 1].Name = "tabPage" + currentTab;
                new_tp.Name = "addPage";
                new_tp.Text = "+";
                new_tp.Paint += Page_DrawLines;
                new_tp.AutoScroll = true;
                new_tp.SetAutoScrollMargin((tabControl.Width - block1_1.Width) / 2, 0);
                new_tp.BackColor = Color.FromArgb(207, 231, 247);
                tabControl.TabPages.Add(new_tp);
                tabControl.SelectedTab.Controls.Add(nb);
                nb.drawLines();
                tabControl.SelectedTab.ScrollControlIntoView(nb);

            }
            else
            {
                currentTab = getNumber(tabControl.SelectedTab.Text);
                Block nb = ((Block)((TabControl)sender).SelectedTab.Controls.Find("block" + currentTab + "_1", false).FirstOrDefault());
                tabControl.SelectedTab.ScrollControlIntoView(nb);
                nb.drawLines();
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
            tp_delete.Click += (sen, ee) => { tabControl.TabPages.Remove(tabControl.TabPages[(int)delPage.Tag]); };
            delPage.MenuItems.Add(tp_delete);
        }

        private void tabControl_MouseUp(object sender, MouseEventArgs e)
        {
            // проверяем что нажата была правая кнопка
            if (e.Button == MouseButtons.Right)
            {
                // проходим циклом по всем табам для поиска на котором был клик
                for (int i = 0; i < tabControl.TabCount - 1; i++)
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
            tabControl.SelectedTab.AutoScrollPosition = new Point(1500 - tabControl.SelectedTab.HorizontalScroll.Value - tabControl.Width / 2 + block1_1.Width / 2, 0);
            Refresh();
        }

        private Bitmap CreateImg(TabPage currentPage)
        {
            Block first = (Block)currentPage.Controls.Find("block" + getNumber(currentPage.Text) + "_1", false).FirstOrDefault(), left = first, right = first, last = first;
            foreach (Block b in currentPage.Controls)
            {
                b.Extend.Visible = false;
                b.AddChild.Visible = false;
            }

            while (left.MyChilds.Count > 0)
                left = left.MyChilds[0];

            while (right.MyChilds.Count > 0)
                right = right.MyChilds[right.MyChilds.Count - 1];

            foreach (Block b in first.Branch)
            {
                if (b.lvl > last.lvl)
                    last = b;
            }
            currentPage.SetAutoScrollMargin(0, 0);
            currentPage.AutoScroll = false;
            currentPage.AutoScrollPosition = new Point(left.Location.X, 0);

            Refresh();

            int max = Convert.ToInt32(Math.Ceiling((double)(right.Location.X - left.Location.X + 300) / tabControl.Width)), x = 0, count = 1,
                max_v = Convert.ToInt32(Math.Ceiling((double)(last.Location.Y + 100) / tabControl.Height)), y = 0, count_v = 1;

            //if (max < 1) max = 1;
            //if (max_v < 1) max_v = 1;

            Graphics from_tab = currentPage.CreateGraphics();
            Bitmap largeBmp;

            if (max == 1 && max_v == 1)
            {
                largeBmp = new Bitmap(currentPage.Width, currentPage.Height, from_tab);
                Graphics g = Graphics.FromImage(largeBmp);
                g.CopyFromScreen(currentPage.PointToScreen(Point.Empty), new Point(x, y), tabControl.Size);
            }

            else
            {
                largeBmp = new Bitmap(right.Location.X - left.Location.X + left.Width, last.Location.Y + last.Height, from_tab);
                Graphics g = Graphics.FromImage(largeBmp);

                while (count_v <= max_v)
                {
                    x = 0;
                    count = 1;

                    while (count <= max)
                    {
                        g = Graphics.FromImage(largeBmp);

                        Bitmap smallBmp = new Bitmap(tabControl.Width, tabControl.Height, from_tab);
                        Refresh();

                        if (count == max && max > 1)
                        {
                            int small_x = right.Location.X - left.Location.X + left.Width - x;
                            if (count_v == max_v && max_v > 1)
                            {
                                int small_y = last.Location.Y + last.Height - y;
                                g.CopyFromScreen(currentPage.PointToScreen(Point.Empty), new Point(x - (tabControl.Width - small_x), y - (tabControl.Height - small_y)), tabControl.Size);
                            }
                            else
                                g.CopyFromScreen(currentPage.PointToScreen(Point.Empty), new Point(x - (tabControl.Width - small_x), y), tabControl.Size);
                        }

                        else if (count_v == max_v && max_v > 1)
                        {
                            int small_y = last.Location.Y + last.Height - y;
                            g.CopyFromScreen(currentPage.PointToScreen(Point.Empty), new Point(x, y - (tabControl.Height - small_y)), tabControl.Size);
                        }
                        else
                        {
                            g.CopyFromScreen(currentPage.PointToScreen(Point.Empty), new Point(x, y), tabControl.Size);
                        }

                        currentPage.AutoScrollPosition = new Point(-currentPage.AutoScrollPosition.X + tabControl.Width, -currentPage.AutoScrollPosition.Y);

                        count++;
                        x += tabControl.Width-10;

                    }

                    currentPage.AutoScrollPosition = new Point(left.Location.X, -currentPage.AutoScrollPosition.Y + tabControl.Height);
                    count_v++;
                    y += tabControl.Height;
                }

            }
            currentPage.AutoScroll = true;

            foreach (Block b in currentPage.Controls)
            {
                b.Extend.Visible = true;
                b.AddChild.Visible = true;
            }

            currentPage.AutoScrollPosition = new Point(0, 0);
            currentPage.SetAutoScrollMargin(200, 0);
            return largeBmp;
        }


        private void ImgStr_Click(object sender, EventArgs e)
        {
            Bitmap largeBmp = CreateImg(tabControl.SelectedTab);

            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "Изображения .bmp (*.bmp)|*.bmp";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = false;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileStream fstream = new FileStream(saveFileDialog.FileName, FileMode.Create);
                largeBmp.Save(fstream, System.Drawing.Imaging.ImageFormat.Bmp);
                fstream.Close();
            }
        }

        private void CreateReport_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Word.Application wordApp;
            Microsoft.Office.Interop.Word.Document wordDoc;
            wordApp = new Microsoft.Office.Interop.Word.Application();
            wordDoc = wordApp.Documents.Add();
            wordDoc.Characters.Last.Select();
            wordApp.Selection.InsertAfter("Варианты организационных структур для " + this.org_name.Text);
            wordDoc.Characters.Last.Select();
            wordApp.Selection.InsertAfter("\r");

            foreach (TabPage tp in tabControl.TabPages)
            {
                if (tp.Text != "+")
                {
                    tabControl.SelectedTab = tp;
                    Clipboard.SetImage(CreateImg(tp));
                    wordDoc.Characters.Last.Select();
                    wordApp.Selection.InsertParagraphAfter();
                    wordApp.Selection.Paste();
                    wordDoc.Characters.Last.Select();
                    wordApp.Selection.InsertAfter("\r");
                    wordDoc.Tables.Add(wordDoc.Characters.Last, 2, 4);
                    Microsoft.Office.Interop.Word.Table tbl = wordDoc.Tables[wordDoc.Tables.Count];
                    tbl.AllowAutoFit = true;
                    CreateResult();
                    result.Show();
                    tbl.Cell(1, 1).Range.Text = result.dataGridView.Rows[0].Cells[0].Value.ToString();
                    tbl.Cell(1, 2).Range.Text = result.dataGridView.Rows[1].Cells[0].Value.ToString();
                    tbl.Cell(1, 3).Range.Text = result.dataGridView.Rows[2].Cells[0].Value.ToString();
                    tbl.Cell(1, 4).Range.Text = result.dataGridView.Rows[3].Cells[0].Value.ToString();
                    tbl.Cell(2, 1).Range.Text = result.dataGridView.Rows[0].Cells[tp.Text].Value.ToString();
                    tbl.Cell(2, 2).Range.Text = result.dataGridView.Rows[1].Cells[tp.Text].Value.ToString();
                    tbl.Cell(2, 3).Range.Text = result.dataGridView.Rows[2].Cells[tp.Text].Value.ToString();
                    tbl.Cell(2, 4).Range.Text = result.dataGridView.Rows[3].Cells[tp.Text].Value.ToString();
                    tbl.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                    tbl.Borders.InsideLineWidth = Microsoft.Office.Interop.Word.WdLineWidth.wdLineWidth075pt;
                    tbl.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                    tbl.Borders.OutsideLineWidth = Microsoft.Office.Interop.Word.WdLineWidth.wdLineWidth075pt;
                    wordDoc.Characters.Last.Select();
                    wordApp.Selection.InsertParagraphAfter();
                }
            }

            wordDoc.Content.Select();
            wordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
            wordApp.Selection.Font.Size = 14;
            wordApp.Selection.Font.Name = "Times new Roman";

            wordApp.Visible = true;
        }

        int getNumber(string s)
        {
            Regex regex = new Regex(@"\D");
            s = regex.Replace(s, "");
            return Convert.ToInt16(s);
        }

        private void CrossButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RollButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void SizeButton_Click(object sender, EventArgs e)
        {
            if (Size.Width < 1500)
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

                Size = new Size(1154, 760);
                Location = new Point(pos_x, pos_y);
                SizeButton.Image = Properties.Resources.size_max;
            }
        }

        public void CreateResult()
        {
            int[] lvls = new int[15];
            int cur_i = -1;

            currentTab = getNumber(tabControl.SelectedTab.Text);
            Block first_here = (Block)tabControl.SelectedTab.Controls.Find("block" + currentTab + "_1", false).FirstOrDefault();

            foreach (Block b in first_here.Branch)
            {
                lvls[b.lvl]++;
            }

            for (int i = 0; i < result.dataGridView.Columns.Count; i++)
            {
                if (result.dataGridView.Columns[i].Name == tabControl.SelectedTab.Text)
                {
                    cur_i = i;
                    break;
                }
            }

            if (cur_i == -1)
            {
                result.dataGridView.Columns.Add(tabControl.SelectedTab.Text, tabControl.SelectedTab.Text);
                cur_i = result.dataGridView.Columns.Count - 1;
            }

            result.dataGridView.ColumnHeadersVisible = true;
            result.dataGridView.Rows[0].Cells[cur_i].Value = Math.Round(Math.Log10(first_here.Branch.Count), 3).ToString();
            result.dataGridView.Rows[1].Cells[cur_i].Value = Math.Round(Math.Log(first_here.Branch.Count, 2), 3).ToString();
            result.dataGridView.Rows[2].Cells[cur_i].Value = Math.Round(((double)lvls[2] / first_here.Branch.Count), 3).ToString();
            result.dataGridView.Rows[3].Cells[cur_i].Value = Math.Round(((double)lvls.Length / first_here.Branch.Count), 3).ToString();
        }

        private void AnalyzeStr_Click(object sender, EventArgs e)
        {
            foreach (TabPage tp in tabControl.TabPages)
            {
                tabControl.SelectedTab = tp;
                CreateResult();
            }
            result.Show();
        }

        private void OpenStruc_Click(object sender, EventArgs e)
        {
            Organizations orgDialog = new Organizations();
            orgDialog.OrgList.Items.Clear();
            Block first_here = new Block();
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
                this.org_name.Text = orgDialog.OrgList.SelectedItem.ToString();
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

                        if (tabControl.TabPages.Count > 0)
                            currentTab = getNumber(tabControl.TabPages[tabControl.TabPages.Count - 1].Text) + 1;

                        else currentTab = 1;

                        TabPage new_tp = new TabPage("tabPage" + currentTab);
                        new_tp.Text = "Оргструктура" + currentTab;
                        new_tp.Paint += Page_DrawLines;
                        new_tp.AutoScroll = true;
                        new_tp.SetAutoScrollMargin((tabControl.Width - block1_1.Width) / 2, 0);
                        new_tp.BackColor = Color.FromArgb(207, 231, 247);
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
                            currentTab = getNumber(tabControl.TabPages[tabControl.TabPages.Count - 1].Text);
                            first_here = new Block();
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
                            tabControl.SelectedTab.AutoScrollMinSize = new Size(3000, 3000);
                            first_here.Location = new Point(1500, 20);
                            new_tp.Controls.Add(first_here);
                            reader_block.Close();

                            command_block.CommandText = "SELECT * FROM (Блоки AS b1 LEFT JOIN Блоки AS b2 ON b1.Родитель=b2.Код) LEFT JOIN Блоки AS b3 ON b2.Родитель=b3.Код WHERE (b1.Структура=" + id_str + ") AND (b1.Родитель IS NOT NULL) ORDER BY b1.Родитель, b1.[Номер на уровне]";
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
                                    currentTab = getNumber(tabControl.SelectedTab.Text);

                                    if (reader_block["b2.Родитель"] == DBNull.Value)
                                        par = first_here;
                                    else
                                        par = (Block)tabControl.SelectedTab.Controls.Find("block" + currentTab + "_" + ((int)reader_block["b2.Уровень"]).ToString() + "_" + ((int)reader_block["b3.Номер на уровне"]).ToString() + "(" + ((int)reader_block["b2.Номер на уровне"]).ToString() + ")", true).FirstOrDefault();

                                    try
                                    { par.Add(par, 1, nb, (string)reader_block["b1.Тип"]); }
                                    catch { MessageBox.Show("block" + currentTab + "_" + (reader_block["b2.Уровень"]).ToString() + "_" + (reader_block["b3.Номер на уровне"]).ToString() + "(" + (reader_block["b2.Номер на уровне"]).ToString() + ")"); }
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
                    add_tp.SetAutoScrollMargin((tabControl.Width - block1_1.Width) / 2, 0);
                    add_tp.BackColor = Color.FromArgb(207, 231, 247);
                    tabControl.TabPages.Add(add_tp);
                    tabControl.Selected += addPage_Selected;
                }

                oleDbConnection1.Close();
            }
            tabControl.SelectedTab.ScrollControlIntoView(first_here);
        }

        private void TabControl_Selected(object sender, TabControlEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SaveStruc_Click(object sender, EventArgs e)
        {
            if (org_name.Text == "Введите наименование организации" || org_name.Text == "")
                MessageBox.Show("Введите наименование организации!");
            else
            {
                //проверка заполненности блоков

                //foreach (TabPage tp in tabControl.TabPages)
                //{
                //    currentTab = getNumber(tp.Text);
                //    Block first_here = (Block)tp.Controls.Find("block" + currentTab + "_1", false).FirstOrDefault();
                //    foreach (Block b in first_here.Branch)
                //    {
                //        if (b.BlockData.Text == "")
                //        {
                //            MessageBox.Show("Не все блоки заполнены! Проверьте уровень " + b.lvl + " структуры" + tp.Text);
                //            return;
                //        }
                //    }
                //}
                command = new OleDbCommand();
                OleDbDataReader reader;
                oleDbConnection1.Open();
                command.Connection = oleDbConnection1;
                string query = "Select @@Identity";

                command.CommandText = "SELECT (Код) FROM Организации WHERE (Наименование='" + org_name.Text + "')";

                if (command.ExecuteScalar() != null)
                {
                    reader = command.ExecuteReader();
                    reader.Read();
                    id_org = (int)reader["Код"];
                    reader.Close();
                    command.CommandText = "DELETE * FROM Организации WHERE (Код=" + id_org + ")";
                    command.ExecuteNonQuery();
                }

                    command.CommandText = "insert into Организации (наименование) values ('" + org_name.Text + "')";
                    command.ExecuteNonQuery();
                    command.CommandText = query;
                    id_org = (int)command.ExecuteScalar();

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
                        currentTab = getNumber(tp.Text);
                        Block first_here = (Block)tp.Controls.Find("block" + currentTab + "_1", false).FirstOrDefault();
                        foreach (Block b in first_here.Branch)
                        {
                            if (b.myParent != null)
                            {
                                command.CommandText = "SELECT (Код) FROM Блоки WHERE (Имя='" + b.myParent.BlockData.Text + "') AND (Структура=" + id_str + ")";
                                command.ExecuteNonQuery();
                                reader = command.ExecuteReader();
                                reader.Read();
                                id_par = reader.GetInt32(0);
                                reader.Close();

                                command.CommandText = "insert into Блоки (Структура, Уровень, [Номер на уровне], Имя, Родитель, Тип) values (" + id_str + "," + b.lvl + "," + b.number + ",'" + b.BlockData.Text + "'," + id_par + ",'" + b.TypeOfBlock + "')";
                                command.ExecuteNonQuery();
                                command.CommandText = query;
                                id_block = (int)command.ExecuteScalar();
                            }
                            else
                            {
                                command.CommandText = "insert into Блоки (Структура, Уровень, [Номер на уровне], Имя, Тип) values (" + id_str + "," + b.lvl + "," + b.number + ",'" + b.BlockData.Text + "','" + b.TypeOfBlock + "')";
                                command.ExecuteNonQuery();
                                command.CommandText = query;
                                id_block = (int)command.ExecuteScalar();
                            }

                            if (b.TypeOfBlock == "department")
                            {
                                foreach (TreeNode tn in b.MyTreeView.treeView.Nodes)
                                {
                                    command.CommandText = "insert into Функции ( Наименование, Блок) values ('" + tn.Text + "'," + id_block + ")";
                                    command.ExecuteNonQuery();
                                    command.CommandText = query;
                                    id_node_par = (int)command.ExecuteScalar();
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
                    }
                }
                oleDbConnection1.Close();
                MessageBox.Show("Данные успешно сохранены");
            }
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


            // дети удаляются из всех ветвей
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


            if (currentBlock.myParent.MyChilds.Count > currentBlock.number)
            {
                //находим самого левого ребенка в удаляемой ветви, до его позиции будем сдвигать братьев удаляемого блока
                Block left_del = currentBlock;
                while (left_del.MyChilds.Count > 0)
                    left_del = left_del.MyChilds[0];

                Block left_next = currentBlock.myParent.MyChilds[currentBlock.number];

                while (left_next.MyChilds.Count > 0)
                    left_next = left_next.MyChilds[0];

                int newpos = left_next.Location.X - left_del.Location.X;

                for (int i = currentBlock.number; i < currentBlock.myParent.MyChilds.Count; i++)
                {
                    foreach (Block b in currentBlock.myParent.MyChilds[i].Branch)
                    {
                        b.Location = new Point(b.Location.X - newpos, b.Location.Y);
                        b.number--;
                    }

                    currentBlock.myParent.MyChilds[i].Location = new Point(currentBlock.myParent.MyChilds[i].Location.X, currentBlock.myParent.MyChilds[i].Location.Y);
                }

                Block par = currentBlock;
                while (par.myParent.myParent != null)
                {
                    par = par.myParent;
                    for (int i = par.number; i < par.myParent.MyChilds.Count; i++)
                    {
                        foreach (Block b in par.myParent.MyChilds[i].Branch)
                        {
                            b.Location = new Point(b.Location.X - newpos, b.Location.Y);
                        }

                    }
                }
            }


            tabControl.TabPages[tabControl.SelectedIndex].Controls.Remove(currentBlock);
            currentBlock.myParent.MyChilds.Remove(currentBlock);
            currentBlock.myParent.Location = new Point(currentBlock.myParent.MyChilds[0].Location.X + (currentBlock.myParent.MyChilds[currentBlock.myParent.MyChilds.Count - 1].Location.X - currentBlock.myParent.MyChilds[0].Location.X) / 2, currentBlock.myParent.Location.Y);
            currentBlock.first.Location = new Point(currentBlock.first.MyChilds[0].Location.X + (currentBlock.first.MyChilds[currentBlock.first.MyChilds.Count - 1].Location.X - currentBlock.first.MyChilds[0].Location.X) / 2, currentBlock.first.Location.Y);
            currentBlock.Dispose();
            currentBlock.first.drawLines();
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

            try
            {
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
            catch { MessageBox.Show("Извините, прозошла ошибка при копировании!"); }
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
