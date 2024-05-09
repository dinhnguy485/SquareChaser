// ISC3U
//6th May 2024
//Square Chaser
//Tri Nguyen

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
        //Set up players variables.
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
        SoundPlayer winnerSound = new SoundPlayer(Properties.Resources.winningSound);

        // Set up players stats.
        int player1Score = 0;
        int player2Score = 0;
        int player1Speed = 4;
        int player2Speed = 4;
        int bullets1Direction = 0;
        int bullets2Direction = 0;
        int player1BulletSpeed = 4;
        int player2BulletSpeed = 4;

        //set up players movement.
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

        //set up the colors and pictures.
        SolidBrush yellowBrush = new SolidBrush(Color.Yellow);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        Pen whitePen = new Pen(Color.White,10);
        Font drawFont = new Font("Times New Roman", 13, FontStyle.Bold);

        public MainForm()
        {
            // generate random position at the start for every orbs.
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

        //set up the keys when it is pressed.
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
                    mPressed = true;
                    break;
            }
        }

        //set up the keys when it is released.
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

        //create a new point position everytime the method is called.
        private void UpdatePointOrb()
        {
            int xNewRandom = randGen.Next(60, 390);
            int yNewRandom = randGen.Next(60, 340);
            pointOrbs.X = xNewRandom;
            pointOrbs.Y = yNewRandom;
        }

        //create a new speed point position everytime the method is called.
        private void UpdateSpeedOrb()
        {
            int xNewRandom = randGen.Next(60, 390);
            int yNewRandom = randGen.Next(60, 340);
            speedOrbs.X = xNewRandom;
            speedOrbs.Y = yNewRandom;
        }

        //create a new negative point position everytime the method is called.
        private void UpdateNegativeOrb()
        {
            int xNewRandom = randGen.Next(60, 390);
            int yNewRandom = randGen.Next(60, 340);
            negativeOrbs.X = xNewRandom;
            negativeOrbs.Y = yNewRandom;
        }

        //Handle when players hit the negative orbs, decrease their speed
        private void HandleNegativeIntersection()
        {
            if (negativeOrbs.IntersectsWith(player1))
            {
                UpdateNegativeOrb();
                player1Speed -= 1;
                player1BulletSpeed -= 1;
                negativePoint.Play();
            }

            if (negativeOrbs.IntersectsWith(player2))
            {
                UpdateNegativeOrb();
                player2Speed -= 1;
                player2BulletSpeed -= 1;
                negativePoint.Play();
            }
        }

        //Handle when players hit the speed orbs, increae their speed
        private void HandleSpeedrInctersection()
        {
            if (speedOrbs.IntersectsWith(player1))
            {
                UpdateSpeedOrb();
                player1Speed += 1;
                player1BulletSpeed += 1;
                speedPoint.Play();
            }
            if (speedOrbs.IntersectsWith(player2))
            {
                UpdateSpeedOrb();
                player2Speed += 1;
                player2BulletSpeed += 1;
                speedPoint.Play();
            }
        }

        //Handle when player hit the point orbs, increase their score
        private void HandlePointOrbIntersections()
        {
            if (pointOrbs.IntersectsWith(player2))
            {
                UpdatePointOrb();
                player2Score++;
                player2ScoreLabel.Text = $"{player2Score}";
                collectPoint.Play();
            }
            if (pointOrbs.IntersectsWith(player1))
            {
                UpdatePointOrb();
                player1Score++;
                player1ScoreLabel.Text = $"{player1Score}";
                collectPoint.Play();
            }
        }

        //Handle when players hit the boundary, teleport them to the other side.
        //For Bullets, handle when it hit the boundary, reset the direction, teleport the bullet to the player's position
        private void HandleBoundary(ref Rectangle player, ref Rectangle bullet, ref int bulletDirection)
        {
            if (player.Y <= 55)
            {
                player.Y = 375;
                bullet.Y = player.Y + 7;
            }
            else if (player.Y >= 375)
            {
                player.Y = 55;
                bullet.Y = player.Y + 7;
            }
            else if (player.X <= 55)
            {
                player.X = 425;
                bullet.X = player.X + 7;
            }
            else if (player.X >= 425)
            {
                player.X = 55;
                bullet.X = player.X + 7;
            }

            ////Reset Bullets position when collides with the boundary

            if (bullet.Y <= 55)
            {
                bullet.Y = player.Y + 7;
                bulletDirection = 0;
                
            }
            else if (bullet.Y >= 375)
            {
                bullet.Y = player.Y + 7;
                bulletDirection = 0;
            }
            else if (bullet.X <= 55)
            {
                bullet.X = player.X + 7;
                bulletDirection = 0;
            }
            else if (bullet.X >= 425)
            {
                bullet.X = player.X + 7;
                bulletDirection = 0;
            }
        }

        //Teleports point orbs when it isn't hit by players every 2 seconds
        private void MovePointOrb()
        {
            if (!pointOrbs.IntersectsWith(player1) && !pointOrbs.IntersectsWith(player2))
            {
                if (orbMoveStopwatch.ElapsedMilliseconds >= 2000)
                {
                    int xNewRandom = randGen.Next(60, 390);
                    int yNewRandom = randGen.Next(60, 340);
                    pointOrbs.X = xNewRandom;
                    pointOrbs.Y = yNewRandom;
                    orbMoveStopwatch.Restart();
                }
            }
        }

        //Make the point orbs move around randomly and when it hit the boundary, teleport to a random position
        private void HandleAutomaticallyMovement()
        {
            if (pointOrbs.Y > 55 && pointOrbs.Y < 375 && pointOrbs.X > 55 && pointOrbs.X < 425)
            {
                int xNewRandom = randGen.Next(-5, 5);
                int yNewRandom = randGen.Next(-5, 5);
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

        }

        //Winning condition
        private void CheckEndGame()
        {
            if (player1Score == 5 || player2Speed == 0 || bulletsPlayer1.IntersectsWith(player2))
            {
                winnerLabel.Text = "Player 1 Win";
                winnerSound.Play();
                gameTimer.Stop();

            }
            else if (player2Score == 5 || player1Speed == 0|| bulletsPlayer2.IntersectsWith(player1))
            {
                winnerLabel.Text = "Player 2 Win";
                winnerSound.Play();
                gameTimer.Stop();
            }
        }

        //player 1 and 2 movement, set the bullet direction for every key.
        private void PlayerMovement()
        {

            if (wPressed == true && player1.Y > 55)
            {
                if (bullets1Direction == 0)
                {

                    bulletsPlayer1.X = player1.X + 7;
                    bulletsPlayer1.Y = player1.Y + 7;
                    player1.Y = player1.Y - player1Speed;
                    bulletsPlayer1.Y = bulletsPlayer1.Y - player1BulletSpeed;
                }

                if (gPressed == true)
                {
                    bullets1Direction = 1;

                }
                if (bullets1Direction != 0)
                {
                    player1BulletSpeed = 0;
                    player1.Y = player1.Y - player1Speed;
                    bulletsPlayer1.Y = bulletsPlayer1.Y - player1BulletSpeed;
                }

            }

            if (sPressed == true && player1.Y < 375)
            {
                if (bullets1Direction == 0)
                {
                    bulletsPlayer1.X = player1.X + 7;
                    bulletsPlayer1.Y = player1.Y + 7;
                    player1.Y = player1.Y + player1Speed;
                    bulletsPlayer1.Y = bulletsPlayer1.Y + player1BulletSpeed;
                }
                if (gPressed == true)
                {
                    bullets1Direction = 2;
                }
                if (bullets1Direction != 0)
                {
                    player1BulletSpeed = 0;
                    player1.Y = player1.Y + player1Speed;
                    bulletsPlayer1.Y = bulletsPlayer1.Y + player1BulletSpeed;
                }
            }

            if (aPressed == true && player1.X > 55)
            {
                if (bullets1Direction == 0)
                {
                    bulletsPlayer1.X = player1.X + 7;
                    bulletsPlayer1.Y = player1.Y + 7;
                    player1.X = player1.X - player1Speed;
                    bulletsPlayer1.X = bulletsPlayer1.X - player1BulletSpeed;
                }
               
                if (gPressed == true)
                {
                    bullets1Direction = 3;
                }
                if (bullets1Direction != 0)
                {
                    player1BulletSpeed = 0;
                    player1.X = player1.X - player1Speed;
                    bulletsPlayer1.X = bulletsPlayer1.X - player1BulletSpeed;
                }
            }

            if (dPressed == true && player1.X < 425)
            {
                if (bullets1Direction == 0)
                {
                    bulletsPlayer1.X = player1.X + 7;
                    bulletsPlayer1.Y = player1.Y + 7;
                    player1.X = player1.X + player1Speed;
                    bulletsPlayer1.X = bulletsPlayer1.X + player1BulletSpeed;
                }

                if (gPressed == true)
                {
                    bullets1Direction = 4;
                }

                if (bullets1Direction != 0)
                {
                    player1BulletSpeed = 0;
                    player1.X = player1.X + player1Speed;
                    bulletsPlayer1.X = bulletsPlayer1.X + player1BulletSpeed;
                }
            }

            if (upPressed == true && player2.Y > 55)
            {
                if (bullets2Direction == 0)
                {
                    bulletsPlayer2.X = player2.X + 7;
                    bulletsPlayer2.Y = player2.Y + 7;
                    player2.Y = player2.Y - player2Speed;
                    bulletsPlayer2.Y = bulletsPlayer2.Y - player2BulletSpeed;
                }

                if (mPressed == true)
                {
                    bullets2Direction = 1;
                }

                if (bullets2Direction != 0)
                {
                    player2BulletSpeed = 0;
                    player2.Y = player2.Y - player2Speed;
                    bulletsPlayer2.Y = bulletsPlayer2.Y - player2BulletSpeed;
                }
            }

            if (downPressed == true && player2.Y < 375)
            {
                if (bullets2Direction == 0)
                {
                    bulletsPlayer2.X = player2.X + 7;
                    bulletsPlayer2.Y = player2.Y + 7;
                    player2.Y = player2.Y + player2Speed;
                    bulletsPlayer2.Y = bulletsPlayer2.Y + player2BulletSpeed;
                }

                if (mPressed == true)
                {
                    bullets2Direction = 2;
                }

                if (bullets2Direction != 0)
                {
                    player2BulletSpeed = 0;
                    player2.Y = player2.Y + player2Speed;
                    bulletsPlayer2.Y = bulletsPlayer2.Y + player2BulletSpeed;
                }
            }

            if (leftPressed == true && player2.X > 55)
            {
                if (bullets2Direction == 0)
                {
                    bulletsPlayer2.X = player2.X + 7;
                    bulletsPlayer2.Y = player2.Y + 7;
                    player2.X = player2.X - player2Speed;
                    bulletsPlayer2.X = bulletsPlayer2.X - player2BulletSpeed;
                }

                if (mPressed == true)
                {
                    bullets2Direction = 3;
                }

                if (bullets2Direction != 0)
                {
                    player2BulletSpeed = 0;
                    player2.X = player2.X - player2Speed;
                    bulletsPlayer2.X = bulletsPlayer2.X - player2BulletSpeed;
                }
            }

            if (rightPressed == true && player2.X < 425)
            {
                if (bullets2Direction == 0)
                {
                    bulletsPlayer2.X = player2.X + 7;
                    bulletsPlayer2.Y = player2.Y + 7;
                    player2.X = player2.X + player2Speed;
                    bulletsPlayer2.X = bulletsPlayer2.X + player2BulletSpeed;
                }

                if (mPressed == true)
                {
                    bullets2Direction = 4;
                }

                if (bullets2Direction != 0)
                {
                    player2BulletSpeed = 0;
                    player2.X = player2.X + player2Speed;
                    bulletsPlayer2.X = bulletsPlayer2.X + player2BulletSpeed;
                }
            }
        }

        //Bullet direction conditions
        private void HandleBulletsDirection()
        {
            if (bullets1Direction == 1)
            {
                bulletsPlayer1.Y -= player1Speed;
            }
            else if (bullets1Direction == 2)
            {
                bulletsPlayer1.Y += player1Speed;
            }
            else if (bullets1Direction == 3)
            {
                bulletsPlayer1.X -= player1Speed;
            }
            else if (bullets1Direction == 4)
            {
                bulletsPlayer1.X += player1Speed;
            }

            if (bullets2Direction == 1)
            {
                bulletsPlayer2.Y -= player2Speed;
            }
            else if (bullets2Direction == 2)
            {
                bulletsPlayer2.Y += player2Speed;
            }
            else if (bullets2Direction == 3)
            {
                bulletsPlayer2.X -= player2Speed;
            }
            else if (bullets2Direction == 4)
            {
                bulletsPlayer2.X += player2Speed;
            }
        }

        //Call all of the methods
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            HandleBulletsDirection();
            PlayerMovement();
            HandleAutomaticallyMovement();
            MovePointOrb();
            HandleBoundary(ref player1, ref bulletsPlayer1, ref bullets1Direction);
            HandleBoundary(ref player2, ref bulletsPlayer2, ref bullets2Direction);
            HandlePointOrbIntersections();
            HandleSpeedrInctersection();
            HandleNegativeIntersection();
            CheckEndGame();
            Refresh();
        }
        
        // Game Drawing
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(player1Image, player1);
            e.Graphics.DrawImage(player2Image, player2);
            e.Graphics.DrawRectangle(whitePen, 50, 50, 400, 350);
            e.Graphics.FillRectangle(whiteBrush,pointOrbs);
            e.Graphics.FillRectangle(yellowBrush, speedOrbs);
            e.Graphics.FillRectangle(redBrush, negativeOrbs);
            e.Graphics.DrawImage(player1Image, bulletsPlayer1);
            e.Graphics.DrawImage(player2Image, bulletsPlayer2);
            //Game Rules
            e.Graphics.DrawString("Reach 5 to win\nWhite Orbs = point\nYellow Orbs = Speed Boost\nRed Orbs = Negative Boost\nPlayer 1 press G to shoot\nPlayer 2 press M to shoot\n", drawFont, whiteBrush, 50, 420);
        }
    }
}
