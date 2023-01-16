using UnityEngine;
using GamePlay.Pooling;

public abstract class HideableUI : MonoBehaviour, IInitializeable
{
    [SerializeField] protected bool _startHided;

    private bool _initialized;

    public abstract void Show();
    public abstract void Hide();
    public abstract void HideImmidiate();

    protected abstract void OnInitialize();

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        if (_initialized)
            return;

        _initialized = true;
        OnInitialize();
    }
}
