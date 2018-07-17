using System;
using JetBrains.Annotations;

namespace Editor.Mocks
{
    public class ConstantsMock : IConstants
    {
        [NotNull]
        private Func<int> mGetSectorsInSegment = () => 0;
        [NotNull]
        private Func<int> mGetPlanetsInSector = () => 0;
        [NotNull]
        private Func<int> mGetCellsInSector = () => 0;
        [NotNull]
        private Func<int> mGetSectorSideSize = () => 0;
        [NotNull]
        private Func<float> mGetPlanetsPercent = () => 0;
        [NotNull]
        private Func<int> mGetMaxCameraSize = () => 0;
        [NotNull]
        private Func<int> mGetMinCameraSize = () => 0;
        [NotNull]
        private Func<int> mGetMaxPlanetScore = () => 0;
        [NotNull]
        private Func<int> mGetMinPlanetScore = () => 0;
        [NotNull]
        private Func<int> mGetPlanetToVisualize = () => 0;
        [NotNull]
        private Func<int> mGetPlayerScore = () => 0;

        public void SetupGetSectorsInSegment([NotNull]Func<int> func)
        {
            mGetSectorsInSegment = func;
        }

        public void SetupGetPlanetsInSector([NotNull] Func<int> func)
        {
            mGetPlanetsInSector = func;
        }

        public void SetupGetCellsInSector([NotNull] Func<int> func)
        {
            mGetCellsInSector = func;
        }

        public void SetupGetSectorSideSize([NotNull] Func<int> func)
        {
            mGetSectorSideSize = func;
        }

        public void SetupGetPlanetPercent([NotNull] Func<float> func)
        {
            mGetPlanetsPercent = func;
        }

        public void SetupGetMaxCameraSize([NotNull] Func<int> func)
        {
            mGetMaxCameraSize = func;
        }

        public void SetupGetMinCameraSize([NotNull] Func<int> func)
        {
            mGetMinCameraSize = func;
        }

        public void SetupGetMaxPlanetScore([NotNull] Func<int> func)
        {
            mGetMaxPlanetScore = func;
        }

        public void SetupGetMinPlanetScore([NotNull] Func<int> func)
        {
            mGetMinPlanetScore = func;
        }

        public void SetupGetPlanetToVisualize([NotNull] Func<int> func)
        {
            mGetPlanetToVisualize = func;
        }

        public void SetupGetPlayerScore([NotNull] Func<int> func)
        {
            mGetPlayerScore = func;
        }

        public int GetSectorsInSegment()
        {
            return mGetSectorsInSegment.Invoke();
        }

        public int GetPlanetsInSector()
        {
            return mGetPlanetsInSector.Invoke();
        }

        public int GetCellsInSector()
        {
            return mGetCellsInSector.Invoke();
        }

        public int GetSectorSideSize()
        {
            return mGetSectorSideSize.Invoke();
        }

        public float GetPlanetsPercent()
        {
            return mGetPlanetsPercent.Invoke();
        }

        public int GetMaxCameraSize()
        {
            return mGetMaxCameraSize.Invoke();
        }

        public int GetMinCameraSize()
        {
            return mGetMinCameraSize.Invoke();
        }

        public int GetMaxPlanetScore()
        {
            return mGetMaxPlanetScore.Invoke();
        }

        public int GetMinPlanetScore()
        {
            return mGetMinPlanetScore.Invoke();
        }

        public int GetPlanetsToVisualize()
        {
            return mGetPlanetToVisualize.Invoke();
        }

        public int GetPlayerScore()
        {
            return mGetPlayerScore.Invoke();
        }
    }
}
