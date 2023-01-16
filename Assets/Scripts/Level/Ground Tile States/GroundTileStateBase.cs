using System;

namespace GamePlay.Level.GroundTileStates
{
    public abstract class GroundTileStateBase
    {
        private Action<GroundTileStateBase> _setState;
        private GroundTile _groundTile;

        protected Action<GroundTileStateBase> SetState => _setState;
        protected GroundTile GroundTile => _groundTile;

        public void Initialize(Action<GroundTileStateBase> setState, GroundTile groundTile)
        {
            _setState = setState;
            _groundTile = groundTile;
        }

        public abstract void StateEntered();
        public abstract void Update(float deltaTile);
    }
}