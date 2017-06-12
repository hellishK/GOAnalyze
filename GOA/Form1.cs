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
        MenuItem mi_copy, mi_copy_block, mi_copy_branch, mi_paste, mi_paste_block_down, mi_paste_branch_down, mi_paste_block_excange, mi_paste_branch_excange, mi_delete, typeItem;
        bool isCopyBlock = false, isCopyBranch = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateMenu();
            block1_1.lvl = 1;
            block1_1.first = block1_1;
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
