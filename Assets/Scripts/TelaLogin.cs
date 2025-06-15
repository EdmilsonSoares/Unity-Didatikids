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
    [SerializeField] private Toggle toggleLembrar;
    private const string LAST_USER_EMAIL = "LastUserEmail";
    private const string REMEMBER_EMAIL = "RememberEmail"; // Para a preferência do toggle
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
        // Inicializa o Toggle com o valor salvo e adiciona o listener
        if (toggleLembrar != null)
        {
            // Lê a preferência do PlayerPrefs (0 = false, 1 = true, padrão é 0)
            toggleLembrar.isOn = PlayerPrefs.GetInt(REMEMBER_EMAIL, 0) == 1;

            // Adiciona um listener para quando o valor do toggle mudar
            toggleLembrar.onValueChanged.AddListener(SeToggleLembrarMudar);
        }
        else
        {
            Debug.LogError("Remember Me Toggle não atribuído no Inspector do script TelaLogin!");
        }
    }

    private void LembrarUsuario()
    {
        // Verifica se a opção "Lembrar Usuário" está ativada (lida do PlayerPrefs)
        bool rememberEmail = PlayerPrefs.GetInt(REMEMBER_EMAIL, 0) == 1;

        if (rememberEmail)
        {
            string savedEmail = PlayerPrefs.GetString(LAST_USER_EMAIL, "");
            if (!string.IsNullOrEmpty(savedEmail))
            {
                inputEmail.text = savedEmail;
                inputSenha.text = "123Aa*";
                Debug.Log($"Email lembrado: {savedEmail}");
            }
            else
            {
                Debug.Log("Opção 'Lembrar Email' ativada, mas nenhum email foi salvo anteriormente.");
                inputEmail.text = ""; // Garante que o campo esteja vazio se não houver email salvo
            }
        }
        else
        {
            Debug.Log("Opção 'Lembrar Email' desativada.");
            inputEmail.text = ""; // Limpa o campo se a opção não estiver ativa
        }

    }

    // Chamado quando o Toggle "Lembrar Usuário" muda de estado
    private void SeToggleLembrarMudar(bool isOn)
    {
        PlayerPrefs.SetInt(REMEMBER_EMAIL, isOn ? 1 : 0); // Salva 1 se true, 0 se false
        PlayerPrefs.Save(); // Garante que os dados sejam salvos imediatamente
        Debug.Log($"Preferência 'Lembrar Email' salva como: {isOn}");

        // Se o usuário desabilitar "Lembrar Usuário", limpa o e-mail salvo
        if (!isOn)
        {
            PlayerPrefs.DeleteKey(LAST_USER_EMAIL);
            PlayerPrefs.Save();
            Debug.Log("Email salvo removido dos PlayerPrefs.");
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

        bool usuarioEncontrado = ProcurarUsuario();
        if (usuarioEncontrado)
        {
            // Compara o email e a senha digitados com os dados lidos do JSON
            if (emailDigitado == usuarioSalvo.userEmail && senhaDigitada == usuarioSalvo.userSenha)
            {
                Debug.Log("Login bem-sucedido!");
                // Salva o e-mail recém-logado APENAS se o toggle "Lembrar Usuário" estiver ativo
                if (toggleLembrar != null && toggleLembrar.isOn)
                {
                    PlayerPrefs.SetString(LAST_USER_EMAIL, emailDigitado);
                    PlayerPrefs.Save();
                    Debug.Log($"Email '{emailDigitado}' salvo para lembrar.");
                }
                // Aqui os dados do usuário não são carregados, apeas a lista de crianças é carregada no GameManager
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
            usuarioSalvo = null;
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
            usuarioSalvo = null;
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
