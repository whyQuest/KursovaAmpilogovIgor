using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ecosystem
{
    public partial class Setting : UserControl
    {
        private bool InEsecuzione;
        public Setting()
        {
            InitializeComponent();
            InEsecuzione = false;
        }

        public event EventHandler Pause;
        public event EventHandler Play;
        public event EventHandler Stop;
        private void btnPlayPause_Click(object sender, EventArgs e)
        {
            if (InEsecuzione)
            {
                btnPlayPause.Image = Ecosystem.Properties.Resources.play_button;

                Pause?.Invoke(this, null);
            }
            else
            {
                btnPlayPause.Image = Ecosystem.Properties.Resources.pause;

                Play?.Invoke(this, null);
            }
            InEsecuzione = !InEsecuzione;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Stop?.Invoke(this, null);
        }
    }
}
