using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public interface IBordersChangeListener
    {
        void NewBorders(int top, int bottom, int left, int right);
    }
}
