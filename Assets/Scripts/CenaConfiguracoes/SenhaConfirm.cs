using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class SenhaConfirm : MonoBehaviour
{
    [SerializeField] private Button btnCancelar;
    [SerializeField] private Button btnOk;
    [SerializeField] private TMP_InputField inputSenha;
    [SerializeField] private TMP_Text textoFeedback;
    [SerializeField] private SwitchTela switchTela;
    private ChildModel criancaParaRemover;

    private void Awake()
    {
        btnCancelar.onClick.AddListener(DesativarPainelSenha);
        btnOk.onClick.AddListener(VerificarSenha);
    }

    private void OnEnable()
    {
        textoFeedback.text = "";
        inputSenha.text = "";
    }

    private void DesativarPainelSenha()
    {
        switchTela.ExibirAviso("");
    }

    private void VerificarSenha()
    {
        textoFeedback.text = "";
        string senhaDigitada = inputSenha.text;
        if (string.IsNullOrEmpty(inputSenha.text))
        {
            Debug.Log("Senha vazia");
            textoFeedback.text = "Senha vazia";
            return;
        }

        //o usuário está no gamemanager
        if (GameManager.Instance.UserProfile != null)
        {
            if (senhaDigitada == GameManager.Instance.UserProfile.userSenha)
            {
                if (ExcluirPerfilDeCriancaEmMemoria())
                {
                    ExcluirPerfilDeCriancaDoJSON();
                }
            }
            else
            {
                textoFeedback.text = "Senha Incorreta";
            }
        }
        else
        {
            Debug.LogError("Não há Usuario carregado no GameManager.");
        }
    }

    private bool ExcluirPerfilDeCriancaEmMemoria()
    {
        if (GameManager.Instance.ChildProfiles != null)
        {
            criancaParaRemover = GameManager.Instance.CurrentSelectedChild;
            Debug.LogWarning($"Removendo {criancaParaRemover.childNome}");
            bool removedFromManager = GameManager.Instance.ChildProfiles.Remove(criancaParaRemover); // Remove da lista em memoria e retorna true se remover
            GameManager.Instance.SetCurrentSelectedChild(null); // Limpa a crianca selecionada no GameManager
            GameManager.Instance.QuandoListaCriancaMudar?.Invoke();
        }
        else
        {
            Debug.LogWarning("Não há criança selecionada no gameManager");
            return false;
        }
        return true;
    }

    private void ExcluirPerfilDeCriancaDoJSON()
    {
        string caminhoDoArquivo = Path.Combine(Application.persistentDataPath, "DadosUsuario.json");
        if (File.Exists(caminhoDoArquivo))
        {
            try
            {
                string jsonResponsavel = File.ReadAllText(caminhoDoArquivo);  // Lê o conteúdo do arquivo JSON
                UserModel user = JsonUtility.FromJson<UserModel>(jsonResponsavel); // Desserializa o JSON para um objeto DadosUsuario

                int initialCount = user.children.Count;
                user.children.RemoveAll(c => c.childNome == criancaParaRemover.childNome && c.childData == criancaParaRemover.childData);

                if (user.children.Count < initialCount)
                {
                    string updatedJsonResponsavel = JsonUtility.ToJson(user, true); // Converte a instância para uma string JSON
                    File.WriteAllText(caminhoDoArquivo, updatedJsonResponsavel); // Reescreve o Json com dados alterados
                    Debug.Log($"Perfil de '{criancaParaRemover.childNome}' removido do arquivo JSON.");
                }
                else
                {
                    Debug.LogWarning($"Perfil de '{criancaParaRemover.childNome}' nao encontrado no arquivo JSON para remocao ou ja foi removido.");
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Erro ao remover perfil de criança do JSON: {e.Message}");
            }
        }
        else
        {
            Debug.LogWarning("Arquivo DadosUsuario.json nao encontrado ao tentar remover perfil de crianca.");
        }
        switchTela.Mostrar("Childrens");
        DesativarPainelSenha();
    }
}
