using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    internal class TelaVerificarCodigo : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputCodigo;
        [SerializeField] private Button btnEnviar;
        [SerializeField] private TelaGerenciador telaGerenciador;
        private string codigo;

        private void Awake()
        {
            btnEnviar.onClick.AddListener(FinalizarCadastro);
        }

        private async void FinalizarCadastro()
        {
            if (string.IsNullOrEmpty(codigo))
            {
                Debug.LogError("Insira o código de 6 dígitos fornecido por e-mail.");
                return;
            }
            if (!Regex.IsMatch(codigo, @"^\d{6}$"))
            {
                Debug.LogError("Código inválido.");
                return;
            }

            var responsavel = new Responsavel(); // aqui tem que passar os dados da tela de cadastro, criei essa instância só pra saber os atributos

            if (codigo is not null && codigo.Length > 0)
            {
                responsavel.Codigo = int.Parse(codigo);
            }

            var response = await responsavel.CadastroAsync();

            telaGerenciador.MostrarTela("Login");
        }
        
    }
}
