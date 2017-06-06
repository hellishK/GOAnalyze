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
        Block curentBlock;
        ContextMenu menu = new ContextMenu();
        MenuItem mi_copy, mi_copy_block, mi_copy_branch, mi_paste, mi_paste_block_down, mi_paste_branch_down, mi_paste_block_excange, mi_paste_branch_excange, mi_delete;
        bool isCopyBlock = false, isCopyBranch = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateMenu();
            block1_1.lvl = 1;
            block1_1.first = block1_1;
            block1_1.ContextMenu = menu;
            block1_1.BlockData.ContextMenu = menu;
            block1_1.Extend.Visible = false;
            block1_1.Location = new Point(tabControl.SelectedTab.Width / 2 - block1_1.Width / 2, 15);
            tabPage1.AutoScroll = true;
            tabControl.Selected += addPage_Selected;
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
                new_tp.AutoScroll = true;
                tabControl.TabPages.Add(new_tp);
                tabControl.SelectedTab.Controls.Add(nb);
                nb.drawLines();
            }
            else
            {

                ((Block)((TabControl)sender).SelectedTab.Controls.Find("block" + (tabControl.SelectedIndex + 1) + "_1", false).FirstOrDefault()).drawLines();
                //tabControl.SelectedTab.Invalidate();
                //MessageBox.Show("Сработал элс");
            }
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


            mi_paste.MenuItems.AddRange(new[] { mi_paste_block_down, mi_paste_branch_down, mi_paste_block_excange, mi_paste_branch_excange });
            mi_paste.Select += menuVisible;
            mi_copy.MenuItems.AddRange(new[] { mi_copy_block, mi_copy_branch });
            menu.MenuItems.AddRange(new[] { mi_copy, mi_paste, mi_delete });
        }

        private void menuVisible(object sender, EventArgs e)
        {
            if (isCopyBlock == false)
            {
                //MessageBox.Show("Блок не был скопирован");
                mi_paste_block_down.Enabled = false;
                mi_paste_block_excange.Enabled = false;
            }
            else
            {
                //MessageBox.Show("Блок был скопирован");
                mi_paste_block_down.Enabled = true;
                mi_paste_block_excange.Enabled = true;
            }

            if (isCopyBranch == false)
            {
                //MessageBox.Show("Ветка не была скопирована");
                mi_paste_branch_down.Enabled = false;
                mi_paste_branch_excange.Enabled = false;
            }
            else
            {
                //MessageBox.Show("Ветка была скопирована");
                mi_paste_branch_down.Enabled = true;
                mi_paste_branch_excange.Enabled = true;
            }
        }

        private void menuDelete_Click(object sender, EventArgs e)
        {
            if (((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent.GetType() == typeof(Block))
                curentBlock = (Block)((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent;

            else if (((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent.GetType() == typeof(Panel))
                curentBlock = (Block)((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl;

            for (int i = curentBlock.Branch.Count - 1; i > -1; i--)
            {
                Block par = curentBlock;
                while (par.myParent != null)
                {
                    par = par.myParent;
                    par.Branch.Remove(curentBlock.Branch[i]);
                }

                tabControl.TabPages[tabControl.SelectedIndex].Controls.Remove(curentBlock.Branch[i]);
            }

            curentBlock.myParent.MyChilds.Remove(curentBlock);
            tabControl.TabPages[tabControl.SelectedIndex].Controls.Remove(curentBlock);
            curentBlock.Dispose();

            block1_1.drawLines();
        }


        private void menuPasteBranchExcange_Click(object sender, EventArgs e)
        {

            if (((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent.GetType() == typeof(Block))
                curentBlock = (Block)((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent;

            else if (((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent.GetType() == typeof(Panel))
                curentBlock = (Block)((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl;

            curentBlock.copyNodes(copyBlock.MyTreeView, curentBlock.MyTreeView);
            curentBlock.BlockData.Text = copyBlock.BlockData.Text;
            curentBlock.TypeOfBlock = copyBlock.TypeOfBlock;
            curentBlock.AddType(curentBlock);

            if (curentBlock.TypeOfBlock == "department")
                curentBlock.MyTreeView = copyBlock.MyTreeView;

            for (int i = curentBlock.Branch.Count - 1; i > -1; i--)
            {

                if (curentBlock.Branch[i].Name != curentBlock.Name)
                {
                    Block par = curentBlock;
                    while (par.myParent != null)
                    {
                        par = par.myParent;
                        MessageBox.Show("удаляю блок " + curentBlock.Branch[i].Name + " из ветки " + par.Name);
                        par.Branch.Remove(curentBlock.Branch[i]);
                    }
                    MessageBox.Show("удаляю c формы - " + curentBlock.Branch[i].Name);
                    tabControl.TabPages[tabControl.SelectedIndex].Controls.Remove(curentBlock.Branch[i]);
                    curentBlock.Branch.Remove(curentBlock.Branch[i]);
                }
            }
            curentBlock.MyChilds.Clear();

            pasteChilds(curentBlock, copyBlock);

            isCopyBranch = false;
        }


        private void menuPasteBranchDown_Click(object sender, EventArgs e)
        {

            if (((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent.GetType() == typeof(Block))
                curentBlock = (Block)((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent;

            else if (((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent.GetType() == typeof(Panel))
                curentBlock = (Block)((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl;

            foreach (Block c in copyBlock.Branch)
            {
                if (curentBlock.Name == c.Name)
                {
                    MessageBox.Show("Ошибка: Нельзя копировать ветвь саму в себя!");
                    return;
                }

            }

            curentBlock.Add(curentBlock, 1, copyBlock, copyBlock.TypeOfBlock);

            curentBlock = curentBlock.MyChilds[curentBlock.MyChilds.Count - 1];
            MessageBox.Show(curentBlock.BlockData.Text);

            pasteChilds(curentBlock, copyBlock);

            isCopyBranch = false;
        }


        private void pasteChilds(Block parentBlock, Block copyBlock)
        {

            foreach (Block child in copyBlock.MyChilds)
            {
                curentBlock = parentBlock;
                curentBlock.Add(curentBlock, 1, child, child.TypeOfBlock);

                if (child.MyChilds.Count > 0)
                {
                    curentBlock = curentBlock.MyChilds[curentBlock.MyChilds.Count - 1];
                    MessageBox.Show(copyBlock.MyChilds.Count.ToString());

                    pasteChilds(curentBlock, child);
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
                curentBlock = (Block)((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent;

            else if (((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent.GetType() == typeof(Panel))
                curentBlock = (Block)((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl;

            curentBlock.BlockData.Text = copyBlock.BlockData.Text;

            if (curentBlock.TypeOfBlock == "department")
                curentBlock.MyTreeView = copyBlock.MyTreeView;

            isCopyBlock = false;
        }


        private void menuPasteBlockDown_Click(object sender, EventArgs e)
        {
            if (((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent.GetType() == typeof(Block))
                curentBlock = (Block)((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent;

            else if (((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent.GetType() == typeof(Panel))
                curentBlock = (Block)((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl;

            curentBlock.Add(curentBlock, 1, copyBlock, copyBlock.TypeOfBlock);

            isCopyBlock = false;
        }

    }
}
