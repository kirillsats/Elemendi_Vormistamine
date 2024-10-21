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
    public partial class EsimeneVorm : Form
    {
        Button btn;
        Button btn2;
        Button btn3;
        Button btn4;
        PictureBox pb1 = new PictureBox();
        ColorDialog cd1 = new ColorDialog();
        System.Windows.Forms.CheckBox chk1;
        OpenFileDialog ofd = new OpenFileDialog();
        Button btnChangePicture;
        Label lblTime;
        System.Windows.Forms.Timer timeTimer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        List<string> imageFiles = new List<string> { @"..\..\..\osel.png", @"..\..\..\shokshrek.jpg", @"..\..\..\kot.png" };
        int currentImageIndex = 0;

        public EsimeneVorm(int h, int w)
        {
            this.Height = h;
            this.Width = w;
            this.Text = "Pildid";

            // Создание и добавление кнопок
            btn = new Button();
            btn.Text = "Close";
            btn.Location = new Point(300, 440);
            btn.Click += closeButton_Click;
            Controls.Add(btn);

            btn2 = new Button();
            btn2.Text = "Show picture";
            btn2.Location = new Point(375, 440);
            btn2.Click += Click_ShowPictureButton;
            Controls.Add(btn2);

            btn3 = new Button();
            btn3.Text = "Clear Picture";
            btn3.Location = new Point(450, 440);
            btn3.Click += clearButton_Click;
            Controls.Add(btn3);

            btn4 = new Button();
            btn4.Text = "Set the background color";
            btn4.Location = new Point(525, 440);
            btn4.Click += backgroundButton_Click;
            Controls.Add(btn4);

            // CheckBox
            chk1 = new System.Windows.Forms.CheckBox();
            chk1.Checked = false;
            chk1.Text = "Stretch";
            chk1.Size = new Size(75, 20);
            chk1.Location = new Point(20, 440);
            chk1.Click += checkBox1_CheckedChanged;
            Controls.Add(chk1);

            // Добавляем функции изменения изображения и слайдшоу
            AddChangePictureButton();
            AddImageSlideshowFunctionality();
            AddTimeDisplayFunctionality();
        }

        // Добавление кнопки изменения изображения
        private void AddChangePictureButton()
        {
            btnChangePicture = new Button();
            btnChangePicture.Text = "Change Picture";
            btnChangePicture.Location = new Point(700, 440);
            btnChangePicture.Click += btnChangePicture_Click;
            Controls.Add(btnChangePicture);
        }

        private void btnChangePicture_Click(object sender, EventArgs e)
        {
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pb1.Image = Image.FromFile(ofd.FileName);
            }
        }

        // Добавление функции слайд-шоу
        private void AddImageSlideshowFunctionality()
        {
            timer.Interval = 3000; // Интервал 3 секунды
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (imageFiles.Count > 0)
            {
                pb1.Image = Image.FromFile(imageFiles[currentImageIndex]);
                currentImageIndex = (currentImageIndex + 1) % imageFiles.Count;
            }
        }

        // Отображение текущего времени
        private void AddTimeDisplayFunctionality()
        {
            lblTime = new Label();
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
            lblTime.Font = new Font("Arial", 14, FontStyle.Bold);
            lblTime.Location = new Point(20, 20);
            lblTime.AutoSize = true;
            Controls.Add(lblTime);

            timeTimer.Interval = 1000; // 1 секунда
            timeTimer.Tick += timeTimer_Tick;
            timeTimer.Start();
        }

        private void timeTimer_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        // Кнопка "Clear Picture"
        private void clearButton_Click(object sender, EventArgs e)
        {
            pb1.Image = null;
        }

        // Установка фона
        private void backgroundButton_Click(object sender, EventArgs e)
        {
            if (cd1.ShowDialog() == DialogResult.OK)
            {
                this.BackColor = cd1.Color;
            }
        }

        // Закрытие формы
        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Показать картинку
        private void Click_ShowPictureButton(object? sender, EventArgs e)
        {
            pb1.Image = Image.FromFile(@"..\..\..\ratas.png");
            pb1.Location = new Point(0, 0);
            pb1.Size = new Size(800, 900);
            this.Controls.Add(pb1);
        }

        // Изменение режима отображения картинки
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chk1.Checked)
                pb1.SizeMode = PictureBoxSizeMode.StretchImage;
            else
                pb1.SizeMode = PictureBoxSizeMode.Normal;
        }
    }
}