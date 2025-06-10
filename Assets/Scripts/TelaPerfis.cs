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
        btnChild1.onClick.AddListener(PerfilSelecionado);
        btnChild2.onClick.AddListener(PerfilSelecionado);
        btnChild3.onClick.AddListener(PerfilSelecionado);
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

        List<ChildModel> children = GameManager.Instance.ChildProfiles;
        // 3. Itera sobre a lista de crianças e exibe até 3
        if (children.Count == 0)
        {
            Debug.Log("O responsável logado não possui perfis de crianças cadastrados.");
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

    private void NovoPerfil()
    {
        telaGerenciador.MostrarTela("NovoPerfil");
    }

    public void PerfilSelecionado()
    {
        telaGerenciador.MostrarTela("PerfilSelecionado");
    }

}
