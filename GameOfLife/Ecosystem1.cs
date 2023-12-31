﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ecosystem
{
    public partial class Ecosystem1 : Form
    {
        private int numbRabbit;
        private int numbWolf;
        private int numbCarrot;
        private int xPOS;
        private int yPOS;
        public Ecosystem1()
        {
            InitializeComponent();
            numbCarrot = (int)nudCarrot.Value;
            numbWolf = (int)nudWolf.Value;
            numbRabbit = (int)nudRabbit.Value;
            xPOS = (int)nudXpos.Value;
            yPOS = (int)nudYpos.Value;
        }


        //зчитуємо інфу, яку ввів користувач і передаємо її
        private void play_Click(object sender, EventArgs e)
        {
            numbCarrot = (int)nudCarrot.Value;
            numbWolf = (int)nudWolf.Value;
            numbRabbit = (int)nudRabbit.Value;
            xPOS = (int)nudXpos.Value;
            yPOS = (int)nudYpos.Value;
            Ecosystem2 game = new Ecosystem2(xPOS, yPOS, numbRabbit, numbWolf, numbCarrot, (int)intervallSpawn.Value);
            this.Hide();
            game.ShowDialog();
            this.Close();
        }


        //метод, коли ми змінюємо числа в нашому інтерфейсі
        private void ValueChanged(object sender, EventArgs e)
        {
            int numBox = (int)(nudXpos.Value * nudYpos.Value);
            //перевірка, що об'єктів на полі буде не більше ніж половина від загальної к-ть клітинок
            if((int)(nudCarrot.Value + nudWolf.Value + nudRabbit.Value) > numBox / 2){
                nudCarrot.Value = numbCarrot;
                nudRabbit.Value = numbRabbit;
                nudWolf.Value = numbWolf;
                nudXpos.Value = xPOS;
                nudYpos.Value = yPOS;
            }
            else
            {
                numbCarrot = (int)nudCarrot.Value;
                numbWolf = (int)nudWolf.Value;
                numbRabbit = (int)nudRabbit.Value;
                xPOS = (int)nudXpos.Value;
                yPOS = (int)nudYpos.Value;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void main_Click(object sender, EventArgs e)
        {

        }

        private void Ecosystem1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
