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
        inputData.placeholder.GetComponent<UnityEngine.UI.Text>().text = "DD/MM/AAAA";
        // Adiciona um Listener para o evento de clique do botão
        btnCadastrar.onClick.AddListener(Cadastrar);
        btnPossuoConta.onClick.AddListener(PossuoConta);
    }

    private void Cadastrar()
    {
        // Pega o texto dos InputFields
        string nome = inputNome.text;
        string data = inputData.text;
        string email = inputEmail.text;
        string senha = inputSenha.text;

        if (string.IsNullOrEmpty(nome))
        {
            Debug.LogError("Digite o nome!");
            return;
        }
        else if(string.IsNullOrEmpty(data))
        {
            Debug.LogError("Digite a data de nascimento!");
            return;
        }
        else if(string.IsNullOrEmpty(email))
        {
            Debug.LogError("Digite o e-mail!");
            return;
        }
        else if (string.IsNullOrEmpty(senha))
        {
            Debug.LogError("Digite a senha!");
            return;
        }

        if (!Regex.IsMatch(email, @"^[^@]+@[^@]+\.[a-zA-Z]{2,}$"))
        {
            Debug.LogError("Digite a um e-mail válido!");
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
