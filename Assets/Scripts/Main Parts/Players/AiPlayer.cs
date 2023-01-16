using GamePlay.Level;
using UnityEngine;

namespace GamePlay.Main.Players
{
    public class AiPlayer : Player
    {
        [Space]
        [SerializeField] private GroundCheck _rightGroundCheck;
        [SerializeField] private GroundCheck _leftGroundCheck;

        protected override void HandleInput(float deltaTime)
        {
            float currentBallSpeed = _ball.CurrentSpeed;
            bool isMovingLeft = IsLeftSpeed(currentBallSpeed);

            if(_leftGroundCheck.IsAboveGround() == false && isMovingLeft)
            {
                _ball.ChangeDirection();
            }
            else if(_rightGroundCheck.IsAboveGround() == false && isMovingLeft == false)
            {
                _ball.ChangeDirection();
            }
        }

        private bool IsLeftSpeed(float speed)
        {
            return speed < 0f;
        }
    }
}