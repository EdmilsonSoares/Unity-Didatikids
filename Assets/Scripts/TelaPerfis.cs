using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TelaPerfis : MonoBehaviour
{
    [SerializeField] private Button btnAdicionarPerfil;
    [SerializeField] private Button btnChild1;
    [SerializeField] private Button btnChild2;
    [SerializeField] private Button btnChild3;
    [SerializeField] private TelaGerenciador telaGerenciador; // Referência ao script TelaGerenciador

    private void Awake()
    {
        btnAdicionarPerfil.onClick.AddListener(NovoPerfil);
        btnChild1.onClick.AddListener(Atividades);
        btnChild2.onClick.AddListener(Atividades);
        btnChild3.onClick.AddListener(Atividades);
    }

    private void NovoPerfil()
    {
        telaGerenciador.MostrarTela("NovoPerfil"); // Desativa todas telas e ativa tela Novo perfil
    }
    public void Atividades()
    { 
        telaGerenciador.MostrarTela("Atividades"); 
    }
    private void TesteLoading()
    {
        LoadGerenciador.Instance.Carregar("CenaTeste"); // Chama o método Carregar da classe LoadGerenciador para carregar a cena desejada
    }

}
