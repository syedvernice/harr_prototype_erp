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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 p = new Form2();
            p.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form6 f6 = new Form6();
            f6.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form7 f7 = new Form7();
            f7.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string info = "Software Support Team\n\n" +
                 "Email: support@harr.com\n" +
                 "Phone: 9999999999\n" +
                 "Website: www.harr.com\n" +
                 "Working Hours: 9 AM - 6 PM (Mon - Sat)";

            MessageBox.Show(info, "Contact Software Team", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            profile pr = new profile();
            pr.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
