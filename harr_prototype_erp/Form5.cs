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
        private void Form5_Load(object sender, EventArgs e)
{
    progressBar1.Value = 0; // Reset progress bar at start
    timer1.Start();
}

private void timer1_Tick(object sender, EventArgs e)
{
    if (progressBar1.Value < 100)
    {
        progressBar1.Value += 1;
        label1.Text = progressBar1.Value.ToString() + "%";
    }
    else
    {
        timer1.Stop();
        Form1 loginForm = new Form1();  // Make sure Form1 exists
        loginForm.Show();
        this.Hide(); // Hide current splash form
    }
}

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
