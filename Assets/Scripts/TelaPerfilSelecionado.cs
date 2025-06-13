using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO; // Necessário para manipulacao de arquivos
using System.Collections.Generic; // Necessário para List

public class TelaPerfilSelecionado : MonoBehaviour
{
    [SerializeField] private Button btnBack;
    [SerializeField] private Button btnSettings;
    [SerializeField] private Button btnEditar;
    [SerializeField] private Button btnExcluir;
    [SerializeField] private Button btnAtividades;
    [SerializeField] private TelaGerenciador telaGerenciador;

    [Header("Elementos da Criança Selecionada")]
    [SerializeField] private Image childAvatarImage;
    [SerializeField] private TMP_Text childNameText;
    [SerializeField] private TMP_Text childDataText;
    [SerializeField] private TMP_Text childTopicoText;

    // Esta variável armazena a referência para a ChildModel que está sendo exibida e manipulada.
    private ChildModel currentChildBeingEdited;

    private void Awake()
    {
        btnBack.onClick.AddListener(Perfis);
        btnSettings.onClick.AddListener(Settings);
        btnEditar.onClick.AddListener(EditChildProfile);
        btnExcluir.onClick.AddListener(DeleteChildProfile);
        btnAtividades.onClick.AddListener(Atividades);
    }

