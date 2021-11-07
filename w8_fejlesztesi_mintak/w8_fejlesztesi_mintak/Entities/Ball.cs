using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using w8_fejlesztesi_mintak.Abstractions;

namespace w8_fejlesztesi_mintak.Entities
{
    public class Ball : Toy
    {
                    /* public Ball() //ez már nem kell, toyban van. 
                     {
                         AutoSize = false;
                         Width = 50;
                         Height = 50; 
                         Paint += Ball_Paint;
                     }

                     private void Ball_Paint(object sender, PaintEventArgs e)
                     {
                         DrawImage(e.Graphics);
                     }*/


        public SolidBrush BallColor { get; private set; }//SB tipúsú prop

        public Ball(Color color)
        {
            BallColor = new SolidBrush(color);//példányosítsd a SB-t, megkapott color alapján
        }
        protected override void DrawImage(Graphics g)//meglévő toy fgv-t módosítjuk
        {
            //g.FillEllipse(new SolidBrush(Color.Blue), 0, 0, Width, Height); //kék kör kirajzolása
            g.FillEllipse(BallColor, 0, 0, Width, Height);
        }
               
        
        
        
        /*
                public void MoveBall()
                {
                    Left +=1;//eggyel növeljük 
                }*/
    }
}
