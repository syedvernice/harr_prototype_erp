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
using static System.Collections.Specialized.BitVector32;
using System.Xml.Linq;

namespace harr_prototype_erp
{
    public partial class Form3 : Form
    {
        string connection = @"Data Source=SYEDVERNICE-9SL\SQLEXPRESS;Initial Catalog=DMPSchool;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public Form3()
        {
            InitializeComponent();

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dMPSchoolDataSet11.Year2024' table. You can move, or remove it, as needed.
            this.year2024TableAdapter.Fill(this.dMPSchoolDataSet11.Year2024);
            // TODO: This line of code loads data into the 'dMPSchoolDataSet10.Year2023' table. You can move, or remove it, as needed.
            this.year2023TableAdapter.Fill(this.dMPSchoolDataSet10.Year2023);
            // TODO: This line of code loads data into the 'dMPSchoolDataSet9.Year2022' table. You can move, or remove it, as needed.
            this.year2022TableAdapter.Fill(this.dMPSchoolDataSet9.Year2022);
            // TODO: This line of code loads data into the 'dMPSchoolDataSet2.students' table. You can move, or remove it, as needed.
            //this.studentsTableAdapter.Fill(this.dMPSchoolDataSet2.students);

            comboyear.SelectedIndexChanged += ComboYear_SelectedIndexChanged;
            comboyear.SelectedItem = "2024";

        }
        private void ComboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboyear.SelectedItem != null)
            {
                string selectedYear = comboyear.SelectedItem.ToString();
                LoadYearData(selectedYear);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

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

        private void button5_Click(object sender, EventArgs e)
        {
            profile pr = new profile();
            pr.Show();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            string name = searchbox.Text;

            foreach (DataGridViewRow row in datagrid_student.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value.ToString().Contains(name))
                {
                    row.Selected = true;
                    return;
                }
            }

            MessageBox.Show("Teacher not found.");
        }
        private void LoadYearData(string year)
        {
            string connection = @"Data Source=SYEDVERNICE-9SL\SQLEXPRESS;Initial Catalog=DMPSchool;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
            string tableName = $"[Year{year}]";  
            string query = $"SELECT * FROM {tableName}";
            

            using (SqlConnection conn = new SqlConnection(connection))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                datagrid_student.DataSource = dt;
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            name.Text= datagrid_student.CurrentRow.Cells[1].Value.ToString();
            maths.Text = datagrid_student.CurrentRow.Cells[2].Value.ToString();
            science.Text = datagrid_student.CurrentRow.Cells[3].Value.ToString();
            sst.Text = datagrid_student.CurrentRow.Cells[4].Value.ToString();
            computer.Text = datagrid_student.CurrentRow.Cells[5].Value.ToString();
            english.Text = datagrid_student.CurrentRow.Cells[6].Value.ToString();
            urdu.Text = datagrid_student.CurrentRow.Cells[7].Value.ToString();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (maths.Text == "" || science.Text == "" || sst.Text == "" || computer.Text == "" || english.Text == "" || urdu.Text == "")
            {
                MessageBox.Show("Please Enter all the credentials");
            }

            else
            {



                string query = "update  Year2024 set Mathematics=@maths,Science=@science ,Social_Science=@sst,Computer_Science=@computer,English=@english,Urdu=@urdu where Student_ID=@id";
                SqlConnection conn = new SqlConnection(connection);
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", datagrid_student.CurrentRow.Cells[0].Value.ToString());

                cmd.Parameters.AddWithValue("@name", name.Text);
                cmd.Parameters.AddWithValue("@maths", maths.Text);
                cmd.Parameters.AddWithValue("@science", science.Text);
                cmd.Parameters.AddWithValue("@sst", sst.Text);
                cmd.Parameters.AddWithValue("@computer", computer.Text);
                cmd.Parameters.AddWithValue("@english", english.Text);
                cmd.Parameters.AddWithValue("@urdu", urdu.Text);
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Teacher Edited Successfully! ");
                conn.Close();
                button3.PerformClick();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string query = "select * from Year2024";
            SqlConnection conn = new SqlConnection(connection);
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            datagrid_student.DataSource = dt;
        }
    }
}
