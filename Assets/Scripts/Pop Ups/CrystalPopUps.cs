using GamePlay.Level.PickUps;
using GamePlay.Pooling;
using UnityEngine;

namespace GamePlay.UI.PopUp
{
    public class CrystalPopUps : MonoBehaviour
    {
        [SerializeField] private CrystalFinder _crystalFinder;
        [Space]
        [SerializeField] private ScorePopUp _scorePopUpPrefab;
        [SerializeField] private int _initialPoolSize;

        [Header("Animation Params")]
        [SerializeField] private Vector3 _startOffset;
        [SerializeField] private Vector3 _targetOffset;

        private SimplePool<ScorePopUp> _popUpsPool;

        private void Start()
        {
            _crystalFinder.CrystalPickedUp += OnCrystalPickedUp;
            _popUpsPool = new SimplePool<ScorePopUp>(_scorePopUpPrefab, transform, _initialPoolSize, (s) => s.gameObject.SetActive(false));
            _popUpsPool.OnItemReturned += OnPopUpReturned;
        }

        private void OnDestroy()
        {
            _crystalFinder.CrystalPickedUp -= OnCrystalPickedUp;

            _popUpsPool.Clear();
        }

        private void OnCrystalPickedUp(Crystal crystal)
        {
            Vector3 crystalPosition = crystal.transform.position;

            Vector3 startPosition = crystalPosition + _startOffset;
            Vector3 targetPosition = crystalPosition + _targetOffset;

            ScorePopUp popUp = _popUpsPool.GetItem();

            popUp.StartAnimation(startPosition, targetPosition);
        }

        private void OnPopUpReturned(ScorePopUp popUp)
        {
            popUp.gameObject.SetActive(false);
        }
    }
}