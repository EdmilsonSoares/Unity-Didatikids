using UnityEngine;
using UnityEngine.UI;

public class ExitConfirm : MonoBehaviour
{
    [SerializeField] private Button btnCancelar;
    [SerializeField] private Button btnExit;
    [SerializeField] private SwitchTela switchTela;

    void Awake()
    {
        btnCancelar.onClick.AddListener(Cancelar);
        btnExit.onClick.AddListener(SairDoAplicativo);
    }
    private void Cancelar()
    {
        switchTela.ExibirAviso("");
    }

    private void SairDoAplicativo()
    {
        Debug.LogWarning("Saindo do aplicativo...");
        Application.Quit();
    }
}