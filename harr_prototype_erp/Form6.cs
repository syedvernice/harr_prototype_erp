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
    public partial class Form6 : Form
    {
        string connection = @"Data Source=LAPTOP-PLH51RCN\SQLEXPRESS;Initial Catalog=Harr_project;Integrated Security=True;TrustServerCertificate=True;";


        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {


            if (s_id.Text == "" || s_name.Text == "" || s_age.Text == "" || s_gender.Text == "" ||
        sgrade.SelectedItem == null || comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please enter all the credentials and select a class.");
                return;
            }
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a class.");
                return;
            }

            string selectedClass = comboBox1.SelectedItem.ToString().Trim(); // used for filtering / Grade_Level
            string gradeLevel = sgrade.SelectedItem.ToString().Trim(); // stored in Students table

            string connectionString = @"Data Source=LAPTOP-PLH51RCN\SQLEXPRESS;Initial Catalog=Harr_project;Integrated Security=True;TrustServerCertificate=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // 1. Insert into overall Students table
                    // update try
                    string insertOverall = @"
                INSERT INTO Students (Student_ID, Name, Age, Gender, Grade_Level)
                VALUES (@id, @name, @age, @gender, @grade)";
                    using (SqlCommand cmd = new SqlCommand(insertOverall, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", s_id.Text.Trim());
                        cmd.Parameters.AddWithValue("@name", s_name.Text.Trim());
                        cmd.Parameters.AddWithValue("@age", s_age.Text.Trim());
                        cmd.Parameters.AddWithValue("@gender", s_gender.Text.Trim());
                        cmd.Parameters.AddWithValue("@grade", gradeLevel);
                        cmd.ExecuteNonQuery();
                    }

                    // 2. Always insert into Year2024 (only ID and Name)
                    string insertYear2024 = "INSERT INTO Year2024 (Student_ID, Name) VALUES (@id, @name)";
                    using (SqlCommand cmd2 = new SqlCommand(insertYear2024, conn))
                    {
                        cmd2.Parameters.AddWithValue("@id", s_id.Text.Trim());
                        cmd2.Parameters.AddWithValue("@name", s_name.Text.Trim());
                        cmd2.ExecuteNonQuery();
                    }

                    MessageBox.Show("Student added successfully!");
                    RefreshCurrentClass();
                    ClearInputs();


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding student: " + ex.Message);
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            s_id.Text= dataGridView1.CurrentRow.Cells[0].Value.ToString();
            s_name.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            s_age.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            s_gender.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            sgrade.SelectedItem = dataGridView1.CurrentRow.Cells[4].Value.ToString();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            string query = "select * from students";
            SqlConnection conn = new SqlConnection(connection);
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            string query = "DELETE FROM students WHERE Student_ID = @id";

            if (MessageBox.Show("Are you sure you want to delete this student?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        button9.PerformClick();
                    }
                }

                MessageBox.Show("Student deleted successfully.");
            }
            else
            {
                MessageBox.Show("Deletion cancelled.");
            }
            RefreshCurrentClass();
            ClearInputs();


        }

        private void update_button_Click(object sender, EventArgs e)
        {
            if (s_name.Text == "" || s_age.Text == "" ||s_gender.Text == "" || sgrade.SelectedItem == null)
            {
                MessageBox.Show("Please Enter all the credentials");
            }

            else
            {



                string query = "update students set Name=@name,age=@age,Grade_level=@Grade_Level,gender=@gender where Student_Id=@id";
                SqlConnection conn = new SqlConnection(connection);
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[0].Value.ToString());
      
                cmd.Parameters.AddWithValue("@name", s_name.Text);
                cmd.Parameters.AddWithValue("@age", s_age.Text);
                cmd.Parameters.AddWithValue("@gender", s_gender.Text);
                cmd.Parameters.AddWithValue("@Grade_Level", sgrade.SelectedItem);

                if (comboBox1.SelectedItem == null)
                {
                    MessageBox.Show("Please select a class.");
                    return;
                }


                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student Edited Successfully! ");
                conn.Close();
                button9.PerformClick();

                string filterQuery = "SELECT Student_ID, Name, Age, Gender, Grade_Level FROM Students WHERE Grade_Level = @Class";
                using (SqlCommand cmd3 = new SqlCommand(filterQuery, conn))
                {
                    string selectedClass = comboBox1.SelectedItem.ToString().Trim();
                    cmd3.Parameters.AddWithValue("@Class", selectedClass);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd3))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }

            }
            RefreshCurrentClass();
            ClearInputs();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            string name = searchbox.Text;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value.ToString().Contains(name))
                {
                    row.Selected = true;
                    return;
                    button9.PerformClick();
                }
            }

            MessageBox.Show("Student not found.");
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

        private void button2_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7();
            form7.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form6 f6= new Form6();
            f6.Show();
        }

        private void s_class_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null) return;

            string selectedClass = comboBox1.SelectedItem.ToString().Trim();
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                string query = "SELECT Student_ID, Name, Age, Gender, Grade_Level FROM Students WHERE Grade_Level = @Class";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Class", selectedClass);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
            }

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            string name = searchbox.Text;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value.ToString().Contains(name))
                {
                    row.Selected = true;
                    return;


                }
                button9.PerformClick();
            }

            MessageBox.Show("Student not found.");
        }

        private void sgrade_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void RefreshCurrentClass()
        {
            if (comboBox1.SelectedItem == null) return;
            string grade = comboBox1.SelectedItem.ToString().Trim();
            using (SqlConnection c = new SqlConnection(connection))
            {
                c.Open();
                string q = "SELECT Student_ID, Name, Age, Gender, Grade_Level FROM Students WHERE Grade_Level = @Class";
                using (SqlCommand cmd = new SqlCommand(q, c))
                {
                    cmd.Parameters.AddWithValue("@Class", grade);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
         
           }
        }
        private void ClearInputs()
        {
            s_id.Text = "";
            s_name.Text = "";
            s_age.Text = "";
            s_gender.Text = "";
            sgrade.SelectedItem = null;
            searchbox.Text = "";
            // if you want to deselect any current row:
            dataGridView1.ClearSelection();
        }
    }
}
