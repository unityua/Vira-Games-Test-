using GamePlay.Main;
using UnityEngine;

namespace GamePlay.UI
{
    public class SoundButtonUI : MonoBehaviour
    {
        [SerializeField] private ButtonToggle _target;
        [Space]
        [SerializeField] private GameSounds _gameSounds;

        private void OnEnable()
        {
            _target.SetValueWithoutNotify(!_gameSounds.Muted);
        }
    }
}