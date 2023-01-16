using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GamePlay.Pooling
{
    public class SimplePool<T> where T : MonoBehaviour, IInitializeable, IResetable, IReleasable<T>
    {
        private T _prefab;
        private Transform _parent;

        private Queue<T> _freeItems;
        private Action<T> _onItemCreated;

        // Кэшируем, чтобы не было создания нового делегата при подписке на события
        private Action<T> _onItemReleased;

        public event Action<T> OnItemReturned;

        public SimplePool(T prefab, Transform parent, int startCount, Action<T> onItemCreated)
        {
            _prefab = prefab;
            _parent = parent;
            _onItemCreated = onItemCreated;
            _onItemReleased = OnItemReleased;

            _freeItems = new Queue<T>(startCount);
            CreateObjects(startCount);
        }

        public T GetItem()
        {
            T item;
            if (_freeItems.Count > 0)
                item = _freeItems.Dequeue();
            else
                item = CreateNewItem();

            return item;
        }

        public void Clear()
        {
            _freeItems.Clear();
            _freeItems = null;

            _prefab = null;
            _parent = null;

            _onItemCreated = null;
            _onItemReleased = null;

            OnItemReturned = null;
        }

        private void CreateObjects(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _freeItems.Enqueue(CreateNewItem());
            }
        }

        private T CreateNewItem()
        {
            T newItem = Object.Instantiate(_prefab, _parent);

            newItem.Initialize();
            newItem.AddActionOnReleased(_onItemReleased);

            _onItemCreated(newItem);

            return newItem;
        }

        private void OnItemReleased(T item)
        {
            item.ResetStats();
            _freeItems.Enqueue(item);

            OnItemReturned?.Invoke(item);
        }
    }
}