using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Planets;

namespace Assets.Scripts
{
    public interface ISegmentCreator
    {
        ISector[] CreateSectors();
    }
}
