using GamePlay.Main;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.UI
{
    public class BestScoreDisplay : MonoBehaviour
    {
        [SerializeField] private GameScore _gameScore;
        [Space]
        [SerializeField] private Text _target;
        [SerializeField] private string _firstPart = "Best Score: ";

        private void OnEnable()
        {
            _target.text = _firstPart + _gameScore.BestScore.ToString();
        }
    }
}