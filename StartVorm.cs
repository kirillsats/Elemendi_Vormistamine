using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using Microsoft.VisualBasic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Elemendid_vormis_TARpv23
{
    public partial class StartVorm : Form
    {
        List<string> elemendid = new List<string> { "Nupp", "Silt", "Pilt", "Märkeruut", "Raadionupp", "Tekstikast", "Loetelu", "Tabel", "Dialoogi aknad", "Kalkulaator", "Game" };

        List<string> rbtn_list = new List<string> { "Üks", "Kaks", "Kolm" };

        TreeView tree;
        Button btn;
        Label lbl;
        PictureBox pbox;
        CheckBox chk1, chk2;
        RadioButton rbtn, rbtn1, rbtn2;
        TextBox txt;
        ListBox lb;
        DataSet ds;
        DataGridView dg;
        Button btnCalculator;
        Button btnKolmasVorm;
        public StartVorm()
        {

            this.Height = 500;
            this.Width = 700;
            this.Text = "Vorm elementidega";
            tree = new TreeView();
            tree.Dock = DockStyle.Left;
            tree.AfterSelect += Tree_AfterSelect;
            TreeNode tn = new TreeNode("Elemendid:");
            foreach (var element in elemendid)
            {
                tn.Nodes.Add(new TreeNode(element));
            }

            tree.Nodes.Add(tn);
            this.Controls.Add(tree);
            //nupp-button
            btn = new Button();
            btn.Text = "Vajuta siia";
            btn.Height = 50;
            btn.Width = 70;
            btn.Location = new Point(150, 50);
            btn.Click += Btn_Click;
            //silt-label
            lbl = new Label();
            lbl.Text = "Aknade elemendid c# abil";
            lbl.Font = new Font("Arial", 26, FontStyle.Underline);
            lbl.Size = new Size(520, 50);
            lbl.Location = new Point(150, 0);
            lbl.MouseHover += Lbl_MouseHover;
            lbl.MouseLeave += Lbl_MouseLeave;

            pbox = new PictureBox();
            pbox.Size = new Size(60, 60);
            pbox.Location = new Point(150, btn.Height + lbl.Height + 5);
            pbox.SizeMode = PictureBoxSizeMode.Zoom;
            pbox.Image = Image.FromFile(@"..\..\..\ratas.png");
            pbox.DoubleClick += Pbox_DoubleClick;


            // Создание кнопки для калькулятора
            btnCalculator = new Button();
            btnCalculator.Text = "Открыть Калькулятор";
            btnCalculator.Height = 50;
            btnCalculator.Width = 150;
            btnCalculator.Location = new Point(150, 400); // Позиция кнопки на форме
            btnCalculator.Click += BtnCalculator_Click; // Подписка на событие

            //Создание кнопки для игры
            Button btnKolmasVorm = new Button();
            btnKolmasVorm.Text = "Ava Kolmas Vorm";
            btnKolmasVorm.Height = 50;
            btnKolmasVorm.Width = 150;
            btnKolmasVorm.Location = new Point(150, 320); 
            btnKolmasVorm.Click += BtnKolmasVorm_Click; 
            Controls.Add(btnKolmasVorm);
            

        }
        int tt = 0;
        private void Pbox_DoubleClick(object? sender, EventArgs e)
        {
            string[] pildid = { "esimene.png", "teine.png", "kolmas.png" };
            string fail = pildid[tt];
            pbox.Image = Image.FromFile(@"..\..\..\" + fail);
            tt++;
            if (tt == 3) { tt = 0; }
        }

        private void Lbl_MouseLeave(object? sender, EventArgs e)
        {
            lbl.Font = new Font("Arial", 30, FontStyle.Underline);
        }
        private void Lbl_MouseHover(object? sender, EventArgs e)
        {
            lbl.Font = new Font("Arial", 32, FontStyle.Underline);
            lbl.ForeColor = Color.FromArgb(70, 50, 150, 200);

        }
        int t = 0;
        private void Btn_Click(object? sender, EventArgs e)
        {
            t++;
            if (t % 2 == 0)
            {
                btn.BackColor = Color.Red;
            }
            else
            {
                btn.BackColor = Color.White;
            }
            TeineVorm teineVorm = new TeineVorm(200, 200);
            teineVorm.Show();
        }

        private void BtnKolmasVorm_Click(object? sender, EventArgs e)
        {
            KolmasVorm kolmasVorm = new KolmasVorm();
            kolmasVorm.Show();
        }





        private void BtnCalculator_Click(object? sender, EventArgs e)
        {
            Kalkulaator kalkulaator = new Kalkulaator(); // Создание нового экземпляра калькулятора
            kalkulaator.Show(); // Показать калькулятор
        }

        private void Tree_AfterSelect(object? sender, TreeViewEventArgs e)
        {
            if (e.Node.Text == "Nupp")
            {
                Controls.Add(btn);
            }
            else if (e.Node.Text == "Silt")
            {
                Controls.Add(lbl);
            }
            else if (e.Node.Text == "Pilt")
            {
                Controls.Add(pbox);
            }
            else if (e.Node.Text == "Märkeruut")
            {
                chk1 = new CheckBox();
                chk1.Checked = false;
                chk1.Text = e.Node.Text;
                chk1.Size = new Size(chk1.Text.Length * 10, chk1.Size.Height);
                chk1.Location = new Point(150, btn.Height + lbl.Height + pbox.Height + 10);
                chk1.CheckedChanged += new EventHandler(Chk_CheckedChanged);

                chk2 = new CheckBox();
                chk2.Checked = false;
                //chk2.Image = Image.FromFile(@"..\..\..\ratas.png");
                chk2.BackgroundImage = Image.FromFile(@"..\..\..\ratas.png");
                chk2.BackgroundImageLayout = ImageLayout.Zoom;
                chk2.Size = new Size(100, 100);
                chk2.Location = new Point(150, btn.Height + lbl.Height + pbox.Height + chk1.Height + 15);
                chk2.CheckedChanged += new EventHandler(Chk_CheckedChanged);

                Controls.Add(chk1);
                Controls.Add(chk2);
            }
            else if (e.Node.Text == "Raadionupp")
            {
                //1.variant
                rbtn1 = new RadioButton();
                rbtn1.Text = "Must teema";
                rbtn1.Location = new Point(150, 420);
                rbtn2 = new RadioButton();
                rbtn2.Text = "Valge teema";
                rbtn2.Location = new Point(150, 440);
                this.Controls.Add(rbtn1);
                this.Controls.Add(rbtn2);
                rbtn1.CheckedChanged += new EventHandler(Rbtn_Checked);
                rbtn2.CheckedChanged += new EventHandler(Rbtn_Checked);
                //2.variant
                int x = 20;
                for (int i = 0; i < rbtn_list.Count; i++)
                {
                    rbtn = new RadioButton();
                    rbtn.Checked = false;
                    rbtn.Text = rbtn_list[i];
                    rbtn.Height = x;
                    x = x + 20;
                    rbtn.Location = new Point(150, btn.Height + lbl.Height + pbox.Height + chk1.Height + chk2.Height + rbtn.Height);
                    rbtn.CheckedChanged += new EventHandler(Btn_CheckedChanged);

                    Controls.Add(rbtn);
                }
            }
            else if (e.Node.Text == "Tekstikast")
            {
                txt = new TextBox();
                txt.Location = new Point(150 + btn.Width + 5, btn.Height);
                txt.Font = new Font("Arial", 12);
                txt.Width = 200;
                txt.TextChanged += Txt_TextChanged;
                Controls.Add(txt);
            }
            else if (e.Node.Text == "Loetelu")
            {
                lb = new ListBox();
                foreach (string item in rbtn_list)
                {
                    lb.Items.Add(item);
                }
                lb.Height = 30;
                lb.Location = new Point(160 + btn.Width + txt.Width, btn.Height);
                lb.SelectedIndexChanged += Lb_SelectedIndexChanged;
                Controls.Add(lb);
            }
            else if (e.Node.Text == "Tabel")
            {
                ds = new DataSet("XML fail");
                ds.ReadXml(@"..\..\..\menu.xml");
                dg = new DataGridView();
                dg.Location = new Point(160 + chk1.Width, txt.Height + lbl.Height + 10);
                dg.DataSource = ds;
                dg.DataMember = "food";
                dg.RowHeaderMouseClick += Dg_RowHeaderMouseClick;
                Controls.Add(dg);
            }
            else if (e.Node.Text == "Dialoogi aknad")
            {
                MessageBox.Show("Dialoog", "See on lihtne aken");
                var vastus = MessageBox.Show("Sisestame andmed", "Kas tahad InputBoxi kasutada?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (vastus == DialogResult.Yes)
                {
                    string text = Interaction.InputBox("Sisesta midagi siia", "andmete sisestamine");
                    Random random = new Random();
                    DataRow dr = ds.Tables["food"].NewRow();
                    dr["name"] = text;
                    dr["price"] = "$" + (random.NextSingle() * 10).ToString();
                    dr["description"] = "Väga maitsev ";
                    dr["calories"] = random.Next(10, 1000);

                    ds.Tables["food"].Rows.Add(dr);
                    if (ds == null) { return; }
                    ds.WriteXml(@"..\..\..\menu.xml");
                    MessageBox.Show("Oli sisestatud " + text);
                }
            }
            else if (e.Node.Text == "Kalkulaator")  
            {
                Controls.Add(btnCalculator); // Добавляем кнопку калькулятора на форму при выборе узла
            }
            else if (e.Node.Text == "Game") 
            {
                Controls.Add(btnKolmasVorm);
            }



        }

        private void Dg_RowHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            txt.Text = dg.Rows[e.RowIndex].Cells[0].Value.ToString() + " hind " + dg.Rows[e.RowIndex].Cells[1].Value.ToString();

        }

        private void Lb_SelectedIndexChanged(object? sender, EventArgs e)
        {
            switch (lb.SelectedIndex)
            {
                case 0: tree.BackColor = Color.Chocolate; break;
                case 1: tree.BackColor = Color.IndianRed; break;
                case 2: tree.BackColor = Color.Lavender; break;
            }
        }

        private void Txt_TextChanged(object? sender, EventArgs e)
        {
            lbl.Text = txt.Text;
        }

        private void Rbtn_Checked(object? sender, EventArgs e)
        {
            if (rbtn1.Checked)
            {
                this.BackColor = Color.Black;
                this.ForeColor = Color.White;
            }
            else if (rbtn2.Checked)
            {
                this.BackColor = Color.White;
                this.ForeColor = Color.Black;
            }
        }

        private void Btn_CheckedChanged(object? sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            lbl.Text = rb.Text;
        }

        private void Chk_CheckedChanged(object? sender, EventArgs e)
        {
            if (chk1.Checked && chk2.Checked)
            {
                lbl.BorderStyle = BorderStyle.Fixed3D;
                pbox.BorderStyle = BorderStyle.Fixed3D;
            }
            else if (chk1.Checked)
            {
                lbl.BorderStyle = BorderStyle.Fixed3D;
                pbox.BorderStyle = BorderStyle.None;
            }
            else if (chk2.Checked)
            {
                pbox.BorderStyle = BorderStyle.Fixed3D;
                lbl.BorderStyle = BorderStyle.None;
            }
            else
            {
                lbl.BorderStyle = BorderStyle.None;
                pbox.BorderStyle = BorderStyle.None;
            }
        }

        private void StartVorm_Load(object sender, EventArgs e)
        {

        }
    }
}
