using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace pacman
{
    public partial class Form2 : Form
    {
        private Dbhandler.SchoolDB db = new Dbhandler.SchoolDB();
        private int user;
        public int Score { get; private set; } = 0;
        public Form2()
        {
            InitializeComponent();
        }


        public static class Program
        {
            [STAThread]
            static void Main()
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                //Dbhandler.SchoolDB db = new Dbhandler.SchoolDB();
                int user = 0;

                Form2 form2 = new Form2(user);
                form2.ShowDialog(); 

                if (form2.DialogResult == DialogResult.OK)
                {
                    Application.Run(new Form1());
                }
            }
        }






        public Form2(int user) : this()
        {
            this.user = user;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public string naam = null;
        private void button1_Click(object sender, EventArgs e)
        {
            if (db.CreateUser(textBox1.Text))
            {
              //  Score = int.Parse(textBox1.Text);
                bool success = db.InsertScore(Score, textBox1.Text);
                naam = textBox1.Text;

                if (success)
                {
                    MessageBox.Show("Score inserted successfully.");
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Failed to insert score.");
                }
            }
        }


    }








}


