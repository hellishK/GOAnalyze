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
        Block last;
        ContextMenu menu = new ContextMenu();

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateMenu();
            block1.lvl = 1;
            block1.ContextMenu = menu;
            block1.BlockData.ContextMenu = menu;
            block1.Extend.Visible = false;
        }


        public void CreateMenu()
        {

            MenuItem mi_copy = new MenuItem { Name = "Копировать", Text = "Копировать" };
            MenuItem mi_copy_block = new MenuItem { Name = "Блок", Text = "Блок" };
            MenuItem mi_copy_branch = new MenuItem { Name = "Ветвь", Text = "Ветвь" };
            mi_copy_block.Click += CopyMenuItem_Click;
            mi_copy_branch.Click += CopyMenuItem_Click;

            MenuItem mi_paste = new MenuItem { Name = "Вставить", Text = "Вставить" };
            MenuItem mi_paste_block_down = new MenuItem { Name = "Блок ниже", Text = "Блок ниже" };
            mi_paste_block_down.Click += menuPasteBlockDown_Click;
            MenuItem mi_paste_branch_down = new MenuItem { Name = "Ветвь ниже", Text = "Ветвь ниже" };
            mi_paste_branch_down.Click += menuPasteBranchDown_Click;
            MenuItem mi_paste_block_excange = new MenuItem { Name = "Блок с заменой", Text = "Блок с заменой текущего" };
            mi_paste_block_excange.Click += menuPasteText_Click;
            MenuItem mi_paste_branch_excange = new MenuItem { Name = "Ветвь с заменой", Text = "Ветвь с заменой текущей" };
            mi_paste_branch_excange.Click += menuPasteBranchExcange_Click;

            MenuItem mi_delete = new MenuItem { Name = "Удалить", Text = "Удалить" };
            MenuItem mi_delete_block = new MenuItem { Name = "Удалить блок", Text = "Блок" };
           // mi_delete_block.Click += menuDeleteBranch_Click;
            MenuItem mi_delete_branch = new MenuItem { Name = "Удалить ветвь", Text = "Ветвь" };
            mi_delete_branch.Click += menuDeleteBranch_Click;


            mi_paste.MenuItems.AddRange(new[] { mi_paste_block_down, mi_paste_branch_down, mi_paste_block_excange, mi_paste_branch_excange });
            mi_copy.MenuItems.AddRange(new[] { mi_copy_block, mi_copy_branch });
            mi_delete.MenuItems.AddRange(new[] { mi_delete_block, mi_delete_branch });
            menu.MenuItems.AddRange(new[] { mi_copy, mi_paste, mi_delete });
        }


        private void menuDeleteBranch_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Я сработал");
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
                        MessageBox.Show("удаляю блок " + curentBlock.Branch[i].Name + " из ветки " + par.Name);
                        par.Branch.Remove(curentBlock.Branch[i]);
                    }

                    panel1.Controls.Remove(curentBlock.Branch[i]);
            }
  
            curentBlock.myParent.Childs.Remove(curentBlock);
            panel1.Controls.Remove(curentBlock);
            curentBlock.Dispose();

            block1.drawLines();
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

            for ( int i = curentBlock.Branch.Count-1; i >-1; i--)
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
                    panel1.Controls.Remove(curentBlock.Branch[i]);
                    curentBlock.Branch.Remove(curentBlock.Branch[i]);
                }                    
            }
            curentBlock.Childs.Clear();

            pasteChilds(curentBlock, copyBlock);
        }


        private void menuPasteBranchDown_Click(object sender, EventArgs e)
        {

            if (((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent.GetType() == typeof(Block))
                curentBlock = (Block)((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent;

            else if (((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent.GetType() == typeof(Panel))
                curentBlock = (Block)((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl;

            foreach(Block c in copyBlock.Branch)
            {
                if (curentBlock.Name == c.Name)
                {
                    MessageBox.Show("Ошибка: Нельзя копировать ветвь саму в себя!");
                    return;
                }

            }

            curentBlock.Add(curentBlock, 1, copyBlock, copyBlock.TypeOfBlock);
        
            curentBlock = curentBlock.Childs[curentBlock.Childs.Count - 1];
            MessageBox.Show(curentBlock.BlockData.Text);

            pasteChilds(curentBlock, copyBlock);
        }


        private void pasteChilds(Block parentBlock, Block copyBlock)
        {

            foreach (Block child in copyBlock.Childs)
            {
                curentBlock = parentBlock;
                curentBlock.Add(curentBlock, 1, child, child.TypeOfBlock);
                //curentBlock = curentBlock.Childs[curentBlock.Childs.Count - 1];

                if (child.Childs.Count > 0)
                {
                    curentBlock = curentBlock.Childs[curentBlock.Childs.Count - 1];
                    MessageBox.Show(copyBlock.Childs.Count.ToString());

                    //if (child.Name == last.Name)
                    //    return;

                    pasteChilds(curentBlock, child);
                    //curentBlock = parentBlock;
                }
            }
            //curentBlock = curentBlock.Childs[curentBlock.Childs.Count - 1];
        }


        private void CopyMenuItem_Click(object sender, EventArgs e)
        {
            if (((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent.GetType() == typeof(Block))
                copyBlock = (Block)((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent;

            else if (((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent.GetType() == typeof(Panel))
                copyBlock = (Block)((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl;

            last = copyBlock;
            while (last.Childs.Count > 0)
                last = last.Childs[last.Childs.Count - 1];
        }


        private void menuPasteText_Click(object sender, EventArgs e)
        {
            if (((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.GetType() == typeof(TextBox))
                ((TextBox)((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl).Text = copyBlock.BlockData.Text;

            else if (((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.GetType() == typeof(Panel))
                ((TextBox)((Block)((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent).BlockData).Text = copyBlock.BlockData.Text;
        }


        private void menuPasteBlockDown_Click(object sender, EventArgs e)
        {
            if (((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent.GetType() == typeof(Block))
                curentBlock = (Block)((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent;

            else if (((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent.GetType() == typeof(Panel))
                curentBlock = (Block)((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl;

            curentBlock.Add(curentBlock, 1, copyBlock, copyBlock.TypeOfBlock);
            //MessageBox.Show("Активный - " + ((Block)ActiveControl).BlockData.Text.ToString());

        }

    }
}
