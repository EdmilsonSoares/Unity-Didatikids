using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Responsavel;

namespace Assets.Scripts
{
    internal class TelaRecuperacaoEmail : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputEmail;
        [SerializeField] private Button btnEnviarCodigo;
        [SerializeField] private Button btnVoltar;
        [SerializeField] private TelaGerenciador telaGerenciador;
        private string Email { get; set; }

        private void Awake()
        {
            btnEnviarCodigo.onClick.AddListener(EnviarCodigo);
            btnVoltar.onClick.AddListener(VoltarLogin);
        }

        private async void EnviarCodigo()
        {
            Email = inputEmail.text;

            if (string.IsNullOrEmpty(Email))
            {
                Debug.Log("Insira o email cadastrado");
                return;
            }
            if (!Regex.IsMatch(Email, @"^[^@]+@[^@]+\.[a-zA-Z]{2,}$"))
            {
                Debug.Log("Email inválido.");
                return;
            }

            var responsavel = new Responsavel()
            {
                Email = Email
            };
            string json = JsonConvert.SerializeObject(responsavel);
            string caminhoDoArquivo = Path.Combine(Application.persistentDataPath, "DadosUsuario.json");

            telaGerenciador.MostrarTela("RecuperacaoSenha");
            await responsavel.EnviarCodigoSenhaAsync();
        }

        private void VoltarLogin()
        {
            telaGerenciador.MostrarTela("Login");
        }
        
    }
}
