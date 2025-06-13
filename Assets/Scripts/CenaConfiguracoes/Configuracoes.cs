using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Configuracoes : MonoBehaviour
{
    [SerializeField] private Button btnBack;

    private void Awake()
    {
        btnBack.onClick.AddListener(Back);
    }

    private void Back()
    {
        SceneManager.UnloadSceneAsync("Configuracoes");
    }

}
