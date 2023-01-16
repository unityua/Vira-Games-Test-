using GamePlay.Level.PickUps;
using UnityEngine;

namespace GamePlay.Main
{
    public class CrystalsScore : MonoBehaviour
    {
        [SerializeField] private CrystalFinder _crystalFinder;

        private int _currentCrystalsCount;

        public int CurrentCrystalsCount { get => _currentCrystalsCount; set => _currentCrystalsCount = value; }

        private void Start()
        {
            _crystalFinder.CrystalPickedUp += OnCrystalPickedUp;
        }

        private void OnDestroy()
        {
            _crystalFinder.CrystalPickedUp -= OnCrystalPickedUp;
        }

        private void OnCrystalPickedUp(Crystal crystal)
        {
            _currentCrystalsCount += 1;
        }
    }
}
