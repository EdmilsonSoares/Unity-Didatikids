using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TelaAtividades : MonoBehaviour
{

    [SerializeField] private Button btnBack;
    [SerializeField] private Button btnSettings;

    [SerializeField] private Button btnAtividadeLink;
    [SerializeField] private Button btnAtividadeMat;
    [SerializeField] private Button btnAtividadeTermo;
    [SerializeField] private TelaGerenciador telaGerenciador;

    private void Awake()
    {
        btnAtividadeLink.onClick.AddListener(ActivityLink);
        btnAtividadeTermo.onClick.AddListener(ActivityTermo);
        btnBack.onClick.AddListener(Perfis);
        btnSettings.onClick.AddListener(Settings);
    }

    private void Perfis()
    { 
        telaGerenciador.MostrarTela("Perfis"); 
    }

    private void Settings()
    {
        SceneManager.LoadSceneAsync("Configuracoes", LoadSceneMode.Additive);
    }

    public void ActivityLink()
    {
        GameManager.Instance.CarregarComAnimacao("MainMenu");
    }

    public void Matematica()
    {
        SceneManager.LoadScene("Matematica");
    }

    public void ActivityTermo()
    {
        SceneManager.LoadScene("Termo");
    }
}
