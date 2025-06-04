using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TelaAtividades : MonoBehaviour
{
    [SerializeField] private Button btnVoltarPerfis;
    [SerializeField] private Button btnAtividadeCC;
    [SerializeField] private Button btnAtividadeMat;
    [SerializeField] private Button btnAtividadePort;
    [SerializeField] private TelaGerenciador telaGerenciador; // Referência ao script TelaGerenciador

    private void Awake()
    {
        btnVoltarPerfis.onClick.AddListener(Perfis);
        btnAtividadeCC.onClick.AddListener(ConnectColors);
    }

    public void Perfis()
    { 
        telaGerenciador.MostrarTela("Perfis"); 
    }
    
    public void ConnectColors()
    {
        SceneManager.LoadScene("MainMenu");        
    }
    public void Matematica()
    {
        LoadGerenciador.Instance.Carregar("Matematica");
    }
    public void Portugeus()
    {
        LoadGerenciador.Instance.Carregar("Portugues");
    }

}
