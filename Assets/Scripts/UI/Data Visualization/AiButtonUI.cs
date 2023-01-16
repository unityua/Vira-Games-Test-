using GamePlay.Main;
using GamePlay.Main.Players;
using UnityEngine;

namespace GamePlay.UI
{
    public class AiButtonUI : MonoBehaviour
    {
        [SerializeField] private ButtonToggle _buttonToggle;
        [Space]
        [SerializeField] private MainGame _mainGame;
        [SerializeField] private Player _humanPlayer;
        [SerializeField] private Player _aiPlayer;

        private void OnEnable()
        {
            Player currentPlayer = _mainGame.CurrentPlayer;
            _buttonToggle.SetValueWithoutNotify(currentPlayer == _aiPlayer);
        }
    }
}