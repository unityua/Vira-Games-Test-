using GamePlay.Pooling;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.UI.PopUp
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ScorePopUp : MonoBehaviour, IInitializeable, IResetable, IReleasable<ScorePopUp>
    {
        [Min(0.01f)]
        [SerializeField] private float _animationTime;
        [SerializeField] private AnimationCurve _colorCurve;
        [SerializeField] private AnimationCurve _positionCurve;
        [Space]
        [SerializeField] private Color _targetColor;

        private float _t;

        private Color _startColor;

        private Vector3 _startPosition;
        private Vector3 _targetPosition;

        private List<Action<ScorePopUp>> _releasedActions = new List<Action<ScorePopUp>>();

        private SpriteRenderer _spriteRenderer;

        private bool _initialized;

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            if (_t < 1f)
                UpdateAnimation();
        }

        public void Initialize()
        {
            if (_initialized)
                return;

            _initialized = true;

            _spriteRenderer = GetComponent<SpriteRenderer>();
            _startColor = _spriteRenderer.color;
        }

        public void StartAnimation(Vector3 startPosition, Vector3 targetPosition)
        {
            _startPosition = startPosition;
            _targetPosition = targetPosition;

            _spriteRenderer.color = _startColor;
            transform.position = startPosition;

            _t = 0f;

            gameObject.SetActive(true);
        }

        public void Release()
        {
            gameObject.SetActive(false);

            for (int i = _releasedActions.Count - 1; i >= 0; i--)
                _releasedActions[i].Invoke(this);
        }

        public void AddActionOnReleased(Action<ScorePopUp> action)
        {
            _releasedActions.Add(action);
        }

        public void RemoveReleasedAction(Action<ScorePopUp> action)
        {
            _releasedActions.Remove(action);
        }

        public void ResetStats()
        {
            _t = 0f;
            _spriteRenderer.color = _startColor;
            gameObject.SetActive(true);
        }

        private void UpdateAnimation()
        {
            _t += Time.deltaTime / _animationTime;

            float positionT = _positionCurve.Evaluate(_t);
            float colorT = _colorCurve.Evaluate(_t);

            transform.position = Vector3.Lerp(_startPosition, _targetPosition, positionT);
            _spriteRenderer.color = Color.Lerp(_startColor, _targetColor, colorT);

            if (_t >= 1f)
                Release();
        }
    }
}