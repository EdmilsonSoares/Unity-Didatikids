using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TelaCadastro : MonoBehaviour
{
    [SerializeField] private Button btnEnviar;
    [SerializeField] private Button btnPossuoConta;

    private void Awake(){
        btnEnviar.onClick.AddListener(Enviar);
        btnPossuoConta.onClick.AddListener(PossuoConta);
    }

    private void Enviar()
    {
        Debug.Log("Botão Enviar clicado!");
    }
    private void PossuoConta()
    {
        //Debug.Log("Botão Enviar clicado!");
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}
