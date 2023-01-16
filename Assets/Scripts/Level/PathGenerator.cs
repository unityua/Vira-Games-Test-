using System;
using UnityEngine;
using System.Collections.Generic;
using GamePlay.Pooling;
using Random = UnityEngine.Random;

namespace GamePlay.Level
{
    public class PathGenerator : MonoBehaviour, IInitializeable, IMovable, IPausable
    {
        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private int _tilesCount;
        [Space]
        [SerializeField] private Vector3 _rightTileOffset;
        [SerializeField] private Vector3 _leftTileOffset;
        [Space]
        [SerializeField] private float _leftBoundsX;
        [SerializeField] private float _rightBoundsX;
        [Space]
        [SerializeField] private GroundTile _tilePrefab;

        private GroundTile _lastCreatedTile;

        private SimplePool<GroundTile> _tilesPool;
        private List<GroundTile> _aliveTiles = new List<GroundTile>();

        private bool _initialized;

        private bool _tilesMoving;
        private float _tilesSpeed;

        public event Action<GroundTile> TileGenerated;

        private void Start()
        {
            Initialize();
        }

        private void OnDestroy()
        {
            _tilesPool.OnItemReturned -= OnGroundTileReleased;
            _tilesPool.Clear();

            TileGenerated = null;
        }

        public void Initialize()
        {
            if (_initialized)
                return;

            _initialized = true;
            _tilesPool = new SimplePool<GroundTile>(_tilePrefab, transform, _tilesCount,
                                                item => item.gameObject.SetActive(false));
        }

        public void CreateStartPath()
        {
            _tilesPool.OnItemReturned += OnGroundTileReleased;

            _lastCreatedTile = PlaceTile(_startPosition);

            CreatePath(_tilesCount);
        }

        public void ClearAllTiles()
        {
            _tilesPool.OnItemReturned -= OnGroundTileReleased;

            _tilesMoving = false;

            for (int i = _aliveTiles.Count - 1; i >= 0; i--)
            {
                _aliveTiles[i].Release();
            }

            _aliveTiles.Clear();
        }

        public void StartMoving()
        {
            _tilesMoving = true;

            foreach (var tile in _aliveTiles)
                tile.StartMoving();
        }

        public void StopMoving()
        {
            _tilesMoving = false;

            foreach (var tile in _aliveTiles)
                tile.StopMoving();
        }

        public void SetSpeed(float speed)
        {
            _tilesSpeed = speed;

            foreach (var tile in _aliveTiles)
                tile.SetSpeed(speed);
        }

        public void Pause()
        {
            foreach (var tile in _aliveTiles)
                tile.Pause();
        }

        public void Unpause()
        {
            foreach (var tile in _aliveTiles)
                tile.Unpause();
        }

        private void CreatePath(int tilesCount)
        {
            Vector3 lastTilePosition = _lastCreatedTile.transform.position;
            for (int i = 0; i < tilesCount; i++)
            {
                Vector3 newTileOffset = Random.value > 0.5f ? _rightTileOffset : _leftTileOffset;

                if (IsValidPosition(lastTilePosition + newTileOffset) == false)
                    newTileOffset.x = -newTileOffset.x;

                lastTilePosition += newTileOffset;

                _lastCreatedTile = PlaceTile(lastTilePosition);
            }
        }

        private bool IsValidPosition(Vector3 position)
        {
            return position.x > _leftBoundsX && position.x < _rightBoundsX;
        }

        private GroundTile PlaceTile(Vector3 position)
        {
            GroundTile tile = _tilesPool.GetItem();
            tile.transform.position = position;
            tile.gameObject.SetActive(true);
            _aliveTiles.Add(tile);

            tile.SetSpeed(_tilesSpeed);
            if (_tilesMoving)
                tile.StartMoving();

            TileGenerated?.Invoke(tile);

            return tile;
        }

        private void OnGroundTileReleased(GroundTile tile)
        {
            _aliveTiles.Remove(tile);
            CreatePath(1);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0f, 1f, 0.4f, 0.3f);
            Gizmos.DrawCube(new Vector3(_leftBoundsX, 0f, 0f), new Vector3(0.1f, 5f, 40f));
            Gizmos.DrawCube(new Vector3(_rightBoundsX, 0f, 0f), new Vector3(0.1f, 5f, 40f));
        }
#endif
    }
}