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
        string connection = @"Data Source=SYEDVERNICE-9SL\SQLEXPRESS;Initial Catalog=DMPSchool;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dMPSchoolDataSet13.students' table. You can move, or remove it, as needed.
            this.studentsTableAdapter1.Fill(this.dMPSchoolDataSet13.students);
            // TODO: This line of code loads data into the 'dMPSchoolDataSet3.students' table. You can move, or remove it, as needed.
            //this.studentsTableAdapter.Fill(this.dMPSchoolDataSet3.students);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dMPSchoolDataSet13.students' table. You can move, or remove it, as needed.
            this.studentsTableAdapter1.Fill(this.dMPSchoolDataSet13.students);
            // TODO: This line of code loads data into the 'dMPSchoolDataSet3.students' table. You can move, or remove it, as needed.
            //this.studentsTableAdapter.Fill(this.dMPSchoolDataSet3.students);

            if (s_name.Text == "" || s_age.Text == "" || s_gender.Text == "" || sgrade.SelectedItem==null)
            {
                MessageBox.Show("Please Enter all the credentials");
            }



            else
            {
                string query = "insert into students values(@name,@age,@gender,@Grade_Level)";
                using (SqlConnection conn = new SqlConnection(connection))
                {

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        
                        cmd.Parameters.AddWithValue("@name", s_name.Text);
                        cmd.Parameters.AddWithValue("@age", s_age.Text);
                        cmd.Parameters.AddWithValue("@gender", s_gender.Text);
                        cmd.Parameters.AddWithValue("@Grade_Level", sgrade.SelectedItem);



                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Student Added Successfully! ");
                        conn.Close();
                        button9.PerformClick();
                    }
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            s_name.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            s_age.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            s_gender.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            
            
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
            string query = "DELETE FROM students WHERE id = @id";

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
        }

        private void update_button_Click(object sender, EventArgs e)
        {
            if (s_name.Text == "" || s_age.Text == "" ||s_gender.Text == "" || sgrade.SelectedItem == null)
            {
                MessageBox.Show("Please Enter all the credentials");
            }

            else
            {



                string query = "update students set name=@name,age=@age,Grade_level=@Grade_Level,gender=@gender,section=@section where id=@id";
                SqlConnection conn = new SqlConnection(connection);
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                
                cmd.Parameters.AddWithValue("@name", s_name.Text);
                cmd.Parameters.AddWithValue("@age", s_age.Text);
                cmd.Parameters.AddWithValue("@gender", s_gender.Text);
                cmd.Parameters.AddWithValue("@Grade_Level", s_gender.Text);
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student Edited Successfully! ");
                conn.Close();
                button9.PerformClick();
            }
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
            // string selectedClass = comboBox1.SelectedItem.ToString();
            // SqlConnection con = new SqlConnection(@"Data Source=SYEDVERNICE-9SL\SQLEXPRESS;Initial Catalog=DMPSchool;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
            // {
            // con.Open();
            //string query = "SELECT Student_ID, Name, Age, Gender, Grade_Level FROM Students WHERE Grade_Level = @Class";
            //SqlCommand cmd = new SqlCommand(query, con);
            //cmd.Parameters.AddWithValue("@Class", selectedClass);

            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //da.Fill(dt);

            //dataGridView1.DataSource = dt;
            //}

            // con.Close();

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
    }
}
