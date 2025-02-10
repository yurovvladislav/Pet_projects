using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.Logging;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace proj_rpbd
{
    public partial class Form3 : Form
    {
        string status;
        string type;
        int column;
        int row;
        int id;
        string table;
        NpgsqlConnection conn = new NpgsqlConnection(@"Server=127.0.0.1;Port=5432;User Id=postgres;Password=;Database=postgres");
        public Form3(string status)
        {
            InitializeComponent();
            this.status = status;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
   
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            type = comboBox1.Text;
            switch (type)
            {
                case "Блюда":
                    show("SELECT * FROM proj_dishes");
                    table = "proj_dishes";
                    break;
                case "Состав":
                    show("SELECT * FROM proj_compound");
                    table = "proj_compound";
                    break;
                case "Ингредиенты":
                    show("SELECT * FROM proj_ingredients");
                    table = "proj_ingredients";
                    break;
                case "Контракты":
                    show("SELECT * FROM proj_contracts");
                    table = "proj_contracts";
                    break;
                case "Поставщики":
                    show("SELECT * FROM proj_providers");
                    table = "proj_providers";
                    break;
            }
            if (status == "manager") { panel1.Visible = true; }
            else if (status == "user") { panel1.Visible = false; }
        }

        public void show(string sql)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            conn.Close();
            dataGridView1.DataSource = dt;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                column = e.ColumnIndex;
                id = Convert.ToInt32(dataGridView1[0, e.RowIndex].Value);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4(type, "Добавить", status, table);
            this.Hide();
            f.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (column > 0)
            {
                Form4 f = new Form4(type, "Обновить", status, column, id, table);
                this.Hide();
                f.Show();
            }
            else
            {
                MessageBox.Show("ID нельзя изменять");
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4("Удалить", status, id, table);
            this.Hide();
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

    }
}
