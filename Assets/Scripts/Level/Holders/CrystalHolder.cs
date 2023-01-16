using UnityEngine;
using System.Collections.Generic;
using GamePlay.Level.PickUps;
using GamePlay.Pooling;
using System;
using Random = UnityEngine.Random;

namespace GamePlay.Level
{
    public class CrystalHolder : MonoBehaviour, IInitializeable
    {
        [SerializeField] private GroundTile _parentTile;
        [SerializeField] private float _possibleRadius;

        private List<Crystal> _activeCrystals = new List<Crystal>();

        private Action<GroundTile> _onTileReleased;
        private Action<Crystal> _onCrystalReleased;

        private bool _initialized;

        public int CrystalsCount => _activeCrystals.Count;

        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            if (_initialized)
                return;

            _initialized = true;
            _onTileReleased = OnTileReleased;
            _onCrystalReleased = OnCrystalReleased;

            _parentTile.AddActionOnReleased(_onTileReleased);
        }

        public void AddCrystal(Crystal crystal)
        {
            crystal.transform.SetParent(transform);
            crystal.transform.localPosition = GetRandomStartPosition();

            crystal.AddActionOnReleased(_onCrystalReleased);
        }

        private void OnTileReleased(GroundTile groundTile)
        {
            for (int i = _activeCrystals.Count - 1; i >= 0; i--)
            {
                _activeCrystals[i].Release();
            }
        }

        private void OnCrystalReleased(Crystal crystal)
        {
            crystal.gameObject.SetActive(false);
            crystal.RemoveReleasedAction(_onCrystalReleased);
            _activeCrystals.Remove(crystal);
        }

        private Vector3 GetRandomStartPosition()
        {
            Vector3 startPosition = transform.localPosition;
            Vector2 randomOffset = Random.insideUnitCircle * _possibleRadius;
            startPosition.x += randomOffset.x;
            startPosition.y = 0f;
            startPosition.z += randomOffset.y;

            return startPosition;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            UnityEditor.Handles.color = Color.red;
            UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, _possibleRadius);
        }
#endif
    }
}