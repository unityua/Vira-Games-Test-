using UnityEngine;
using GamePlay.Level.GroundTileStates;
using GamePlay.Pooling;
using System.Collections.Generic;
using System;

namespace GamePlay.Level
{
    public class GroundTile : MonoBehaviour, IInitializeable, IResetable, IReleasable<GroundTile>, IMovable, IPausable
    {
        [SerializeField] private GroundTileMove _moveState = new GroundTileMove();
        [SerializeField] private GroundTileFall _fallState = new GroundTileFall();
        [Space]
        [SerializeField] private CrystalHolder _crystalHolder;

        private GroundTileIdle _idleState = new GroundTileIdle();

        private bool _paused;
        private List<GroundTileStateBase> _currentStates = new List<GroundTileStateBase>();

        private bool _initialized;

        // При подписке/отписке от события создается мусор. Такая схема помогает его избегать
        private List<Action<GroundTile>> _releasedActions = new List<Action<GroundTile>>();

        public CrystalHolder CrystalHolder => _crystalHolder;

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            if (_paused)
                return;

            for (int i = _currentStates.Count - 1; i >= 0; i--)
            {
                _currentStates[i].Update(Time.deltaTime);
            }
        }

        private void OnDestroy()
        {
            _releasedActions.Clear();
            _releasedActions = null;
        }

        public void Initialize()
        {
            if (_initialized)
                return;

            _initialized = true;

            _moveState.Initialize(SetState, this);
            _fallState.Initialize(SetState, this);
            _idleState.Initialize(SetState, this);

            _crystalHolder.Initialize();
        }

        public void ResetStats()
        {
            _fallState.ResetStats();

            StopMoving();

            gameObject.SetActive(true);
        }

        public void StartMoving()
        {
            SetState(_moveState);
        }

        public void StopMoving()
        {
            SetState(_idleState);
        }

        public void StartFall()
        {
            AddState(_fallState);
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
            _moveState.Speed = speed;
        }

        public void Release()
        {
            gameObject.SetActive(false);

            for (int i = _releasedActions.Count - 1; i >= 0; i--)
            {
                _releasedActions[i].Invoke(this);
            }
        }

        public void AddActionOnReleased(Action<GroundTile> action)
        {
            _releasedActions.Add(action);
        }

        public void RemoveReleasedAction(Action<GroundTile> action)
        {
            _releasedActions.Remove(action);
        }

        private void SetState(GroundTileStateBase newState)
        {
            _currentStates.Clear();

            AddState(newState);
        }

        private void AddState(GroundTileStateBase newState)
        {
            newState.StateEntered();

            _currentStates.Add(newState);
        }
    }
}