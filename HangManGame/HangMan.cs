using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace HangManGame
{
    public partial class HangMan : Form
    {
        public HangMan()
        {
            InitializeComponent();
            btnSubmitLetter.Enabled = false;
            btnSubmitWord.Enabled = false;
        }

        private string word = "";
        List<Label> labels = new List<Label>();
        private int amount = 0;

        enum BodyParts
        {
            Head,
            Left_Eye,
            Right_Eye,
            Mouth,
            Right_Arm,
            Left_Arm,
            Body,
            Right_Leg,
            Left_Leg
        }

        void DrawBodyPart(BodyParts bp)
        {
            Graphics g = panel1.CreateGraphics();
            Pen p = new Pen(Color.Blue, 2);
            if (bp == BodyParts.Head)
            {
                g.DrawEllipse(p, 40, 50, 40, 40);
            }
            else if (bp == BodyParts.Left_Eye)
            {
                SolidBrush s = new SolidBrush(Color.Black);
                g.FillEllipse(s, 50, 60, 5, 5);
            }
            else if (bp == BodyParts.Right_Eye)
            {
                SolidBrush s = new SolidBrush(Color.Black);
                g.FillEllipse(s, 63, 60, 5, 5);
            }
            else if (bp == BodyParts.Mouth)
            {
                g.DrawArc(p, 50, 60, 20, 20, 45, 90);
            }
            else if (bp == BodyParts.Body)
            {
                g.DrawLine(p, new Point(60, 90), new Point(60, 170));
            }
            else if (bp == BodyParts.Left_Arm)
            {
                g.DrawLine(p, new Point(60, 100), new Point(30, 85));
            }
            else if (bp == BodyParts.Right_Arm)
            {
                g.DrawLine(p, new Point(60, 100), new Point(90, 85));
            }
            else if (bp == BodyParts.Left_Leg)
            {
                g.DrawLine(p, new Point(60, 170), new Point(30, 190));
            }
            else if (bp == BodyParts.Right_Leg)
            {
                g.DrawLine(p, new Point(60, 170), new Point(90, 190));
            }
        }

        string GetRandom()
        {
            WebClient wc = new WebClient();
            string wordlist =
                wc.DownloadString(
                    "https://drive.google.com/uc?authuser=0&id=1JAxpAvZ5bC5y33udFwGdukFxAJ1uF6Lb&export=download");
            string[] words = wordlist.Split('\n');
            Random ran = new Random();
            return words[ran.Next(0, words.Length - 1)];
        }

        void DrawingHangPost()
        {
            Graphics g = panel1.CreateGraphics();
            Pen p = new Pen(Color.Brown, 10);
            g.DrawLine(p, new Point(130, 218), new Point(130, 5));
            g.DrawLine(p, new Point(135, 5), new Point(65, 5));
            g.DrawLine(p, new Point(60, 0), new Point(60, 50));
            //DrawBodyPart(BodyParts.Head);
            //DrawBodyPart(BodyParts.Left_Eye);
            //DrawBodyPart(BodyParts.Right_Eye);
            //DrawBodyPart(BodyParts.Mouth);
            //DrawBodyPart(BodyParts.Body);
            //DrawBodyPart(BodyParts.Left_Arm);
            //DrawBodyPart(BodyParts.Right_Arm);
            //DrawBodyPart(BodyParts.Left_Leg);
            //DrawBodyPart(BodyParts.Right_Leg);
            MakeLabel();
        }

        void MakeLabel()
        {
            word = GetRandom();
            //MessageBox.Show("The Word is : "+ word);
            char[] chars = word.ToCharArray();
            int between = 330 / chars.Length - 1;
            for (int i = 0; i < chars.Length - 1; i++)
            {
                labels.Add(new Label());
                labels[i].Location = new Point((i * between) + 10, 80);
                labels[i].Text = "͟";
                labels[i].Parent = groupBox2;
                labels[i].BringToFront();
                labels[i].CreateControl();
            }

            label1.Text = "Word Length : " + (chars.Length - 1).ToString();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            DrawingHangPost();
        }

        private void btnSubmitLetter_Click(object sender, EventArgs e)
        {
            char letter = textBox1.Text.ToLower().ToCharArray()[0];
            if (!char.IsLetter(letter))
            {
                textBox1.Text = "";
                MessageBox.Show("You Can only Submit Letters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (word.Contains(letter))
            {
                char[] letters = word.ToCharArray();
                for (int i = 0; i <= letters.Length - 1; i++)
                {
                    if (letters[i] == letter)
                    {
                        labels[i].Text = letter.ToString();
                    }
                    textBox1.Text = "";
                }
                foreach (Label l in labels)
                {
                    if (l.Text == "͟") return;
                }

                MessageBox.Show("You Win","Congrates");
                ResetGame();
            }
            else
            {
                MessageBox.Show("You Missed!The Character That you guessed is not contains in the word", "Sorry");
                label2.Text += " " + letter.ToString() + ",";
                DrawBodyPart((BodyParts) amount);
                amount++;
                if (amount > 8)
                {
                    MessageBox.Show("Sorry You Loose.\nThe word was :\t " + word, "Game Over");
                    ResetGame();
                }
            }

            textBox1.Text = "";
        }

        void ResetGame()
        {
            Graphics g = panel1.CreateGraphics();
            g.Clear(panel1.BackColor);
            GetRandom();
            MakeLabel();
            DrawingHangPost();
            label2.Text = "Missed :";
            label3.Text = "Missed Words :";
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void btnSubmitWord_Click(object sender, EventArgs e)
        {
            if (word == textBox2.Text + "\r")
            {
                char[] letters = word.ToCharArray();
                char[] txtBoxWord = textBox2.Text.ToCharArray();
                for (int i = 0; i <= letters.Length - 2; i++)
                {
                        labels[i].Text = txtBoxWord[i].ToString();
                }
                MessageBox.Show("You Win","Congrates");
                ResetGame();
            }
            else
            {
                MessageBox.Show("The word you guessed is Wrong", "Sorry");
                label3.Text += " " + textBox2.Text + ",";
                DrawBodyPart((BodyParts) amount);
                amount++;
                if (amount > 8)
                {
                    MessageBox.Show("Sorry You Loose.\nThe word was :\t " + word, "Game Over");
                    ResetGame();
                    //else
                    //{
                    //    ResetGame();
                    //    MessageBox.Show("You Have Won!", "Congrates");
                    //}
                }
            }
            textBox2.Text = ""; 
        }

        private void HangMan_FormClosed(object sender, FormClosedEventArgs e)
        {
            t = new Timer();
            t.Interval = 2000;
            t.Start();
            SpashScreen s = new SpashScreen();
            s.lblGoodBye.Visible = true;
            s.Show();
            t.Tick += T_Tick;
        }
        void exit()
        {
            Application.Exit();
        }
        public static Timer t;

        public void T_Tick(object sender, EventArgs e)
        {
            t.Stop();
            this.Hide();
            exit();
        }
        private void HangMan_FormClosing(object sender, FormClosingEventArgs e)
        {
            //SpashScreen s = new SpashScreen();
            //s.lblGoodBye.Visible = true;
            //s.Show();
            //this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                btnSubmitLetter.Enabled = false;
            }
            else
            {
                btnSubmitLetter.Enabled = true;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                btnSubmitWord.Enabled = false;
            }
            else
            {
                btnSubmitWord.Enabled = true;
            }
        }
    }
}
