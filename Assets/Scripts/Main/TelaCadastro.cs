using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using static Responsavel;

public class TelaCadastro : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputNome;
    [SerializeField] private TMP_InputField inputEmail;
    [SerializeField] private TMP_InputField inputSenha;
    [SerializeField] private Button btnCadastrar;
    [SerializeField] private Button btnPossuoConta;
    [SerializeField] private DataInputValidador dataInputValidador;
    [SerializeField] private TelaGerenciador telaGerenciador; //Referência ao script TelaGerenciador
    private string nome;
    private string data;
    private string email;
    private string senha;
    // internal string nome { get; set; }
    // internal string data { get; set; }
    // internal string email { get; set; }
    //internal string senha { get; set; }

    private void Awake()
    {
        btnCadastrar.onClick.AddListener(Cadastrar); // Adiciona um Listener para o evento de clique do botão
        btnPossuoConta.onClick.AddListener(PossuoConta);
    }

    private void Cadastrar() //async
    {
        // Pega o texto dos InputFields
        nome = inputNome.text;
        data = dataInputValidador.GetDataTextValidada();
        email = inputEmail.text;
        senha = inputSenha.text;

        if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(data) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
        {
            Debug.LogError("Todos os campos devem ser preenchidos corretamente!");
            return;
        }
        else
        {
            if (!Regex.IsMatch(email, @"^[^@]+@[^@]+\.[a-zA-Z]{2,}$"))
            {
                Debug.LogError("Digite um e-mail válido!");
                return;
            }

            if (!Regex.IsMatch(inputSenha.text, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{5,}$"))
            {
                Debug.LogError("Digite uma senha válida!");
                return;
            }
            SalvarUsuario();
            /*
            var responsavel = new Responsavel()
            {
                Email = email,
                Nome = nome,
                Senha = senha,
                DtNascimento = data
            };
            telaGerenciador.MostrarTela("Verificacao");
            await responsavel.EnviarCodigoAsync();
            */
            Debug.Log("Nome: " + nome + ", Data: " + data + ", Email: " + email + ", Senha: " + senha);
            telaGerenciador.MostrarTela("Login");
        }
    }

    // SALVAR OS DADOS DO RESPONSÁVEL PARA SEREM ENVIADOS NA TELA DE VERIFICAÇÃO DO CÓDIGO

    private void SalvarUsuario()
    {
        /*
        var responsavel = new ResponsavelLocal()
        {
            nome = nome,
            email = email,
            senha = senha,
            dt_nascimento = data
        };
        string json = JsonConvert.SerializeObject(responsavel);
        string caminhoDoArquivo = Path.Combine(Application.persistentDataPath, "DadosUsuario.json");*/

        UserModel novoUsuario = new UserModel(nome, data, email, senha); // 1. Cria uma instância da classe DadosUsuario
        string json = JsonUtility.ToJson(novoUsuario, true); // 2. Converte a instância para uma string JSON
        string caminhoDoArquivo = Path.Combine(Application.persistentDataPath, "DadosUsuario.json"); // 3. Define o caminho do arquivo para Application.persistentDataPath

        try
        {
            File.WriteAllText(caminhoDoArquivo, json);
            Debug.Log($"Dados do usuário salvos com sucesso em: {caminhoDoArquivo}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Erro ao salvar o arquivo JSON: {e.Message}");
        }
    }

    private void PossuoConta()
    {
        telaGerenciador.MostrarTela("Login"); // Desativa todas as telas e ativa tela login
    }
}

// CADASTRO
// Usuario insere todos os dados
// Sistema salva os dados do usuário em uma instância temporária
// Sistema envia código para e-mail (smtp)
// Usuario fornece o codigo recebido
// Sistema realiza o cadastro enviando os dados do usuário e o código fornecido
// Sistema volta para a tela de login


