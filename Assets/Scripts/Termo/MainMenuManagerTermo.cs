using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

namespace Termo
{
    public class MainMenuManagerTermo : MonoBehaviour
    {
        public static MainMenuManagerTermo Instance;

        [SerializeField] private GameObject _titlePanel;
        [SerializeField] private GameObject _stagePanel;
        [SerializeField] private GameObject _levelPanel;

        private void Awake()
        {
            Instance = this;

            _titlePanel.SetActive(true);
            _stagePanel.SetActive(false);
            _levelPanel.SetActive(false);
        }

        public void ClickedPlay()
        {
            _titlePanel.SetActive(false);
            _stagePanel.SetActive(true);
        }

        public void ClickedBackToTitle()
        {
            _titlePanel.SetActive(true);
            _stagePanel.SetActive(false);
        }

        public void ClickedBackToStage()
        {
            _stagePanel.SetActive(true);
            _levelPanel.SetActive(false);
        }

        public UnityAction LevelOpened;

        [HideInInspector]
        public Color CurrentColor;

        [SerializeField]
        private TMP_Text _levelTitleText;
        [SerializeField]
        private UnityEngine.UI.Image _levelTitleImage;

        public void ClickedStage(string stageName, Color stageColor)
        {
            _stagePanel.SetActive(false);
            _levelPanel.SetActive(true);
            CurrentColor = stageColor;
            _levelTitleText.text = stageName;
            _levelTitleImage.color = CurrentColor;
            LevelOpened?.Invoke();
        }
    }
}

