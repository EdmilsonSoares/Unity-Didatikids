using UnityEngine;

public class TelaGerenciador : MonoBehaviour
{
    [SerializeField] private GameObject telaLogin;
    [SerializeField] private GameObject telaCadastro;

    public void MostrarLogin()
    {
        if (telaLogin != null)
        {
            telaLogin.SetActive(true);
        }
        if (telaCadastro != null)
        {
            telaCadastro.SetActive(false);
        }
    }

    public void MostrarCadastro()
    {
        if (telaLogin != null)
        {
            telaLogin.SetActive(false);
        }
        if (telaCadastro != null)
        {
            telaCadastro.SetActive(true);
        }
    }

    // Adicione outros métodos para gerenciar outras telas do seu aplicativo
}
