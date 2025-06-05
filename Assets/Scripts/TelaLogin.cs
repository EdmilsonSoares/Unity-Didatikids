using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class TelaLogin : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputEmail;
    [SerializeField] private TMP_InputField inputSenha;
    [SerializeField] private Button btnEntrar;
    [SerializeField] private Button btnEsqueceuSenha;
    [SerializeField] private Button btnCadastro;
    [SerializeField] private TelaGerenciador telaGerenciador; //Referência ao script TelaGerenciador

    private void Awake()
    {
        btnEntrar.onClick.AddListener(Entrar);
        btnEsqueceuSenha.onClick.AddListener(EsqueceuSenha);
        btnCadastro.onClick.AddListener(Cadastro);
    }

    private void Entrar()
    {
        if (!Regex.IsMatch(inputEmail.text, @"^[^@]+@[^@]+\.[a-zA-Z]{2,}$"))
        {
            Debug.LogError("Digite um e-mail válido!");
            return;
        }

        if (string.IsNullOrEmpty(inputEmail.text))
        {
            Debug.LogError("Digite o e-mail!");
            return;
        }

        if (string.IsNullOrEmpty(inputSenha.text))
        {
            Debug.LogError("Digite o e-mail!");
            return;
        }
        // Ao entrar vai para a tela de perfis
        telaGerenciador.MostrarTela("Perfis"); // Desativa todas telas e ativa tela de perfis
        //LoadGerenciador.Instance.Carregar("CenaTeste"); // Chama o método Carregar da classe LoadGerenciador para carregar a cena desejada
    }

    private void EsqueceuSenha()
    {
        Debug.Log("Botão Esqueceu senha clicado!");
    }
    
    private void Cadastro()
    {
        telaGerenciador.MostrarTela("Cadastro"); // Desativa todas telas e ativa tela de cadastro
    }
}
