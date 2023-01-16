using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GamePlay.UI
{
    [RequireComponent(typeof(Button))]
    public class ButtonToggle : MonoBehaviour
    {
        [SerializeField] private bool _isToggledOn;
        [Space]
        [SerializeField] private Image _targetIcon;
        [SerializeField] private Sprite _onIcon;
        [SerializeField] private Sprite _offIcon;
        [Space]
        [SerializeField] private UnityEvent _onBecameOn = new UnityEvent();
        [SerializeField] private UnityEvent _onBecameOff = new UnityEvent();

        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => ToggleValue());
        }

        public void ToggleValue()
        {
            SetValue(!_isToggledOn);
        }

        public void SetValue(bool value)
        {
            SetValueWithoutNotify(value);

            if (value)
                _onBecameOn?.Invoke();
            else
                _onBecameOff?.Invoke();
        }

        public void SetValueWithoutNotify(bool value)
        {
            _isToggledOn = value;

            if (_targetIcon != null)
                _targetIcon.sprite = value ? _onIcon : _offIcon;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            SetValueWithoutNotify(_isToggledOn);
        }
#endif
    }
}