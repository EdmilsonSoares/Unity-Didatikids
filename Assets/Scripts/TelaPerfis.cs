using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TelaPerfis : MonoBehaviour
{
    [SerializeField] private Button btnAdicionarPerfil;
    [SerializeField] private Button btnTeste;
    [SerializeField] private TelaGerenciador telaGerenciador; // Referência ao script TelaGerenciador

    private void Awake()
    {
        btnAdicionarPerfil.onClick.AddListener(NovoPerfil);
        btnTeste.onClick.AddListener(TesteLoading);
    }

    private void NovoPerfil()
    {
        telaGerenciador.MostrarNovoPerfil(); // Desativa todas telas e ativa tela Novo perfil
    }

    private void TesteLoading()
    {
        LoadGerenciador.Instance.Carregar("CenaTeste"); // Chama o método Carregar da classe LoadGerenciador para carregar a cena desejada
    }

}
