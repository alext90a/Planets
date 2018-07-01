using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public interface ISectorManager
    {
        void Init();
        void GetVisiblePlanets(List<PlanetData> planetData);
    }
}
