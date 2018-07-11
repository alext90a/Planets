using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts;
using Planets;

namespace Assets.Editor.UnitTests
{
    public sealed class SectorMock : ISector
    {
        private Func<int, int> mGetPlanetFunc = (i) => 1;
        private Func<int> mGetXFunc = () =>0;
        private Func<int> mGetYFunc = () => 0;
        private Func<int, int> mGetPlanetRatingFunc = (i) => 0;
        private Func<int, PlanetData> mGetPlanetDataFunc = (i) => new PlanetData(0,0,0);
        private Func<int> mGetPlanetsFunc = () => 0;

        public void SetupGetPlanet(Func<int, int> func)
        {
            mGetPlanetFunc = func;
        }

        public void SetupGetX(Func<int> func)
        {
            mGetXFunc = func;
        }

        public void SetupGetY(Func<int> func)
        {
            mGetYFunc = func;
        }

        public void SetupGetPlanetRating(Func<int, int> func)
        {
            mGetPlanetRatingFunc = func;
        }

        public void SetupGetPlanetAmount(Func<int> func)
        {
            mGetPlanetsFunc = func;
        }

        public void SetupGetPlanetData(Func<int, PlanetData> func)
        {
            mGetPlanetDataFunc = func;
        }

        public int GetPlanet(int index)
        {
            return mGetPlanetFunc.Invoke(index);
        }

        public int GetX()
        {
            return mGetXFunc.Invoke();
        }

        public int GetY()
        {
            return mGetYFunc.Invoke();
        }

        public int GetPlanetRating(int index)
        {
            return mGetPlanetRatingFunc.Invoke(index);
        }

        public PlanetData GetPlanetData(int index)
        {
            return mGetPlanetDataFunc.Invoke(index);
        }

        public int GetPlanetAmount()
        {
            return mGetPlanetsFunc.Invoke();
        }
    }
}
