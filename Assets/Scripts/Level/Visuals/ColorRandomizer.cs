using UnityEngine;

namespace GamePlay.Level.Visual
{
    public class ColorRandomizer : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _target;
        [SerializeField] private Color[] _aviableColors;

        private int _currentColorIndex;

        public int CurrentColorIndex => _currentColorIndex;

        public void SetColor(int _colorIndex)
        {
            if(_colorIndex >= _aviableColors.Length)
                _colorIndex = 0;

            _currentColorIndex = Mathf.Clamp(_colorIndex, 0, _aviableColors.Length - 1);

            _target.color = _aviableColors[_currentColorIndex];
        }

        public void SetNextColor()
        {
            SetColor(_currentColorIndex + 1);
        }

        public void SetRandomAviableColor()
        {
            SetColor(Random.Range(0, _aviableColors.Length));
        }

        public void SetTotallyRandomColor()
        {
            Color color = Random.ColorHSV();
            color.a = 1f;

            _target.color = color;
        }
    }
}