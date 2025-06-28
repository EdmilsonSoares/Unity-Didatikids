using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class Childrens : MonoBehaviour
{
    [SerializeField] private SwitchTela switchTela;
    [SerializeField] private Button btnBack;
    [Header("Containers dos Perfis")]
    [SerializeField] private GameObject conteinerChild1;
    [SerializeField] private GameObject conteinerChild2;
    [SerializeField] private GameObject conteinerChild3;
    [Header("Botões de Seleção de Criança")]
    [SerializeField] private Button btnChild1;
    [SerializeField] private Button btnChild2;
    [SerializeField] private Button btnChild3;
    [Header("Imagens dos Avatares")]
    [SerializeField] private GameObject imgChild1;
    [SerializeField] private GameObject imgChild2;
    [SerializeField] private GameObject imgChild3;
    // Modelos das crianças para os botões
    private ChildModel currentChild1;
    private ChildModel currentChild2;
    private ChildModel currentChild3;

    private void Awake()
    {
        btnBack.onClick.AddListener(Back);
        // Adiciona listeners aos botões de seleção de criança
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
        // 1. Desativa todos os containers de criança inicialmente
        conteinerChild1.SetActive(false);
        conteinerChild2.SetActive(false);
        conteinerChild3.SetActive(false);

        // Zera as referências das crianças
        currentChild1 = null;
        currentChild2 = null;
        currentChild3 = null;

        // 2. Tenta carregar a lista de crianças do GameManager
        List<ChildModel> children = new List<ChildModel>();
        if (GameManager.Instance != null && GameManager.Instance.ChildProfiles != null)
        {
            children = GameManager.Instance.ChildProfiles;
        }
        else
        {
            Debug.LogWarning("GameManager.Instance ou ChildProfiles não encontrado/s ou nulo/s. Verifique se o GameManager está carregado e os perfis das crianças foram carregados.");
        }

        // 3. Itera sobre a lista de crianças e exibe até 3 perfis
        if (children.Count == 0)
        {
            Debug.Log("O responsável logado não possui perfis de crianças cadastrados.");
            // Neste cenário, todos os containers já estão desativados, o que é o comportamento desejado.
            return;
        }

        for (int i = 0; i < children.Count && i < 3; i++)
        {
            ChildModel child = children[i];
            switch (i)
            {
                case 0:
                    SetChildProfileDisplay(conteinerChild1, btnChild1, imgChild1, child);
                    currentChild1 = child;
                    break;
                case 1:
                    SetChildProfileDisplay(conteinerChild2, btnChild2, imgChild2, child);
                    currentChild2 = child;
                    break;
                case 2:
                    SetChildProfileDisplay(conteinerChild3, btnChild3, imgChild3, child);
                    currentChild3 = child;
                    break;
            }
        }
    }

    // Método auxiliar para configurar e exibir um perfil de criança
    private void SetChildProfileDisplay(GameObject container, Button botao, GameObject imgGameObject, ChildModel child)
    {
        // Ativa o container do perfil
        container.SetActive(true);

        TMP_Text textoDoBotao = botao.GetComponentInChildren<TMP_Text>();
        if (textoDoBotao != null)
        {
            textoDoBotao.text = child.childNome;
        }

        // Tenta carregar e definir a imagem do avatar
        if (!string.IsNullOrEmpty(child.avatarIconPath))
        {
            Sprite avatarSprite = Resources.Load<Sprite>(child.avatarIconPath);
            if (avatarSprite != null)
            {
                // Pega o componente Image do GameObject imgGameObject e define o sprite
                Image avatarImage = imgGameObject.GetComponent<Image>();
                if (avatarImage != null)
                {
                    avatarImage.sprite = avatarSprite;
                }
                else
                {
                    Debug.LogWarning($"Componente Image não encontrado no GameObject '{imgGameObject.name}' para avatar da criança '{child.childNome}'.");
                }
            }
            else
            {
                Debug.LogWarning($"Avatar sprite não carregado para a criança: {child.childNome} no caminho: {child.avatarIconPath}. Verifique se o asset está na pasta Resources e o caminho está correto.");
            }
        }
        else
        {
            Debug.LogWarning($"Caminho do avatar vazio para a criança: {child.childNome}.");
        }
    }

    // Método para lidar com o clique nos botões de criança
    private void SelecionarCrianca(ChildModel selectedChild)
    {
        if (selectedChild == null)
        {
            Debug.LogWarning("Tentativa de selecionar uma criança nula.");
            return;
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetCurrentSelectedChild(selectedChild); // Salva a criança selecionada no GameManager
            switchTela.Mostrar("EditarPerfil"); // Usa o SwitchTela para navegar
        }
        else
        {
            Debug.LogWarning("GameManager.Instance não encontrado ao tentar selecionar criança.");
        }
    }

    private void Back()
    {
        switchTela.Mostrar("Settings"); // Volta para a tela de configurações ou qualquer outra tela definida
    }
}