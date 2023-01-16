using System;
using UnityEngine;

namespace GamePlay.Level.GroundTileStates
{
    [Serializable]
    public class GroundTileMove : GroundTileStateBase
    {
        [SerializeField] private Vector3 _direction;
        [SerializeField] private float _speed;

        public float Speed { get => _speed; set => _speed = value; }

        public override void StateEntered()
        {

        }

        public override void Update(float deltaTime)
        {
            GroundTile.transform.position += _direction * (_speed * deltaTime);
        }
    }
}