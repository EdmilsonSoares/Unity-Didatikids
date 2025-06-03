using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TelaAtividades : MonoBehaviour
{
    [SerializeField] private Button btnAdicionarPerfil;
    [SerializeField] private Button btnChild1;
    [SerializeField] private Button btnChild2;
    [SerializeField] private Button btnChild3;
    [SerializeField] private TelaGerenciador telaGerenciador; // Referência ao script TelaGerenciador

    private void Awake()
    {
        btnAdicionarPerfil.onClick.AddListener(Perfis);
        btnChild1.onClick.AddListener(Atividades);
        btnChild2.onClick.AddListener(Atividades);
        btnChild3.onClick.AddListener(Atividades);
    }

    public void Perfis()
    { 
        telaGerenciador.MostrarTela("Perfis"); 
    }
    

}
