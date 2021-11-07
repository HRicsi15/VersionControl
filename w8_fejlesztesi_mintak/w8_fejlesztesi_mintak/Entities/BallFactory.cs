using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using w8_fejlesztesi_mintak.Abstractions;

namespace w8_fejlesztesi_mintak.Entities
{
    public class BallFactory : IToyFactory //fontos hogy public 
    {
        public Color BallColor { get; set; }

        public Toy CreateNew() //Ball/ Toy viszatérési érték
        {
            return new Ball(BallColor); //fgv-ben léterhozunk 1Ball példányt és visszaadjuk az értékét
            //propertyként() a property értékét kapja BallColor
        }
    }
}
