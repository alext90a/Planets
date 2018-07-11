using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Planets;

namespace Assets.Scripts
{
    public sealed class VisiblePlanetsProvider : IVisiblePlanetsProvider
    {
        [NotNull]
        private readonly ICamera mCamera;

        [NotNull] private readonly IConstants mConstants;
        [NotNull] private readonly IPlayer mPlayer;

        public VisiblePlanetsProvider([NotNull] ICamera camera, [NotNull] IConstants constants, [NotNull] IPlayer player)
        {
            mCamera = camera;
            mConstants = constants;
            mPlayer = player;
        }

        public void GetVisiblePlanets([NotNull]List<PlanetData> planets, ISector[] sectorStore)
        {
            planets.Clear();
            //sectors.Clear();

            var cameraTop = mCamera.GetTop();
            var cameraBottom = mCamera.GetBottom();
            var cameraLeft = mCamera.GetLeft();
            var cameraRight = mCamera.GetRight();

            var planetsInRange = 0;

            for (int i = 0; i < sectorStore.Length; ++i)
            {
                var inspectedSector = sectorStore[i];



                if (!IsCameraInercectSector(inspectedSector.GetX(), inspectedSector.GetY(), cameraTop, cameraBottom, cameraLeft, cameraRight))
                {
                    continue;
                }

                int posToInsert = -1;

                for (int k = 0; k < inspectedSector.GetPlanetAmount(); ++k)
                {
                    if (!IsPlanetInCamera(inspectedSector, k, cameraTop, cameraLeft, cameraBottom, cameraRight))
                    {
                        continue;
                    }

                    posToInsert = FindPosToInsert(planets, inspectedSector, k);

                    if (posToInsert != -1)
                    {
                        planets.Insert(posToInsert, inspectedSector.GetPlanetData(k));
                        posToInsert = -1;
                        if (planets.Count > mConstants.GetPlanetsToVisualize())
                        {
                            try
                            {
                                planets.RemoveAt(planets.Count-1);
                            }
                            catch 
                            {
                                
                            }
                            break;
                        }
                        continue;
                    }
                    if (planets.Count < mConstants.GetPlanetsToVisualize())
                    {
                        planets.Add(inspectedSector.GetPlanetData(k));
                        continue;
                    }
                    break;
                }
            }
        }

        private int FindPosToInsert(List<PlanetData> planets, ISector inspectedSector, int k)
        {
            var posToInsert = -1;
            for (int j = mConstants.GetPlanetsToVisualize() - 1; j > -1; --j)
            {

                if (j > planets.Count - 1)
                {
                    continue;
                }

                if (Math.Abs(mPlayer.Score - planets[j].Score) > Math.Abs(mPlayer.Score - inspectedSector.GetPlanetRating(k)))
                {
                    posToInsert = j;
                    continue;
                }
                break;
            }
            return posToInsert;
        }

        public bool IsCameraInercectSector(int sectorIndX, int sectorIndY, int cameraTop, int cameraBottom, int cameraLeft, int cameraRight)
        {
            int sectorLeft = sectorIndX * mConstants.GetSectorSideSize();
            int sectorRight = sectorLeft + mConstants.GetSectorSideSize() - 1;
            int sectorBottom = sectorIndY * mConstants.GetSectorSideSize();
            int sectorTop = sectorBottom + mConstants.GetSectorSideSize() - 1;


            float cameraX = cameraLeft + (cameraRight - cameraLeft) / 2f;
            float cameraY = cameraBottom + (cameraTop - cameraBottom) / 2f;
            float sectorX = sectorLeft + (sectorRight - sectorLeft) / 2f;
            float sectorY = sectorBottom + (sectorTop - sectorBottom) / 2f;

            float sectorWidth = mConstants.GetSectorSideSize();
            float sectorHeight = mConstants.GetSectorSideSize();
            float cameraWidth = cameraRight - cameraLeft + 1;
            float cameraHeight = cameraTop - cameraBottom + 1;

            return (Math.Abs(sectorX - cameraX) * 2 < (sectorWidth + cameraWidth)) &&
                   (Math.Abs(sectorY - cameraY) * 2 < (sectorHeight + cameraHeight));
        }

        private bool IsPlanetInCamera([NotNull]ISector sector, int planetIndex, int cameraTop, int cameraLeft, int cameraBottom, int cameraRight)
        {
            var planetData = sector.GetPlanetData(planetIndex);


            if ((cameraTop >= planetData.Y && cameraBottom <= planetData.Y) &&
                (cameraLeft <= planetData.X && cameraRight >= planetData.X))
            {
                return true;
            }

            if (Math.Abs(planetData.Y) < 2 && Math.Abs(planetData.X) < 2)
            {
                int k = 9;
            }
            return false;
        }
    }
}
