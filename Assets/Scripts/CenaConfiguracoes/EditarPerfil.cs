using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO; // Necessário para manipulacao de arquivos

public class EditarPerfil : MonoBehaviour
{

    [SerializeField] private SwitchTela switchTela;
    [SerializeField] private Button btnBack;
    [SerializeField] private Button btnEditar;
    [SerializeField] private Button btnExcluir;

    [Header("Elementos da Criança Selecionada")]
    [SerializeField] private Image childAvatarImage;
    [SerializeField] private TMP_Text childNameText;
    [SerializeField] private TMP_Text childDataText;
    [SerializeField] private TMP_Text childTopicoText;

    private ChildModel currentChildBeingEdited;

    private void OnEnable()
    {
        LoadSelectedChildProfile();
    }

    private void Awake()
    {
        btnBack.onClick.AddListener(Back);
        btnEditar.onClick.AddListener(HabilitarAvisoEditar);
        btnExcluir.onClick.AddListener(HabilitarAvisoDeletar);
    }

    private void Back()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetCurrentSelectedChild(null); // limpar a criança selecionada no GameManager
        }
        switchTela.Mostrar("Childrens");
    }

    // Carregar a criança na tela
    private void LoadSelectedChildProfile()
    {
        if (GameManager.Instance != null && GameManager.Instance.CurrentSelectedChild != null)
        {
            currentChildBeingEdited = GameManager.Instance.CurrentSelectedChild; // Atribui a crianca selecionada
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
            Debug.LogError("Erro: Nenhuma criança selecionada no GameManager para exibir.");
            Back(); // Volta para a tela anterior se nao houver crianca selecionada
        }
    }

    private void HabilitarAvisoDeletar()
    {
        switchTela.ExibirAviso("DeleteConfirm"); // Lógica de deletar na tela de confirmar senha
    }

    private void HabilitarAvisoEditar()
    {
        Debug.Log("Botão Editar pressionado");
    }
}
