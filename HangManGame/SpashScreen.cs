using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HangManGame
{
    public partial class SpashScreen : Form
    {
        public SpashScreen()
        {
            InitializeComponent();
          //  this.BackColor = Color.Lime;
          //  this.TransparencyKey = Color.Lime;
        }

        public static Timer t;

        public  void SpashScereen_Shown(object sender, EventArgs e)
        {
            t = new Timer();
            t.Interval = 2000;
            t.Start();
            t.Tick += T_Tick;
        }

        public void T_Tick(object sender, EventArgs e)
        {
            t.Stop();
           HangMan f= new HangMan();
            f.Show();
            this.Hide();
        }
    }
}
