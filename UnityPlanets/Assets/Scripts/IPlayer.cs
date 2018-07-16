using Assets.Scripts;

public interface IPlayer
{

    int Score { get; }
    void MoveLeft();
    void MoveRight();
    void MoveTop();
    void MoveBottom();
    void AddListener(IPlayerListener listener);
    int GetX();
    int GetY();
}