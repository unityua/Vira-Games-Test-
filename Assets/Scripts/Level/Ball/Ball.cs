using UnityEngine;
using GamePlay.Pooling;
using GamePlay.Level.BallStates;
using System;

namespace GamePlay.Level
{
    public class Ball : MonoBehaviour, IInitializeable, IResetable, IMovable, IPausable
    {
        [SerializeField] private BallStateMove _moveState = new BallStateMove();
        [SerializeField] private BallStateFall _fallState = new BallStateFall();

        private BallStateIdle _idleState = new BallStateIdle();

        private BallStateBase _currentState;
        private bool _paused;

        private bool _initialized;

        public event Action<Ball> DirectionChanged;
        public event Action<Ball> FallingStarted;

        public float CurrentSpeed => _moveState.CurrentSpeed;

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            if (_paused == false)
                _currentState.Update(Time.deltaTime);
        }

        private void OnDestroy()
        {
            DirectionChanged = null;
            FallingStarted = null;
        }

        public void Initialize()
        {
            if (_initialized)
                return;

            _initialized = true;

            _moveState.Initialize(SetState, this);
            _fallState.Initialize(SetState, this);
            _idleState.Initialize(SetState, this);
        }

        public void ChangeDirection()
        {
            _moveState.CurrentSpeed = -_moveState.CurrentSpeed;
            DirectionChanged?.Invoke(this);
        }

        public void StartMoving()
        {
            SetState(_moveState);
        }

        public void StopMoving()
        {
            SetState(_idleState);
        }

        public void StartFalling()
        {
            SetState(_fallState);
            FallingStarted?.Invoke(this);
        }

        public void Pause()
        {
            _paused = true;
        }

        public void Unpause()
        {
            _paused = false;
        }

        public void SetSpeed(float speed)
        {
            _moveState.CurrentSpeed = speed;
        }

        public void ResetStats()
        {
            _fallState.ResetStats();

            SetState(_idleState);
        }

        private void SetState(BallStateBase newState)
        {
            newState.StateEntered();

            _currentState = newState;
        }
    }
}