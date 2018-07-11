using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IUnityPlanetData
{
    void Activate(int xPos, int yPos, int score);
    void Deactivate();
}