using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using System.IO;

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

    private void Awake()
    {
        btnCadastrar.onClick.AddListener(Cadastrar); // Adiciona um Listener para o evento de clique do botão
        btnPossuoConta.onClick.AddListener(PossuoConta);
    }

    private void Cadastrar()
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
            Debug.Log("Nome: " + nome + ", Data: " + data + ", Email: " + email + ", Senha: " + senha);
            telaGerenciador.MostrarTela("Login"); // Desativa todas telas e ativa tela de perfis
        }
    }

    private void SalvarUsuario()
    {
        // 1. Cria uma instância da classe DadosUsuario
        UserModel novoUsuario = new UserModel(nome, data, email, senha);
        // 2. Converte a instância para uma string JSON
        // O segundo parâmetro 'true' é para formatar o JSON de forma legível (pretty print)
        string json = JsonUtility.ToJson(novoUsuario, true);
        // 3. Define o caminho do arquivo para Application.persistentDataPath
        string caminhoDoArquivo = Path.Combine(Application.persistentDataPath, "DadosUsuario.json");

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
