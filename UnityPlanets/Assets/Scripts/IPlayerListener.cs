using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public interface IPlayerListener
    {
        void PositionCanged(int posX, int posY);
    }
}
