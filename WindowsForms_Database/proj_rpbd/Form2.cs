using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proj_rpbd
{
    public partial class Form2 : Form
    {
        string login;
        string password;
        string user = "user";
        int result = 0;
        NpgsqlCommand cmd;
        NpgsqlConnection conn = new NpgsqlConnection(@"Server=127.0.0.1;Port=5432;User Id=postgres;Password=;Database=postgres");

        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                login = textBox1.Text;
                password = textBox2.Text;

                string sql = @"INSERT into users (login, password, status) VALUES ((:_login), (:_password), (:_status))";
                conn.Open();
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_login", login);
                cmd.Parameters.AddWithValue("_password", password);
                cmd.Parameters.AddWithValue("_status", user);
                cmd.ExecuteScalar();
                conn.Close();
                
                if (check())
                {
                    Form1 f = new Form1();
                    f.Show();
                    this.Hide();
                }
                else
                {
                    label1.Visible = true;
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }

        bool check()
        {
            bool flag = false;
            string sql = @"SELECT * FROM users WHERE login = (:_login) AND password = (:_password)";
            conn.Open();
            cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("_login", login);
            cmd.Parameters.AddWithValue("_password", password);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            conn.Close();
            DataRow row;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                row = dt.Rows[i];
                if (row[1].ToString() == login && row[2].ToString() == password) { flag = true; }
            }
            return flag;
        }
    }
}
