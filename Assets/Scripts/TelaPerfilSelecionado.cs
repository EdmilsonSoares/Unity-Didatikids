using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TelaPerfilSelecionado : MonoBehaviour
{
    [SerializeField] private Button btnBack;
    [SerializeField] private Button btnSettings;
    [SerializeField] private TelaGerenciador telaGerenciador;

    private void Awake()
    {
        btnBack.onClick.AddListener(Perfis);
    }

    public void Perfis()
    {
        telaGerenciador.MostrarTela("Perfis");
    }


}
