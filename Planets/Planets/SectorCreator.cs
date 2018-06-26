using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planets
{
    public class SectorCreator : ISectorCreator
    {
        public ISector CreateSector(int x, int y)
        {
            var sector = new Sector();
            sector.Init();

            sector.GetY = x;
            sector.GetX = y;
            return sector;
        }
    }
}
