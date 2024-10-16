using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Elemendid_vorm_TARpv23
{
    public partial class KolmasVorm : Form
    {
        List<int> numbers = new List<int> { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6 };
        string firstChoice, secondChoice;
        int tries;
        List<PictureBox> pictures = new List<PictureBox>();
        PictureBox picA, picB;
        Label lblStatus, lblTimeLeft;
        System.Windows.Forms.Timer GameTimer;
        int totalTime = 60, countDownTime;
        bool gameOver = false;

        public KolmasVorm(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.ClientSize = new Size(width, height);
            GameTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            GameTimer.Tick += TimerEvent;

            lblStatus = new Label { Location = new Point(20, 200), Size = new Size(200, 30) };
            lblTimeLeft = new Label { Location = new Point(20, 230), Size = new Size(200, 30) };
            this.Controls.Add(lblStatus);
            this.Controls.Add(lblTimeLeft);

            LoadPictures();
        }

        private void TimerEvent(object sender, EventArgs e)
        {
            countDownTime--;
            lblTimeLeft.Text = "Jääb aega: " + countDownTime;
            if (countDownTime < 1)
            {
                GameOver("Aeg on läbi, sa kaotasid");
                foreach (PictureBox x in pictures)
                {
                    if (x.Tag != null)
                    {
                        try
                        {
                            x.Image = Image.FromFile(@"..\..\..\princ.png");
                            x.Image = Image.FromFile(@"..\..\..\kot.png");
                            x.Image = Image.FromFile(@"..\..\..\shokshrek.png");
                            x.Image = Image.FromFile(@"..\..\..\shrek.png");
                            x.Image = Image.FromFile(@"..\..\..\osel.png");
                            x.Image = Image.FromFile(@"..\..\..\minishrek.png");
                            x.Image = Image.FromFile(@"..\..\..\da.png");
                            x.Image = Image.FromFile(@"..\..\..\fiona.png");

                        }
                        catch
                        {
                            // Handle image loading exception (e.g., log it or show a message)
                        }
                    }
                }
            }
        }

        private void LoadPictures()
        {
            int leftPos = 20, topPos = 20, rows = 0;
            for (int i = 0; i < 12; i++)
            {
                PictureBox newPic = new PictureBox
                {
                    Height = 50,
                    Width = 50,
                    BackColor = Color.LightGray,
                    SizeMode = PictureBoxSizeMode.StretchImage
                };
                newPic.Click += NewPic_Click;
                pictures.Add(newPic);
                newPic.Left = leftPos;
                newPic.Top = topPos;
                this.Controls.Add(newPic);
                leftPos += 60;

                if (++rows == 4)
                {
                    leftPos = 20;
                    topPos += 60;
                    rows = 0;
                }
            }
            RestartGame();
        }

        private void NewPic_Click(object sender, EventArgs e)
        {
            if (gameOver) return;

            PictureBox clickedPic = sender as PictureBox;
            if (clickedPic.Image != null) return; // Prevent clicking already revealed pictures

            if (firstChoice == null)
            {
                picA = clickedPic;
                if (picA.Tag != null)
                {
                    try
                    {
                        picA.Image = Image.FromFile(@"..\..\..\" + (string)picA.Tag + ".png");
                        firstChoice = (string)picA.Tag;
                    }
                    catch
                    {
                        // Handle image loading exception
                    }
                }
            }
            else if (secondChoice == null)
            {
                picB = clickedPic;
                if (picB.Tag != null)
                {
                    try
                    {
                        picB.Image = Image.FromFile(@"..\..\..\" + (string)picB.Tag + ".png");
                        secondChoice = (string)picB.Tag;
                    }
                    catch
                    {
                        // Handle image loading exception
                    }
                }

                CheckPictures(picA, picB);
            }
        }

        private void RestartGame()
        {
            numbers = numbers.OrderBy(x => Guid.NewGuid()).ToList();
            for (int i = 0; i < pictures.Count; i++)
            {
                pictures[i].Image = null;
                pictures[i].Tag = numbers[i].ToString(); 
            }
            tries = 0;
            lblStatus.Text = "Mismatched: " + tries + " korda.";
            lblTimeLeft.Text = "Jääb aega: " + totalTime;
            gameOver = false;
            countDownTime = totalTime;
            GameTimer.Start();
        }

        private void CheckPictures(PictureBox A, PictureBox B)
        {
            if (firstChoice == secondChoice)
            {
                A.Tag = null; // Очищаем Tag, если картинки совпадают
                B.Tag = null;
            }
            else
            {
                tries++;
                lblStatus.Text = "Mismatched " + tries + " korda.";
            }
            firstChoice = null;
            secondChoice = null;

            // Если не совпадают, сбросим картинки через секунду
            if (A.Tag != null && B.Tag != null && A.Tag != B.Tag)
            {
                System.Windows.Forms.Timer resetTimer = new System.Windows.Forms.Timer { Interval = 1000 };
                resetTimer.Tick += (s, e) =>
                {
                    A.Image = null;
                    B.Image = null;
                    resetTimer.Stop();
                };
                resetTimer.Start();
            }

            // Проверка на завершение игры
            if (pictures.All(o => o.Tag == null))
            {
                GameOver("Suurepärane töö, sa võitsid!!!!");
            }
        }

        private void GameOver(string msg)
        {
            GameTimer.Stop();
            gameOver = true;
            MessageBox.Show(msg); // Display the game over message
        }
    }
}
