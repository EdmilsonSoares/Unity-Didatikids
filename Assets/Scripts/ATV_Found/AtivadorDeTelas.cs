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
    // Variáveis setadas de acordo com a dificuldade
    private int levelDificult;
    private int limitValid;
    private int limitNotValid;
    private int maxNivel;
    private int tentativas;

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

    public int GetMaxNivel()
    {
        return maxNivel;
    }

    public int GetTentativas()
    {
        return tentativas;
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
            maxNivel = 8;
            tentativas = 7;

        }
        else if (gameModo == "Medio")
        {
            levelDificult = 2;
            limitValid = 1;
            limitNotValid = 2;
            maxNivel = 10;
            tentativas = 5;
        }
        else if (gameModo == "Dificil")
        {
            levelDificult = 3;
            limitValid = 1;
            limitNotValid = 3;
            maxNivel = 12;
            tentativas = 3;
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
