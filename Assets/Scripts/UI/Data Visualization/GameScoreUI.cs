using GamePlay.Main;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.UI
{
    [RequireComponent(typeof(Text))]
    public class GameScoreUI : MonoBehaviour
    {
        [SerializeField] private GameScore _gameScore;

        private Text _scoreText;

        private void Start()
        {
            _scoreText = GetComponent<Text>();
            _gameScore.ScoreChanged += SetScoreText;

            SetScoreText(_gameScore.CurrentScore);
        }

        private void SetScoreText(int newScore)
        {
            _scoreText.text = newScore.ToString();
        }
    }
}