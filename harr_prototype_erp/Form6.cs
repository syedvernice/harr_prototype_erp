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
            // TODO: This line of code loads data into the 'dMPSchoolDataSet3.students' table. You can move, or remove it, as needed.
            this.studentsTableAdapter.Fill(this.dMPSchoolDataSet3.students);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (s_name.Text == "" || s_age.Text == "" || s_class.SelectedItem == null  || s_gender.Text == "" ||  s_section.Text == "")
            {
                MessageBox.Show("Please Enter all the credentials");
            }



            else
            {
                string query = "insert into students values(@name,@age,@grade_level,@gender,@section)";
                using (SqlConnection conn = new SqlConnection(connection))
                {

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        
                        cmd.Parameters.AddWithValue("@name", s_name.Text);
                        cmd.Parameters.AddWithValue("@age", s_age.Text);
                        cmd.Parameters.AddWithValue("@grade_level", s_class.SelectedItem);
                        cmd.Parameters.AddWithValue("@gender", s_gender.Text);
                        
                        cmd.Parameters.AddWithValue("@section", s_section.Text);

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
            s_class.SelectedValue = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            s_gender.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            
            s_section.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
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
            if (s_name.Text == "" || s_age.Text == "" || s_class.SelectedItem == null || s_gender.Text == ""  || s_section.Text == "")
            {
                MessageBox.Show("Please Enter all the credentials");
            }

            else
            {



                string query = "update students set name=@name,age=@age,grade_level=@grade_level,gender=@gender,section=@section where id=@id";
                SqlConnection conn = new SqlConnection(connection);
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                
                cmd.Parameters.AddWithValue("@name", s_name.Text);
                cmd.Parameters.AddWithValue("@age", s_age.Text);
                cmd.Parameters.AddWithValue("@grade_level", s_class.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@gender", s_gender.Text);
                
                cmd.Parameters.AddWithValue("@section", s_section.Text);
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Teacher Edited Successfully! ");
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
            Form2 form2 = new Form2();
            form2.Show();
        }
    }
}