    private void Perfis()
    {
        telaGerenciador.MostrarTela("Perfis");
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetCurrentSelectedChild(null); // limpar a criança selecionada no GameManager
        }
    }

    private void Settings()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetCurrentSelectedChild(null); // limpar a criança selecionada no GameManager
        }
        SceneManager.LoadSceneAsync("Configuracoes", LoadSceneMode.Additive);
    }

    private void Atividades()
    {
        telaGerenciador.MostrarTela("Atividades");
    }

    private void OnEnable()
    {
        // Chamada ao método para carregar e exibir os detalhes da crianca
        LoadSelectedChildProfile();
    }

    // Metodo para carregar e exibir os dados da crianca selecionada
    private void LoadSelectedChildProfile()
    {
        if (GameManager.Instance != null && GameManager.Instance.CurrentSelectedChild != null)
        {
            // Atribui a crianca selecionada a nossa variavel local
            currentChildBeingEdited = GameManager.Instance.CurrentSelectedChild;

            // Preenche os elementos da UI com os dados da crianca
            if (childNameText != null)
            {
                childNameText.text = currentChildBeingEdited.childNome;
            }
            if (childDataText != null)
            {
                childDataText.text = currentChildBeingEdited.childData;
            }
            if (childTopicoText != null)
            {
                childTopicoText.text = currentChildBeingEdited.childTopico;
            }

            // Carrega e exibe o avatar
            if (childAvatarImage != null && !string.IsNullOrEmpty(currentChildBeingEdited.avatarIconPath))
            {
                Sprite avatarSprite = Resources.Load<Sprite>(currentChildBeingEdited.avatarIconPath);
                if (avatarSprite != null)
                {
                    childAvatarImage.sprite = avatarSprite;
                    childAvatarImage.preserveAspect = true; // Mantem a proporcao da imagem
                    childAvatarImage.gameObject.SetActive(true); // Garante que a imagem esta ativa
                }
                else
                {
                    Debug.LogWarning($"Avatar '{currentChildBeingEdited.avatarIconPath}' não encontrado em Resources para a criança {currentChildBeingEdited.childNome}.");
                    childAvatarImage.gameObject.SetActive(false); // Oculta se nao encontrar
                }
            }
            else if (childAvatarImage != null)
            {
                childAvatarImage.gameObject.SetActive(false); // Oculta se nao houver caminho de avatar
            }

            Debug.Log($"Detalhes do perfil de '{currentChildBeingEdited.childNome}' carregados na tela.");
        }
        else
        {
            Debug.LogError("Erro: Nenhuma criança selecionada no GameManager para exibir. Voltando para a tela de perfis.");
            Perfis(); // Volta para a tela de perfis se nao houver crianca selecionada
        }
    }

    // Metodo para iniciar o processo de edicao do perfil
    private void EditChildProfile()
    {
        if (currentChildBeingEdited != null)
        {
            Debug.Log($"Iniciando edição do perfil de: {currentChildBeingEdited.childNome}");
            // Aqui, você deve navegar para uma tela de edicao.
            // A tela de edicao (que pode ser a TelaNovoPerfil adaptada ou uma nova)
            // acessara GameManager.Instance.CurrentSelectedChild para preencher seus campos.
            //telaGerenciador.MostrarTela("TelaDeEdicaoDePerfil"); // Substitua por o nome da sua tela de edicao real
        }
        else
        {
            Debug.LogError("Nenhuma criança selecionada para editar. currentChildBeingEdited é nulo.");
        }
    }

    // Metodo para excluir o perfil da crianca
    private void DeleteChildProfile()
    {
        if (currentChildBeingEdited == null)
        {
            Debug.LogError("Nenhuma criança selecionada para excluir. currentChildBeingEdited é nulo.");
            return;
        }

        Debug.LogWarning($"Solicitação de exclusão para o perfil de: {currentChildBeingEdited.childNome}");

        // --- IMPORTANTE: ADICIONE AQUI UMA JANELA DE CONFIRMACAO! ---
        // NUNCA exclua dados permanentemente sem uma confirmacao do usuario.
        // Ex: Mostrar uma UI de "Tem certeza que deseja excluir o perfil de [nome da crianca]?"
        // Se o usuario confirmar, entao execute o codigo abaixo.
        // Por simplicidade, o codigo abaixo executa a exclusao diretamente.

        // 1. Remove a crianca da lista em memoria no GameManager
        if (GameManager.Instance != null && GameManager.Instance.ChildProfiles != null)
        {
            // Usamos Remove(object) que remove a primeira ocorrencia do objeto
            bool removedFromManager = GameManager.Instance.ChildProfiles.Remove(currentChildBeingEdited);
            if (removedFromManager)
            {
                Debug.Log($"Perfil de '{currentChildBeingEdited.childNome}' removido do GameManager em memoria.");
                GameManager.Instance.SetCurrentSelectedChild(null); // Limpa a crianca selecionada no GameManager
            }
            else
            {
                Debug.LogWarning($"Perfil de '{currentChildBeingEdited.childNome}' nao encontrado no GameManager para remocao em memoria.");
            }
        }
        else
        {
            Debug.LogError("GameManager.Instance ou ChildProfiles nao esta disponivel para remocao em memoria.");
        }

        // 2. Remove a crianca do arquivo JSON (requer ler, modificar e salvar)
        string caminhoDoArquivoUser = Path.Combine(Application.persistentDataPath, "DadosUsuario.json");
        if (File.Exists(caminhoDoArquivoUser))
        {
            try
            {
                string jsonResponsavel = File.ReadAllText(caminhoDoArquivoUser);
                UserModel currentUser = JsonUtility.FromJson<UserModel>(jsonResponsavel);

                // IMPORTANTE: Para remocao robusta, a ChildModel deveria ter um ID unico.
                // Como nao temos um ID unico, estamos usando o childNome e childData para tentar identificar.
                // Isso pode falhar se houver varias criancas com o mesmo nome e data.
                int initialCount = currentUser.children.Count;
                currentUser.children.RemoveAll(c => c.childNome == currentChildBeingEdited.childNome &&
                                                    c.childData == currentChildBeingEdited.childData);

                if (currentUser.children.Count < initialCount)
                {
                    string updatedJsonResponsavel = JsonUtility.ToJson(currentUser, true);
                    File.WriteAllText(caminhoDoArquivoUser, updatedJsonResponsavel);
                    Debug.Log($"Perfil de '{currentChildBeingEdited.childNome}' removido do arquivo JSON.");
                }
                else
                {
                    Debug.LogWarning($"Perfil de '{currentChildBeingEdited.childNome}' nao encontrado no arquivo JSON para remocao ou ja foi removido.");
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

        // 3. Volta para a TelaPerfis. Ela recarregara os dados atualizados do GameManager.
        telaGerenciador.MostrarTela("Perfis");
    }
}