using Assets.Scripts;
using Boo.Lang;
using JetBrains.Annotations;
using Zenject;

namespace Planets
{
    public class Player : IPlayer
    {
        private int mX = 0;
        private int mY = 0;
        private List<IPlayerListener> mPlayerListeners = new List<IPlayerListener>();
        [Inject] [NotNull] private readonly IConstants mConstants;

        public Player([NotNull] IConstants constants)
        {
        }



        public int Score { get { return mConstants.GetPlayerScore(); } }
        public void MoveLeft()
        {
            mX -= 1;
            UpdateListeners();
        }

        public void MoveRight()
        {
            mX += 1;
            UpdateListeners();
        }

        public void MoveTop()
        {
            mY += 1;
            UpdateListeners();
        }

        public void MoveBottom()
        {
            mY -= 1;
            UpdateListeners();
        }


        public void AddListener(IPlayerListener listener)
        {
            mPlayerListeners.Add(listener);
        }

        public int GetX()
        {
            return mX;
        }

        public int GetY()
        {
            return mY;
        }

        private void UpdateListeners()
        {
            for (int i = 0; i < mPlayerListeners.Count; ++i)
            {
                mPlayerListeners[i].PositionCanged(mX, mY);
            }
        }
    }
}
