using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TelaLogin : MonoBehaviour
{
    [SerializeField] private Button btnEntrar;
    [SerializeField] private Button btnEsqueceuSenha;
    [SerializeField] private Button btnCadastro;
    [SerializeField] private TelaGerenciador telaGerenc; //Referência ao TelaManager

    private void Awake()
    {
        btnEntrar.onClick.AddListener(Entrar);
        btnEsqueceuSenha.onClick.AddListener(EsqueceuSenha);
        btnCadastro.onClick.AddListener(Cadastro);
    }

    private void Entrar()
    {
        Debug.Log("Botão Entrar clicado!");
    }
    private void EsqueceuSenha()
    {
        Debug.Log("Botão Esqueceu senha clicado!");
    }
    private void Cadastro()
    {
        //Debug.Log("Botão Cadastro clicado!");
        //SceneManager.LoadScene("Cadastro", LoadSceneMode.Single); // Usado anteriormente para carregar a cena de cadastro
        // Agora, em vez de carregar uma nova cena, vamos mostrar a tela de cadastro
        telaGerenc.MostrarCadastro();
        
    }
}
