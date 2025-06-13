using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Configuracoes : MonoBehaviour
{
    [SerializeField] private Button btnBack;
    [SerializeField] private Button btnConta;
    [SerializeField] private Button btnPerfis;
    [SerializeField] private Button btnAudio;
    [SerializeField] private Button btnLogout;
    [SerializeField] private Button btnClose;

    private void Awake()
    {
        btnBack.onClick.AddListener(Back);
    }

    private void Back()
    {
        SceneManager.UnloadSceneAsync("Configuracoes");
    }

    

}
