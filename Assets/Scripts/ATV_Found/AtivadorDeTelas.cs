using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AtivadorDeTelas : MonoBehaviour
{
    [SerializeField] private Button btnBackMain;
    [SerializeField] private Button btnFacil;
    [SerializeField] private Button btnMedio;
    [SerializeField] private Button btnDificil;
    [SerializeField] private Button btnMenuGameOver;
    [SerializeField] private Button btnMenuVictory;

    [SerializeField] private GameObject regrasPanel;
    [SerializeField] private GameObject levelPanel;
    [SerializeField] private GameObject niveisFacil;
    [SerializeField] private GameObject niveisMedio;
    [SerializeField] private GameObject niveisDificil;
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
    //private int maxNivel;
    private int tentativas;
    private int nivel;

    void Awake()
    {
        btnBackMain.onClick.AddListener(VoltarParaActivity);
        btnMenuVictory.onClick.AddListener(Recarregar);
        btnMenuGameOver.onClick.AddListener(Recarregar);
        btnFacil.onClick.AddListener(() => CarregarNiveis("Facil"));
        btnMedio.onClick.AddListener(() => CarregarNiveis("Medio"));
        btnDificil.onClick.AddListener(() => CarregarNiveis("Dificil"));
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

    /*public int GetMaxNivel()
    {
        return maxNivel;
    }*/

    public int GetTentativas()
    {
        return tentativas;
    }
    public int GetNivel()
    {
        return nivel;
    }

    public void SetNivel(int numeroDoBotao)
    {
        this.nivel = numeroDoBotao;
    }

    private void VoltarParaActivity()
    {
        SceneManager.LoadScene("Atividades");
    }

    private void CarregarNiveis(String gameModo)
    {
        if (gameModo == "Facil")
        {
            levelDificult = 1;
            limitValid = 1;
            limitNotValid = 1;
            //maxNivel = 8;
            tentativas = 5;

        }
        else if (gameModo == "Medio")
        {
            levelDificult = 2;
            limitValid = 1;
            limitNotValid = 2;
            //maxNivel = 10;
            tentativas = 5;
        }
        else if (gameModo == "Dificil")
        {
            levelDificult = 3;
            limitValid = 1;
            limitNotValid = 3;
            //maxNivel = 12;
            tentativas = 5;
        }
        else
        {
            Debug.LogWarning("AtivadorDeTelas: ERRO: Dificuldade não setada!");
        }

        /*canvasMenu.SetActive(false);
        gameplay.SetActive(true);
        gameFacil.SetActive(gameModo == "Facil");
        gameMedio.SetActive(gameModo == "Medio");
        gameDificil.SetActive(gameModo == "Dificil");*/
        canvasMenu.SetActive(false);
        levelPanel.SetActive(true);
        niveisFacil.SetActive(gameModo == "Facil");
        niveisMedio.SetActive(gameModo == "Medio");
        niveisDificil.SetActive(gameModo == "Dificil");
    }

    public void CarregarJogo()
    {
        niveisFacil.SetActive(false);
        niveisMedio.SetActive(false);
        niveisDificil.SetActive(false);
        gameplay.SetActive(true);

        switch (levelDificult)
        {
            case 1:
                gameFacil.SetActive(true);
                gameMedio.SetActive(false);
                gameDificil.SetActive(false);
                break;
            case 2:
                gameFacil.SetActive(false);
                gameMedio.SetActive(true);
                gameDificil.SetActive(false);
                break;
            case 3:
                gameFacil.SetActive(false);
                gameMedio.SetActive(false);
                gameDificil.SetActive(true);
                break;
            default:
                break;
        }
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

    public void RegrasOnOff(string tela)
    {
        regrasPanel.SetActive(tela == "Regras");
        canvasMenu.SetActive(tela == "Menu");
        gameFacil.SetActive(tela == "Facil");
        gameMedio.SetActive(tela == "Medio");
        gameDificil.SetActive(tela == "DIficil");

    }

}
