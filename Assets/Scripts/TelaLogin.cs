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

    private void Awake()
    {
        btnEntrar.onClick.AddListener(Entrar);
        btnEsqueceuSenha.onClick.AddListener(EsqueceuSenha);
        btnCadastro.onClick.AddListener(Cadastro);
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

        string caminhoDoArquivo = Path.Combine(Application.persistentDataPath, "DadosUsuario.json");
        if (!File.Exists(caminhoDoArquivo))
        {
            Debug.LogError("Arquivo 'DadosUsuario.json' não encontrado.");
            return;
        }

        try
        {
            string jsonLido = File.ReadAllText(caminhoDoArquivo); // Lê o conteúdo do arquivo JSON
            UserModel usuarioSalvo = JsonUtility.FromJson<UserModel>(jsonLido); // Desserializa o JSON para um objeto DadosUsuario
            // Compara o email e a senha digitados com os dados lidos do JSON
            if (emailDigitado == usuarioSalvo.userEmail && senhaDigitada == usuarioSalvo.userSenha)
            {
                Debug.Log("Login bem-sucedido!");
                // Salvar os dados no objeto persistente GameManager
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
        catch (System.Exception e)
        {
            Debug.LogError($"Erro ao ler ou processar o arquivo JSON: {e.Message}");
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
