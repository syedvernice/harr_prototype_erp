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
    public partial class Form2 : Form
    {
        string connection = @"Data Source=SYEDVERNICE-9SL\SQLEXPRESS;Initial Catalog=DMPSchool;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {// TODO: This line of code loads data into the 'dMPSchoolDataSet16.teachers' table. You can move, or remove it, as needed.
            this.teachersTableAdapter2.Fill(this.dMPSchoolDataSet16.teachers);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            profile pr = new profile();
            pr.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {

            // TODO: This line of code loads data into the 'dMPSchoolDataSet12.teachers' table. You can move, or remove it, as needed.
            this.teachersTableAdapter1.Fill(this.dMPSchoolDataSet12.teachers);
            // TODO: This line of code loads data into the 'dMPSchoolDataSet.teachers' table. You can move, or remove it, as needed.
            //this.teachersTableAdapter.Fill(this.dMPSchoolDataSet.teachers);


            
            if (t_password.Text == "" ||t_class.SelectedItem==null||t_email.Text == "" || t_name.Text=="" || t_phone.Text=="")
            {
                MessageBox.Show("Please Enter all the credentials");
            }



            else {
                        string query = "insert into teachers values(@password,@name,@class,@email,@phone)";
                        using (SqlConnection conn = new SqlConnection(connection))
                        {

                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@password", t_password.Text);
                                cmd.Parameters.AddWithValue("@name", t_name.Text);
                                cmd.Parameters.AddWithValue("@class", t_class.SelectedItem);
                                cmd.Parameters.AddWithValue("@email", t_email.Text);
                                cmd.Parameters.AddWithValue("@phone", t_phone.Text);
                                conn.Open();
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Teacher Added Successfully! ");
                                conn.Close();
                                button9.PerformClick();
                            }
                        }
              }
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string query = "select * from teachers";
            SqlConnection conn = new SqlConnection(connection);
            SqlDataAdapter adapter =new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void update_button_Click(object sender, EventArgs e)
        {
            if (t_password.Text == "" || t_class.SelectedItem.ToString() == null || t_email.Text == "" || t_name.Text == "" || t_phone.Text == "")
            {
                MessageBox.Show("Please Enter all the credentials");
            }

            else
            {



                string query = "update teachers set teacher_password=@password,teacher_name=@name,teacher_class=@class,teacher_email=@email,teacher_phone=@phone where teacher_id=@id";
                SqlConnection conn = new SqlConnection(connection);
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                cmd.Parameters.AddWithValue("@password", t_password.Text);
                cmd.Parameters.AddWithValue("@name", t_name.Text);
                cmd.Parameters.AddWithValue("@class", t_class.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@email", t_email.Text);
                cmd.Parameters.AddWithValue("@phone", t_phone.Text);
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Teacher Edited Successfully! ");
                conn.Close();
                button9.PerformClick();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

            t_password.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            t_name.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            t_class.SelectedItem = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            t_email.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            t_phone.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            string query = "DELETE FROM teachers WHERE teacher_id = @id";

            if (MessageBox.Show("Are you sure you want to delete this teacher?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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

                MessageBox.Show("Teacher deleted successfully.");
            }
            else
            {
                MessageBox.Show("Deletion cancelled.");
            }


        }

        private void button6_Click(object sender, EventArgs e)
        {

            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }

        private void search_button_Click(object sender, EventArgs e)
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

            MessageBox.Show("Teacher not found.");
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

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

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
                button9.PerformClick();
            }

            MessageBox.Show("Teacher not found.");
        }

        private void searchbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();  
            form6.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7();
            form7.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

