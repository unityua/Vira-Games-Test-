using UnityEngine;

namespace GamePlay.Main
{
    public enum SoundType { GameTap, MenuClick, ColorChanged, CrystalPickedUp, Losed }

    [RequireComponent(typeof(AudioSource))]
    public class GameSounds : MonoBehaviour
    {
        [SerializeField] private bool _muted;
        [Space]
        [SerializeField] private SoundSettings[] _allSounds;

        private AudioSource _source;

        public bool Muted => _muted;

        private void Start()
        {
            _source = GetComponent<AudioSource>();
        }

        public void ToggleMuted()
        {
            _muted = !_muted;
        }

        public void SetMuted(bool value)
        {
            _muted = value;
        }

        public void PlaySound(SoundType soundType) 
        {
            if (_muted)
                return;

            SoundSettings sound = _allSounds[(int)soundType];
            _source.PlayOneShot(sound.audioClip, sound.volume);
        }

        [System.Serializable]
        private struct SoundSettings
        {
            public AudioClip audioClip;
            public float volume;
        }
    }
}