
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class TelaPerfis : MonoBehaviour
{
    [Header("Botões")]
    [SerializeField] private Button btnSettings;
    [SerializeField] private Button btnChild1;
    [SerializeField] private Button btnChild2;
    [SerializeField] private Button btnChild3;
    [SerializeField] private Button btnAdicionarPerfil;
    [Header("Campos de texto")]
    [SerializeField] private TMP_Text childNameText1;
    [SerializeField] private TMP_Text childNameText2;
    [SerializeField] private TMP_Text childNameText3;
    [SerializeField] private TelaGerenciador telaGerenciador; // Referência ao script TelaGerenciador
    [SerializeField] private Image imageMax;
    private ChildModel currentChild1;
    private ChildModel currentChild2;
    private ChildModel currentChild3;

    private void Awake()
    {
        btnSettings.onClick.AddListener(Settings);
        btnAdicionarPerfil.onClick.AddListener(NovoPerfil);
        btnChild1.onClick.AddListener(() => SelecionarCrianca(currentChild1));
        btnChild2.onClick.AddListener(() => SelecionarCrianca(currentChild2));
        btnChild3.onClick.AddListener(() => SelecionarCrianca(currentChild3));
    }

    private void OnEnable()
    {
        LoadAndDisplayChildProfiles();
    }

    private void LoadAndDisplayChildProfiles()
    {
        btnChild1.gameObject.SetActive(false);
        btnChild2.gameObject.SetActive(false);
        btnChild3.gameObject.SetActive(false);
        currentChild1 = null;
        currentChild2 = null;
        currentChild3 = null;

        List<ChildModel> children = GameManager.Instance.ChildProfiles;
        // 3. Itera sobre a lista de crianças e exibe até 3
        if (children.Count == 0)
        {
            Debug.Log("O responsável logado não possui perfis de crianças cadastrados.");
            SetVisibilityBotaoAdicionarPerfil(0);
            return;
        }
        // Exibe os perfis das crianças existentes
        for (int i = 0; i < children.Count && i < 3; i++)
        {
            ChildModel child = children[i];
            switch (i)
            {
                case 0:
                    SetChildPerfil(btnChild1, childNameText1, child);
                    currentChild1 = child;
                    break;
                case 1:
                    SetChildPerfil(btnChild2, childNameText2, child);
                    currentChild2 = child;
                    break;
                case 2:
                    SetChildPerfil(btnChild3, childNameText3, child);
                    currentChild3 = child;
                    break;
            }
        }
        SetVisibilityBotaoAdicionarPerfil(children.Count);
    }

    private void SetChildPerfil(Button btnChild, TMP_Text childNameText, ChildModel child)
    {
        childNameText.text = child.childNome;
        btnChild.gameObject.SetActive(true);

        if (!string.IsNullOrEmpty(child.avatarIconPath))
        {
            Sprite avatarSprite = Resources.Load<Sprite>(child.avatarIconPath);
            if (avatarSprite != null)
            {
                btnChild.GetComponentInChildren<Image>().sprite = avatarSprite;
            }
        }
        else
        {
            Debug.LogWarning($"Caminho do avatar vazio para a criança: {child.childNome}");
        }
    }

    // Método para lidar com o clique nos botões de criança
    private void SelecionarCrianca(ChildModel selectedChild)
    {
        if (selectedChild == null)
        {
            Debug.LogError("Tentativa de selecionar uma criança nula.");
            return;
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetCurrentSelectedChild(selectedChild); // Salva a criança selecionada no GameManager
            telaGerenciador.MostrarTela("PerfilSelecionado"); // Vai para a tela de detalhes da criança
        }
        else
        {
            Debug.LogError("GameManager.Instance não encontrado ao tentar selecionar criança.");
        }
    }

    private void NovoPerfil()
    {
        telaGerenciador.MostrarTela("NovoPerfil");
    }

    private void Settings()
    {
        GameManager.Instance.CarregarConfiguracao();
    }

    private void SetVisibilityBotaoAdicionarPerfil(int currentChildCount)
    {
        if (btnAdicionarPerfil != null)
        {
            btnAdicionarPerfil.gameObject.SetActive(currentChildCount < 3);
            imageMax.gameObject.SetActive(currentChildCount == 3);
            if (currentChildCount >= 3)
            {
                Debug.Log("Botão 'Adicionar Perfil' desativado, limite de crianças atingido.");
            }
        }
    }

}
