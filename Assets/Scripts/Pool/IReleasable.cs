using System;

namespace GamePlay.Pooling
{
    public interface IReleasable<T>
    {
        void Release();

        void AddActionOnReleased(Action<T> action);

        void RemoveReleasedAction(Action<T> action);
    }
}