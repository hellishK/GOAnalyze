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

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateMenu();
            block1.lvl = 1;
            block1.ContextMenu= menu;
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
            MenuItem mi_paste_block_excange = new MenuItem { Name = "Блок с заменой", Text = "Блок с заменой текущего" };
            mi_paste_block_excange.Click += menuPasteText_Click;
            MenuItem mi_paste_branch_excange = new MenuItem { Name = "Ветвь с заменой", Text = "Ветвь с заменой текущей" };

            MenuItem mi_delete = new MenuItem { Name = "Удалить", Text = "Удалить" };
            MenuItem mi_delete_block = new MenuItem { Name = "Удалить блок", Text = "Блок" };
            MenuItem mi_delete_branch = new MenuItem { Name = "Удалить ветвь", Text = "Ветвь" };


            mi_paste.MenuItems.AddRange(new[] { mi_paste_block_down, mi_paste_branch_down, mi_paste_block_excange, mi_paste_branch_excange });
            mi_copy.MenuItems.AddRange(new[] { mi_copy_block, mi_copy_branch });
            mi_delete.MenuItems.AddRange(new[] { mi_delete_block, mi_delete_branch });
            menu.MenuItems.AddRange(new[] { mi_copy, mi_paste, mi_delete });
        }

        private void CopyMenuItem_Click(object sender, EventArgs e)
        {
            if (((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent.GetType() == typeof(Block))
            {
                //MessageBox.Show(((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.ToString());
                copyBlock = (Block)((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent;
                MessageBox.Show(((Block)((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent).TypeOfBlock);
            }

            else if (((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.Parent.GetType() == typeof(Panel))
            {
                //MessageBox.Show(((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl.ToString());
                copyBlock = (Block)((sender as MenuItem).GetContextMenu() as ContextMenu).SourceControl;
            }    
            //copyText = copyBlock.BlockData.Text;
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

            //MessageBox.Show("Скопированный плок" + copyBlock.BlockData.TextToString());
            curentBlock.Add(curentBlock, 1, copyBlock, copyBlock.TypeOfBlock);
            
        }
    }
}
