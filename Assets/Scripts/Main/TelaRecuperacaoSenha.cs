using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Responsavel;

namespace Assets.Scripts
{
    internal class TelaRecuperacaoSenha : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputCodigo;
        [SerializeField] private TMP_InputField inputSenha;
        [SerializeField] private Button btnEnviarSenha;
        [SerializeField] private Button btnVoltar;
        [SerializeField] private TelaGerenciador telaGerenciador;
        private string Codigo { get; set; }
        private string NovaSenha { get; set; }

        private void Awake()
        {
            btnEnviarSenha.onClick.AddListener(EnviarSenha);
            btnVoltar.onClick.AddListener(VoltarLogin);
        }

        private async void EnviarSenha()
        {
            Codigo = inputCodigo.text;
            NovaSenha = inputSenha.text;

            if (string.IsNullOrEmpty(Codigo))
            {
                Debug.LogError("Insira o código de 6 dígitos fornecido por e-mail.");
                return;
            }
            if (!Regex.IsMatch(Codigo, @"^\d{6}$"))
            {
                Debug.LogError("Código inválido.");
                return;
            }
            if (!Regex.IsMatch(NovaSenha, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{5,}$"))
            {
                Debug.LogError("Digite uma senha válida!");
                return;
            }

            string caminhoDoArquivo = Path.Combine(Application.persistentDataPath, "DadosUsuarioLogin.json");

            if (!File.Exists(caminhoDoArquivo))
            {
                Debug.LogWarning("Arquivo 'DadosUsuario.json' não encontrado.");
                return;
            }
            
            string jsonLido = File.ReadAllText(caminhoDoArquivo);
            var User = JsonConvert.DeserializeObject<Responsavel>(jsonLido);
            File.Delete(caminhoDoArquivo);
            

            if (Codigo is not null && Codigo.Length > 0)
            {
                User.Codigo = int.Parse(Codigo);
            }

            await User.RecuperarSenhaAsync();
            telaGerenciador.MostrarTela("Login");
        }

        private void VoltarLogin()
        {
            telaGerenciador.MostrarTela("Login");
        }
        
    }
}
