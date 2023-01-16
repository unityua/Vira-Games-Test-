
namespace GamePlay.Level
{
    public interface IMovable
    {
        void StartMoving();
        
        void StopMoving();

        void SetSpeed(float speed);
    }
}