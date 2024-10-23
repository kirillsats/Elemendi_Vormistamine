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
        Button btnLeft; // Кнопка для перемещения влево
        Button btnRight; // Кнопка для перемещения вправо
        Button btn3; // Кнопка для удаления изображения
        Button btn4; // Кнопка для изменения цвета фона
        PictureBox pb1 = new PictureBox();
        ColorDialog cd1 = new ColorDialog();
        System.Windows.Forms.CheckBox chk1;
        OpenFileDialog ofd = new OpenFileDialog();
        Button btnChangePicture;
        List<string> imageFiles = new List<string>(); // Список загруженных изображений
        int currentImageIndex = -1; // Индекс текущего изображения

        public EsimeneVorm(int h, int w)
        {
            this.Height = h;
            this.Width = w;
            this.Text = "Pildid";

            // Создание панели для кнопок
            FlowLayoutPanel panel = new FlowLayoutPanel();
            panel.Dock = DockStyle.Bottom;
            panel.Height = 100;
            panel.BackColor = SystemColors.Control;

            // Создание и добавление кнопок
            btnLeft = new Button();
            btnLeft.Text = "Vasak"; // Кнопка для перемещения влево
            btnLeft.Click += btnLeft_Click;
            panel.Controls.Add(btnLeft);

            btnRight = new Button();
            btnRight.Text = "Parempoolne"; // Кнопка для перемещения вправо
            btnRight.Click += btnRight_Click;
            panel.Controls.Add(btnRight);

            btn3 = new Button();
            btn3.Text = "Kustuta pilti"; // Кнопка для удаления изображения
            btn3.Click += clearButton_Click;
            panel.Controls.Add(btn3);

            btn4 = new Button();
            btn4.Text = "Värv"; // Кнопка для изменения цвета фона
            btn4.Click += backgroundButton_Click;
            panel.Controls.Add(btn4);

            btn = new Button();
            btn.Text = "Sule"; // Кнопка закрытия
            btn.Click += closeButton_Click;
            panel.Controls.Add(btn);

            // CheckBox
            chk1 = new System.Windows.Forms.CheckBox();
            chk1.Checked = false;
            chk1.Text = "Stretch";
            chk1.Size = new Size(75, 20);
            chk1.Click += checkBox1_CheckedChanged;
            panel.Controls.Add(chk1);

            // Добавление кнопки изменения изображения на панель
            AddChangePictureButton(panel);

            // Добавляем панель на форму
            Controls.Add(panel);
            Controls.Add(pb1);

            // Настройка PictureBox
            pb1.Dock = DockStyle.Fill; // Заполняет всю доступную область
            pb1.SizeMode = PictureBoxSizeMode.StretchImage; // Растягиваем изображение
        }

        // Добавление кнопки изменения изображения
        private void AddChangePictureButton(FlowLayoutPanel panel)
        {
            btnChangePicture = new Button();
            btnChangePicture.Text = "Lisa pilti"; // Кнопка для добавления изображения
            btnChangePicture.Click += btnChangePicture_Click;
            panel.Controls.Add(btnChangePicture);
        }

        private void btnChangePicture_Click(object sender, EventArgs e)
        {
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                imageFiles.Add(ofd.FileName); // Добавляем загруженное изображение в список
                currentImageIndex = imageFiles.Count - 1; // Устанавливаем индекс на последнее добавленное изображение
                pb1.Image = Image.FromFile(imageFiles[currentImageIndex]); // Показываем загруженное изображение
            }
        }

        // Кнопка "Clear Picture"
        private void clearButton_Click(object sender, EventArgs e)
        {
            pb1.Image = null;
            imageFiles.Clear(); // Очищаем список загруженных изображений
            currentImageIndex = -1; // Сбрасываем индекс изображения
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

        // Кнопка для перемещения влево
        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (imageFiles.Count > 0)
            {
                currentImageIndex--; // Уменьшаем индекс
                if (currentImageIndex < 0) currentImageIndex = imageFiles.Count - 1; // Циклическое перемещение
                pb1.Image = Image.FromFile(imageFiles[currentImageIndex]); // Показать изображение
            }
        }

        // Кнопка для перемещения вправо
        private void btnRight_Click(object sender, EventArgs e)
        {
            if (imageFiles.Count > 0)
            {
                currentImageIndex++; // Увеличиваем индекс
                if (currentImageIndex >= imageFiles.Count) currentImageIndex = 0; // Циклическое перемещение
                pb1.Image = Image.FromFile(imageFiles[currentImageIndex]); // Показать изображение
            }
        }

        // Изменение режима отображения картинки
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chk1.Checked)
                pb1.SizeMode = PictureBoxSizeMode.StretchImage;
            else
                pb1.SizeMode = PictureBoxSizeMode.Normal;
        }

        private void EsimeneVorm_Load(object sender, EventArgs e)
        {
            // Можно добавить любую инициализацию здесь, если потребуется
        }
    }
}