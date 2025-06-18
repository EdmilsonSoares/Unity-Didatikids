using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    internal class TelaVerificacao : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputCodigo;
        [SerializeField] private Button btnValidar;
        [SerializeField] private Button btnReenviar;
        [SerializeField] private TelaGerenciador telaGerenciador;
        private string Codigo { get; set; }

        private void Awake()
        {
            btnValidar.onClick.AddListener(FinalizarCadastro);
            btnReenviar.onClick.AddListener(ReenviarCodigo);
        }

        private async void FinalizarCadastro()
        {
            Codigo = inputCodigo.text;

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

            string path = Path.Combine(Application.persistentDataPath, "DadosUsuario.json");
            string json = File.ReadAllText(path);
            Responsavel responsavel = JsonConvert.DeserializeObject<Responsavel>(json);


            if (Codigo is not null && Codigo.Length > 0)
            {
                responsavel.Codigo = int.Parse(Codigo);
            }

            var response = await responsavel.CadastroAsync();

            if (response == System.Net.HttpStatusCode.OK)
            {
                telaGerenciador.MostrarTela("Login");
            }
            else
            {
                Debug.LogError("Código inválido.");
            }
        }

        private async void ReenviarCodigo()
        {
            string path = Path.Combine(Application.persistentDataPath, "DadosUsuario.json");
            string json = File.ReadAllText(path);
            Responsavel responsavel = JsonConvert.DeserializeObject<Responsavel>(json);
            await responsavel.EnviarCodigoAsync();
        }
        
    }
}
