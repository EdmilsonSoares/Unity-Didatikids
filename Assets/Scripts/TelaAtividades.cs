using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TelaAtividades : MonoBehaviour
{
    [SerializeField] private Button btnBack;
    [SerializeField] private Button btnSettings;
    [SerializeField] private Button btnAtividadeCC;
    [SerializeField] private Button btnAtividadeMat;
    [SerializeField] private Button btnAtividadePort;
    [SerializeField] private TelaGerenciador telaGerenciador; // Referência ao script TelaGerenciador

    private void Awake()
    {
        btnBack.onClick.AddListener(Perfis);
        btnSettings.onClick.AddListener(Settings);
        btnAtividadeCC.onClick.AddListener(ConnectColors);
    }

    private void Perfis()
    { 
        telaGerenciador.MostrarTela("Perfis"); 
    }

    private void Settings()
    {
        SceneManager.LoadSceneAsync("Configuracoes", LoadSceneMode.Additive);
    }

    public void ConnectColors()
    {
        GameManager.Instance.CarregarComAnimacao("MainMenu");
    }

    public void Matematica()
    {
        SceneManager.LoadScene("Matematica");
    }

    public void Portugeus()
    {
        SceneManager.LoadScene("Portugues");
    }
}
