using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace harr_prototype_erp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
    }

        private void button1_Click(object sender, EventArgs e)
        {

            String user = textBox1.Text;
            String password = textBox2.Text;
            if (comboBox1.SelectedItem == "Admin")

            {
                if (user == "" || password == "")
                {
                    label5.Text = "Please enter your credentials";
                }

                else if (user == "abc" && password == "123")
                {
                    label5.Text = "Welcome";

                    Form7 p = new Form7();
                    p.Show();
                    this.Hide();
                }
                else
                {
                    label5.Text = "Incorrect Credentials";
                }
            }

            else if (comboBox1.SelectedItem == "User")
            {
                
                if (user == "" || password == "")
                {
                    label5.Text = "Please enter your credentials";
                }

                else if (user != "" && password != "")
                {
                    string connection = @"Data Source=SYEDVERNICE-9SL\SQLEXPRESS;Initial Catalog=DMPSchool;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                    using (SqlConnection conn = new SqlConnection(connection))
                    {

                        string query = "SELECT * FROM teachers where teacher_id=@id AND teacher_password=@password";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", user);
                            cmd.Parameters.AddWithValue("@password", password);
                            conn.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            if (reader.HasRows)
                            {
                                Form8 f8 = new Form8();
                                f8.Show();
                                this.Hide();
                               // SqlDataReader reader = cmd.ExecuteReader();

                                //string query1 ="Select  teacher_name=@name,teacher_class=@class,teacher_email=@mail,teacher_phone=@phone where teacher_d=@id"
                                //l_name.Text = reader["teacher_name"].ToString();
                                //l_class.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                                //l_mail.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                                //l_phone.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                            }
                            else
                            {
                                label5.Text = "Incorrect credentials";
                            }

                        }
                    }
                }
                
            
            }
            else
            {
                label5.Text = "Select a Role";
            }

        }
        

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
