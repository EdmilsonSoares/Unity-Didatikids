using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TelaNovoPerfil : MonoBehaviour
{
    public Button btnAvatar;
    [SerializeField] private TMP_InputField inputNome;
    [SerializeField] private TMP_InputField inputData;
    [SerializeField] private Button btnCadastrar;
    [SerializeField] private Button btnCancelar;
    [SerializeField] private TelaGerenciador telaGerenciador; // Referência ao script TelaGerenciador
    private Image avatarImageBTN; // Componente Image do botão de escolha de avatar

    private void Awake()
    {
        btnAvatar.onClick.AddListener(MostrarTelaAvatares);
        btnCadastrar.onClick.AddListener(Cadastrar);
        btnCancelar.onClick.AddListener(Cancelar);
    }

    private void Start()
    {
        // Garante que o componente Image exista no botão de escolha de avatar
        avatarImageBTN = btnAvatar.GetComponent<Image>();
    }

    // Método para receber e exibir a imagem de avatar selecionada
    public void AtualizarAvatarSelecionado(Sprite avatar)
    {
        if (avatarImageBTN != null)
        {
            avatarImageBTN.sprite = avatar;
            avatarImageBTN.preserveAspect = true; // Manter a proporção (círculo)
        }
    }

    void MostrarTelaAvatares()
    {
        telaGerenciador.MostrarAvatares(); // Desativa todas telas e ativa tela de Avatares
    }

    private void Cadastrar()
    {
        // Pega o texto dos InputFields
        string nome = inputNome.text;
        string data = inputData.text;

        if (nome == "" || data == "")
        {
            Debug.LogError("Todos os campos devem ser preenchidos!");
            return;
        }
        Debug.Log("Nome: " + nome + ", Data: " + data);
        telaGerenciador.MostrarPerfis(); // Desativa todas telas e ativa tela de perfis
    }

    private void Cancelar()
    {
        telaGerenciador.MostrarPerfis(); // Desativa todas telas e ativa tela de perfis
    }


}
