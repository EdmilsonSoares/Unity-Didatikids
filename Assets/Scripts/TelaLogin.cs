using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Collections.Generic;

public class TelaLogin : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputEmail;
    [SerializeField] private TMP_InputField inputSenha;
    [SerializeField] private Button btnEntrar;
    [SerializeField] private Button btnEsqueceuSenha;
    [SerializeField] private Button btnCadastro;
    [SerializeField] private TelaGerenciador telaGerenciador; //Referência ao script TelaGerenciador
    private UserModel usuarioSalvo;

    private void OnEnable()
    {
        LembrarUsuario();
    }

    private void Awake()
    {
        btnEntrar.onClick.AddListener(Entrar);
        btnEsqueceuSenha.onClick.AddListener(EsqueceuSenha);
        btnCadastro.onClick.AddListener(Cadastro);
    }

    private void LembrarUsuario()
    {
        bool retorno = ProcurarUsuario();
        if (retorno)
        {
            if (string.IsNullOrEmpty(usuarioSalvo.userEmail) || string.IsNullOrEmpty(usuarioSalvo.userSenha))
            {
                Debug.LogWarning("DadosUsuario.json encontrado, mas 'userNome' ou 'userDataNascimento' ausentes/vazios.");
                return;
            }
            else
            {
                Debug.Log("Dados do usuário encontrados e válidos: " + usuarioSalvo.userNome);
                inputEmail.text = usuarioSalvo.userEmail;
                inputSenha.text = usuarioSalvo.userSenha;
            }
        }
    }


    private void Entrar()
    {
        string emailDigitado = inputEmail.text;
        string senhaDigitada = inputSenha.text;

        if (string.IsNullOrEmpty(inputEmail.text) || string.IsNullOrEmpty(inputSenha.text))
        {
            Debug.LogError("Todos os campos devem ser preenchidos!");
            return;
        }

        bool retorno = ProcurarUsuario();
        if (retorno)
        {
            // Compara o email e a senha digitados com os dados lidos do JSON
            if (emailDigitado == usuarioSalvo.userEmail && senhaDigitada == usuarioSalvo.userSenha)
            {
                Debug.Log("Login bem-sucedido!");
                // Salvar apenas as crianças no objeto persistente GameManager
                if (GameManager.Instance != null)
                    GameManager.Instance.SetChildProfiles(usuarioSalvo.children); // Passa a lista de childrenProfiles diretamente
                else
                    Debug.LogError("GameManager.Instance não encontrado!");
                telaGerenciador.MostrarTela("Perfis"); // Desativa todas telas e ativa tela de perfis
            }
            else
            {
                Debug.LogError("Email ou senha incorretos.");
            }
        }
    }

    private bool ProcurarUsuario()
    {
        string caminhoDoArquivo = Path.Combine(Application.persistentDataPath, "DadosUsuario.json");
        if (!File.Exists(caminhoDoArquivo))
        {
            Debug.LogWarning("Arquivo 'DadosUsuario.json' não encontrado.");
            return false;
        }

        try
        {
            string jsonLido = File.ReadAllText(caminhoDoArquivo); // Lê o conteúdo do arquivo JSON
            usuarioSalvo = JsonUtility.FromJson<UserModel>(jsonLido); // Desserializa o JSON para um objeto DadosUsuario
            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Erro ao ler ou processar o arquivo JSON: {e.Message}");
            return false;
        }
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
