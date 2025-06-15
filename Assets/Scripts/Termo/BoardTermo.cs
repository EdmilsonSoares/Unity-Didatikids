using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

namespace Termo
{
    public class BoardTermo : MonoBehaviour
    {
        private static readonly KeyCode[] SUPPORTED_KEYS = new KeyCode[]
        {
            KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E,
            KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.I, KeyCode.J,
            KeyCode.K, KeyCode.L, KeyCode.M, KeyCode.N, KeyCode.O,
            KeyCode.P, KeyCode.Q, KeyCode.R, KeyCode.S, KeyCode.T,
            KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X, KeyCode.Y,
            KeyCode.Z,
        };

        private Row[] rows;

        //private string[] solutions;
        //private string[] validWords;
        private string word;

        private int rowIndex;
        private int columnIndex;

        [Header("States")]
        public Tile.State emptyState;
        public Tile.State occupiedState;
        public Tile.State CorrectState;
        public Tile.State wrongSpotState;
        public Tile.State incorrectState;

        private RootData data;

        private void Awake()
        {
            rows = GetComponentsInChildren<Row>();
        }

        private void Start()
        {
            LoadJson();
            //SetRandomWord();
            word = GetWord();
        }

        private void LoadJson()
        {
            //TextAsset textFile = Resources.Load("WordsListAnimals") as TextAsset;
            //validWords = textFile.text.Split('\n');

            //TextAsset textFile = Resources.Load("WordsListAnimals") as TextAsset;
            //solutions = textFile.text.Split('\n');
            TextAsset jsonFile = Resources.Load<TextAsset>("Words");
            data = JsonUtility.FromJson<RootData>(jsonFile.text);
        }

        //private void SetRandomWord()
        //{
        //    word = solutions[UnityEngine.Random.Range(0, solutions.Length)];
        //    word = word.ToLower().Trim();
        //}

        public string GetWord()
        {
            string difficulty = GameManagerTermo.Instance.CurrentStage.ToString();
            string level = "level_" + GameManagerTermo.Instance.CurrentLevel.ToString();

            DifficultyData entry = data.difficulties.Find(d => d.id == difficulty);
            if (entry != null)
            {
                return level switch
                {
                    "level_1" => entry.level_1,
                    "level_2" => entry.level_2,
                    "level_3" => entry.level_3,
                    "level_4" => entry.level_4,
                    "level_5" => entry.level_5,
                    _ => null
                };
            }

            return null;
        }

        private void Update()
        {
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
            else
            {
                for (int i = 0; i < SUPPORTED_KEYS.Length; i++)
                {
                    if (Input.GetKeyDown(SUPPORTED_KEYS[i]))
                    {
                        currentRow.tiles[columnIndex].SetLetter((char)SUPPORTED_KEYS[i]);
                        currentRow.tiles[columnIndex].SetState(occupiedState);
                        columnIndex++;
                        break;
                    }
                }
            }
        }

        private void SubmitRow(Row row)
        {
            string remaining = word;

            for (int i = 0; i < row.tiles.Length; i++)
            {
                Tile tile = row.tiles[i];

                if (tile.letter == word[i])
                {
                    tile.SetState(CorrectState);

                    remaining = remaining.Remove(i, 1);
                    remaining = remaining.Insert(i, " ");
                }
                else if (!word.Contains(tile.letter))
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
                GameManagerTermo.Instance.UnlockLevel();
            }

            rowIndex++;
            columnIndex = 0;

            if (rowIndex >= rows.Length)
            {
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
        public string level_1;
        public string level_2;
        public string level_3;
        public string level_4;
        public string level_5;
    }
    #endregion
}