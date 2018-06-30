using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planets
{
    public class Player : IPlayer
    {
        public Player()
        {
            Score = 5000;
        }

        public int Score { get; }
    }
}
