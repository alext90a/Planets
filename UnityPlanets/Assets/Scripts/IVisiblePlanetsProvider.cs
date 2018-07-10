using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Planets;

namespace Assets.Scripts
{
    public interface IVisiblePlanetsProvider
    {
        void GetVisiblePlanets(List<PlanetData> visiblePlanets, ISector[] sectors);
    }
}
