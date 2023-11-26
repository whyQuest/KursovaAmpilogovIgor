using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ecosystem
{ 

    public partial class Ecosystem2 : Form
    {         
        public Nets net { get; set; }
        //розмір клітини (НЕ ЗМІНЮВАТИ)
        public const int boxes = 40;
        private int intSpawnCarrot;

        public PictureBox[,] Cells;


        //об'єкт, щоб розмістити кнопки СТАРТ, СТОП, ПАУЗА
        private Panel panel;

        public Ecosystem2(int xPos, int yPos, int numbRabbit, int numbWolf, int numbCarrot, int intervallSpawn)
        {
            InitializeComponent();

            panel = new Panel();
            panel.Location = new Point(0, 0);
            panel.Size = new Size(xPos * boxes, 60);
            panel.BackColor = Color.White;
            Setting setting = new Setting();
            setting.Location = new Point(0, 0);
            setting.Pause += Pause1;
            setting.Play += Play1;
            setting.Stop += Stop1;
            panel.Controls.Add(setting);
            this.Controls.Add(panel);


            //створення поля
            net = new Nets(xPos, yPos);

            net.Simulate += Result;

            //викликаємо метод розміщення об'єктів
            net.Init(numbRabbit, numbWolf, numbCarrot, intervallSpawn);

            intSpawnCarrot = intervallSpawn;
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;


            //Створення та налаштування графічних елементів для відображення клітин екосистеми.
            Cells = new PictureBox[xPos, yPos];
            for (int x = 0; x < xPos; x++)
            {
                for (int y = 0; y < yPos; y++)
                {
                    net.cells[x, y].CellsUP += UPcells;
                    
                    Cells[x,y] = new PictureBox();
                    Cells[x, y].Size = new Size(boxes, boxes);
                    Cells[x, y].Location = new Point(x * boxes, 60 + y * boxes);
                    Cells[x, y].SizeMode = PictureBoxSizeMode.StretchImage;
                    if (net.cells[x, y].Element is Rabbit || net.cells[x, y].Element is Wolf)
                    {
                        Cells[x, y].BackColor = ColorChange(10);
                    }
                    Cells[x, y].Image = ImageElem(net.cells[x, y].Element);
           
                    this.Controls.Add(Cells[x, y]);
                }
            }
        }


        //rgb зміна кольору об'єктів, яке відображає іх час до загибелі
        private Color ColorChange(int life)
        {
            return Color.FromArgb(100, (10 - life) * 255 / 10, life * 255 / 10, 0);
        }


        //картинки
        private Image ImageElem(ElementG element)
        {
            if (element is Carrot)
                return Ecosystem.Properties.Resources.carota;
            else if (element is Wolf)
                return Ecosystem.Properties.Resources.lupo;
            else if (element is Rabbit)
                return Ecosystem.Properties.Resources.coniglio;
            return null;
        }


        public void UPcells(object sender, CellsUPEventArgs e)
        {
            BeginInvoke((Action)(() => Cells[e.X, e.Y].Image = ImageElem(e.Elements)));

            var element1 = e.Elements as AnimalSett;
            if(element1 == null)
            {
                BeginInvoke((Action)(() => Cells[e.X, e.Y].BackColor = Color.Transparent));
            }
            else
            {
                BeginInvoke((Action)(() => Cells[e.X, e.Y].BackColor = ColorChange(element1.life)));
            }
        }


        //вивід результатів
        public async void Result(object sender, SimulateUPEventArgs e)
        {
            await Task.Delay(1000);
            Ecosystem3 results = new Ecosystem3(e.rabbitLive, e.wolfLive);
            this.Hide();
            results.ShowDialog();
            this.Close();
        }     

        public void Pause1(object sender, EventArgs e)
        {
            net.Pause();
        }

        public void Play1(object sender, EventArgs e)
        {
            net.Play();
        }

        public void Stop1(object sender, EventArgs e)
        {
            net.Stop();
        }

        private void SimulateClose(object sender, FormClosingEventArgs e)
        {
            if(!net.simulateFinish)
                net.Stop();
        }
    }
}
