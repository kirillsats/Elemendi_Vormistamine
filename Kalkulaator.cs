using Microsoft.VisualBasic;
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
    public partial class Kalkulaator : Form // Убедитесь, что 'partial' здесь
    {
        private TextBox txtInput;
        private Button[] answerBoxes;
        private Button btnStart;
        private Label lblTimeLeft;
        private System.Windows.Forms.Timer timer; // Явное указание на таймер
        private int timeLeft = 30; // Время для теста в секундах
        private string[] questions;
        private int[] answers;

        public Kalkulaator()
        {
            
            CreateCalculatorUI();
            LoadQuestions();
        }

       

        private void CreateCalculatorUI()
        {
            this.Text = "Math Quiz";
            this.Size = new Size(300, 400);

            lblTimeLeft = new Label
            {
                Location = new Point(20, 20),
                Width = 240,
                Font = new Font("Arial", 14)
            };
            this.Controls.Add(lblTimeLeft);

            // Создание кнопок для ответов
            answerBoxes = new Button[4];
            for (int i = 0; i < 4; i++)
            {
                answerBoxes[i] = new Button { Text = "0", Font = new Font("Arial", 18), Location = new Point(20, 60 + i * 60), Size = new Size(240, 50) };
                this.Controls.Add(answerBoxes[i]);
            }

            // Кнопка для начала теста
            btnStart = new Button { Text = "Start the quiz", Location = new Point(20, 300), Size = new Size(240, 50) };
            btnStart.Click += BtnStart_Click;
            this.Controls.Add(btnStart);

            // Таймер
            timer = new System.Windows.Forms.Timer(); // Явное указание на таймер
            timer.Interval = 1000; // Интервал 1 секунда
            timer.Tick += Timer_Tick;
        }

        private void LoadQuestions()
        {
            // Пример вопросов и ответов
            questions = new string[]
            {
            "26 + 34 = ?",
            "47 - 26 = ?",
            "3 × 3 = ?",
            "64 ÷ 8 = ?"
            };

            answers = new int[] { 60, 21, 9, 8 };
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            timeLeft = 30; // Сброс времени
            lblTimeLeft.Text = $"Time Left: {timeLeft} seconds";
            timer.Start(); // Запуск таймера

            // Отображение вопросов
            for (int i = 0; i < answerBoxes.Length; i++)
            {
                answerBoxes[i].Text = $"{questions[i]}";
                answerBoxes[i].Click += AnswerBox_Click; // Подписка на событие клика
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft--;
                lblTimeLeft.Text = $"Time Left: {timeLeft} seconds";
            }
            else
            {
                timer.Stop();
                MessageBox.Show("Время истекло!");
                ResetQuiz();
            }
        }

        private void AnswerBox_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null) // Проверка на null
            {
                int index = Array.IndexOf(answerBoxes, clickedButton);
                if (index != -1)
                {
                    string answer = Microsoft.VisualBasic.Interaction.InputBox("Введите ваш ответ:", "Ваш ответ");
                    if (int.TryParse(answer, out int userAnswer))
                    {
                        if (userAnswer == answers[index])
                        {
                            MessageBox.Show("Правильно!");
                        }
                        else
                        {
                            MessageBox.Show("Неправильно! Правильный ответ: " + answers[index]);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Введите числовое значение.");
                    }
                }
            }
        }

        private void ResetQuiz()
        {
            for (int i = 0; i < answerBoxes.Length; i++)
            {
                answerBoxes[i].Text = "0";
            }
            lblTimeLeft.Text = "Time Left: 30 seconds";
        }
    }

}
