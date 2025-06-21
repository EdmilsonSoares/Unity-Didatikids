using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

namespace Termo
{
    public class LevelButtonTermo : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private UnityEngine.UI.Image _image;
        [SerializeField] TMP_Text _levelText;
        [SerializeField] private Color _inactiveColor;

        private bool isLevelUnlocked;
        private int currentLevel;
        private void Awake()
        {
            _button.onClick.AddListener(Clicked);
        }

        private void OnEnable()
        {
            MainMenuManagerTermo.Instance.LevelOpened += LevelOpened;
        }
        private void OnDisable()
        {
            MainMenuManagerTermo.Instance.LevelOpened -= LevelOpened;
        }
        public void LevelOpened()
        {
            string gameObjectName = gameObject.name;
            string[] parts = gameObjectName.Split('_');
            _levelText.text = parts[parts.Length - 1];
            currentLevel = int.Parse(_levelText.text);
            isLevelUnlocked = GameManagerTermo.Instance.IsLevelUnlocked(currentLevel);

            _image.color = isLevelUnlocked ? MainMenuManagerTermo.Instance.CurrentColor : _inactiveColor;
        }
        private void Clicked()
        {
            if (!isLevelUnlocked)
                return;
            
            GameManagerTermo.Instance.CurrentLevel = currentLevel;
            GameManagerTermo.Instance.GoToGameplay();
        }
    }
}
