using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planets
{
    public class Constants : IConstants
    {
        public Constants()
        {
            MinCameraSize = 5;
            MaxCameraSize = 100;
            SectorSideSize = 100;
            CellsInSector = SectorSideSize * SectorSideSize;
            SectorsInSegment = (MaxCameraSize * MaxCameraSize) / (CellsInSector);
            PlanetsPercent = 0.3f;
            PlanetsInSector = (int)(CellsInSector * PlanetsPercent);
            MaxPlanetScore = 10000;
            MinPlanetScore = 0;
        }
        public int SectorsInSegment { get; }
        public int PlanetsInSector { get; }
        public int CellsInSector { get; }
        public int SectorSideSize { get; }
        public float PlanetsPercent { get; }
        public int MaxCameraSize { get; }
        public int MinCameraSize { get; }
        public int MaxPlanetScore { get; }
        public int MinPlanetScore { get; }
    }
}
