using UnityEngine;

namespace GamePlay.Main.Players
{
    public class HumanPlayer : Player
    {
        protected override void HandleInput(float deltaTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _ball.ChangeDirection();
            }
        }
    }
}