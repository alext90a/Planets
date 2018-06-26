using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planets
{
    public interface ISector
    {
        int GetPlanet(int index);
        int GetX { get; }
        int GetY { get; }
        int PlanetsInSector { get; }
        int GetPlanetRating(int index);
    }
}
