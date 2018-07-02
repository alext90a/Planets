﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public interface IUnityPlanetVisualizer
    {
        void Visualize(List<PlanetData> planets);
    }
}