using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TelaAtividades : MonoBehaviour
{
<<<<<<< HEAD
    [SerializeField] private Button btnVoltarPerfis;
    [SerializeField] private Button btnAtividadeLink;
=======
    [SerializeField] private Button btnBack;
    [SerializeField] private Button btnSettings;
    [SerializeField] private Button btnAtividadeCC;
>>>>>>> e3da6b768984497cf17c611b6364bae0dd824baf
    [SerializeField] private Button btnAtividadeMat;
    [SerializeField] private Button btnAtividadeTermo;
    [SerializeField] private TelaGerenciador telaGerenciador;

    private void Awake()
    {
<<<<<<< HEAD
        btnVoltarPerfis.onClick.AddListener(Perfis);
        btnAtividadeLink.onClick.AddListener(ActivityLink);
        btnAtividadeLink.onClick.AddListener(ActivityTermo);
=======
        btnBack.onClick.AddListener(Perfis);
        btnSettings.onClick.AddListener(Settings);
        btnAtividadeCC.onClick.AddListener(ConnectColors);
>>>>>>> e3da6b768984497cf17c611b6364bae0dd824baf
    }

    private void Perfis()
    { 
        telaGerenciador.MostrarTela("Perfis"); 
    }

<<<<<<< HEAD
    public void ActivityLink()
=======
    private void Settings()
    {
        SceneManager.LoadSceneAsync("Configuracoes", LoadSceneMode.Additive);
    }

    public void ConnectColors()
>>>>>>> e3da6b768984497cf17c611b6364bae0dd824baf
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
