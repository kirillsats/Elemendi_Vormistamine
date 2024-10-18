using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Elemendid_vormis_TARpv23
{
    public partial class KolmasVorm : Form
    {
        // Инициализация данных игры
        List<int> numbers = new List<int> { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8 }; // 16 карточек (8 пар)
        string firstChoice, secondChoice; // выбранные карточки
        int tries; // количество попыток
        List<PictureBox> pictures = new List<PictureBox>(); // список изображений
        PictureBox picA, picB; // выбранные изображения
        Label lblStatus, lblTimeLeft; // метки статуса и времени
        DateTime startTime; // время начала игры
        int totalTime = 60; // общее время в секундах
        bool gameOver = false; // флаг окончания игры

        public KolmasVorm()
        {
            // Настройка формы
            this.Width = 700;
            this.Height = 600;

            // Настройка элементов интерфейса
            Label lblTitle = new Label();
            lblTitle.Text = "Kolmas Vorm";
            lblTitle.Location = new Point(100, 100);
            this.Controls.Add(lblTitle);

            lblStatus = new Label();
            lblStatus.Location = new Point(50, 100);
            this.Controls.Add(lblStatus);

            lblTimeLeft = new Label();
            lblTimeLeft.Location = new Point(50, 120);
            this.Controls.Add(lblTimeLeft);

            LoadPictures(); // Загрузка изображений
            RestartGame(); // Начало игры
        }

        // Функция загрузки изображений
        private void LoadPictures()
        {
            numbers = numbers.OrderBy(x => Guid.NewGuid()).ToList(); // Перемешивание номеров
            int leftPos = 20, topPos = 20, rows = 0;
            for (int i = 0; i < numbers.Count; i++)
            {
                PictureBox newPic = new PictureBox
                {
                    Height = 50,
                    Width = 50,
                    BackColor = Color.LightGray,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Tag = numbers[i].ToString() // Присвоение тега номеру
                };
                newPic.Click += NewPic_Click;
                pictures.Add(newPic);
                newPic.Left = leftPos;
                newPic.Top = topPos;
                this.Controls.Add(newPic);
                leftPos += 60;

                // Конец ряда
                if (++rows == 4)
                {
                    leftPos = 20;
                    topPos += 60;
                    rows = 0;
                }
            }
        }

        private void NewPic_Click(object sender, EventArgs e)
        {
            if (gameOver) return;

            PictureBox clickedPic = sender as PictureBox;

            // Если изображение уже открыто, ничего не делаем
            if (clickedPic.Image != null) return;

            // Проверяем, сколько времени прошло с начала игры
            if ((DateTime.Now - startTime).TotalSeconds >= totalTime)
            {
                GameOver("Aeg on läbi, sa kaotasid");
                return;
            }

            // Если первый выбор не сделан
            if (firstChoice == null)
            {
                picA = clickedPic;
                if (picA.Tag != null)
                {
                    try
                    {
                        // Загрузка изображения и назначение первого выбора
                        picA.Image = LoadImageByTag((string)picA.Tag); // Загрузка изображения по тегу
                        firstChoice = (string)picA.Tag;
                    }
                    catch
                    {
                        // Обработка ошибок загрузки изображений
                    }
                }
            }
            // Если первый выбор сделан, делаем второй выбор
            else if (secondChoice == null)
            {
                picB = clickedPic;
                if (picB.Tag != null)
                {
                    try
                    {
                        // Загрузка изображения и назначение второго выбора
                        picB.Image = LoadImageByTag((string)picB.Tag); // Загрузка изображения по тегу
                        secondChoice = (string)picB.Tag;
                    }
                    catch
                    {
                        // Обработка ошибок загрузки изображений
                    }
                }

                // Проверяем, совпадают ли выбранные изображения
                CheckPictures(picA, picB);
            }
        }

        // Функция загрузки изображения по тегу
        private Image LoadImageByTag(string tag)
        {
            switch (tag)
            {
                case "1":
                    return Image.FromFile(@"..\..\..\princ.png"); // картинка 1
                case "2":
                    return Image.FromFile(@"..\..\..\kot.png"); // картинка 2
                case "3":
                    return Image.FromFile(@"..\..\..\shokshrek.png"); // картинка 3
                case "4":
                    return Image.FromFile(@"..\..\..\shrek.png"); // картинка 4
                case "5":
                    return Image.FromFile(@"..\..\..\osel.png"); // картинка 5
                case "6":
                    return Image.FromFile(@"..\..\..\minishrek.png"); // картинка 6
                case "7":
                    return Image.FromFile(@"..\..\..\da.png"); // картинка 7
                case "8":
                    return Image.FromFile(@"..\..\..\fiona.png"); // картинка 8
                default:
                    return null;
            }
        }

        // Функция перезагрузки игры
        private void RestartGame()
        {
            for (int i = 0; i < pictures.Count; i++)
            {
                pictures[i].Image = null; // Очистка изображений
            }
            tries = 0;
            lblStatus.Text = "Mismatched: " + tries + " korda.";
            lblTimeLeft.Text = "Jääb aega: " + totalTime + " sekundit";
            gameOver = false;
            startTime = DateTime.Now; // Запоминаем время начала
        }

        // Функция для проверки выбранных изображений
        private void CheckPictures(PictureBox A, PictureBox B)
        {
            if (firstChoice == secondChoice)
            {
                // Если изображения совпадают, очищаем тег
                A.Tag = null;
                B.Tag = null;
            }
            else
            {
                tries++;
                lblStatus.Text = "Mismatched " + tries + " korda.";
            }
            firstChoice = null;
            secondChoice = null;

            // Если изображения не совпадают, скрываем их через некоторое время
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

            // Проверяем, закончена ли игра
            if (pictures.All(o => o.Tag == null))
            {
                GameOver("Suurepärane töö, sa võitsid!!!!");
            }
        }

        // Функция окончания игры
        private void GameOver(string msg)
        {
            gameOver = true; // Игра окончена
            MessageBox.Show(msg); // Показываем сообщение об окончании игры
        }
    }
}
