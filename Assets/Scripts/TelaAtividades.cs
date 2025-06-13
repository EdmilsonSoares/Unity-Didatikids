using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TelaAtividades : MonoBehaviour
{
    [SerializeField] private Button btnVoltarPerfis;
    [SerializeField] private Button btnAtividadeLink;
    [SerializeField] private Button btnAtividadeMat;
    [SerializeField] private Button btnAtividadeTermo;
    [SerializeField] private TelaGerenciador telaGerenciador;

    private void Awake()
    {
        btnVoltarPerfis.onClick.AddListener(Perfis);
        btnAtividadeLink.onClick.AddListener(ActivityLink);
        btnAtividadeLink.onClick.AddListener(ActivityTermo);
    }

    public void Perfis()
    { 
        telaGerenciador.MostrarTela("Perfis"); 
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
