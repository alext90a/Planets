using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planets
{
    public class Segment
    {
        public Segment()
        {

            for (int i = 0; i < totalSize; ++i)
            {
                cellStore[i] = new Sector();
                cellStore[i].Init();

                cellStore[i].GetY = i / 100;
                cellStore[i].GetX = i - cellStore[i].GetY * 100;
            }
        }
        public Sector[] cellStore = new Sector[totalSize];
        const int totalSize = 10000;
    }
}
