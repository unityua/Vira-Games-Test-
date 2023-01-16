using GamePlay.Pooling;
using System;
using UnityEngine;

namespace GamePlay.Level.PickUps
{
    public abstract class Pickable : MonoBehaviour, IInitializeable
    {
        public event Action<Pickable> PickedUp;

        private bool _initialized;

        protected abstract void OnPickedUp();
        protected virtual void OnInitialize() { }
        protected virtual void OnDestroyed() { }

        private void Start()
        {
            Initialize();
        }

        private void OnDestroy()
        {
            PickedUp = null;
            OnDestroyed();
        }

        public void Initialize()
        {
            if (_initialized) 
                return;

            OnInitialize();
        }

        public void PickUp()
        {
            OnPickedUp();
            PickedUp?.Invoke(this);
        }
    }
}