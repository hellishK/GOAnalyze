using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOA
{
    public partial class Block : UserControl
    {

        public Block()
        {
            InitializeComponent();
        }

        List<Block> childs = new List<Block>();
        List<Block> branch = new List<Block>();
        public int lvl = 0;
        public int margin = 25;
        public int newloc = 0;
        Block myParent;
        


        private void AddChild_Click(object sender, EventArgs e)
        {
            Block first = Form.ActiveForm.Controls.Find("block1", true).FirstOrDefault() as Block;
            Block papa = (sender as Control).Parent as Block;
            Block deda = papa.myParent;
            Block pra;
            Block left = first;
            Block right = first;

            Block newBlock = new Block();
            newBlock.myParent = papa;
            newBlock.lvl = papa.lvl + 1;
            int index = 1;

            if (papa != null)
            {
                foreach (Block ch in papa.childs)
                {
                    index++;
                    if (newBlock.Name == ch.Name) break;
                }
            }
            newBlock.Name = "block" + newBlock.lvl + "-" + index + "(" + (papa.childs.Count+1) + ")";
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
                while (pra.myParent != null);
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
                    //MessageBox.Show("Добавили в ветку " + pra.Name);
                }
                while (pra.myParent != null);

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
                                //MessageBox.Show("Нашли индекс!");
                                break;
                            }
                        }

                        //берем всех правых братьев родителя
                        for (int idx = index; idx < pra.myParent.childs.Count; idx++)
                        {
                            //MessageBox.Show("Зашли в цикл,но ... дедей у дедушки: " + deda.childs.Count + ", индекс папы:  " + idx);
                            //и двигаем ВСЮ ИХ ВЕТКУ вправа

                            foreach (Block ch in pra.myParent.childs[idx].branch)
                            {
                                ch.Location = new Point(ch.Location.X + pra.Width + margin, ch.Location.Y);
                                // MessageBox.Show("Переместили " + ch.Name);
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
                right = right.childs[right.childs.Count-1];
            }

            //вычисляем координаты для блока1 (середину)
            first.Location = new Point (left.Location.X + (right.Location.X - left.Location.X)/2, first.Location.Y);
            newBlock.BlockData.Text = newBlock.Name;
            Form.ActiveForm.Controls.Find("panel1",false).FirstOrDefault().Controls.Add(newBlock);  
        }
    }
}
