using GamePlay.Level;
using UnityEngine;

namespace GamePlay.Main.Players
{
    public abstract class Player : MonoBehaviour, IPausable
    {
        [SerializeField] protected Ball _ball;

        private bool _paused;

        public bool Paused => _paused;

        protected abstract void HandleInput(float deltaTime);

        private void Update()
        {
            if (_paused == false)
                HandleInput(Time.deltaTime);
        }

        public void Pause()
        {
            _paused = true;
        }

        public void Unpause()
        {
            _paused = false;
        }
    }
}