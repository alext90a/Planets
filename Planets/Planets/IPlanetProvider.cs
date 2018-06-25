using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planets
{
    public interface IPlanetProvider
    {
        int[] GetPlanets(ICamera camera);
    }
}
