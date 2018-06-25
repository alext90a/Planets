﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planets
{
    public interface ISectorCreator
    {
        ISector CreateSector(int x, int y);
    }
}
