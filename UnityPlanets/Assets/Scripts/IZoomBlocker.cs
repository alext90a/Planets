using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Assets.Scripts
{
    public interface IZoomBlocker
    {
        void AddListener([NotNull] IZoomBlockerListener listener);
        void BlockZoom();
        void UnblockZoom();
    }
}
