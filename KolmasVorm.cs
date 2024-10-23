using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Elemendid_vormis_TARpv23
{
    public partial class KolmasVorm : Form
    {
        List<int> numbers = new List<int> { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8 };
        string firstChoice, secondChoice;
        int tries;
        List<PictureBox> pictures = new List<PictureBox>();
        PictureBox picA, picB;
        Label lblStatus, lblTimeLeft;
        DateTime startTime;
        int totalTime;
        int cardCount;
        bool gameOver = false;
        System.Windows.Forms.Timer gameTimer;

        public KolmasVorm()
        {
            // Настройка формы
            this.Width = 700;
            this.Height = 600;

            // Настройка элементов интерфейса
            Label lblTitle = new Label();
            lblTitle.Font = new Font("Arial", 12);
            lblTitle.Text = "Kolmas Vorm";
            lblTitle.Location = new Point(500, 100);
            this.Controls.Add(lblTitle);

            lblStatus = new Label();
            lblStatus.Font = new Font("Arial", 12);
            lblStatus.Location = new Point(500, 120);
            this.Controls.Add(lblStatus);

            lblTimeLeft = new Label();
            lblTimeLeft.Font = new Font("Arial", 10);
            lblTimeLeft.Location = new Point(500, 140);
            this.Controls.Add(lblTimeLeft);

            // Инициализация таймера
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 1000;
            gameTimer.Tick += GameTimer_Tick;

            // Добавляем кнопки для выбора сложности
            Button btnEasy = new Button();
            btnEasy.Text = "Lihtne";
            btnEasy.Location = new Point(500, 160);
            btnEasy.Click += (s, e) => SetDifficulty(60, 8); // Легкий уровень - 8 карточек
            this.Controls.Add(btnEasy);

            Button btnNormal = new Button();
            btnNormal.Text = "Normaalne";
            btnNormal.Location = new Point(500, 190);
            btnNormal.Click += (s, e) => SetDifficulty(40, 12); // Нормальный уровень - 12 карточек
            this.Controls.Add(btnNormal);

            Button btnHard = new Button();
            btnHard.Text = "Raske";
            btnHard.Location = new Point(500, 220);
            btnHard.Click += (s, e) => SetDifficulty(25, 16); // Сложный уровень - 16 карточек
            this.Controls.Add(btnHard);

            Button btnRestart = new Button();
            btnRestart.Text = "Taaskäivitamine";
            btnRestart.Location = new Point(500, 250);
            btnRestart.Click += (s, e) => RestartGame();
            this.Controls.Add(btnRestart);

            RestartGame(); // Инициализация игры с выбранным уровнем
        }


        // Метод для установки времени на основе уровня сложности
        private void SetDifficulty(int time, int cards)
        {
            totalTime = time;  // Устанавливаем время для выбранного уровня
            cardCount = cards; // Устанавливаем количество карточек для выбранного уровня
            RestartGame();     // Перезапускаем игру с новыми параметрами
        }

        // Обработчик события тика таймера
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            int timeElapsed = (int)(DateTime.Now - startTime).TotalSeconds;
            int timeLeft = totalTime - timeElapsed;

            if (timeLeft <= 0)
            {
                gameTimer.Stop();
                GameOver("Aeg on läbi, sa kaotasid");
            }
            else
            {
                lblTimeLeft.Text = "Jääb aega: " + timeLeft + " sekundit";
            }
        }

        
        private void LoadImages()
        {
            numbers = numbers.OrderBy(x => Guid.NewGuid()).ToList();
            int leftPos = 20, topPos = 20, rows = 0;
            for (int i = 0; i < numbers.Count; i++)
            {
                PictureBox newPic = new PictureBox
                {
                    Height = 100,
                    Width = 100,
                    BackColor = Color.LightGray,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Tag = numbers[i].ToString()
                };
                newPic.Click += NewPic_Click;
                pictures.Add(newPic);
                newPic.Left = leftPos;
                newPic.Top = topPos;
                this.Controls.Add(newPic);
                leftPos += 110;

                if (++rows == 4)
                {
                    leftPos = 20;
                    topPos += 110;
                    rows = 0;
                }
            }
        }

        private void NewPic_Click(object sender, EventArgs e)
        {
            if (gameOver) return;

            PictureBox clickedPic = sender as PictureBox;
            int index = pictures.IndexOf(clickedPic); // Получаем индекс картинки

            if (clickedPic.Image != null) return; // Проверяем, если картинка уже открыта

            if ((DateTime.Now - startTime).TotalSeconds >= totalTime)
            {
                GameOver("Aeg on läbi, sa kaotasid");
                return;
            }

            if (firstChoice == null)
            {
                picA = clickedPic;
                picA.Image = LoadImageByIndex(numbers[index]); // Загружаем картинку по индексу
                firstChoice = index.ToString(); // Сохраняем индекс первой картинки
            }
            else if (secondChoice == null)
            {
                picB = clickedPic;
                picB.Image = LoadImageByIndex(numbers[index]); // Загружаем картинку по индексу
                secondChoice = index.ToString(); // Сохраняем индекс второй картинки

                CheckPictures(picA, picB);
            }
        }

        // Словарь для хранения путей к изображениям
        Dictionary<int, string> imagePaths = new Dictionary<int, string>
        {
            { 1, @"..\..\..\princ.png" },
            { 2, @"..\..\..\kot.png" },
            { 3, @"..\..\..\shokshrek.jpg" },
            { 4, @"..\..\..\shrek.png" },
            { 5, @"..\..\..\osel.png" },
            { 6, @"..\..\..\minishrek.png" },
            { 7, @"..\..\..\da.png" },
            { 8, @"..\..\..\fiona.png" }
        };

        // Метод для загрузки изображения по индексу
        private Image LoadImageByIndex(int index)
        {
            if (imagePaths.ContainsKey(index))
            {
                return Image.FromFile(imagePaths[index]); // Загружаем изображение по индексу
            }
            return null; // Возвращаем null, если нет изображения
        }

        private void RestartGame()
        {
            // Перемешиваем список заново перед каждой новой игрой
            numbers = numbers.OrderBy(x => Guid.NewGuid()).ToList();

            LoadImages(); // Загружаем изображения на форму

            // Сбрасываем все картинки
            foreach (var pic in pictures)
            {
                pic.Image = null;         // Убираем изображение с картинки
                pic.Enabled = true;       // Делаем картинку снова кликабельной
            }

            tries = 0;
            lblStatus.Text = "Mismatched: " + tries + " korda.";
            lblTimeLeft.Text = "Jääb aega: " + totalTime + " sekundit";

            gameOver = false;
            startTime = DateTime.Now;
            gameTimer.Start();
        }




        private void CheckPictures(PictureBox A, PictureBox B)
        {
            int indexA = int.Parse(firstChoice);
            int indexB = int.Parse(secondChoice);

            // Проверяем, совпадают ли значения картинок
            if (numbers[indexA] == numbers[indexB])
            {
                // Отключаем возможность кликов по картинкам
                pictures[indexA].Enabled = false;
                pictures[indexB].Enabled = false;

                // Оставляем картинки открытыми
                A.Image = LoadImageByIndex(numbers[indexA]);
                B.Image = LoadImageByIndex(numbers[indexB]);
            }
            else
            {
                // Если не совпали, увеличиваем счетчик ошибок
                tries++;
                lblStatus.Text = "Mismatched " + tries + " korda.";

                // Устанавливаем таймер для скрытия изображений
                System.Windows.Forms.Timer resetTimer = new System.Windows.Forms.Timer { Interval = 1000 };
                resetTimer.Tick += (s, e) =>
                {
                    A.Image = null;
                    B.Image = null;
                    resetTimer.Stop();
                };
                resetTimer.Start();
            }

            // Сбрасываем выбор
            firstChoice = null;
            secondChoice = null;

            // Проверяем, выиграл ли игрок (все картинки отключены)
            if (pictures.All(o => o.Enabled == false))
            {
                GameOver("Suurepärane töö, sa võitsid!!!!");
            }
        }


        private void GameOver(string msg)
        {
            gameOver = true;
            gameTimer.Stop();
            MessageBox.Show(msg);
        }

        private void KolmasVorm_Load(object sender, EventArgs e)
        {

        }
    }
}
