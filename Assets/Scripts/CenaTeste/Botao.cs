using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Botao : MonoBehaviour
{
    [SerializeField] private Button botao;

    void Start()
    {
        botao.onClick.AddListener(VoltarMenu);
    }

    void VoltarMenu()
    {
        SceneManager.LoadScene("Main");
    }
}