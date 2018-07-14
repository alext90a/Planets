using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts;

public interface IUnityPlanetData
{
    void Activate(PlanetData planetData);
    void Deactivate();
}