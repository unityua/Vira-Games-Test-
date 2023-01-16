using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GamePlay.UI
{
    public class MovingRectUI : HideableUI
    {
        [SerializeField] private Vector2 _showedPosition;
        [SerializeField] private Vector2 _hidedPosition;
        [SerializeField] private AnimationCurve _curve;
        [Min(0.01f)]
        [SerializeField] private float _animationTime;
        [Space]
        [SerializeField] private UnityEvent onBecameShowimg = new UnityEvent();
        [SerializeField] private UnityEvent onBecameHiding = new UnityEvent();

        private RectTransform _rectTransform;

        private float _t;
        private Vector2 _startPosition;
        private Vector2 _targetPosition;

        protected override void OnInitialize()
        {
            _rectTransform = GetComponent<RectTransform>();

            _t = 1f;

            if (_startHided)
                HideImmidiate();
        }

        private void Update()
        {
            if (_t < 1f)
                MoveRect();
        }

        public override void Show()
        {
            InitializeAnimation(_hidedPosition, _showedPosition);

            onBecameShowimg?.Invoke();
        }

        public override void Hide()
        {
            InitializeAnimation(_showedPosition, _hidedPosition);

            onBecameHiding?.Invoke();
        }

        public override void HideImmidiate()
        {
            _t = 1f;
            _targetPosition = _hidedPosition;
            _rectTransform.anchoredPosition = _hidedPosition;

            gameObject.SetActive(false);
        }

        private void MoveRect()
        {
            _t += Time.deltaTime / _animationTime;
            UpdatePosition();

            if (_t >= 1f && _targetPosition == _hidedPosition)
            {
                gameObject.SetActive(false);
            }
        }

        private void InitializeAnimation(Vector2 startPosition, Vector2 targetPosition)
        {
            _startPosition = startPosition;
            _targetPosition = targetPosition;

            _t = CalculateT(startPosition, targetPosition, _rectTransform.anchoredPosition);
            
            UpdatePosition();

            gameObject.SetActive(true);
        }

        private void UpdatePosition()
        {
            float curvedT = _curve.Evaluate(_t);

            Vector2 newPosition = Vector2.Lerp(_startPosition, _targetPosition, curvedT);

            _rectTransform.anchoredPosition = newPosition;
        }

        private float CalculateT(Vector2 startPosition, Vector2 targetPosition, Vector2 anchoredPosition)
        {
            bool useX = startPosition.x != targetPosition.x;

            float start, target, current;

            if (useX)
            {
                start = startPosition.x;
                target = targetPosition.x;
                current = anchoredPosition.x;
            }
            else
            {
                start = startPosition.y;
                target = targetPosition.y;
                current = anchoredPosition.y;
            }

            return Mathf.InverseLerp(start, target, current);
        }
    }
}
