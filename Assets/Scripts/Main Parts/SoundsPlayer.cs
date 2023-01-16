using GamePlay.Level;
using GamePlay.Level.PickUps;
using GamePlay.Level.Visual;
using UnityEngine;

namespace GamePlay.Main
{
    public class SoundsPlayer : MonoBehaviour
    {
        [SerializeField] private GameSounds _gameSounds;
        [Space]
        [SerializeField] private Ball _ball;
        [SerializeField] private CrystalFinder _crystalFinder;
        [SerializeField] private MainGame _mainGame;
        [SerializeField] private ScoreDetector _scoreDetector;

        private void Start()
        {
            _ball.DirectionChanged += (b) => _gameSounds.PlaySound(SoundType.GameTap);
            _crystalFinder.CrystalPickedUp += (c) => _gameSounds.PlaySound(SoundType.CrystalPickedUp);
            _mainGame.GameEnded += () => _gameSounds.PlaySound(SoundType.Losed);
            _scoreDetector.ScoreDetected += () => _gameSounds.PlaySound(SoundType.ColorChanged);
        }

        public void PlayMenuClick()
        {
            _gameSounds.PlaySound(SoundType.MenuClick);
        }
    }
}