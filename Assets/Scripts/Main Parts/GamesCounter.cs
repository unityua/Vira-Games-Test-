using UnityEngine;

namespace GamePlay.Main
{
    public class GamesCounter : MonoBehaviour
    {
        [SerializeField] private MainGame _mainGame;

        private int _gamesPlayed;

        public int GamesPlayed { get => _gamesPlayed; set => _gamesPlayed = value; }

        private void Start()
        {
            _mainGame.GameStarted += OnGameStarted;
        }

        private void OnDestroy()
        {
            _mainGame.GameStarted -= OnGameStarted;
        }

        private void OnGameStarted()
        {
            _gamesPlayed += 1;
        }
    }
}