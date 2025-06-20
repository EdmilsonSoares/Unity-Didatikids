using Connect.Common;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Termo
{
    public class GameManagerTermo : MonoBehaviour
    {
        #region START_METHODS

        public static GameManagerTermo Instance;
        public static BoardTermo board;
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] public GameObject _winText;
        [SerializeField] private GameObject telaMenu;
        [SerializeField] private GameObject telaGameplay;
        [SerializeField] public GameObject easyBoard;
        [SerializeField] public GameObject mediumBoard;
        [SerializeField] public GameObject hardBoard;
        [SerializeField] private Button btnNextLevel;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                Init();
                DontDestroyOnLoad(gameObject);
                return;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        public void Start()
        {
            GoToMainMenu();
        }

        private void Init()
        {
            CurrentStage = 1;
            CurrentLevel = 1;

            Levels = new Dictionary<string, LevelDataTermo>();

            foreach (var item in _allLevels.Levels)
            {
                Levels[item.LevelName] = item;
            }
        }


        #endregion

        #region GAME_VARIABLES

        [HideInInspector]
        public int CurrentStage;

        [HideInInspector]
        public int CurrentLevel;

        [HideInInspector]
        public string StageName;

        public bool IsLevelUnlocked(int level)
        {
            string levelName = "Level" + "T" + CurrentStage.ToString() + level.ToString();

            if (level == 1)
            {
                PlayerPrefs.SetInt(levelName, 1);
                return true;
            }

            if (PlayerPrefs.HasKey(levelName))
            {
                return PlayerPrefs.GetInt(levelName) == 1;
            }

            PlayerPrefs.SetInt(levelName, 0);
            return false;
        }

        public void UnlockLevel()
        {
            CurrentLevel++;

            if (CurrentLevel == 51)
            {
                CurrentLevel = 1;
                CurrentStage++;

                if (CurrentStage == 8)
                {
                    CurrentStage = 1;
                    GoToMainMenu();
                }
            }

            string levelName = "Level" + "T" + CurrentStage.ToString() + CurrentLevel.ToString();
            PlayerPrefs.SetInt(levelName, 1);
        }

        #endregion

        #region LEVEL_DATA

        [SerializeField]
        private LevelDataTermo DefaultLevel;

        [SerializeField]
        private LevelListTermo _allLevels;

        private Dictionary<string, LevelDataTermo> Levels;

        public LevelDataTermo GetLevel()
        {
            string levelName = "Level" + CurrentStage.ToString() + CurrentLevel.ToString();

            if (Levels.ContainsKey(levelName))
            {
                return Levels[levelName];
            }

            return DefaultLevel;
        }

        #endregion

        #region SCENE_LOAD

        //private const string MainMenu = "MainMenu";
        //private const string Gameplay = "Gameplay";

        public void GoToMainMenu()
        {
            telaMenu.SetActive(true);
            telaGameplay.SetActive(false);    
        }

        public void GoToGameplay()
        {
            UnityEngine.Debug.Log("goToGameplay");
            telaMenu.SetActive(false);
            telaGameplay.SetActive(true);

            easyBoard.SetActive(CurrentStage == 1);
            mediumBoard.SetActive(CurrentStage == 2);
            hardBoard.SetActive(CurrentStage == 3);       

            _winText.SetActive(false);
            _titleText.gameObject.SetActive(true);
            _titleText.text = StageName +
                " - " + CurrentLevel.ToString();

            if (CurrentStage == 1)
                easyBoard.GetComponent<BoardTermo>().GetWord();
            else if (CurrentStage == 2)
                mediumBoard.GetComponent<BoardTermo>().GetWord();
            else if (CurrentStage == 3)
                hardBoard.GetComponent<BoardTermo>().GetWord();
        }
        #endregion

        #region ON GAME BUTTONS
        public void ResetLevel()
        {
            board.ResetBoard();
        }
        #endregion
    }
}
