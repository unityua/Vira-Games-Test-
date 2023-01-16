using UnityEngine;
using GamePlay.Level;
using GamePlay.Pooling;
using System;
using UnityEngine.Events;
using GamePlay.Main.Players;

namespace GamePlay.Main
{
    public class MainGame : MonoBehaviour, IInitializeable, IMovable, IPausable
    {
        [SerializeField] private float _startSpeed = 2f;
        [SerializeField] private Player _player;
        [Space]
        [Header("GamePlay")]
        [SerializeField] private Ball _ball;
        [SerializeField] private Vector3 _ballStartPosition;
        [Space]
        [SerializeField] private GroundTile _startTile;
        [SerializeField] private Vector3 _startTileStartPosition;
        [Space]
        [SerializeField] private PathGenerator _pathGenerator;
        [SerializeField] private CrystalsGenerator _crystalsGenerator;

        [Header("Events")]
        [SerializeField] private UnityEvent _onGameStarted = new UnityEvent();
        [SerializeField] private UnityEvent _onGameEnded = new UnityEvent();

        private bool _initialized;

        public event Action ScenePrepared;
        public event Action GameStarted;
        public event Action GameEnded;

        public Player CurrentPlayer => _player;

        private void Start()
        {
            Initialize();
            PrepareStartScene();
        }

        private void OnDestroy()
        {
            ScenePrepared = null;
            GameStarted = null;
            GameEnded = null;
        }

        public void Initialize()
        {
            if (_initialized)
                return;

            _initialized = true;

            _ball.Initialize();
            _startTile.Initialize();
            _pathGenerator.Initialize();
            _crystalsGenerator.Initialize();

            _ball.FallingStarted += (ball) => GameLosed(); 
        }

        public void PrepareStartScene()
        {
            SetSpeed(_startSpeed);

            _startTile.ResetStats();
            _startTile.transform.position = _startTileStartPosition;

            _crystalsGenerator.ClearAllCrystals();

            _pathGenerator.ClearAllTiles();
            _pathGenerator.CreateStartPath();

            _ball.ResetStats();
            _ball.transform.position = _ballStartPosition;

            ScenePrepared?.Invoke();
        }

        public void StartGame()
        {
            StartMoving();

            _onGameStarted?.Invoke();
            GameStarted?.Invoke();

            _player.enabled = true;
            _player.Unpause();
        }

        public void Pause()
        {
            _startTile.Pause();
            _pathGenerator.Pause();
            _ball.Pause();
            _player.Pause();
        }

        public void Unpause()
        {
            _startTile.Unpause();
            _pathGenerator.Unpause();
            _ball.Unpause();
            _player.Unpause();
        }

        public void StartMoving()
        {
            _startTile.StartMoving();
            _pathGenerator.StartMoving();
            _ball.StartMoving();
        }

        public void StopMoving()
        {
            _startTile.StopMoving();
            _pathGenerator.StopMoving();
        }

        public void SetSpeed(float speed)
        {
            _startTile.SetSpeed(speed);
            _pathGenerator.SetSpeed(speed);
            _ball.SetSpeed(speed);
        }

        public void GameLosed()
        {
            _player.enabled = false;

            StopMoving();
            _onGameEnded?.Invoke();
            GameEnded?.Invoke();
        }

        public void SetPlayer(Player newPlayer)
        {
            Player oldPlayer = _player;

            if (oldPlayer.Paused)
                newPlayer.Pause();
            else
                newPlayer.Unpause();

            newPlayer.enabled = oldPlayer.enabled;

            oldPlayer.enabled = false;
            _player = newPlayer;
        }
    }
}