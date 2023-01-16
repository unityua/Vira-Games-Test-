using UnityEngine;
using GamePlay.Pooling;
using System;
using System.Collections.Generic;

namespace GamePlay.Level.PickUps
{
    public class Crystal : Pickable, IResetable, IReleasable<Crystal>
    {
        [SerializeField] private int _score = 1;

        // При подписке/отписке от события создается мусор. Такая схема помогает его избегать
        private List<Action<Crystal>> _releaseActions = new List<Action<Crystal>>();

        public int Score => _score;

        public void Release()
        {
            gameObject.SetActive(false);

            for (int i = _releaseActions.Count - 1; i >= 0; i--)
            {
                _releaseActions[i].Invoke(this);
            }
        }

        public void AddActionOnReleased(Action<Crystal> action)
        {
            _releaseActions.Add(action);
        }

        public void RemoveReleasedAction(Action<Crystal> action)
        {
            _releaseActions.Remove(action);
        }

        public void ResetStats()
        {
            gameObject.SetActive(true);
        }

        protected override void OnPickedUp()
        {
            Release();
        }

        protected override void OnDestroyed()
        {
            for (int i = _releaseActions.Count - 1; i >= 0; i--)
                _releaseActions[i] = null;

            _releaseActions.Clear();
        }
    }
}