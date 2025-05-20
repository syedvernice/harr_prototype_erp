using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace harr_prototype_erp
{
    public partial class splashscreen : Form
    {
        public splashscreen()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            this.Hide(); // or this.Close() if you prefer
            Form1 loginForm = new Form1();  // your login form
            loginForm.Show();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
