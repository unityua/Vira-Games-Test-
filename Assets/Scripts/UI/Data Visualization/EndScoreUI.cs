using GamePlay.Main;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.UI
{
    public class EndScoreUI : MonoBehaviour
    {
        [SerializeField] private GameScore _gameScore;
        [Space]
        [SerializeField] private Text _currentScore;
        [SerializeField] private Text _bestScore;
        [Space]
        [SerializeField] private ColorScheme _defaultColors;
        [SerializeField] private ColorScheme _newBestScore;

        [Header("Color Sceme Targets")]
        [SerializeField] private Image _background;
        [SerializeField] private Text[] _texts;

        private void OnEnable()
        {
            int currentScore = _gameScore.CurrentScore;
            int bestScore = _gameScore.BestScore;

            _currentScore.text = currentScore.ToString();
            _bestScore.text = bestScore.ToString();

            bool isNewRecord = currentScore == bestScore && currentScore > 0;

            ColorScheme colorScheme = isNewRecord  ? _newBestScore : _defaultColors;
            SetColorScheme(colorScheme);
        }

        private void SetColorScheme(ColorScheme colorScheme)
        {
            _background.color = colorScheme.backgroundColor;
            foreach (var text in _texts)
            {
                text.color = colorScheme.textColor;
            }
        }

        [System.Serializable]
        private struct ColorScheme
        {
            public Color backgroundColor;
            public Color textColor;
        }
    }
}