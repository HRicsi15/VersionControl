using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w8_fejlesztesi_mintak.Entities
{
    public class BallFactory //fontos hogy public 
    {
        public Ball CreateNew() //Ball viszatérési érték
        {
            return new Ball(); //fgv-ben léterhozunk 1Ball példányt és visszaadjuk az értékét
        }
    }
}
