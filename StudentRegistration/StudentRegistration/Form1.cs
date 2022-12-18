using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace StudentRegistration
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            load();
        }

        SqlConnection conn = new SqlConnection("Data Source=LAPTOP-HD64O589\\SQLEXPRESS;Initial Catalog =StudentRegistration; User Id =sa; Password=allah123");
        SqlCommand cmd;
        SqlDataReader read;
        string id;
        bool Mode = true;
        string sql;
        SqlDataAdapter drr;
        // mode true => add else update the record



        public void load()
        {
            try
            {

                sql = "SELECT * FROM student";
                cmd = new SqlCommand(sql, conn);
                conn.Open();

                read = cmd.ExecuteReader();

                

                dataGridView1.Rows.Clear();

                while (read.Read())
                {
                    dataGridView1.Rows.Add(read[0], read[1], read[2], read[3]);
                }
                conn.Close();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void getID(string id)
        {
            sql = "SELECT * FROM student where Id ='"+id+"'";

            cmd = new SqlCommand(sql, conn);
            conn.Open();
            read = cmd.ExecuteReader();
            while (read.Read())
            {
                txtName.Text = read[1].ToString();
                txtCourse.Text = read[2].ToString();
                txtFee.Text = read[3].ToString();





            }
            conn.Close();

        }























        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {



            if(e.ColumnIndex == dataGridView1.Columns["Edit"].Index&&e.RowIndex >= 0)
            {
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                getID(id);
                button1.Text = "Edit";
            }
            else if( (e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex >= 0))
            {
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "delete from student where Id = @id";
                conn.Open();
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleted ");
                conn.Close();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string course = txtCourse.Text;
            string fee = txtFee.Text;

            if(Mode == true)
            {
                sql = "insert into student(studentName,Course,Fee) values(@studentName,@Course,@Fee)";
                conn.Open();
                cmd = new SqlCommand(sql,conn);
                cmd.Parameters.AddWithValue("@studentName", name);
                cmd.Parameters.AddWithValue("@Course",course);
                cmd.Parameters.AddWithValue("@Fee", fee);
                MessageBox.Show("Record Added ");
                cmd.ExecuteNonQuery();

                txtName.Clear();
                txtCourse.Clear();
                txtFee.Clear();
                txtName.Focus();


            }
            else
            {
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "update student set studentName = @studentName , Course = @Course, Fee= @Fee where id =@id";
                conn.Open();
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@studentName", name);
                cmd.Parameters.AddWithValue("@Course", course);
                cmd.Parameters.AddWithValue("@Fee", fee);
                cmd.Parameters.AddWithValue("@Id", id);
                MessageBox.Show("Record Added ");
                cmd.ExecuteNonQuery();

                txtName.Clear();
                txtCourse.Clear();
                txtFee.Clear();
                txtName.Focus();
                button1.Text = "Save";
                Mode = true;
            }
            conn.Close();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            load();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtCourse.Clear();
            txtFee.Clear();
            txtName.Focus();
            button1.Text = "Save";
            Mode = true;
        }
    }
}
