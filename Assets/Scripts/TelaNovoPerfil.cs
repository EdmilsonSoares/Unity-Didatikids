using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO; // Para manipulação de arquivos
using System.Collections.Generic; // Para usar List

public class TelaNovoPerfil : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputNome;
    [SerializeField] private TMP_InputField inputData;
    [SerializeField] private TMP_Dropdown dropdownTopicos; // Menu Dropdow
    [SerializeField] private Button btnCadastrar;
    [SerializeField] private Button btnCancelar;
    [SerializeField] private DataInputValidador dataInputValidador;
    [SerializeField] private TelaGerenciador telaGerenciador; // Referência ao script TelaGerenciador
    [SerializeField] private GameObject telaAvatares; // Referencia ao CanvasAvatares
    public Button btnAvatar; // Botão de escolha do avatar
    private Image avatarImageBTN; // Componente Image do botão de escolha de avatar
    private Sprite defaultAvatarSprite; // Guarda o sprite default do botão de excolha de avatar
    private string selectedAvatarPath = "";
    private string nomeCrianca;
    private string dataCrianca;
    private string topico;
    private const int MAX_CHILDREN_PROFILES = 3;

    private void Awake()
    {
        btnAvatar.onClick.AddListener(MostrarTelaAvatares);
        btnCadastrar.onClick.AddListener(Cadastrar);
        btnCancelar.onClick.AddListener(Cancelar);
        avatarImageBTN = btnAvatar.GetComponent<Image>();
        defaultAvatarSprite = avatarImageBTN.sprite;
    }

    private void OnEnable()
    {
        selectedAvatarPath = "";
        nomeCrianca = "";
        dataCrianca = "";
        topico = "Nenhum"; // Define o tópico padrão inicial
        inputNome.text = ""; // Limpa o texto do campo de nome
        inputData.text = ""; // Limpa o texto do campo de data
        avatarImageBTN.sprite = defaultAvatarSprite;
        dropdownTopicos.value = 0; // Define o valor para o primeiro item (Nenhum)
        dropdownTopicos.RefreshShownValue(); // Atualiza a UI do dropdown
    }

    // Método para receber e exibir a imagem de avatar selecionada
    public void AtualizarAvatarSelecionado(Sprite avatar, string avatarPath)
    {
        if (avatarImageBTN != null)
        {
            avatarImageBTN.sprite = avatar;
            avatarImageBTN.preserveAspect = true; // Manter a proporção (círculo)
            selectedAvatarPath = avatarPath;
        }
    }

    private void MostrarTelaAvatares()
    {
        telaAvatares.SetActive(true); // Ativa tela de Avatares
    }

    private void Cadastrar()
    {
        // Pega o texto dos InputFields
        nomeCrianca = inputNome.text;
        dataCrianca = dataInputValidador.GetDataTextValidada();

        if (string.IsNullOrEmpty(nomeCrianca) || string.IsNullOrEmpty(dataCrianca))
        {
            Debug.LogError("Todos os campos devem ser preenchidos corretamente!");
            return;
        }

        string caminhoDoArquivoUser = Path.Combine(Application.persistentDataPath, "DadosUsuario.json");
        if (!File.Exists(caminhoDoArquivoUser))
        {
            Debug.LogError("Erro: O arquivo de dados do responsável não foi encontrado. Cadastre-se primeiro.");
            return;
        }

        try
        {
            string jsonResponsavel = File.ReadAllText(caminhoDoArquivoUser); // 3. Lê o conteúdo do arquivo JSON do responsável
            UserModel currentUser = JsonUtility.FromJson<UserModel>(jsonResponsavel); // 4. Desserializa o JSON para um objeto UserModel

            if (currentUser.children != null && currentUser.children.Count >= MAX_CHILDREN_PROFILES)
            {
                Debug.LogWarning($"Limite de {MAX_CHILDREN_PROFILES} perfis de crianças atingido para este responsável.");
                // Opcional: Mostrar uma mensagem na UI para o usuário
                // Ex: GetComponentInParent<SomeUIMessageScript>().ShowMessage("Você já cadastrou o número máximo de crianças.");
                telaGerenciador.MostrarTela("Perfis"); // Retorna para a tela de perfis sem cadastrar
                return;
            }
            // 5. Cria um novo perfil de criança
            ChildModel novaCrianca = new ChildModel(nomeCrianca, dataCrianca, topico, selectedAvatarPath);
            currentUser.AddChildProfile(novaCrianca); // 6. Adiciona o novo perfil da criança à lista de crianças do responsável
            // Atualiza os dados no objeto persistente GameManager
            if (GameManager.Instance != null)
                GameManager.Instance.AddChildProfile(novaCrianca);
            else
                Debug.LogError("GameManager.Instance não encontrado!");

            // 7. Serializa o objeto UserModel atualizado de volta para JSON
            string updatedJsonResponsavel = JsonUtility.ToJson(currentUser, true);
            // 8. Salva o JSON atualizado, sobrescrevendo o arquivo existente
            File.WriteAllText(caminhoDoArquivoUser, updatedJsonResponsavel);
            Debug.Log($"Perfil da criança '{nomeCrianca}' salvo com sucesso!");
            telaGerenciador.MostrarTela("Perfis"); // Volta para a tela de perfis
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Erro ao cadastrar perfil da criança: {e.Message}");
        }
    }

    private void Cancelar()
    {
        telaGerenciador.MostrarTela("Perfis"); // Desativa todas telas e ativa tela de perfis
    }

    public void DropdownTopicos(int indice)
    {
        switch (indice)
        {
            case 0: topico = "Nenhum"; Debug.Log(topico); break;
            case 1: topico = "Gramática"; Debug.Log(topico); break;
            case 2: topico = "Lógica"; Debug.Log(topico); break;
            case 3: topico = "Ciências"; Debug.Log(topico); break;
        }
    }

}
