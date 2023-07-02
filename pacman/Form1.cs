using System;

using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Dbhandler;
using pacman;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.Common;

namespace pacman
{
    public partial class Form1 : Form
    {
        bool goup, godown, goleft, goright, isGameOver;

        int id, score, playerSpeed,redGhostSpeed, yellowGhostSpeed, pinkGhostX, pinkGhostY;

        private pacman.Form2 login = new pacman.Form2();

       
        public Form1()
        {
            InitializeComponent();
            resetGame();

        }
        
        Dbhandler.SchoolDB db = new Dbhandler.SchoolDB();
        MySqlConnection _connection = new MySqlConnection("Server=localhost;Database=cruddb;Uid=root;Pwd=;");

        private void Form1_Load(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.FormClosed += Form2_FormClosed; 
            form2.ShowDialog(); 
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form2 form2 = (Form2)sender;
            if (form2.DialogResult == DialogResult.OK)
            {
                int score = form2.Score;
               
            }
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                goup = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                godown = true;
            }
            if (e.KeyCode == Keys.Left)
            {
                goleft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = true;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Up)
            {
                goup = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                godown = false;
            }
            if (e.KeyCode == Keys.Left)
            {
                goleft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = false;
            }
            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                resetGame();
            }

        }

        private void mainGameTimer(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + score;




            if (goleft == true)
            {
                pacman.Left -= playerSpeed;
                pacman.Image = Properties.Resources.left;
            }
            if (goright == true)
            {
                pacman.Left += playerSpeed;
                pacman.Image = Properties.Resources.right;
            }
            if (godown == true)
            {
                pacman.Top += playerSpeed;
                pacman.Image = Properties.Resources.down;
            }
            if (goup == true)
            {
                pacman.Top -= playerSpeed;
                pacman.Image = Properties.Resources.Up;
            }

            if (pacman.Left < -5)
            {
                pacman.Left = 600;
            }
            if (pacman.Left > 600)
            {
                pacman.Left = -5;
            }

            if (pacman.Top < -5 )
            {
                pacman.Top = 550;
            }
            if (pacman.Top > 550)
            {
                pacman.Top = 0;
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {


                    if ((string)x.Tag == "wall" )
                    {
                        if (pacman.Bounds.IntersectsWith(x.Bounds))
                        {
                            gameOver("you lose");
                        }
                    }

                    if ((string)x.Tag == "coin" && x.Visible == true)
                    {
                        if (pacman.Bounds.IntersectsWith(x.Bounds))
                        {
                            score += 1;
                            x.Visible = false;
                        }
                    }

                   
                }
            }
        }
        private void resetGame()
        {
            txtScore.Text = "score: 0";
            score= 0;

            redGhostSpeed = 5;
            yellowGhostSpeed = 5;
            pinkGhostX = 5;
            pinkGhostY = 5;
            playerSpeed = 8;

            
            pacman.Left = 31;
            pacman.Top = 426;

            redGhost.Left = 46;
            redGhost.Top = 172;

            yellowGhost.Left = 554;
            yellowGhost.Top = 244;

            pinkGhost.Left = 288;
            pinkGhost.Top = 69;

            foreach(Control x in this.Controls)
            {
                 if(x is PictureBox)
                {
                    x.Visible = true;
                }
            }

            gameTimer.Start();

        }
        private void gameOver(string message)
        {
            
            isGameOver = true;
            gameTimer.Stop();
            db.InsertScore(score, login.naam);
            txtScore.Text += Environment.NewLine + message;
            new MySqlConnection();

        }
      
    }
}
