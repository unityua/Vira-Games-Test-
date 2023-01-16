using GamePlay.Level.PickUps;
using GamePlay.Pooling;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GamePlay.Level
{
    public class CrystalsGenerator : MonoBehaviour, IInitializeable
    {
        [Header("Spawn Stats")]
        [Range(0f, 1f)]
        [SerializeField] private float _chanceToSpawn = 0.2f;
        [SerializeField] private PathGenerator _pathGenerator;

        [Header("Pool")]
        [SerializeField] private Crystal _crystalPrefab;
        [SerializeField] private int _initialPoolSize;

        private SimplePool<Crystal> _crystalsPool;

        private List<Crystal> _aliveCrystals = new List<Crystal>();

        private bool _initialized;

        private void Awake()
        {
            Initialize();
        }

        private void OnDestroy()
        {
            _crystalsPool.OnItemReturned -= OnCrystalReturned;
            _crystalsPool.Clear();

            _pathGenerator.TileGenerated -= TryCreateCrystal;
        }

        public void Initialize()
        {
            if (_initialized)
                return;

            _initialized = true;
            _crystalsPool = new SimplePool<Crystal>(_crystalPrefab, transform, _initialPoolSize, (cr) => cr.gameObject.SetActive(false));
            _pathGenerator.TileGenerated += TryCreateCrystal;
            _crystalsPool.OnItemReturned += OnCrystalReturned;
        }

        public void ClearAllCrystals()
        {
            for (int i = _aliveCrystals.Count - 1; i >= 0; i--)
            {
                _aliveCrystals[i].Release();
            }
        }

        private void TryCreateCrystal(GroundTile newTile)
        {
            CrystalHolder crystalHolder = newTile.CrystalHolder;

            if (Random.value > _chanceToSpawn || crystalHolder.CrystalsCount > 0)
                return;

            Crystal crystal = _crystalsPool.GetItem();
            crystalHolder.AddCrystal(crystal);
            crystal.gameObject.SetActive(true);

            _aliveCrystals.Add(crystal);
        }

        private void OnCrystalReturned(Crystal crystal)
        {
            _aliveCrystals.Remove(crystal);
            crystal.transform.SetParent(transform);
            crystal.gameObject.SetActive(false);
        }
    }
}