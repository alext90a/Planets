using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Assets.Scripts
{
    public interface IArrayBackgroundWorkerListener
    {
        void OnProgressChange(int progress);
        void OnFinished();
        void OnException([NotNull] string message);
    }
}
