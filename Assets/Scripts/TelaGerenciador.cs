using UnityEngine;

public class TelaGerenciador : MonoBehaviour
{
    [SerializeField] private GameObject telaLogin;
    [SerializeField] private GameObject telaCadastro;
    [SerializeField] private GameObject telaPerfis;
    [SerializeField] private GameObject telaNovoPerfil;
    [SerializeField] private GameObject telaAvatares;

    public void MostrarTela(string telaNome)
    {
        // Compara a string com argumento passado, onde true ativa, onde false desativa 
        telaLogin.SetActive(telaNome == "Login");
        telaCadastro.SetActive(telaNome == "Cadastro");
        telaPerfis.SetActive(telaNome == "Perfis");
        telaNovoPerfil.SetActive(telaNome == "NovoPerfil");
        telaAvatares.SetActive(telaNome == "Avatares");
    }
}
