namespace Планировщик
{
    public partial class Form1 : Form
    {
        int len;
        int wid;
        int lenght;
        int width;
        int xx;
        int yy;
        int dlina;
        int xm;
        int ym;
        int isexist;
        int regim;
        Color col;
        Graphics g;
        Panel pan;
        List<Furniture> list = new List<Furniture>();
        List<WD> list2 = new List<WD>();
        List<PictureBox> list3 = new List<PictureBox>();
        public void Drawlines()
        {
            SolidBrush b = new SolidBrush(Color.Black);
            g.FillRectangle(b, 0, 0, wid + 4, 2);
            g.FillRectangle(b, 0, 0, 2, len + 4);
            g.FillRectangle(b, 0, len + 2, wid + 4, 2);
            g.FillRectangle(b, wid + 2, 0, 2, len + 4);           
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            regim = 0;
        }
        private void MouseDown2(object sender, MouseEventArgs e)
        {
            if (regim == 1 && e.Button == MouseButtons.Left)
            {
                foreach (var item in list2)
                {
                    if (item.Click(e.X, e.Y)) { list2.Remove(item); break; }
                    
                }
                g.Clear(BackColor);
                Drawlines();
                foreach(var item in list2)
                {
                   item.draw(g);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            if (numericUpDown1.Value == 0 || numericUpDown2.Value == 0)
            {
                MessageBox.Show("Значения не должны быть равны нулю");
            }
            else
            {
                if (isexist == 1) pan.Dispose();
                numericUpDown3.Maximum = wid * 2;
                numericUpDown4.Maximum = len * 2;
                panel2.Visible = true;
                pan = new Panel();
                pan.Width = wid + 4;
                pan.Height = len + 4;
                pan.Location = new Point(414, 52);
                Controls.Add(pan);              
                g = pan.CreateGraphics();
                pan.MouseDown += new System.Windows.Forms.MouseEventHandler(MouseDown2);
                Drawlines();
                isexist = 1;
            }
            

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            len = Convert.ToInt32(numericUpDown1.Value) / 2;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            wid = Convert.ToInt32(numericUpDown2.Value) / 2;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            xx = Convert.ToInt32(numericUpDown3.Value) / 2;
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            yy = Convert.ToInt32(numericUpDown4.Value) / 2;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!radioButton1.Checked && !radioButton2.Checked)
            {
                MessageBox.Show("Введены не все значения");
            }
            else
            {

                if (radioButton1.Checked) col = BackColor;
                if (radioButton2.Checked) col = Color.Cyan;
                if (radioButton3.Checked)
                {
                    if (yy == 0) list2.Add(new WD(xx + 2, yy, dlina, 2, col));
                    if (yy == len) list2.Add(new WD(xx + 2, yy + 2, dlina, 2, col));
                    list2[list2.Count - 1].draw(g);
                }
                else if (radioButton4.Checked)
                {
                    if (xx == 0) list2.Add(new WD(xx, yy + 2, 2, dlina, col));
                    if (xx == wid) list2.Add(new WD(xx + 2, yy + 2, 2, dlina, col));
                    list2[list2.Count - 1].draw(g);
                }
                else MessageBox.Show("Введены не все значения");

            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown7_ValueChanged(object sender, EventArgs e)
        {
            dlina = Convert.ToInt32(numericUpDown7.Value);
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            width = Convert.ToInt32(numericUpDown5.Value) / 2;
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            lenght = Convert.ToInt32(numericUpDown6.Value) / 2;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pp = (PictureBox)sender;
            pp.BringToFront();
            if (regim == 0)
            {
                xm = e.X;
                ym = e.Y;
            }
            else if (regim == 1)
            {
                pp.Dispose();
                list.RemoveAt(list3.IndexOf(pp));
                list3.Remove(pp);
            }
        }
        private void MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox pp = (PictureBox)sender;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (e.X + pp.Left - xm >= 3 && e.X + pp.Left - xm <= wid - pp.Width - 0) pp.Left = e.X + pp.Left - xm;
                if (e.Y + pp.Top - ym >= 3 && e.Y + pp.Top - ym <= len - pp.Height - 0) pp.Top = e.Y + pp.Top - ym;
            }
        }
        private void MouseUp(object sender, MouseEventArgs e)
        {
            PictureBox pp = (PictureBox)sender;
            xm = ym = 0;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (numericUpDown5.Value == 0 || numericUpDown6.Value == 0)
            {
                MessageBox.Show("Значения не должны быть равны 0");
            }
            else
            {
                switch (comboBox1.SelectedItem)
                {
                    case "Стол":
                        list.Add(new Table(5, 5, width, lenght));
                        list[list.Count - 1].pb.MouseDown += new System.Windows.Forms.MouseEventHandler(MouseDown);
                        list[list.Count - 1].pb.MouseMove += new System.Windows.Forms.MouseEventHandler(MouseMove);
                        list[list.Count - 1].pb.MouseUp += new System.Windows.Forms.MouseEventHandler(MouseUp);
                        pan.Controls.Add(list[list.Count - 1].pb);
                        list3.Add(list[list.Count - 1].pb);
                        break;
                    case "Стул":
                        list.Add(new Chair(3, 3, width, lenght));
                        pan.Controls.Add(list[list.Count - 1].pb);
                        list[list.Count - 1].pb.MouseDown += new System.Windows.Forms.MouseEventHandler(MouseDown);
                        list[list.Count - 1].pb.MouseMove += new System.Windows.Forms.MouseEventHandler(MouseMove);
                        list[list.Count - 1].pb.MouseUp += new System.Windows.Forms.MouseEventHandler(MouseUp);
                        list3.Add(list[list.Count - 1].pb);
                        break;
                    case "Кровать":
                        list.Add(new Bed(3, 3, width, lenght));
                        pan.Controls.Add(list[list.Count - 1].pb);
                        list[list.Count - 1].pb.MouseDown += new System.Windows.Forms.MouseEventHandler(MouseDown);
                        list[list.Count - 1].pb.MouseMove += new System.Windows.Forms.MouseEventHandler(MouseMove);
                        list[list.Count - 1].pb.MouseUp += new System.Windows.Forms.MouseEventHandler(MouseUp);
                        list3.Add(list[list.Count - 1].pb);
                        break;
                    case "Холодильник":
                        list.Add(new Fridge(3, 3, width, lenght));
                        pan.Controls.Add(list[list.Count - 1].pb);
                        list[list.Count - 1].pb.MouseDown += new System.Windows.Forms.MouseEventHandler(MouseDown);
                        list[list.Count - 1].pb.MouseMove += new System.Windows.Forms.MouseEventHandler(MouseMove);
                        list[list.Count - 1].pb.MouseUp += new System.Windows.Forms.MouseEventHandler(MouseUp);
                        list3.Add(list[list.Count - 1].pb);
                        break;
                    case "Тумба":
                        list.Add(new Nightstand(3, 3, width, lenght));
                        pan.Controls.Add(list[list.Count - 1].pb);
                        list[list.Count - 1].pb.MouseDown += new System.Windows.Forms.MouseEventHandler(MouseDown);
                        list[list.Count - 1].pb.MouseMove += new System.Windows.Forms.MouseEventHandler(MouseMove);
                        list[list.Count - 1].pb.MouseUp += new System.Windows.Forms.MouseEventHandler(MouseUp);
                        list3.Add(list[list.Count - 1].pb);
                        break;
                    case "Шкаф":
                        list.Add(new Cupboard(3, 3, width, lenght));
                        pan.Controls.Add(list[list.Count - 1].pb);
                        list[list.Count - 1].pb.MouseDown += new System.Windows.Forms.MouseEventHandler(MouseDown);
                        list[list.Count - 1].pb.MouseMove += new System.Windows.Forms.MouseEventHandler(MouseMove);
                        list[list.Count - 1].pb.MouseUp += new System.Windows.Forms.MouseEventHandler(MouseUp);
                        list3.Add(list[list.Count - 1].pb);
                        break;
                }
                    
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            button6.Visible = false;
            label5.Visible = false;
            regim = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button6.Visible = true;
            label5.Visible = true;
            regim = 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button6.Visible = true;
            label5.Visible = true;
            regim = 1;
        }
    }
}