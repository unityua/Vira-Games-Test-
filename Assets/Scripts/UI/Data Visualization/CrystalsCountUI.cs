using UnityEngine;
using GamePlay.Main;
using UnityEngine.UI;

namespace GamePlay.UI
{
    public class CrystalsCountUI : MonoBehaviour
    {
        [SerializeField] private CrystalsScore _crystalsScore;
        [SerializeField] private Text _scoreText; 

        private void OnEnable()
        {
            _scoreText.text = _crystalsScore.CurrentCrystalsCount.ToString();
        }
    }
}