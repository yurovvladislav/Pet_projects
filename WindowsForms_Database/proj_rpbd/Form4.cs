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
    public partial class Form4 : Form
    {
        string type;
        string operation;
        string status;
        string table;
        int column;
        int id;
        bool check = true;
        TextBox txt;
        DataRow row;
        Label lbl;
        List<TextBox> list = new List<TextBox>();
        List<Label> labs = new List<Label>();
        NpgsqlConnection conn = new NpgsqlConnection(@"Server=127.0.0.1;Port=5432;User Id=postgres;Password=;Database=postgres");
        public Form4(string type, string operation, string status, string table)
        {
            InitializeComponent();
            this.type = type;
            this.operation = operation;
            this.status = status;
            this.table = table;
        }

        public Form4(string type, string operation, string status, int column, int id, string table)
        {
            InitializeComponent();
            this.type = type;
            this.operation = operation;
            this.status = status;
            this.column = column;
            this.id = id;
            this.table = table;
        }

        public Form4(string operation, string status, int id, string table)
        {
            InitializeComponent();
            this.operation = operation;
            this.status = status;
            this.id = id;
            this.table = table;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            string sql;
            NpgsqlCommand cmd;
            if (operation == "Добавить")
            {
                switch (type)
                {
                    case "Блюда":
                        for (int i = 0; i < 2; i++)
                        {
                            txt = new TextBox();
                            list.Add(txt);
                            list[list.Count - 1].Top = 25 + (i * 30);
                            list[list.Count - 1].Left = 5;
                            this.Controls.Add(list[list.Count - 1]);
                            show2(table);
                        }

                        break;
                    case "Состав":
                        for (int i = 0; i < 3; i++)
                        {
                            txt = new TextBox();
                            list.Add(txt);
                            list[list.Count - 1].Top = 25 + (i * 30);
                            list[list.Count - 1].Left = 5;
                            this.Controls.Add(list[list.Count - 1]);
                            show2(table);
                        }
                        break;
                    case "Ингредиенты":
                        for (int i = 0; i < 6; i++)
                        {
                            txt = new TextBox();
                            list.Add(txt);
                            list[list.Count - 1].Top = 25 + (i * 30);
                            list[list.Count - 1].Left = 5;
                            this.Controls.Add(list[list.Count - 1]);
                            show2(table);
                        }
                        break;
                    case "Контракты":
                        for (int i = 0; i < 2; i++)
                        {
                            txt = new TextBox();
                            list.Add(txt);
                            list[list.Count - 1].Top = 25 + (i * 30);
                            list[list.Count - 1].Left = 5;
                            this.Controls.Add(list[list.Count - 1]);
                            show2(table);
                        }
                        break;
                    case "Поставщики":
                        txt = new TextBox();
                        list.Add(txt);
                        list[list.Count - 1].Top = 25;
                        list[list.Count - 1].Left = 5;
                        this.Controls.Add(list[list.Count - 1]);
                        show2(table);
                        break;
                }
            }    
            else if (operation == "Обновить")
            {
                txt = new TextBox();
                list.Add(txt);
                list[list.Count - 1].Top = 25;
                list[list.Count - 1].Left = 5;
                this.Controls.Add(list[list.Count - 1]);
                sql = "SELECT column_name, data_type FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name = (:_table) AND ordinal_position = (:_op)";
                conn.Open();
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_table", table);
                cmd.Parameters.AddWithValue("_op", column + 1);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                conn.Close();
                row = dt.Rows[0];
            }
            else if (operation == "Удалить")
            {
                label1.Visible = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sql;
            NpgsqlCommand cmd;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Text == "") { check = false; break; }
            }
            if (!check)
            {
                MessageBox.Show("Должны быть заполнены все поля");
            }
            else if (check)
            {
                switch (operation)
                {
                    case "Добавить":
                        switch (type)
                        {
                            case "Блюда":
                                sql = "INSERT INTO proj_dishes (name, cost) VALUES ((:_name), (:_cost))";
                                conn.Open();
                                cmd = new NpgsqlCommand(sql, conn);
                                cmd.Parameters.AddWithValue("_name", list[0].Text);
                                cmd.Parameters.AddWithValue("_cost", Convert.ToInt32(list[1].Text));
                                cmd.ExecuteScalar();
                                conn.Close();
                                break;
                            case "Состав":
                                sql = "INSERT INTO proj_compound (id_dish, id_ing, quantity) VALUES ((:_dish), (:_iding), (:_quantity))";
                                conn.Open();
                                cmd = new NpgsqlCommand(sql, conn);
                                cmd.Parameters.AddWithValue("_dish", Convert.ToInt32(list[0].Text));
                                cmd.Parameters.AddWithValue("_iding", Convert.ToInt32(list[1].Text));
                                cmd.Parameters.AddWithValue("_quantity", Convert.ToInt32(list[2].Text));
                                cmd.ExecuteScalar();
                                conn.Close();
                                break;
                            case "Ингредиенты":
                                sql = "INSERT INTO proj_ingredients (name, id_contract, prod_date, cost, quantity, exp) VALUES ((:_name), (:_idcontr), (:_prdate), (:_cost), (:_quantity), (:_exp))";
                                conn.Open();
                                cmd = new NpgsqlCommand(sql, conn);
                                cmd.Parameters.AddWithValue("_name", list[0].Text);
                                cmd.Parameters.AddWithValue("_idcontr", Convert.ToInt32(list[1].Text));
                                cmd.Parameters.AddWithValue("_prdate", Convert.ToDateTime(list[2].Text));
                                cmd.Parameters.AddWithValue("_cost", Convert.ToInt32(list[3].Text));
                                cmd.Parameters.AddWithValue("_quantity", Convert.ToDecimal(list[4].Text));
                                cmd.Parameters.AddWithValue("_exp", Convert.ToDateTime(list[5].Text));
                                cmd.ExecuteScalar();
                                conn.Close();
                                break;
                            case "Контракты":
                                sql = "INSERT INTO proj_contracts (id_provider, date) VALUES ((:_idprov), (:_date))";
                                conn.Open();
                                cmd = new NpgsqlCommand(sql, conn);
                                cmd.Parameters.AddWithValue("_idprov", Convert.ToInt32(list[0].Text));
                                cmd.Parameters.AddWithValue("_date", Convert.ToDateTime(list[1].Text));
                                cmd.ExecuteScalar();
                                conn.Close();
                                break;
                            case "Поставщики":
                                sql = "INSERT INTO proj_providers (name) VALUES ((:_name))";
                                conn.Open();
                                cmd = new NpgsqlCommand(sql, conn);
                                cmd.Parameters.AddWithValue("_name", list[0].Text);
                                cmd.ExecuteScalar();
                                conn.Close();
                                break;
                        }
                        break;
                    case "Обновить":
                        sql = $"UPDATE {table} SET {row[0]} = (:_data) WHERE id = (:_id)";
                        conn.Open();
                        cmd = new NpgsqlCommand(sql, conn);
                        if (row[1].ToString() == "character varying")
                        {
                            cmd.Parameters.AddWithValue("_data", list[0].Text);
                        }
                        else if (row[1].ToString() == "integer")
                        {
                            cmd.Parameters.AddWithValue("_data", Convert.ToInt32(list[0].Text));
                        }
                        else if (row[1].ToString() == "date")
                        {
                            cmd.Parameters.AddWithValue("_data", Convert.ToDateTime(list[0].Text));
                        }
                        cmd.Parameters.AddWithValue("_id", id);
                        cmd.ExecuteScalar();
                        conn.Close();
                        break;
                    case "Удалить":
                        sql = $"DELETE from {table} WHERE id = (:_id)";
                        conn.Open();
                        cmd = new NpgsqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("_id", id);
                        cmd.ExecuteScalar();
                        conn.Close();
                        break;

                }
                Form3 f = new Form3(status);
                f.Show();
                this.Hide();
            }
            check = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3(status);
            f.Show();
            this.Hide();
        }

        void show2(string name)
        {
            string sql;
            NpgsqlCommand cmd;
            sql = "SELECT column_name FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name = (:_table)";
            conn.Open();
            cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("_table", name);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            conn.Close();
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                row = dt.Rows[i];
                lbl = new Label();
                labs.Add(lbl);
                labs[labs.Count - 1].Top = 25 + ((i - 1) * 30);
                labs[labs.Count - 1].Left = 120;
                labs[labs.Count - 1].Text = row[0].ToString();
                this.Controls.Add(labs[labs.Count - 1]);
            }
        }
    }
}
