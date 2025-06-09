using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;  // Para manipulação de arquivos
using System.Collections.Generic;

public class TelaPerfis : MonoBehaviour
{
    [Header("Botões")]
    [SerializeField] private Button btnChild1;
    [SerializeField] private Button btnChild2;
    [SerializeField] private Button btnChild3;
    [SerializeField] private Button btnAdicionarPerfil;
    [Header("Campos de texto")]
    [SerializeField] private TMP_Text childNameText1;
    [SerializeField] private TMP_Text childNameText2;
    [SerializeField] private TMP_Text childNameText3;
    [SerializeField] private TelaGerenciador telaGerenciador; // Referência ao script TelaGerenciador

    private void Awake()
    {
        btnAdicionarPerfil.onClick.AddListener(NovoPerfil);
        btnChild1.onClick.AddListener(Atividades);
        btnChild2.onClick.AddListener(Atividades);
        btnChild3.onClick.AddListener(Atividades);
    }

    private void OnEnable()
    {
        LoadAndDisplayChildProfiles();
    }

    private void LoadAndDisplayChildProfiles()
    {
        string caminhoDoArquivoUser = Path.Combine(Application.persistentDataPath, "DadosUsuario.json");

        btnChild1.gameObject.SetActive(false);
        btnChild2.gameObject.SetActive(false);
        btnChild3.gameObject.SetActive(false);

        if (!File.Exists(caminhoDoArquivoUser))
        {
            Debug.LogWarning("Arquivo de dados do responsável não encontrado. Nenhum perfil de criança para exibir.");
            return;
        }

        try
        {
            string jsonResponsavel = File.ReadAllText(caminhoDoArquivoUser); // Lê o conteúdo do arquivo JSON
            UserModel user = JsonUtility.FromJson<UserModel>(jsonResponsavel); // Desserializa o JSON para um objeto UserModel
            // Verifica se há perfis de crianças e os exibe
            if (user != null && user.children != null)
            {
                // Itera sobre a lista de crianças e exibe até 3
                for (int i = 0; i < user.children.Count && i < 3; i++)
                {
                    ChildModel child = user.children[i];
                    // Usa um switch ou if-else para atribuir ao TMP_Text e ativar o GameObject correto
                    switch (i)
                    {
                        case 0:
                            SetChildPerfil(btnChild1, childNameText1, child);
                            break;
                        case 1:
                            SetChildPerfil(btnChild2, childNameText2, child);
                            break;
                        case 2:
                            SetChildPerfil(btnChild3, childNameText3, child);
                            break;
                    }
                }
            }
            else
            {
                Debug.Log("O responsável não possui perfis de crianças cadastrados.");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Erro ao carregar ou exibir perfis de crianças: {e.Message}");
        }
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
    }

    private void NovoPerfil()
    {
        telaGerenciador.MostrarTela("NovoPerfil");
    }

    public void Atividades()
    {
        telaGerenciador.MostrarTela("Atividades");
    }

}
