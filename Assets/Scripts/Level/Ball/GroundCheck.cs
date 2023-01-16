using UnityEngine;

namespace GamePlay.Level
{
    public class GroundCheck : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Vector3 _direction = Vector3.down;
        [SerializeField] private float _distance = 1f;

        public bool IsAboveGround()
        {
            return Physics.Raycast(transform.position, _direction.normalized, _distance, _layerMask);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawRay(transform.position, _direction * _distance);
        }
#endif
    }
}