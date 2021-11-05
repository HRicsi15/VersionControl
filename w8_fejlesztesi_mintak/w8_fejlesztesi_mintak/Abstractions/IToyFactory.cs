using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w8_fejlesztesi_mintak.Abstractions
{
   public interface IToyFactory //publicnak kell lennie!
    {
        Toy CreateNew(); //fgv, visszateresi értéke Toy típusú. Ezt is visszük tovább
    }
}
