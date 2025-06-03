using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    }

    public void Perfis()
    { 
        telaGerenciador.MostrarTela("Perfis"); 
    }
    
    public void ConnectColors()
    {
        LoadGerenciador.Instance.Carregar("ConnectColors");
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
