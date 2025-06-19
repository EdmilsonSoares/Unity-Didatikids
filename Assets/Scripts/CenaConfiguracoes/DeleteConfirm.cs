using UnityEngine;
using UnityEngine.UI;

public class DeleteConfirm : MonoBehaviour
{
    [SerializeField] private Button btnCancelar;
    [SerializeField] private Button btnSim;
    [SerializeField] private SwitchTela switchTela;

    private void Awake()
    {
        btnCancelar.onClick.AddListener(DesativarAviso);
        btnSim.onClick.AddListener(AtivarSenhaConfirm);
    }

    private void DesativarAviso()
    {
        switchTela.ExibirAviso("");
    }

    private void AtivarSenhaConfirm()
    {
        switchTela.ExibirAviso("SenhaConfirm");
    }
    
}
