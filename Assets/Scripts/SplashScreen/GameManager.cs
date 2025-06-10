// Singleton para gerir algumas cenas e carregar consigo dados importantes para as atividades
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public string NomeProxCena { get; set; }
    public List<ChildModel> ChildProfiles { get; private set; } = new List<ChildModel>();
    public ChildModel CurrentSelectedChild { get; private set; } // Armazenar a criança atualmente selecionada/ativa

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Método para definir a lista de crianças após um login bem-sucedido
    public void SetChildProfiles(List<ChildModel> profiles)
    {
        // Limpa a lista atual e adiciona todos os perfis recebidos
        ChildProfiles.Clear();
        if (profiles != null)
        {
            ChildProfiles.AddRange(profiles);
        }
        Debug.Log($"GameManager: {ChildProfiles.Count} perfis de crianças carregados.");
    }

    // Método para adicionar um novo perfil de criança ao usuário atual no GameManager
    public void AddChildProfile(ChildModel child)
    {
        if (ChildProfiles == null)
        {
            ChildProfiles = new List<ChildModel>(); // Garante que a lista exista
        }
        ChildProfiles.Add(child);
        Debug.Log($"GameManager: Criança '{child.childNome}' adicionada à lista em memória.");
    }

    // Método para definir a criança que foi selecionada na TelaPerfis
    public void SetCurrentSelectedChild(ChildModel child)
    {
        CurrentSelectedChild = child;
        if (child != null)
        {
            Debug.Log($"GameManager: Criança selecionada: {child.childNome}");
        }
        else
        {
            Debug.Log("GameManager: Nenhuma criança selecionada (ou seleção removida).");
        }
    }

    // Enquanto a cena é carregada é mostrado a animação de loading
    public void CarregarComAnimacao(string sceneName)
    {
        NomeProxCena = sceneName;
        SceneManager.LoadScene("Loading");
    }
    // Use para carregamentos rapidos que não passam pela animação de loading
    public void Carregar(string sceneName)
    {
        NomeProxCena = sceneName;
        SceneManager.LoadScene(NomeProxCena);
    }
}
