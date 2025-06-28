using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TelaPerfilSelecionado : MonoBehaviour
{
    [SerializeField] private Button btnBack;
    [SerializeField] private Button btnSettings;
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
        btnAtividades.onClick.AddListener(Atividades);
    }

    private void OnEnable()
    {
        CarregarCriancaSelecionada(); // Chamada ao método para carregar e exibir os detalhes da crianca
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
        GameManager.Instance.CarregarConfiguracao();
        telaGerenciador.MostrarTela("Perfis");
    }

    private void Atividades()
    {
        SceneManager.LoadScene("Atividades");
    }

    // Metodo para carregar e exibir os dados da crianca selecionada
    private void CarregarCriancaSelecionada()
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
                string data = currentChildBeingEdited.childData;
                string ano = data.Substring(0, 4);
                string mes = data.Substring(5, 2);
                string dia = data.Substring(8, 2);
                data = $"{dia}/{mes}/{ano}";
                childDataText.text = data;
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
            Debug.LogWarning("Tela Perfil Selecionado: Nenhuma criança selecionada no GameManager para exibir. Voltando para a tela de perfis.");
            Perfis(); // Volta para a tela de perfis se nao houver crianca selecionada
        }
    }
}