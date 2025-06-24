using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BotaoOperador : MonoBehaviour
{
    [SerializeField] private Button btnOperador;
    [SerializeField] private TMP_Text textOperador;
    private bool isSoma = true;

    public bool GetOperador()
    {
        return isSoma;
    }

    void Start()
    {
        btnOperador.onClick.AddListener(PermutarOperador);
    }

    private void PermutarOperador()
    {
        isSoma = !isSoma;
        if (textOperador != null)
        {
            textOperador.text = isSoma ? "+" : "-";
            textOperador.text = isSoma ? "+" : "-";
        }
    }    
}
