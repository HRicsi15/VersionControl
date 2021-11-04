using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using w8_fejlesztesi_mintak.Entities;

namespace w8_fejlesztesi_mintak
{
    public partial class Form1 : Form
    {
        private List<Ball> _balls = new List<Ball>();
       
        private BallFactory _factory;

        public BallFactory Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }

        public Form1()
        {
            InitializeComponent();

            Factory = new BallFactory();// töltsd fel a Factory változót egy BallFactory példánnyal
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            var ball = Factory.CreateNew();//Factory CreateNew metódusát felhasználva hozz létre egy Ball példányt.
            _balls.Add(ball);
            ball.Left = -ball.Width;
            mainPanel.Controls.Add(ball);
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var maxPosition =0;
            foreach (var item in _balls)
            {
                item.MoveBall();
                if (maxPosition< item.Left)
                {
                    maxPosition = item.Left;
                }
            }

            if (maxPosition>=100)
            {
                var oldestBall = _balls[0];
                _balls.Remove(oldestBall);
                mainPanel.Controls.Remove(oldestBall);
            }
            
        }
    }
}
