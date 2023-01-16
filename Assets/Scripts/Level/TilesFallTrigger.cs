using UnityEngine;

namespace GamePlay.Level
{
    public class TilesFallTrigger : MonoBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {
            GroundTile tile = other.GetComponentInParent<GroundTile>();

            if (tile == null)
            {
                Debug.LogWarning("Something Witout Ground Tile Cai=ught By Trigger", other.gameObject);
                return;
            }

            tile.StartFall();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            BoxCollider boxCollider = GetComponent<BoxCollider>();

            if (boxCollider == null)
                return;

            Gizmos.matrix = transform.localToWorldMatrix;

            Gizmos.color = new Color(1f, 0f, 0f, 0.2f);
            Gizmos.DrawCube(boxCollider.center, boxCollider.size);
        }
#endif
    }
}