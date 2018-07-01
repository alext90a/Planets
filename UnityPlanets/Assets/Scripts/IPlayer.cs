using Assets.Scripts;

namespace Planets
{
    public interface IPlayer
    {

        int Score { get; }
        void MoveLeft();
        void MoveRight();
        void MoveTop();
        void MoveBottom();
        void AddListener(IPlayerListener listener);
    }
}
