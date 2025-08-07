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
        public string TeacherClass = "";
        public string TeacherID = "";
       
        string connection = @"Data Source=LAPTOP-PLH51RCN\SQLEXPRESS;Initial Catalog=Harr_project;Integrated Security=True";

        public Form3()
        {
            InitializeComponent();
            this.Load += Form3_Load;

        }
        private void LoadStudentsOfClass()
        {
            if (string.IsNullOrWhiteSpace(TeacherClass)) return;

            string filterClass = TeacherClass.Trim(); // e.g., "9" or "9th"

            // Always use the selected year (default is set to "2024")
            string year = comboyear.SelectedItem?.ToString() ?? "2024";
            string yearTable = "Year" + year; // e.g., Year2024

            // Simple query with implicit join to get student info + marks
            string query = $@"
        SELECT s.Student_ID, s.Name, s.Age, s.Gender, s.Grade_Level,
               y.Mathematics_score, y.Science, y.Social_Science_Score,
               y.Computer_Science_Score, y.English_Score, y.Urdu_Score
        FROM Students s, {yearTable} y
        WHERE s.Student_ID = y.Student_ID
          AND s.Grade_Level = @Class
    ";

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Class", filterClass);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    datagrid_student.DataSource = dt;
                }
            }
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

            //MessageBox.Show("TeacherClass is: [" + TeacherClass + "]");

            this.year2024TableAdapter1.Fill(this.harr_projectDataSet.Year2024);


            // year selector setup
            comboyear.SelectedIndexChanged += ComboYear_SelectedIndexChanged;
            comboyear.SelectedItem = "2024";

            // load only students of the teacher's class passed from Form8
            LoadStudentsOfClass();



        }
        private void ComboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (comboyear.SelectedItem != null)
            //{
            //    string selectedYear = comboyear.SelectedItem.ToString();
            //   LoadYearData(selectedYear);
            //}
            LoadStudentsOfClass();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form8 f8 = new Form8(TeacherID);
            f8.Show();
            this.Close();
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
                    button3.PerformClick();
                }
            }

            MessageBox.Show("Student not found.");
        }
        private void LoadYearData(string year)
        {
            string connection = @"Data Source=LAPTOP-PLH51RCN\SQLEXPRESS;Initial Catalog=Harr_project;Integrated Security=True";
            string tableName = $"[Year{year}]";
            string query = $"SELECT * FROM {tableName} where Grade_Level=10";


            using (SqlConnection conn = new SqlConnection(connection))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                datagrid_student.DataSource = dt;
            }

        }
        //edit
        private void button7_Click(object sender, EventArgs e)
        {
            name.Text = datagrid_student.CurrentRow.Cells[1].Value.ToString();
            maths.Text = datagrid_student.CurrentRow.Cells[2].Value.ToString();
            science.Text = datagrid_student.CurrentRow.Cells[3].Value.ToString();
            sst.Text = datagrid_student.CurrentRow.Cells[4].Value.ToString();
            computer.Text = datagrid_student.CurrentRow.Cells[5].Value.ToString();
            english.Text = datagrid_student.CurrentRow.Cells[6].Value.ToString();
            urdu.Text = datagrid_student.CurrentRow.Cells[7].Value.ToString();
        }
        //update
        private void button9_Click(object sender, EventArgs e)
        {
            if (maths.Text == "" || science.Text == "" || sst.Text == "" || computer.Text == "" || english.Text == "" || urdu.Text == "")
            {
                MessageBox.Show("Please Enter all the credentials");
            }

            else
            {



                string query = "update  Year2024 set Mathematics_score=@maths,Science=@science ,Social_Science_Score=@sst,Computer_Science_Score=@computer,English_Score=@english,Urdu_Score=@urdu where Student_ID=@id";
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
                MessageBox.Show("Student Edited Successfully! ");
                conn.Close();
                button3.PerformClick();
                ClearInputs();

            }
        }


        // view
        private void button3_Click(object sender, EventArgs e)
        {
            //string query = "select * from Year2024";
            //SqlConnection conn = new SqlConnection(connection);
            //SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            //DataTable dt = new DataTable();
            //adapter.Fill(dt);
            //datagrid_student.DataSource = dt;
            LoadStudentsOfClass();
        }

        private void comboyear_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboyear.SelectedItem != "2024")
            {
                b_edit.Enabled = false;
                b_update.Enabled = false;
                p_blue.Enabled = false;
                maths.Enabled = false;
                english.Enabled = false;
                sst.Enabled = false;
                computer.Enabled = false;
                urdu.Enabled = false;
                science.Enabled = false;
            }
            else
            {
                b_edit.Enabled = true;
                b_update.Enabled = true;
                p_blue.Enabled = true;
                maths.Enabled = true;
                english.Enabled = true;
                sst.Enabled = true;
                computer.Enabled = true;
                urdu.Enabled = true;
                science.Enabled = true;
            }
        }

        private void datagrid_student_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void ClearInputs()
        {
            name.Text = "";
            maths.Text = "";
            science.Text = "";
            sst.Text = "";
            computer.Text = "";
            english.Text = "";
            urdu.Text = "";

            datagrid_student.ClearSelection(); // Optional: unselect any selected student row
            name.Focus();
        }
    }
}