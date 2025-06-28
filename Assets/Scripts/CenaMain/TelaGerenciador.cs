using UnityEngine;

public class TelaGerenciador : MonoBehaviour
{
    [SerializeField] private GameObject telaPerfis;
    [SerializeField] private GameObject telaNovoPerfil;
    [SerializeField] private GameObject telaAvatares;
    [SerializeField] private GameObject telaPerfilSelecionado;

    public void MostrarTela(string telaNome)
    {
        // Compara a string com argumento passado, onde true ativa, onde false desativa 
        telaPerfis.SetActive(telaNome == "Perfis");
        telaNovoPerfil.SetActive(telaNome == "NovoPerfil");
        telaAvatares.SetActive(telaNome == "Avatares");
        telaPerfilSelecionado.SetActive(telaNome == "PerfilSelecionado");
    }
}
