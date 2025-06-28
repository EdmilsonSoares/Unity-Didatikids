using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TelaAtividades : MonoBehaviour
{
    [SerializeField] private Button btnBack;
    [SerializeField] private Button btnSettings;
    [SerializeField] private Button btnAtividadeLink;
    [SerializeField] private Button btnFound;
    [SerializeField] private Button btnAtividadeTermo;

    private void Awake()
    {
        btnAtividadeLink.onClick.AddListener(ActivityLink);
        btnAtividadeTermo.onClick.AddListener(ActivityTermo);
        btnFound.onClick.AddListener(ActivityFound);
        btnBack.onClick.AddListener(Perfis);
        btnSettings.onClick.AddListener(Settings);
    }
    private void Perfis()
    {
        SceneManager.LoadScene("Main"); 
    }
    private void Settings()
    {
        SceneManager.LoadSceneAsync("Configuracoes", LoadSceneMode.Additive);
    }
    public void ActivityLink()
    {
        SceneManager.LoadScene("Link");
    }
    public void ActivityFound()
    {
        SceneManager.LoadScene("Found");
    }
    public void ActivityTermo()
    {
        SceneManager.LoadScene("Termo");
    }
}