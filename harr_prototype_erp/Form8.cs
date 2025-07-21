using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace harr_prototype_erp
{
    public partial class Form8 : Form
    {
        private string _teacherID = null;
        string connection = @"Data Source=SYEDVERNICE-9SL\SQLEXPRESS;Initial Catalog=DMPSchool;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        
        public Form8(string teacher_ID)
        {
            InitializeComponent();
            _teacherID = teacher_ID;

           
            this.Load += new EventHandler(Form8_Load);
        }
        public Form8()
        {
            InitializeComponent();
            // No teacher data loaded here
        }
        private void Form8_Load(object sender, EventArgs e)
        {
            if (_teacherID == null)
                return;
            using (SqlConnection conn = new SqlConnection(connection))
            {
                string query = "SELECT * FROM teachers WHERE teacher_id = @id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", _teacherID);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        
                        
                        l_name.Text = reader["teacher_name"].ToString();
                        l_class.Text = reader["teacher_class"].ToString();
                        l_mail.Text = reader["teacher_email"].ToString();
                        l_phone.Text = reader["teacher_phone"].ToString();
                        
                    }
                    else
                    {
                        MessageBox.Show("Teacher details not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void label18_Click(object sender, EventArgs e) { }

        private void label11_Click(object sender, EventArgs e) { }

        private void label12_Click(object sender, EventArgs e) { }

        private void label20_Click(object sender, EventArgs e) { }

        private void label10_Click(object sender, EventArgs e) { }

        private void label14_Click(object sender, EventArgs e) { }

        private void pictureBox6_Click(object sender, EventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {
            string info = "Software Support Team\n\n" +
                          "Email: support@harr.com\n" +
                          "Phone: 9999999999\n" +
                          "Website: www.harr.com\n" +
                          "Working Hours: 9 AM - 6 PM (Mon - Sat)";

            MessageBox.Show(info, "Contact Software Team", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void chart1_Click(object sender, EventArgs e) { }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void button2_Click(object sender, EventArgs e) { }

        private void label15_Click(object sender, EventArgs e) { }

        private void l_name_Click(object sender, EventArgs e) { }
    }
}
