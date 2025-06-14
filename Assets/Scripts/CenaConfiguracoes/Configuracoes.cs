using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Configuracoes : MonoBehaviour
{
    [SerializeField] private Button btnBack;
    [SerializeField] private Button btnConta;
    [SerializeField] private Button btnChild;
    [SerializeField] private Button btnTela;
    [SerializeField] private Button btnAudio;
    [SerializeField] private Button btnLogout;
    [SerializeField] private Button btnClose;
    [SerializeField] private SwitchTela switchTela;

    private void Awake()
    {
        btnBack.onClick.AddListener(Back);
        btnChild.onClick.AddListener(Childrens);
        btnClose.onClick.AddListener(Sair);
    }

    private void Back()
    {
        SceneManager.UnloadSceneAsync("Configuracoes");
    }

    private void Childrens()
    {
        switchTela.Mostrar("Childrens");
    }

    private void Sair()
    {
        switchTela.ExibirAviso("ConfirmExit");
    }

}
