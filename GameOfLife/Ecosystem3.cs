using System;
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
    public partial class Ecosystem3 : Form
    {
        public Ecosystem3(int RabbitLive, int WolfLive)
        {
            InitializeComponent();

            if(RabbitLive == 0 && WolfLive == 0)
            {
                resultTOP.Text = "Нічия!";
            }
            else if(RabbitLive == 0)
            {
                resultTOP.Text = "Перемогли вовки!";
            }
            else if (WolfLive == 0)
            {
                resultTOP.Text = "Перемогли кролики!";
            }
            else
            {
                resultTOP.Text = "Гра перервана! ";
            }

            RabbitTOP.Text = RabbitLive + " кролик(ів)";
            WolfTOP.Text = WolfLive + " вовк(ів)";
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            Ecosystem1 Game = new Ecosystem1();
            this.Hide();
            Game.ShowDialog();
            this.Close();
        }
    }
}
