using GamePlay.Pooling;
using UnityEngine;

namespace GamePlay.Level.Visual
{
    public class MaterialColorUpdater : MonoBehaviour, IResetable
    {
        [SerializeField] private Material _material;
        [Space]
        [SerializeField] private AnimationCurve _curve;
        [Min(0.01f)]
        [SerializeField] private float _animationTime;
        [Space]
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color[] _possibleColors;

        private Color _currentColor;
        private Color _startColor;
        private Color _targetColor;
        private float _t;

        private void Start()
        {
            ResetStats();
        }

        private void OnDestroy()
        {
            SetMaterialColor(_defaultColor);
        }

        private void Update()
        {
            if (_t < 1f)
                UpdateColor();
        }

        public void ResetStats()
        {
            SetMaterialColor(_defaultColor);
            _t = 1f;
        }

        public void ChangeToRandomColor()
        {
            _t = 0f;
            _startColor = _currentColor;
            _targetColor = _possibleColors[Random.Range(0, _possibleColors.Length)];
        }

        private void UpdateColor()
        {
            _t += Time.deltaTime / _animationTime;

            float curvedT = _curve.Evaluate(_t);

            Color newColor = Color.LerpUnclamped(_startColor, _targetColor, curvedT);
            SetMaterialColor(newColor);
        }

        private void SetMaterialColor(Color color)
        {
            _currentColor = color;
            _material.SetColor("_Color", color);
        }
    }
}