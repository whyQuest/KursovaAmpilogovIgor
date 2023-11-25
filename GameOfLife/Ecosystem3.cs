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
        public Ecosystem3(int ConigliRimasti, int LupiRimasti)
        {
            InitializeComponent();

            if(ConigliRimasti == 0 && LupiRimasti == 0)
            {
                lblRisultato.Text = "Pareggio!";
            }
            else if(ConigliRimasti == 0)
            {
                lblRisultato.Text = "I lupi hanno vinto!";
            }
            else if (LupiRimasti == 0)
            {
                lblRisultato.Text = "I conigli hanno vinto!";
            }
            else
            {
                lblRisultato.Text = "Simulazione interrotta!";
            }

            lblNumConigli.Text = ConigliRimasti + " conigli rimasti";
            lblNumLupi.Text = LupiRimasti + " lupi rimasti";
        }

        private void btnChiudi_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNuovaSimulazione_Click(object sender, EventArgs e)
        {
            Ecosystem1 Avvio = new Ecosystem1();
            this.Hide();
            Avvio.ShowDialog();
            this.Close();
        }
    }
}
