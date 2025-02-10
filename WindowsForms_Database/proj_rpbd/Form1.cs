using Microsoft.VisualBasic.Logging;
using System.Threading.Tasks;
using System.Data;
using Npgsql;
namespace proj_rpbd
{
    public partial class Form1 : Form
    {
        string login;
        string password;
        bool flag;
        string status;
        NpgsqlCommand cmd;
        NpgsqlConnection conn = new NpgsqlConnection(@"Server=127.0.0.1;Port=5432;User Id=postgres;Password=;Database=postgres");

        public Form1()
        {
            InitializeComponent();
            flag = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            login = textBox1.Text;
            password = textBox2.Text;
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
                if (row[1].ToString() == login && row[2].ToString() == password) { flag = true; status = row[3].ToString(); break; }
            }
            if (flag)
            {
                Form3 f3 = new Form3(status);
                f3.Show();
                this.Hide();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}