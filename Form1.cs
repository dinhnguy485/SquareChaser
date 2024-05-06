using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SquareChaser
{
    public partial class MainForm : Form
    {
        Random randGen = new Random();
        Stopwatch orbMoveStopwatch = new Stopwatch();

        Rectangle player1 = new Rectangle(100, 350, 20, 20);
        Rectangle player2 = new Rectangle(400, 350, 20,20);
        Rectangle pointOrbs = new Rectangle(250,225,5,5);
        Rectangle speedOrbs = new Rectangle(200, 200, 5, 5);
        Rectangle bulletsPlayer1 = new Rectangle(107, 357, 5, 5);
        Rectangle bulletsPlayer2= new Rectangle(407, 357, 5, 5);
        Rectangle negativeOrbs = new Rectangle(230, 275, 5,5);

        Image player1Image = Properties.Resources.player1;
        Image player2Image = Properties.Resources.player2;
        SoundPlayer collectPoint = new SoundPlayer(Properties.Resources.collectPoint);
        SoundPlayer speedPoint = new SoundPlayer(Properties.Resources.speedPoint);
        SoundPlayer negativePoint = new SoundPlayer(Properties.Resources.negativePoint);


        int player1Score = 0;
        int player2Score = 0;

        int player1Speed = 4;
        int player2Speed = 4;

        bool wPressed = false;
        bool aPressed = false;
        bool dPressed = false;
        bool sPressed = false;

        bool upPressed = false;
        bool downPressed = false;
        bool leftPressed = false;
        bool rightPressed = false;

        bool gPressed = false;
        bool mPressed = false;

        SolidBrush blackBrush = new SolidBrush(Color.Black);
        SolidBrush yellowBrush = new SolidBrush(Color.Yellow);
        SolidBrush pinkBrush = new SolidBrush(Color.HotPink);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush blueBrush = new SolidBrush(Color.LightSkyBlue);
        SolidBrush greenBrush = new SolidBrush(Color.SeaGreen);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        Pen whitePen = new Pen(Color.White,10);
        Font drawFont = new Font("Times New Roman", 13, FontStyle.Bold);



        public MainForm()
        {
            InitializeComponent();
            int xRandom = randGen.Next(60, 390);
            int yRandom = randGen.Next(60, 340);
            pointOrbs.X = xRandom;
            pointOrbs.Y = yRandom;

            xRandom = randGen.Next(60, 390);
            yRandom = randGen.Next(60, 340);
            speedOrbs.X = xRandom;
            speedOrbs.Y = yRandom;


            xRandom = randGen.Next(60, 390);
            yRandom = randGen.Next(60, 340);
            negativeOrbs.X = xRandom;
            negativeOrbs.Y = yRandom;
            orbMoveStopwatch.Start();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wPressed = true;
                    break;
                case Keys.D:
                    dPressed = true; 
                    break;
                case Keys.A:
                    aPressed = true;
                    break;
                case Keys.S:
                    sPressed = true;
                    break;
                case Keys.G:
                    gPressed = true;
                    break;

                case Keys.Up:
                    upPressed = true;
                    break;
                case Keys.Down:
                    downPressed = true; 
                    break;
                case Keys.Left:
                    leftPressed = true;
                    break;
                case Keys.Right:
                    rightPressed = true;
                    break;
                case Keys.M:
                    mPressed = false;
                    break;
            }
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wPressed = false;
                    break;
                case Keys.D:
                    dPressed = false;
                    break;
                case Keys.A:
                    aPressed = false;
                    break;
                case Keys.S:
                    sPressed = false;
                    break;
                case Keys.G:
                    gPressed = false;
                    break;


                case Keys.Up:
                    upPressed = false;
                    break;
                case Keys.Down:
                    downPressed = false;
                    break;
                case Keys.Left:
                    leftPressed = false;
                    break;
                case Keys.Right:
                    rightPressed = false;
                    break;
                case Keys.M:
                    mPressed = false;
                    break;
            }
        }



        private void UpdatePointOrb()
        {
            int xNewRandom = randGen.Next(60, 390);
            int yNewRandom = randGen.Next(60, 340);
            pointOrbs.X = xNewRandom;
            pointOrbs.Y = yNewRandom;
        }

        private void UpdateSpeedOrb()
        {
            int xNewRandom = randGen.Next(60, 390);
            int yNewRandom = randGen.Next(60, 340);
            speedOrbs.X = xNewRandom;
            speedOrbs.Y = yNewRandom;
        }

        private void UpdateNegativeOrb()
        {
            int xNewRandom = randGen.Next(60, 390);
            int yNewRandom = randGen.Next(60, 340);
            negativeOrbs.X = xNewRandom;
            negativeOrbs.Y = yNewRandom;
        }

        private void HandleNegativeIntersection()
        {
            if (negativeOrbs.IntersectsWith(player1))
            {
                UpdateNegativeOrb();
                player1Speed -= 1;
                negativePoint.Play();


            }
            if (negativeOrbs.IntersectsWith(player2))
            {
                UpdateNegativeOrb();
                player2Speed -= 1;
                negativePoint.Play();

            }
        }

        private void HandleSpeedrInctersection()
        {
            if (speedOrbs.IntersectsWith(player1))
            {
                UpdateSpeedOrb();
                player1Speed += 1;
                speedPoint.Play();
            }
            if (speedOrbs.IntersectsWith(player2))
            {
                UpdateSpeedOrb();
                player2Speed += 1;
                speedPoint.Play();
            }
        }

        private void HandlePointOrbIntersections()
        {
            if (pointOrbs.IntersectsWith(player2))
            {
                UpdatePointOrb();
                player2Score++;
                player2ScoreLabel.Text = $"{player2Score}";
                collectPoint.Play();
                orbMoveStopwatch.Restart();
            }
            if (pointOrbs.IntersectsWith(player1))
            {
                UpdatePointOrb();
                player1Score++;
                player1ScoreLabel.Text = $"{player1Score}";
                collectPoint.Play();
                orbMoveStopwatch.Restart();
            }
        }

        private void HandleBoundary(Rectangle player)
        {
            if (player.Y <= 55)
            {
                player.Y = 375;
            }
            else if (player.Y >= 375)
            {
                player.Y = 55;
            }
            else if (player.X <= 55)
            {
                player.X = 425;
            }
            else if (player.X >= 425)
            {
                player.X = 55;
            }
        }

        private void MovePointOrb()
        {
            if (!pointOrbs.IntersectsWith(player1) && !pointOrbs.IntersectsWith(player2))
            {
                if (orbMoveStopwatch.ElapsedMilliseconds >= 5000)
                {
                    int xNewRandom = randGen.Next(60, 390);
                    int yNewRandom = randGen.Next(60, 340);
                    pointOrbs.X = xNewRandom;
                    pointOrbs.Y = yNewRandom;
                    orbMoveStopwatch.Restart();
                }
            }
        }

        private void MoveSpeedOrb()
        {
            if (!speedOrbs.IntersectsWith(player1) && !speedOrbs.IntersectsWith(player2))
            {
                if (orbMoveStopwatch.ElapsedMilliseconds >= 3000)
                {
                    int xNewRandom = randGen.Next(60, 390);
                    int yNewRandom = randGen.Next(60, 340);
                    speedOrbs.X = xNewRandom;
                    speedOrbs.Y = yNewRandom;
                    orbMoveStopwatch.Restart();
                }
            }
        }

        private void CheckEndGame()
        {
            if (player1Score == 5 || player2Speed == 0)
            {
                winnerLabel.Text = "Player 1 Win";
                gameTimer.Stop();

            }
            else if (player2Score == 5 || player1Speed == 0)
            {
                winnerLabel.Text = "Player 2 Win";
                gameTimer.Stop();
            }
        }


        private void gameTimer_Tick(object sender, EventArgs e)
        {
          
            if (pointOrbs.Y > 55 && pointOrbs.Y <375 && pointOrbs.X >55 && pointOrbs.X <425)
            {
                int xNewRandom = randGen.Next(-2, 2);
                int yNewRandom = randGen.Next(-2, 2);
                pointOrbs.Y += yNewRandom;
                pointOrbs.X += xNewRandom;

                if (pointOrbs.Y <= 55)
                {
                   
                    yNewRandom = randGen.Next(60, 340);
                    pointOrbs.Y = yNewRandom;
                }
                else if (pointOrbs.Y >= 375)
                {
                    yNewRandom = randGen.Next(60, 340);
                    pointOrbs.Y = yNewRandom;
                }
                else if (pointOrbs.X <= 55)
                {
                    xNewRandom = randGen.Next(60, 390);
                    pointOrbs.X = xNewRandom;
                }
                else if (pointOrbs.X >= 425)
                {
                    xNewRandom = randGen.Next(60, 390);
                    pointOrbs.X = xNewRandom;
                }
            }



            if (wPressed == true && player1.Y > 55)
            {
                player1.Y = player1.Y - player1Speed;
                bulletsPlayer1.Y = bulletsPlayer1.Y - player1Speed;

            }

            if (sPressed == true && player1.Y < 375)
            {
                player1.Y = player1.Y + player1Speed;
                bulletsPlayer1.Y = bulletsPlayer1.Y + player1Speed;

            }

            if (aPressed == true && player1.X > 55)
            {
                player1.X = player1.X - player1Speed;
                bulletsPlayer1.X = bulletsPlayer1.X - player1Speed;
            }

            if (dPressed == true && player1.X < 425)
            {
                player1.X = player1.X + player1Speed;
                bulletsPlayer1.X = bulletsPlayer1.X + player1Speed;

            }

            // not working
            if (gPressed == true && bulletsPlayer1.Y>55)
            { 
                    bulletsPlayer1.Y -= 3;
            }




            if (upPressed == true && player2.Y > 55)
            {
                player2.Y = player2.Y - player2Speed;
                bulletsPlayer2.Y = bulletsPlayer2.Y - player2Speed;

            }

            if (downPressed == true && player2.Y < 375)
            {
                player2.Y = player2.Y + player2Speed;
                bulletsPlayer2.Y = bulletsPlayer2.Y + player2Speed;

            }

            if (leftPressed == true && player2.X > 55)
            {
                player2.X = player2.X - player2Speed;
                bulletsPlayer2.X = bulletsPlayer2.X - player2Speed;

            }

            if (rightPressed == true && player2.X < 425)
            {
                player2.X = player2.X + player2Speed;
                bulletsPlayer2.X = bulletsPlayer2.X + player2Speed;
            }

            //if (player1.Y <= 55)
            //{
            //    player1.Y = 375;
            //}
            //else if (player1.Y >= 375)
            //{
            //    player1.Y = 55;
            //}
            //else if (player1.X <= 55)
            //{
            //    player1.X = 425;
            //}
            //else if (player1.X >= 425)
            //{
            //    player1.X = 55;
            //}

            //if (player2.Y <= 55)
            //{
            //    player2.Y = 375;
            //}
            //else if (player2.Y >= 375)
            //{
            //    player2.Y = 55;
            //}
            //else if (player2.X <= 55)
            //{
            //    player2.X = 425;
            //}
            //else if (player2.X >= 425)
            //{
            //    player2.X = 55;
            //}

            MovePointOrb();
            MoveSpeedOrb();
            HandleBoundary(player1);
            HandlePointOrbIntersections();
            HandleSpeedrInctersection();
            HandleNegativeIntersection();
            CheckEndGame();
            Refresh();
        }
        
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics.FillRectangle(blueBrush, player1);
            //e.Graphics.FillRectangle(pinkBrush, player2);
            e.Graphics.DrawImage(player1Image, player1);
            e.Graphics.DrawImage(player2Image, player2);
            e.Graphics.DrawRectangle(whitePen, 50, 50, 400, 350);
            e.Graphics.FillRectangle(whiteBrush,pointOrbs);
            e.Graphics.FillRectangle(yellowBrush, speedOrbs);
            e.Graphics.FillRectangle(redBrush, negativeOrbs);
            e.Graphics.FillRectangle(greenBrush, bulletsPlayer1);
            e.Graphics.FillRectangle(greenBrush, bulletsPlayer2);
            e.Graphics.DrawString("Reach 5 to win\nWhite Orbs = point\nYellow Orbs = Speed Boost\nRed Orbs = Negative Boost\nPlayer 1 press G to shoot\nPlayer 2 press M to shoot\n", drawFont, whiteBrush, 50, 420);
        }
    }
}
