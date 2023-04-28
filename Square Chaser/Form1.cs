using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Imaging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using System.Media;

//Square Chaser
//Valentina Montoya
//ISC3U
//(insert description)

namespace Square_Chaser
{
    public partial class Form1 : Form
    {


        Rectangle player1 = new Rectangle(100, 100, 16, 16);
        Rectangle player2 = new Rectangle(100, 300, 16, 16);

        Rectangle pointSquare = new Rectangle(300, 200, 13, 13);
        Rectangle speedBoost = new Rectangle(384, 364, 17, 17);
        Rectangle enemy = new Rectangle(200, 150, 19, 19);

        SolidBrush blueBrush = new SolidBrush(Color.SlateBlue); //player 1
        SolidBrush greenBrush = new SolidBrush(Color.PaleGreen); // player 2
        Pen purplePen = new Pen(Color.Magenta, 3); // enemy

        SolidBrush yellowBrush = new SolidBrush(Color.Khaki); // point
        Pen limePen = new Pen(Color.Lime, 3); // speed boost 


        //controls 
        //player 1
        bool wDown = false;
        bool sDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;

        //player 2
        bool aDown = false;
        bool dDown = false;
        bool leftDown = false;
        bool rightDown = false;

        //variables
        int player1Score = 0;
        int player2Score = 0;

        int player1Speed = 6;
        int player2Speed = 6;

        int enemyXSpeed = -7;
        int enemyYSpeed = 7;

        //timers
        int speedBoostTimer = 0;
        int speedBoostAppear = 0;
        int hitTimer = 0;
        int player1Timer = 0;
        int player2Timer = 0;

   

        Random randGen = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //player 1
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;

                //player 2
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Left:
                    leftDown = true;
                    break;
                case Keys.Right:
                    rightDown = true;
                    break;

            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //player 1
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;

                //player 2
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Left:
                    leftDown = false;
                    break;
                case Keys.Right:
                    rightDown = false;
                    break;

            }

        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            hitTimer++;
            speedBoostTimer++;
            player1Timer++;
            player2Timer++;
            speedBoostAppear++;

            //music
            SoundPlayer enemyHitSound = new SoundPlayer(Properties.Resources.enemyhit);
            SoundPlayer speedBoostSound = new SoundPlayer(Properties.Resources.speedBoost);
            SoundPlayer pointSound = new SoundPlayer(Properties.Resources.rupee);

            //enemy mechanics
            enemy.X += enemyXSpeed;
            enemy.Y += enemyYSpeed;

            //hits top & bottom
            if (enemy.Y < 50 || enemy.Y > this.Height - enemy.Height)
            {
                enemyYSpeed *= -1;
            }
            //hits sides
            else if (enemy.X < 0 || enemy.X > this.Width - enemy.Height)
            {
                enemyXSpeed *= -1;
            }

            //player 1 movement
            if (wDown == true && player1.Y > 50)  //player1.Y > 0 -> makes sure you are on screen / legal move
            {
                player1.Y -= player1Speed;
            }

            if (sDown == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y += player1Speed;
            }

            if (aDown == true && player1.X > 0)
            {
                player1.X -= player1Speed;
            }

            if (dDown == true && player1.X < this.Width - player1.Width)
            {
                player1.X += player1Speed;
            }

            //move player 2 -> same as p1
            if (upArrowDown == true && player2.Y > 50)
            {
                player2.Y -= player2Speed;
            }

            if (downArrowDown == true && player2.Y < this.Height - player2.Height)
            {
                player2.Y += player2Speed;
            }

            if (leftDown == true && player2.X > 0)
            {
                player2.X -= player2Speed;
            }

            if (rightDown == true && player2.X < this.Width - player2.Width)
            {
                player2.X += player2Speed;
            }

            //check for collision
            //player 1

            if(speedBoostAppear % 90 == 0)
            {
                speedBoost.X = randGen.Next(50, 400);
                speedBoost.Y = randGen.Next(50, 450);
            }

            if (player1.IntersectsWith(speedBoost) && speedBoostTimer > 55)
            {
                speedBoostSound.Play();

                speedBoost.X = randGen.Next(50, 400);
                speedBoost.Y = randGen.Next(50, 450);
                speedBoostTimer = 0;

                player1Speed = 10;
            }
            if (player1Timer == 75)
            {
                player1Timer = 0;
                player1Speed = 6;
            }

            else if (player2.IntersectsWith(speedBoost) && speedBoostTimer > 25)
            {
                speedBoostSound.Play();

                speedBoost.X = randGen.Next(50, 400);
                speedBoost.Y = randGen.Next(50, 450);
                speedBoostTimer = 0;

                player2Speed = 10;
            }
            if (player2Timer == 75)
            {
                player2Timer = 0;
                player2Speed = 6;
            }

            if (player1.IntersectsWith(enemy) && hitTimer > 25)
            {
                enemyHitSound.Play();

                player1Score = player1Score - 1;
                player1ScoreLabel.Text = $"{player1Score}";
                hitTimer = 0;

                player1Speed = 3;
            }
            if (player1Timer == 25)
            {                 
                player1Speed = 5;
                player1Timer = 0;
            }

            if (player2.IntersectsWith(enemy) && hitTimer > 25)
            {
                enemyHitSound.Play();

                player2Score--;
                player2ScoreLabel.Text = $"{player2Score}";
                hitTimer = 0;
                
                player2Speed = 3;
            }
            if (player2Timer == 25)
            {
                player2Speed = 6;
                player2Timer = 0;
            }

            if (player1.IntersectsWith(pointSquare))
            {
                pointSound.Play();

                player1Score = player1Score + 1;
                player1ScoreLabel.Text = $"{player1Score}";

                pointSquare.X = randGen.Next(50, 400);
                pointSquare.Y = randGen.Next(50, 400);

            }
            else if (player2.IntersectsWith(pointSquare))
            {
                pointSound.Play();

                player2Score = player2Score + 1;
                player2ScoreLabel.Text = $"{player2Score}";

                pointSquare.X = randGen.Next(50, 400);
                pointSquare.Y = randGen.Next(50, 400);
            }

            determineWinner();

            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            e.Graphics.FillRectangle(blueBrush, player1);
            e.Graphics.FillRectangle(greenBrush, player2);
            e.Graphics.DrawRectangle(purplePen, enemy);

            e.Graphics.FillRectangle(yellowBrush, pointSquare);
            e.Graphics.DrawRectangle(limePen, speedBoost);

        }

        public void determineWinner()
        {
            SoundPlayer winnerSound = new SoundPlayer(Properties.Resources.yay);

            if (player1Score == 10)
            {
                winnerSound.Play();

                gameTimer.Enabled = false;
                winnerLabel.Visible = true;
                winnerLabel.Text = "Player 1 Wins !!!";
            }
            else if (player2Score == 10)
            {
                winnerSound.Play();

                gameTimer.Enabled = false;
                winnerLabel.Visible = true;
                winnerLabel.Text = "Player 2 Wins !!!";
            }
        }

    }
    
}





