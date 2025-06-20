using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System;

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
    private string email { get; set; }
    private string senha { get; set; }

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

    private async void Entrar()
    {
        email = inputEmail.text;
        senha = inputSenha.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
        {
            Debug.LogError("Todos os campos devem ser preenchidos!");
            return;
        }

        var responsavel = new Responsavel()
        {
            Email = email,
            Senha = senha
        };

        // faz login
        var response = await responsavel.LoginAsync(); 
        responsavel.Access = response.Access;
        responsavel.Refresh = response.Refresh;

        string json = JsonConvert.SerializeObject(response);
        string caminhoTokens = Path.Combine(Application.persistentDataPath, "Tokens.json");

        var id = response.IdResponsavel;
        var nome = response.Nome;
        var perfis = await responsavel.GetCriancas(); // retorna uma lista de crianças

        if (perfis is not null)
        {
            Debug.Log("Login bem-sucedido!");
            // Salva o e-mail recém - logado APENAS se o toggle "Lembrar Usuário" estiver ativo
            if (toggleLembrar != null && toggleLembrar.isOn)
            {
                PlayerPrefs.SetString(LAST_USER_EMAIL, email);
                PlayerPrefs.Save();
                Debug.Log($"Email '{email}' salvo para lembrar.");
            }

            if (perfis.Count > 0)
            {
                System.Random random = new System.Random();
                var children = new List<ChildModel>();
                foreach (var perfil in perfis)
                {
                    var topicos_length = perfil.TopicosInteresse.Count;
                    //Debug.Log(topicos_length);
                    var topico = perfil.TopicosInteresse[random.Next(1, topicos_length)];
                    //Debug.Log(topico);
                    var child = new ChildModel(perfil.Nome, perfil.DtNascimento, topico, perfil.Avatar);
                    children.Add(child);
                }
                // Aqui os dados do usuário não são carregados, apeas a lista de crianças é carregada no GameManager
                if (GameManager.Instance != null)
                    GameManager.Instance.SetChildProfiles(children); // Passa a lista de childrenProfiles diretamente
                else
                    Debug.LogError("GameManager.Instance não encontrado!");
            }
            telaGerenciador.MostrarTela("Perfis"); // Desativa todas telas e ativa tela de perfis
        }
    }

    //private bool ProcurarUsuario()
    //{
    //    string caminhoDoArquivo = Path.Combine(Application.persistentDataPath, "DadosUsuarioLogin.json");
    //    if (!File.Exists(caminhoDoArquivo))
    //    {
    //        Debug.LogWarning("Arquivo 'DadosUsuario.json' não encontrado.");
    //        usuarioSalvo = null;
    //        return false;
    //    }

    //    try
    //    {
    //        string jsonLido = File.ReadAllText(caminhoDoArquivo); // Lê o conteúdo do arquivo JSON
    //        usuarioSalvo = JsonConvert.DeserializeObject<Responsavel>(jsonLido); // Desserializa o JSON para um objeto DadosUsuario
    //        return true;
    //    }
    //    catch (System.Exception e)
    //    {
    //        Debug.LogError($"Erro ao ler ou processar o arquivo JSON: {e.Message}");
    //        usuarioSalvo = null;
    //        return false;
    //    }
    //}

    private void EsqueceuSenha()
    {
        Debug.Log("Botão Esqueceu senha clicado!");
    }

    private void Cadastro()
    {
        telaGerenciador.MostrarTela("Cadastro"); // Desativa todas telas e ativa tela de cadastro
    }
}
