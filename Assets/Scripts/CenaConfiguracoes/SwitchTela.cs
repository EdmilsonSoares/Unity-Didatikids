using UnityEngine;

public class SwitchTela : MonoBehaviour
{
    [SerializeField] private GameObject telaSettings;
    [SerializeField] private GameObject telaChildrens;
    [SerializeField] private GameObject telaEditarPerfil;
    [Header("Referências aos avisos")]
    [SerializeField] private GameObject ConfirmExit;


    public void Mostrar(string telaNome)
    {
        telaSettings.SetActive(telaNome == "Settings");
        telaChildrens.SetActive(telaNome == "Childrens");
        telaEditarPerfil.SetActive(telaNome == "EditarPerfil");

    }

    public void ExibirAviso(string aviso)
    {
        ConfirmExit.SetActive(aviso == "ConfirmExit");
    }

}
