using UnityEngine;
using GamePlay.Pooling;

namespace GamePlay.Level.BallStates
{
    [System.Serializable]
    public class BallStateMove : BallStateBase
    {
        [SerializeField] private float _speed;
        [SerializeField] private GroundCheck _groundCheck;

        public float CurrentSpeed { get => _speed; set => _speed = value; }


        public override void StateEntered()
        {

        }

        public override void Update(float deltaTime)
        {
            Ball.transform.Translate(_speed * deltaTime, 0f, 0f);

            if (_groundCheck.IsAboveGround() == false)
                Ball.StartFalling();
        }
    }
}