using UnityEngine;

public class GerenciadorTelaLogin : MonoBehaviour
{
    [SerializeField] private GameObject telaLogin;
    [SerializeField] private GameObject telaCadastro;
    [SerializeField] private GameObject telaVerificacao;
    [SerializeField] private GameObject telaRecuperacaoEmail;
    [SerializeField] private GameObject telaRecuperacaoSenha;

    public void MostrarTela(string telaNome)
    {
        // Compara a string com argumento passado, onde true ativa, onde false desativa 
        telaLogin.SetActive(telaNome == "Login");
        telaCadastro.SetActive(telaNome == "Cadastro");
        //telaVerificacao.SetActive(telaNome == "Verificacao");
        //telaRecuperacaoEmail.SetActive(telaNome == "RecuperacaoEmail");
        //telaRecuperacaoSenha.SetActive(telaNome == "RecuperacaoSenha");
    }
}
