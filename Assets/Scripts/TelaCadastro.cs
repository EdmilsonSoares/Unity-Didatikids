using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TelaCadastro : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputNome;
    [SerializeField] private TMP_InputField inputData;
    [SerializeField] private TMP_InputField inputEmail;
    [SerializeField] private TMP_InputField inputSenha;
    [SerializeField] private Button btnEnviar;
    [SerializeField] private Button btnPossuoConta;
    [SerializeField] private TelaGerenciador telaGerenc; //Referência ao script TelaGerenciador

    private void Awake()
    {
        // Adiciona um Listener para o evento de clique do botão
        btnEnviar.onClick.AddListener(AoPressionarEnviar);
        btnPossuoConta.onClick.AddListener(PossuoConta);
    }

    void AoPressionarEnviar()
    {
        // Pega o texto dos InputFields
        string nome = inputNome.text;
        string data = inputData.text;
        string email = inputEmail.text;
        string senha = inputSenha.text;

        if (nome == "" || data == "" || email == "" || senha == "")
        {
            Debug.LogError("Todos os campos devem ser preenchidos!");
            return;
        }
        Debug.Log("Nome: " + nome + ", Data: " + data + ", Email: " + email + ", Senha: " + senha);
    }

    private void PossuoConta()
    {
        telaGerenc.MostrarLogin(); // Desativa tela cadastro e ativa tela login
    }
}
