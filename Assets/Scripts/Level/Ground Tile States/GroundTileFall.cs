using System;
using UnityEngine;
using GamePlay.Pooling;

namespace GamePlay.Level.GroundTileStates
{
    [Serializable]
    public class GroundTileFall : GroundTileStateBase, IResetable
    {
        [SerializeField] private Transform _target;
        [Space]
        [SerializeField] private Vector3 _startLocalPosition;
        [SerializeField] private Vector3 _targetLocalPosition;
        [SerializeField] private float _timeToFall;
        [SerializeField] private AnimationCurve _curve;

        private float _t;

        public override void StateEntered()
        {
            _t = 0f;
            _target.localPosition = _startLocalPosition;
        }

        public override void Update(float deltaTime)
        {
            _t += deltaTime / _timeToFall;

            float curvedT = _curve.Evaluate(_t);

            Vector3 newPosition = Vector3.Lerp(_startLocalPosition, _targetLocalPosition, curvedT);

            _target.localPosition = newPosition;

            if (_t >= 1f)
                GroundTile.Release();
        }

        public void ResetStats()
        {
            _target.localPosition = _startLocalPosition;
        }
    }
}
