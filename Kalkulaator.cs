using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Elemendid_vormis_TARpv23
{
    public partial class Kalkulaator : Form
    {
        private TextBox[] vastuseSisendid; // Vastuse sisestamise tekstikastide massiiv
        private Button btnKontrolliVastuseid; // Vastuste kontrollimise nupp
        private Button btnUuendaKüsimused; // Küsimuste uuendamise nupp
        private Label lblAegAlles;
        private System.Windows.Forms.Timer taimer;
        private int aegaAlles = 30;
        private string[] küsimused;
        private int[] vastused;
        private Random juhuslik;

        public Kalkulaator()
        {
            juhuslik = new Random();
            LooKalkulaatoriUI();
            GenereeriJuhuslikudKüsimused(); // Genereerime juhuslikud küsimused programmi käivitamisel
            AlustaViktoriini(); // Alustame viktoriini kohe
        }

        private void LooKalkulaatoriUI()
        {
            this.Text = "Matemaatiline Test";
            this.Size = new Size(400, 500); 

            lblAegAlles = new Label
            {
                Location = new Point(20, 20),
                Width = 240,
                Font = new Font("Arial", 14)
            };
            this.Controls.Add(lblAegAlles);

            // Loon tekstiväljad vastuste sisestamiseks ja sildid küsimuste jaoks
            vastuseSisendid = new TextBox[4];
            for (int i = 0; i < 4; i++)
            {
                Label lblKüsimus = new Label
                {
                    Font = new Font("Arial", 18),
                    Location = new Point(20, 60 + i * 60),
                    Size = new Size(200, 50)
                };
                this.Controls.Add(lblKüsimus);

                vastuseSisendid[i] = new TextBox
                {
                    Font = new Font("Arial", 18),
                    Location = new Point(230, 60 + i * 60),
                    Size = new Size(100, 50)
                };
                this.Controls.Add(vastuseSisendid[i]);
            }

            // Nupp vastuste kontrollimiseks
            btnKontrolliVastuseid = new Button { Text = "Kontrolli vastuseid", Location = new Point(20, 370), Size = new Size(240, 50) };
            btnKontrolliVastuseid.Click += BtnKontrolliVastuseid_Click;
            this.Controls.Add(btnKontrolliVastuseid);

            // Nupp küsimuste uuendamiseks
            btnUuendaKüsimused = new Button { Text = "Uuenda küsimusi", Location = new Point(20, 430), Size = new Size(240, 50) };
            btnUuendaKüsimused.Click += BtnUuendaKüsimused_Click;
            this.Controls.Add(btnUuendaKüsimused);

            // Taimer
            taimer = new System.Windows.Forms.Timer();
            taimer.Interval = 1000;
            taimer.Tick += Taimer_Tick;
        }

        private void GenereeriJuhuslikudKüsimused()
        {
            küsimused = new string[4];
            vastused = new int[4];

            for (int i = 0; i < 4; i++)
            {
                int arv1 = juhuslik.Next(1, 50);
                int arv2 = juhuslik.Next(1, 50);
                int tehe = juhuslik.Next(0, 4); // Valime juhuslikult operatsiooni: +, -, *, /

                switch (tehe)
                {
                    case 0:
                        küsimused[i] = $"{arv1} + {arv2} = ?";
                        vastused[i] = arv1 + arv2;
                        break;
                    case 1:
                        küsimused[i] = $"{arv1} - {arv2} = ?";
                        vastused[i] = arv1 - arv2;
                        break;
                    case 2:
                        küsimused[i] = $"{arv1} × {arv2} = ?";
                        vastused[i] = arv1 * arv2;
                        break;
                    case 3:
                        küsimused[i] = $"{arv1} ÷ {arv2} = ?";
                        vastused[i] = arv1 / arv2; // Täisarvuline jagamine
                        break;
                }

                // Kuvame küsimused tekstiväljade kõrvale
                Controls.OfType<Label>().ElementAt(i + 1).Text = küsimused[i];
            }
        }

        private void AlustaViktoriini()
        {
            aegaAlles = 30;
            lblAegAlles.Text = $"Aega alles: {aegaAlles} sekundit";
            taimer.Start();
        }

        private void Taimer_Tick(object sender, EventArgs e)
        {
            if (aegaAlles > 0)
            {
                aegaAlles--;
                lblAegAlles.Text = $"Aega alles: {aegaAlles} sekundit";
            }
            else
            {
                taimer.Stop();
                MessageBox.Show("Aeg sai läbi!");
                LähtestaViktoriin();
            }
        }

        private void BtnKontrolliVastuseid_Click(object sender, EventArgs e)
        {
            // Peata taimer vastuste kontrollimise ajal
            taimer.Stop();

            for (int i = 0; i < 4; i++)
            {
                if (int.TryParse(vastuseSisendid[i].Text, out int kasutajaVastus))
                {
                    if (kasutajaVastus == vastused[i])
                    {
                        MessageBox.Show($"Küsimus {i + 1}: Õige!");
                    }
                    else
                    {
                        MessageBox.Show($"Küsimus {i + 1}: Vale! Õige vastus: {vastused[i]}");
                    }
                }
                else
                {
                    MessageBox.Show($"Küsimus {i + 1}: Palun sisestage number.");
                }
            }
        }


        private void BtnUuendaKüsimused_Click(object sender, EventArgs e)
        {
            // Uuendame küsimused ja taaskäivitame taimeri
            GenereeriJuhuslikudKüsimused();
            AlustaViktoriini(); // Taaskäivitage taimer ja viktoriin
        }

        private void LähtestaViktoriin()
        {
            for (int i = 0; i < vastuseSisendid.Length; i++)
            {
                vastuseSisendid[i].Text = string.Empty;
            }
            lblAegAlles.Text = "Aega alles: 30 sekundit";
        }

        private void Kalkulaator_Load(object sender, EventArgs e)
        {

        }
    }
}
