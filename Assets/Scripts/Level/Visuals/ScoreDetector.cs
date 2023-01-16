using GamePlay.Main;
using System;
using UnityEngine;

namespace GamePlay.Level.Visual
{
    public class ScoreDetector : MonoBehaviour
    {
        [SerializeField] private GameScore _gameScore;
        [SerializeField] private int _updateEveryNScore = 50;
        [Space]
        [SerializeField] private MainGame _mainGame;
        [Space]
        [SerializeField] private MaterialColorUpdater _materialColorUpdater;

        public event Action ScoreDetected;

        private void Start()
        {
            _gameScore.ScoreChanged += OnScoreChanged;
            _mainGame.ScenePrepared += OnScenePreoared;
        }

        private void OnDestroy()
        {
            _gameScore.ScoreChanged -= OnScoreChanged;
            _mainGame.ScenePrepared -= OnScenePreoared;

            ScoreDetected = null;
        }

        private void OnScenePreoared()
        {
            _materialColorUpdater.ResetStats();
        }

        private void OnScoreChanged(int newScore)
        {
            if (newScore % _updateEveryNScore == 0 && newScore != 0)
            {
                _materialColorUpdater.ChangeToRandomColor();
                ScoreDetected?.Invoke();
            }
        }
    }
}