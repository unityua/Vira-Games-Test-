using UnityEngine;
using GamePlay.Level;
using GamePlay.Pooling;
using System;
using GamePlay.Level.PickUps;

namespace GamePlay.Main
{
    public class GameScore : MonoBehaviour, IResetable
    {
        [SerializeField] private Ball _ball;
        [SerializeField] private CrystalFinder _crystalFinder;

        private int _currentScore;
        private int _bestScore;

        public event Action<int> ScoreChanged;

        public int CurrentScore => _currentScore;
        public int BestScore { get => _bestScore; set => _bestScore = value; }

        private void Start()
        {
            _ball.DirectionChanged += OnBallDirectionChanged;
            _crystalFinder.CrystalPickedUp += OnCrystalPickedUp;
        }

        private void OnCrystalPickedUp(Crystal crystal)
        {
            SetScore(_currentScore + crystal.Score);
        }

        private void OnDestroy()
        {
            _ball.DirectionChanged -= OnBallDirectionChanged;
            _crystalFinder.CrystalPickedUp -= OnCrystalPickedUp;

            ScoreChanged = null;
        }

        private void OnBallDirectionChanged(Ball ball)
        {
            SetScore(_currentScore + 1);
        }

        public void ResetStats()
        {
            SetScore(0);
        }

        private void SetScore(int score)
        {
            _currentScore = score;

            if(score > _bestScore)
                _bestScore = score;

            ScoreChanged?.Invoke(score);
        }
    }
}