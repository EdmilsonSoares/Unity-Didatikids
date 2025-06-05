using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

public class TelaCadastro : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputNome;
    [SerializeField] private TMP_InputField inputData;
    [SerializeField] private TMP_InputField inputEmail;
    [SerializeField] private TMP_InputField inputSenha;
    [SerializeField] private Button btnCadastrar;
    [SerializeField] private Button btnPossuoConta;
    [SerializeField] private TelaGerenciador telaGerenciador; //Referência ao script TelaGerenciador

    private void Awake()
    {
        btnCadastrar.onClick.AddListener(Cadastrar); // Adiciona um Listener para o evento de clique do botão
        btnPossuoConta.onClick.AddListener(PossuoConta);
    }

    private void Cadastrar()
    {
        // Pega o texto dos InputFields
        string nome = inputNome.text;
        string data = inputData.text;
        string email = inputEmail.text;
        string senha = inputSenha.text;

        if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(data) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
        {
            Debug.LogError("Todos os campos devem ser preenchidos!");
            return;
        }

        if (!Regex.IsMatch(email, @"^[^@]+@[^@]+\.[a-zA-Z]{2,}$"))
        {
            Debug.LogError("Digite um e-mail válido!");
            return;
        }

        //if (nome == "" || data == "" || email == "" || senha == "")
        //{
        //    Debug.LogError("Todos os campos devem ser preenchidos!");
        //    return;
        //}
        Debug.Log("Nome: " + nome + ", Data: " + data + ", Email: " + email + ", Senha: " + senha);
        telaGerenciador.MostrarTela("Perfis"); // Desativa todas telas e ativa tela de perfis
    }

    private void PossuoConta()
    {
        telaGerenciador.MostrarTela("Login"); // Desativa todas as telas e ativa tela login
    }
}
