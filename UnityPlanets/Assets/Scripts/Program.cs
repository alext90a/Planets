﻿namespace Planets
{
    class Program
    {
        static void Main(string[] args)
        {
            var constants = new Constants();
            
            var segment01 = new Segment(new SectorCreator(constants, new PlanetComparer(new Player(), constants)), constants);
            segment01.Init();
            //var subSeg = segment01.cellStore[9999];
        }
    }
}