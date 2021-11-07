using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using w8_fejlesztesi_mintak.Abstractions;

namespace w8_fejlesztesi_mintak.Entities
{
    public class Present : Toy
    {
        public SolidBrush BoxColor { get; private set; }
        public SolidBrush RibbonColor { get; private set; }
        public Present(Color ribbon, Color box)//2 color tipusu bemeneti
        {
            BoxColor = new SolidBrush(box); //példányosítsd a SB-t, megkapott color alapján
            RibbonColor = new SolidBrush(ribbon); 
        }
        protected override void DrawImage(Graphics g)
        {
           g.FillRectangle(BoxColor, 0, 0, Width, Height);
           g.FillRectangle(RibbonColor, Width/(float)2.4, 0, Width/5, Height);
           g.FillRectangle(RibbonColor, 0, Width / (float)2.4, Width, Height/5);
        }
    }
}
