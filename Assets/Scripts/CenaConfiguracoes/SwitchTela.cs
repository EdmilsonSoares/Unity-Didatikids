using UnityEngine;

public class SwitchTela : MonoBehaviour
{
    [SerializeField] private GameObject telaSettings;
    [SerializeField] private GameObject telaChildrens;
    [SerializeField] private GameObject telaEditarPerfil;
    [Header("Referências aos avisos")]
    [SerializeField] private GameObject ExitConfirm;
    [SerializeField] private GameObject DeleteConfirm;
    [SerializeField] private GameObject SenhaConfirm;
    
    public void Mostrar(string telaNome)
    {
        telaSettings.SetActive(telaNome == "Settings");
        telaChildrens.SetActive(telaNome == "Childrens");
        telaEditarPerfil.SetActive(telaNome == "EditarPerfil");
    }

    public void ExibirAviso(string aviso)
    {
        ExitConfirm.SetActive(aviso == "ExitConfirm");
        DeleteConfirm.SetActive(aviso == "DeleteConfirm");
        SenhaConfirm.SetActive(aviso == "SenhaConfirm");
    }

}
