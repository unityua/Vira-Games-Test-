using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePlay.Pooling;

namespace GamePlay.Level.BallStates
{
    [System.Serializable]
    public class BallStateFall : BallStateBase, IResetable
    {
        [SerializeField] private Transform _fallTransform;
        [SerializeField] private Vector3 _defaultLocalPosition;
        [Space]
        [SerializeField] private Vector3 _direction;
        [SerializeField] private float _speed = 2f;
        [SerializeField] private float _gravity = -9.81f;
        [Space]
        [SerializeField] private GameObject[] _objectsToDisable;

        private Vector3 _moveDirection;
        private float _currentFallSpeed;

        public override void StateEntered()
        {
            _moveDirection = _direction.normalized;
            _moveDirection.x *= Mathf.Sign(Ball.CurrentSpeed);

            _currentFallSpeed = 0f;

            foreach (var item in _objectsToDisable)
                item.SetActive(false);
        }

        public override void Update(float deltaTime)
        {
            Ball.transform.Translate(_moveDirection * (deltaTime * _speed));

            _currentFallSpeed += _gravity * deltaTime;
            _fallTransform.Translate(0f, _currentFallSpeed * deltaTime, 0f, Space.Self);
        }

        public void ResetStats()
        {
            foreach (var item in _objectsToDisable)
                item.SetActive(true);

            _fallTransform.localPosition = _defaultLocalPosition;
        }
    }
}