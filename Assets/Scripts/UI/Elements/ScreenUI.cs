using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.UI
{
    public class ScreenUI : HideableUI
    {
        [SerializeField] private HideableUI[] _movingItems;
        [SerializeField] private GameObject[] _staticItems;

        protected override void OnInitialize()
        {
            foreach (var movingItem in _movingItems)
            {
                movingItem.Initialize();
            }

            if (_startHided)
                HideImmidiate();
            else
                Show();
        }

        public override void Show()
        {
            foreach (var movingItem in _movingItems)
                movingItem.Show();

            foreach (var staticItem in _staticItems)
                staticItem.SetActive(true);
        }

        public override void Hide()
        {
            foreach (var movingItem in _movingItems)
                movingItem.Hide();

            foreach (var staticItem in _staticItems)
                staticItem.SetActive(false);
        }

        public override void HideImmidiate()
        {
            foreach (var movingItem in _movingItems)
                movingItem.HideImmidiate();

            foreach (var staticItem in _staticItems)
                staticItem.SetActive(false);
        }

#if UNITY_EDITOR
        [NaughtyAttributes.Button]
        private void FillItemsArray()
        {
            List<GameObject> staticItems = new List<GameObject>();
            List<HideableUI> movingItems = new List<HideableUI>();

            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);

                HideableUI movingRect = child.GetComponent<HideableUI>();
                if(movingRect != null)
                    movingItems.Add(movingRect);
                else
                    staticItems.Add(child.gameObject);
            }

            UnityEditor.Undo.RecordObject(this, "Fill Screen UI Items Array");

            _movingItems = movingItems.ToArray();
            _staticItems = staticItems.ToArray();
        }
#endif
    }
}