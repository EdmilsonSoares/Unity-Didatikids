using UnityEngine;

public class TelaGerenciador : MonoBehaviour
{
    [SerializeField] private GameObject telaLogin;
    [SerializeField] private GameObject telaCadastro;
    [SerializeField] private GameObject telaPerfis;
    [SerializeField] private GameObject telaNovoPerfil;
    [SerializeField] private GameObject telaAvatares;

    public void MostrarLogin()
    {
        telaLogin.SetActive(true);
        telaCadastro.SetActive(false);
        telaPerfis.SetActive(false);
        telaNovoPerfil.SetActive(false);
        telaAvatares.SetActive(false);
    }

    public void MostrarCadastro()
    {
        telaLogin.SetActive(false);
        telaCadastro.SetActive(true);
        telaPerfis.SetActive(false);
        telaNovoPerfil.SetActive(false);
        telaAvatares.SetActive(false);
    }

    public void MostrarPerfis()
    {
        telaLogin.SetActive(false);
        telaCadastro.SetActive(false);
        telaPerfis.SetActive(true);
        telaNovoPerfil.SetActive(false);
        telaAvatares.SetActive(false);
    }

    public void MostrarNovoPerfil()
    {
        telaLogin.SetActive(false);
        telaCadastro.SetActive(false);
        telaPerfis.SetActive(false);
        telaNovoPerfil.SetActive(true);
        telaAvatares.SetActive(false);
    }

    public void MostrarAvatares()
    {
        telaLogin.SetActive(false);
        telaCadastro.SetActive(false);
        telaPerfis.SetActive(false);
        telaNovoPerfil.SetActive(false);
        telaAvatares.SetActive(true);
    }

}
