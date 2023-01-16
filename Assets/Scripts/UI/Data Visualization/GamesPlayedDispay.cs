using UnityEngine;
using UnityEngine.UI;
using GamePlay.Main;

namespace GamePlay.UI
{
    public class GamesPlayedDispay : MonoBehaviour
    {
        [SerializeField] private GamesCounter _gamesCounter;
        [Space]
        [SerializeField] private Text _target;
        [SerializeField] private string _firstPart = "Games Played: ";

        private void OnEnable()
        {
            _target.text = _firstPart + _gamesCounter.GamesPlayed.ToString();
        }
    }
}