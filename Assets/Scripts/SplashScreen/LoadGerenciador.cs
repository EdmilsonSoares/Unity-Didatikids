// Singleton para receber o nome da próxima cena a ser carregada, carregar a cena de loading
// e depois a próxima cena. O script AnimOfLoading irá carregar a cena desejada
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGerenciador : MonoBehaviour
{
    public static LoadGerenciador Instance { get; private set; }
    public string NomeProxCena { get; set; }

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

    // Enquanto a cena é carregada é mostrado a animação de loading
    public void Carregar(string sceneName)
    {
        NomeProxCena = sceneName;
        SceneManager.LoadScene("Loading");
    }
    // Use para carregamentos muito rapidos não passa pela animação de loading
    public void CarregamentoRapido(string sceneName)
    {
        NomeProxCena = sceneName;
        SceneManager.LoadScene(NomeProxCena);
    }
}
