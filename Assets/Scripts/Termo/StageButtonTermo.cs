using UnityEngine;
using UnityEngine.UI;

namespace Termo
{
    public class StageButtonTermo : MonoBehaviour
    {
        [SerializeField] private string _stageName;
        [SerializeField] private Color _stageColor;
        [SerializeField] private int _stageNumber;
        [SerializeField] private Button _button;

        private void Awake()
        {
            _button.onClick.AddListener(ClickedButton);
        }

        private void ClickedButton()
        {
            GameManagerTermo.Instance.CurrentStage = _stageNumber;
            GameManagerTermo.Instance.StageName = _stageName;
            MainMenuManagerTermo.Instance.ClickedStage(_stageName, _stageColor);
        }
    }
}
