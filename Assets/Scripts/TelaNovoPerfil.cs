using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO; // Para manipulação de arquivos
using System.Collections.Generic; // Para usar List

public class TelaNovoPerfil : MonoBehaviour
{
    public Button btnAvatar;
    [SerializeField] private TMP_InputField inputNome;
    [SerializeField] private TMP_InputField inputData;
    [SerializeField] private Button btnCadastrar;
    [SerializeField] private Button btnCancelar;
    [SerializeField] private TelaGerenciador telaGerenciador; // Referência ao script TelaGerenciador
    private Image avatarImageBTN; // Componente Image do botão de escolha de avatar
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
    }

    private void Start()
    {
        // Garante que o componente Image exista no botão de escolha de avatar
        avatarImageBTN = btnAvatar.GetComponent<Image>();
    }

    // Método para receber e exibir a imagem de avatar selecionada
    public void AtualizarAvatarSelecionado(Sprite avatar, string avatarResourcePath)
    {
        if (avatarImageBTN != null)
        {
            avatarImageBTN.sprite = avatar;
            avatarImageBTN.preserveAspect = true; // Manter a proporção (círculo)
            selectedAvatarPath = avatarResourcePath;
        }
    }

    private void MostrarTelaAvatares()
    {
        telaGerenciador.MostrarTela("Avatares"); // Desativa todas telas e ativa tela de Avatares
    }

    private void Cadastrar()
    {
        // Pega o texto dos InputFields
        nomeCrianca = inputNome.text;
        dataCrianca = inputData.text;
        

        if (string.IsNullOrEmpty(nomeCrianca) || string.IsNullOrEmpty(dataCrianca))
        {
            Debug.LogError("Todos os campos devem ser preenchidos!");
            return;
        }

        // 1. Define o caminho do arquivo JSON do responsável
        string caminhoDoArquivoUser = Path.Combine(Application.persistentDataPath, "DadosUsuario.json");

        // 2. Verifica se o arquivo JSON do responsável existe
        if (!File.Exists(caminhoDoArquivoUser))
        {
            Debug.LogError("Erro: O arquivo de dados do responsável não foi encontrado. Cadastre-se primeiro.");
            return;
        }

        try
        {
            // 3. Lê o conteúdo do arquivo JSON do responsável
            string jsonResponsavel = File.ReadAllText(caminhoDoArquivoUser);
            // 4. Desserializa o JSON para um objeto UserModel
            UserModel currentUser = JsonUtility.FromJson<UserModel>(jsonResponsavel);

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
            // 6. Adiciona o novo perfil da criança à lista de crianças do responsável
            currentUser.AddChildProfile(novaCrianca);

            // 7. Serializa o objeto UserModel atualizado de volta para JSON
            string updatedJsonResponsavel = JsonUtility.ToJson(currentUser, true);
            // 8. Salva o JSON atualizado, sobrescrevendo o arquivo existente
            File.WriteAllText(caminhoDoArquivoUser, updatedJsonResponsavel);

            Debug.Log($"Perfil da criança '{nomeCrianca}' salvo com sucesso!");
            Debug.Log($"Dados atualizados do responsável: {updatedJsonResponsavel}");

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
