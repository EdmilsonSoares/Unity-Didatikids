using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class AjustarTelaParaTeclado : MonoBehaviour
{
    [SerializeField] private List<TMP_InputField> inputFields; // Lista de TMP_InputFields a serem monitorados
    [SerializeField] private RectTransform rectTransform; // O RectTransform que você deseja mover
    [SerializeField] private float verticalMove = 400f; // Ajuste conforme necessário
    [SerializeField] private float velocit = 10f; // velocidade do movimento

    private float originYposi;
    private float alvoYposi;
    private bool estaMovendo = false;
    
    void Start()
    {
        originYposi = rectTransform.anchoredPosition.y;

        // Adiciona listeners para TMP_InputFields
        foreach (var inputField in inputFields)
        {
            if (inputField != null)
            {
                inputField.onSelect.AddListener(AoSelecionarInputField);
                inputField.onDeselect.AddListener(AoDeselecionarInputField);
            }
        }
    }

    void Update()
    {
        if (estaMovendo)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, new Vector2(rectTransform.anchoredPosition.x, alvoYposi), Time.deltaTime * velocit);
            if (Mathf.Abs(rectTransform.anchoredPosition.y - alvoYposi) < 0.1f)
            {
                estaMovendo = false;
            }
        }
    }

    private void AoSelecionarInputField(string texto)
    {
        alvoYposi = originYposi + verticalMove;
        estaMovendo = true;
    }

    private void AoDeselecionarInputField(string texto)
    {
        alvoYposi = originYposi;
        estaMovendo = true;
    }
}