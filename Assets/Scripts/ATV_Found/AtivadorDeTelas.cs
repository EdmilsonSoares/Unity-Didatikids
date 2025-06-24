using System;
using UnityEngine;
using UnityEngine.UI;

public class AtivadorDeTelas : MonoBehaviour
{
    [SerializeField] private Button btnBackMain;
    [SerializeField] private Button btnFacil;
    [SerializeField] private Button btnMedio;
    [SerializeField] private Button btnDificil;
    [SerializeField] private Button btnMenuGameOver;
    [SerializeField] private Button btnMenuVictory;

    [SerializeField] private GameObject gameplay;
    [SerializeField] private GameObject gameFacil;
    [SerializeField] private GameObject gameMedio;
    [SerializeField] private GameObject gameDificil;
    [SerializeField] private GameObject canvasMenu;
    [SerializeField] private GameObject canvasVictory;
    [SerializeField] private GameObject canvasGameOver;
    private int levelDificult;
    private int limitValid;
    private int limitNotValid;

    void Awake()
    {
        btnBackMain.onClick.AddListener(VoltarParaActivity);
        btnMenuVictory.onClick.AddListener(Recarregar);
        btnMenuGameOver.onClick.AddListener(Recarregar);
        btnFacil.onClick.AddListener(() => Carregar("Facil"));
        btnMedio.onClick.AddListener(() => Carregar("Medio"));
        btnDificil.onClick.AddListener(() => Carregar("Dificil"));
    }

    public int GetLimitValid()
    {
        return limitValid;
    }

    public int GetLimitNotValid()
    {
        return limitNotValid;
    }

    public int GetLevelDificult()
    {
        return levelDificult;
    }

    private void VoltarParaActivity()
    {
        GameManager.Instance.Carregar("Main");
    }

    public void Carregar(String gameModo)
    {
        if (gameModo == "Facil")
        {
            levelDificult = 1;
            limitValid = 1;
            limitNotValid = 1;
        }
        else if (gameModo == "Medio")
        {
            levelDificult = 2;
            limitValid = 1;
            limitNotValid = 2;
        }
        else if (gameModo == "Dificil")
        {
            levelDificult = 3;
            limitValid = 1;
            limitNotValid = 3;
        }
        else
        {
            Debug.LogWarning("AtivadorDeTelas: ERRO: Dificuldade não setada!");
        }

        canvasMenu.SetActive(false);
        gameplay.SetActive(true);
        gameFacil.SetActive(gameModo == "Facil");
        gameMedio.SetActive(gameModo == "Medio");
        gameDificil.SetActive(gameModo == "Dificil");
    }

    public void MostrarTela(string telaNome)
    {
        canvasGameOver.SetActive(telaNome == "GameOver");
        canvasVictory.SetActive(telaNome == "Victory");
    }

    public void Recarregar()
    {
        GameManager.Instance.Carregar("Found");
    }


}
