using System;

namespace Editor.Mocks
{
    public sealed class PlayerMock : IPlayer
    {
        public int Score { get; }
        public void MoveLeft()
        {
            throw new NotImplementedException();
        }

        public void MoveRight()
        {
            throw new NotImplementedException();
        }

        public void MoveTop()
        {
            throw new NotImplementedException();
        }

        public void MoveBottom()
        {
            throw new NotImplementedException();
        }

        public void AddListener(IPlayerListener listener)
        {
            throw new NotImplementedException();
        }

        public int GetX()
        {
            throw new NotImplementedException();
        }

        public int GetY()
        {
            throw new NotImplementedException();
        }
    }
}
