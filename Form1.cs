using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SquareChaser
{
    public partial class MainForm : Form
    {
        Rectangle player1 = new Rectangle(10, 350, 10, 10);
        Rectangle player2 = new Rectangle(400, 350, 10,10);
        Rectangle pointOrbs = new Rectangle(250,225,5,5);
        Rectangle speedOrbs = new Rectangle(340, 400, 5, 5);

        int player1Score = 0;
        int player2Score = 0;

        int playerSpeed = 4;

        bool wPressed = false;
        bool aPressed = false;
        bool dPressed = false;
        bool sPressed = false;

        bool upPressed = false;
        bool downPressed = false;
        bool leftPressed = false;
        bool rightPressed = false;

        Random randGen = new Random();
        SolidBrush blackBrush = new SolidBrush(Color.Black);
        SolidBrush yellowBrush = new SolidBrush(Color.Yellow);
        SolidBrush pinkBrush = new SolidBrush(Color.HotPink);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush blueBrush = new SolidBrush(Color.Blue);
        Pen whitePen = new Pen(Color.White);

        Random XrandGen = new Random();
        Random YrandGen = new Random();

        public MainForm()
        {
            InitializeComponent();
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
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {

        }
        
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
