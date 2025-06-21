using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;
using TMPro;
using System.Linq;

namespace Termo
{
    public class BoardTermo : MonoBehaviour
    {   
        [SerializeField] private TMP_InputField hiddenInputField;
        public bool wonGame = false;

        private Row[] rows;
        private int rowIndex;
        private int columnIndex;

        private RootData data;
        private string Word;

        [Header("States")]
        public Tile.State emptyState;
        public Tile.State occupiedState;
        public Tile.State CorrectState;
        public Tile.State wrongSpotState;
        public Tile.State incorrectState;
        private void Awake()
        {
            LoadJson();
            rows = GetComponentsInChildren<Row>();
            GameManagerTermo.board = this;
        }
        private void Start()
        {
            wonGame = false;
            GetWord();

            hiddenInputField.caretWidth = 0; // esconde o caret
            hiddenInputField.text = "";
            hiddenInputField.onValueChanged.AddListener(HandleInput);
        }
        private void LoadJson()
        {
            TextAsset jsonFile = Resources.Load<TextAsset>("Words");
            data = JsonUtility.FromJson<RootData>(jsonFile.text);
        }        
        public string GetWord()
        {
            if (data == null)
                LoadJson();

            string difficulty = GameManagerTermo.Instance.CurrentStage.ToString();
            string level = GameManagerTermo.Instance.CurrentLevel.ToString();
          
            DifficultyData entry = data.difficulties.Find(d => d.id == difficulty);
            if (entry == null) return "";

            LevelData currentLevelData = level switch
            {
                "1" => entry.level_1,
                "2" => entry.level_2,
                "3" => entry.level_3,
                "4" => entry.level_4,
                "5" => entry.level_5,
                _ => null
            };

            if (currentLevelData == null) return "";

            Word = currentLevelData.word;
            return currentLevelData.theme;
        }
        private void Update()
        {
            if(!hiddenInputField.isFocused)
                hiddenInputField.ActivateInputField(); // mantém o foco no campo

            Row currentRow = rows[rowIndex];

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                columnIndex = Mathf.Max(columnIndex - 1, 0);
                currentRow.tiles[columnIndex].SetLetter('\0');
                currentRow.tiles[columnIndex].SetState(emptyState);
            }
            else if (columnIndex >= rows[rowIndex].tiles.Length)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SubmitRow(currentRow);
                }
            }         
        }
        private void HandleInput(string input)
        {
            if(wonGame) return;

            if (string.IsNullOrEmpty(input)) return;

            char newChar = input[0];
            if (!char.IsLetter(newChar)) return;


            Row currentRow = rows[rowIndex];
            if (columnIndex < currentRow.tiles.Length)
            {
                currentRow.tiles[columnIndex].SetLetter(newChar);
                currentRow.tiles[columnIndex].SetState(occupiedState);
                columnIndex++;
            }

            hiddenInputField.text = ""; // limpa o input pra próxima letra
            hiddenInputField.caretWidth = 0; // esconde o caret
        }
        private void SubmitRow(Row row)
        {
            string remaining = Word;

            for (int i = 0; i < row.tiles.Length; i++)
            {
                Tile tile = row.tiles[i];

                if (tile.letter == Word[i])
                {
                    tile.SetState(CorrectState);

                    remaining = remaining.Remove(i, 1);
                    remaining = remaining.Insert(i, " ");
                }
                else if (!Word.Contains(tile.letter))
                {
                    tile.SetState(incorrectState);
                }
            }

            for (int i = 0; i < row.tiles.Length; i++)
            {
                Tile tile = row.tiles[i];

                if (tile.state != CorrectState && tile.state != incorrectState)
                {
                    if (remaining.Contains(tile.letter))
                    {
                        tile.SetState(wrongSpotState);

                        int index = remaining.IndexOf(tile.letter);
                        remaining = remaining.Remove(index, 1);
                        remaining = remaining.Insert(index, " ");
                    }
                    else
                    {
                        tile.SetState(incorrectState);
                    }
                }
            }
            if (HasWon(row))
            {
                GameManagerTermo.Instance._winText.SetActive(true);
                enabled = false;
                wonGame = true;
                hiddenInputField.gameObject.SetActive(false);
                GameManagerTermo.Instance.UnlockLevel();
                MainMenuManagerTermo.Instance.RefreshLevelButtons(); // <-- força a atualização visual dos botões
            }

            rowIndex++;
            columnIndex = 0;

            if (rowIndex >= rows.Length)
            {
                hiddenInputField.gameObject.SetActive(false);
                enabled = false;
            }
        }
        private bool HasWon(Row row)
        {
            for (int i = 0; i < row.tiles.Length; i++)
            {
                if (row.tiles[i].state != CorrectState)
                {
                    return false;
                }
            }
            return true;
        }
        public void ResetBoard()
        {
            if (rows == null)
                return;

            for (int row = 0; row < rows.Length; row++)
            {
                for (int col = 0; col < rows[row].tiles.Length; col++)
                {
                    rows[row].tiles[col].SetLetter('\0');
                    rows[row].tiles[col].SetState(emptyState);
                }
            }
            rowIndex = 0;
            columnIndex = 0;
            GameManagerTermo.Instance._winText.SetActive(false);
            wonGame = false;
            hiddenInputField.gameObject.SetActive(true);
            this.enabled = true; //Reativa o script
        }
    }

    #region classes para pegar o json
    [System.Serializable]
    public class RootData
    {
        public List<DifficultyData> difficulties;
    }

    [System.Serializable]
    public class DifficultyData
    {
        public string id;
        public LevelData level_1;
        public LevelData level_2;
        public LevelData level_3;
        public LevelData level_4;
        public LevelData level_5;
    }

    [Serializable]
    public class LevelData
    {
        public string word;
        public string theme;
    }
    #endregion
}