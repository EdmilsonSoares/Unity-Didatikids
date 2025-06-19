using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Video;

public class Configuracoes : MonoBehaviour
{
    [SerializeField] private Button btnBack;
    [SerializeField] private Button btnConta;
    [SerializeField] private Button btnChild;
    [SerializeField] private Button btnTela;
    [SerializeField] private Button btnAudio;
    [SerializeField] private Button btnLogout;
    [SerializeField] private Button btnClose;
    [SerializeField] private SwitchTela switchTela;
    private UserModel usuario;

    private void Awake()
    {
        CarregarUsuario();
        CarregarListaDeCriancas();
        btnBack.onClick.AddListener(Back);
        btnChild.onClick.AddListener(Childrens);
        btnClose.onClick.AddListener(Sair);
    }

    private void Back()
    {
        DescarregarUsuario();       
        SceneManager.UnloadSceneAsync("Configuracoes");
    }

    private void Childrens()
    {
        switchTela.Mostrar("Childrens");
    }

    private void Sair()
    {
        switchTela.ExibirAviso("ExitConfirm");
    }

    private bool CarregarUsuario()
    {
        string caminhoDoArquivo = Path.Combine(Application.persistentDataPath, "DadosUsuario.json");
        if (!File.Exists(caminhoDoArquivo))
        {
            Debug.LogWarning("Arquivo 'DadosUsuario.json' não encontrado.");
            usuario = null;
            return false;
        }

        try
        {
            string jsonLido = File.ReadAllText(caminhoDoArquivo); // Lê o conteúdo do arquivo JSON
            usuario = JsonUtility.FromJson<UserModel>(jsonLido); // Desserializa o JSON para um objeto DadosUsuario
            GameManager.Instance.SetUserProfile(usuario); // Armazena o usuario no GameManager
            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Erro ao ler ou processar o arquivo JSON: {e.Message}");
            usuario = null;
            return false;
        }
    }

    private void DescarregarUsuario()
    {
        GameManager.Instance.SetUserProfile(null);
        usuario = null;
    }

    private void CarregarListaDeCriancas()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.SetChildProfiles(usuario.children); // Passa a lista de childrenProfiles
        else
            Debug.LogError("GameManager.Instance não encontrado!");
    }

}