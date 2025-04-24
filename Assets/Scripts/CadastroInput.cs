using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CadastroInputs : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputNome;
    [SerializeField] private TMP_InputField inputData;
    [SerializeField] private TMP_InputField inputEmail;
    [SerializeField] private TMP_InputField inputSenha;
    [SerializeField] private Button btnEnviar;

    void Start()
    {
        // Adiciona um Listener para o evento de clique do botão
        btnEnviar.onClick.AddListener(AoPressionarEnviar);
    }

    void AoPressionarEnviar()
    {
        // Pega o texto dos InputFields
        string nome = inputNome.text;
        string data = inputData.text;
        string email = inputEmail.text;
        string senha = inputSenha.text;

        if(nome == "" || data == "" || email == "" || senha == "")
        {
            Debug.LogError("Todos os campos devem ser preenchidos!");
            return;
        }
        Debug.Log("Nome: " + nome + ", Data: " + data + ", Email: " + email + ", Senha: " + senha);
    }
}