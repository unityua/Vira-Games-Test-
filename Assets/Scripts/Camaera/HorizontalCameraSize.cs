using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.CameraStuff
{
    [RequireComponent(typeof(Camera))]
    public class HorizontalCameraSize : MonoBehaviour
    {
        [Min(0.1f)]
        [SerializeField] private float _horizontalSize;

        private void Start()
        {
            SetHorizontalSize(_horizontalSize);
        }

        public void SetHorizontalSize(float horizontalSize)
        {
            _horizontalSize = horizontalSize;

            Camera camera = GetComponent<Camera>();

            float aspect = camera.aspect;

            float height = horizontalSize / aspect;

            camera.orthographicSize = height;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            SetHorizontalSize(_horizontalSize);
        }

        private void OnEnable()
        {
            SetHorizontalSize(_horizontalSize);
        }
#endif
    }
}