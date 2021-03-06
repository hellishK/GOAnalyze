﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GOA
{
    public partial class Tree : Form
    {
        public Tree()
        {
            InitializeComponent();
            BackColor = Color.FromArgb(30, 111, 166);
            treeView.BackColor = Color.FromArgb(30, 111, 166);
            this.MouseDown += Form_MouseDown;
            this.MouseMove += Form_MouseMove;
            this.MouseUp += Form_MouseUp;
        }
        ContextMenu node_menu = new ContextMenu();
        ContextMenu node_menu_add = new ContextMenu();
        ContextMenu node_menu_del = new ContextMenu();
        private bool isDragging = false;
        private Point lastCursor;
        private Point lastForm;

        public void Tree_Load(object sender, EventArgs e)
        {
            CreateNodeMenu(node_menu, node_menu_add, node_menu_del);  
                treeView.Nodes[0].ContextMenu = node_menu_add;
            treeView.NodeMouseClick += treeView_NodeMouseClick;

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

        public void CreateNodeMenu(ContextMenu node_menu, ContextMenu node_menu_add, ContextMenu node_menu_del)
        {
            MenuItem add_node1 = new MenuItem { Name = "Добавить ниже", Text = "Добавить ниже" };
            MenuItem del_node1 = new MenuItem { Name = "Удалить", Text = "Удалить" };
            add_node1.Click += AddNode_Click;
            del_node1.Click += (s, ee) => { treeView.SelectedNode.Remove(); };

            MenuItem add_node2 = new MenuItem { Name = "Добавить ниже", Text = "Добавить ниже" };
            MenuItem del_node2 = new MenuItem { Name = "Удалить", Text = "Удалить" };
            add_node2.Click += AddNode_Click;
            del_node2.Click += (s, ee) => { treeView.SelectedNode.Remove(); };

            node_menu.MenuItems.AddRange(new[] { add_node1, del_node1 });
            node_menu_add.MenuItems.Add(add_node2);
            node_menu_del.MenuItems.Add(del_node2);

        }

        public void AddNode_Click(object sender, EventArgs e)
        {
            TreeNode new_node = new TreeNode();

            if (treeView.SelectedNode.Level == 0)
            {
                new_node.Text = "Новый процесс";
                new_node.ContextMenu = node_menu;
                treeView.SelectedNode.Nodes.Add(new_node);
            }

            else if (treeView.SelectedNode.Level == 1)
            {
                new_node.Text = "Новый подпроцесс";
                new_node.ContextMenu = node_menu;
                treeView.SelectedNode.Nodes.Add(new_node);
            }

            else if (treeView.SelectedNode.Level == 2)
            {
                new_node.Text = "Новая операция";
                new_node.ContextMenu = node_menu_del;
                treeView.SelectedNode.Nodes.Add(new_node);
            }

            treeView.SelectedNode.Expand();
        }

        public void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                treeView.SelectedNode = e.Node;
        }

        public void Tree_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void CrossButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void RollButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}