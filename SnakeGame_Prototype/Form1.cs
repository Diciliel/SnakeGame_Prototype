using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace SnakeGame_Prototype
{
    public partial class SnakeGame : Form
    {
        private List<Body> snake = new List<Body>();
        Random rnd = new Random();
        Head head = new Head();
        private Food food;

        int score;

        Bitmap canvasBitmap;
        Graphics workingGraphics;

        public SnakeGame()
        {
            InitializeComponent();

            snake.Clear();

            head.posx = rnd.Next(0, canvas.Width / 20) * 20;
            head.posy = rnd.Next(0, canvas.Height / 20) * 20;

            CreateFood();

            GameTimer.Start();
        }
        private void CreateFood()
        {
            while(true)
            {
                bool isOnSnake = false;
                int posx = rnd.Next(0, 580 / 20) * 20;
                int posy = rnd.Next(0, 380 / 20) * 20;

                foreach (var body in snake)
                {
                    if (body.posx == posx & body.posy == posy)
                    {
                        isOnSnake = true;
                        break;
                    }
                }
                if (!isOnSnake)
                {
                    food = new Food(posx, posy);
                    break;
                }                
            }                      
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {

            if (snake.Count > 0) 
            {
                for (int i = snake.Count - 1; i > 0; i--) 
                {
                    snake[i].posx = snake[i - 1].posx;
                    snake[i].posy = snake[i - 1].posy;
                }

                snake[0].posx = head.posx;
                snake[0].posy = head.posy;
            }

            head.Move(canvas.Width, canvas.Height);

            if(head.posx == food.posx && head.posy == food.posy)
            {
                Body body = new Body();                
                body.posx = head.posx;
                body.posy = head.posy;
                
                snake.Insert(0, body);
                score++;
                lbl_score.Text = Convert.ToString(score);
                CreateFood();                
            }

            canvasBitmap = new Bitmap(canvas.Width, canvas.Height);
            workingGraphics = Graphics.FromImage(canvasBitmap);

            workingGraphics.Clear(canvas.BackColor);

            foreach (Shapes body in snake)
            {
                body.Draw(workingGraphics);
            }

            head.Draw(workingGraphics);
            food.Draw(workingGraphics);
            
            canvas.Image = canvasBitmap;

            for (int i = 1; i < snake.Count; i++)
            {
                if (snake[i].posx == head.posx && snake[i].posy == head.posy)
                {
                    GameTimer.Stop();
                    MessageBox.Show("You lost!");
                }
            }
        }

        private void SnakeGame_KeyDown(object sender, KeyEventArgs e)
        {
            if (head.posx > 0 && head.posx < canvas.Width && head.posy > 0 && head.posy < canvas.Height) 
            {
                switch (e.KeyCode)
                {
                    case Keys.Left:
                        head.GoLeft();
                        break;
                    case Keys.Right:
                        head.GoRight(canvas.Width);
                        break;
                    case Keys.Up:
                        head.GoUp();
                        break;
                    case Keys.Down:
                        head.GoDown(canvas.Height);
                        break;
                    default:
                        return;
                }
            }            
        }
    }
}
