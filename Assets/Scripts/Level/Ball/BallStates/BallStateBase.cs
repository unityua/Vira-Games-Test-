using System;

namespace GamePlay.Level.BallStates
{
    public abstract class BallStateBase 
    {
        private Action<BallStateBase> _setState;
        private Ball _ball;

        protected Action<BallStateBase> SetState => _setState;
        protected Ball Ball => _ball;

        public void Initialize(Action<BallStateBase> setState, Ball ball)
        {
            _setState = setState;
            _ball = ball;
        }

        public abstract void StateEntered();
        public abstract void Update(float deltaTime);
    }
}
