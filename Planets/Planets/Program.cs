﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planets
{
    class Program
    {
        static void Main(string[] args)
        {
            var segment01 = new Segment(new SectorCreator());
            segment01.Init();
            //var subSeg = segment01.cellStore[9999];
        }
    }
}
