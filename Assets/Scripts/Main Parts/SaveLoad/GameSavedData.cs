using GamePlay.Level.Visual;
using System;
using UnityEngine;

namespace GamePlay.Main
{
    public class GameSavedData : MonoBehaviour
    {
        [SerializeField] private MainGame _mainGame;
        [Space]
        [SerializeField] private GameScore _gameScore;
        [SerializeField] private CrystalsScore _crystalsScore;
        [SerializeField] private GamesCounter _gamesCounter;
        [SerializeField] private ColorRandomizer _ballColor;

        private readonly string _bestScoreKey = "b_score";
        private readonly string _crystalsCountKey = "cr_count";
        private readonly string _gamesPlayedKey = "g_played";
        private readonly string _ballColorKey = "ball_color";

        private void Awake()
        {
            _mainGame.GameEnded += SaveData;

            LoadData();
        }

        private void OnDestroy()
        {
            _mainGame.GameEnded -= SaveData;

            SaveData();
        }

        private void LoadData()
        {
            _gameScore.BestScore = PlayerPrefs.GetInt(_bestScoreKey, 0);
            _crystalsScore.CurrentCrystalsCount = PlayerPrefs.GetInt(_crystalsCountKey, 0);
            _gamesCounter.GamesPlayed = PlayerPrefs.GetInt(_gamesPlayedKey, 0);
            _ballColor.SetColor(PlayerPrefs.GetInt(_ballColorKey, 0));
        }

        private void SaveData()
        {
            PlayerPrefs.SetInt(_bestScoreKey, _gameScore.BestScore);
            PlayerPrefs.SetInt(_crystalsCountKey, _crystalsScore.CurrentCrystalsCount);
            PlayerPrefs.SetInt(_gamesPlayedKey, _gamesCounter.GamesPlayed);
            PlayerPrefs.SetInt(_ballColorKey, _ballColor.CurrentColorIndex);
        }
    }
}