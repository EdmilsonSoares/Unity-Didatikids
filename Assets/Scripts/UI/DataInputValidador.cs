using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class DataInputValidador : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputData;
    [SerializeField] private TMP_Text feedbackText; // Referência para a caixa de texto TMP
    private bool isUserUpdating = true;
    private string dataTextValidada = "";

    public string GetDataTextValidada()
    {
        return dataTextValidada;
    }

    private void OnEnable()
    {
        dataTextValidada = "";
    }

    void Start()
    {
        if (inputData != null)
        {
            // Registra a função ValidateChar ao evento onValidateInput para validar caracteres durante a digitação
            inputData.onValidateInput += ValidateChar;
            // Adiciona o método ValidateDate à lista de listeners (ouvintes) do evento onEndEdit do tmpInputField
            inputData.onEndEdit.AddListener(ValidateDate);
            inputData.onValueChanged.AddListener(FormatDate); //Formata a data enquanto o usuário digita
        }
    }

    // Função que corresponde à assinatura do delegado OnValidateInput para validar caracteres permitidos
    public char ValidateChar(string text, int pos, char ch)
    {
        if (char.IsDigit(ch))
            return ch;
        else
            return '\0';
    }

    private void ValidateDate(string dataText)
    {
        // Reorganizar a string text em dd/MM/yyyy
        if (dataText.Length == 10 && dataText[2] == '/' && dataText[5] == '/')
        {
            string dia = dataText.Substring(0, 2);
            string mes = dataText.Substring(3, 2);
            string ano = dataText.Substring(6, 4);
            dataText = $"{ano}-{mes}-{dia}"; // Reorganiza para "MM/dd/yyyy"
        }
        DateTime dataAnalizada; // Variável para armazenar a data analisada
        /* A próxima linha tenta analisar a string digitada pelo usuario (text)
         * como uma data no formato especificado (dataFormato). 
         * Também verifica ano bissexto e se o dia, mês e ano são válidos.*/
        if (DateTime.TryParseExact(dataText, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out dataAnalizada))
        {
            feedbackText.text = ""; // Limpa o feedbackText se data válida
            dataTextValidada = dataText;
            Debug.Log("Data válida inserida: " + dataText);
        }
        else if (!string.IsNullOrEmpty(dataText))
        {
            Debug.LogWarning("Data inválida inserida. Formato esperado: dd/MM/yyyy");
            feedbackText.text = "Data inválida. Use Dia/Mês/Ano"; // Exibe erro no feedbackText
        }
    }

    void FormatDate(string input)
    {
        if (!isUserUpdating) return;
        isUserUpdating = false;

        input = input.Replace("/", "");

        string formatted = "";
        if (input.Length > 0)
            formatted += input.Substring(0, Mathf.Min(2, input.Length));

        if (input.Length > 2)
            formatted += "/" + input.Substring(2, Mathf.Min(2, input.Length - 2));

        if (input.Length > 4)
            formatted += "/" + input.Substring(4, Mathf.Min(4, input.Length - 4));

        inputData.SetTextWithoutNotify(formatted);

        // Força o cursor pro final no próximo frame
        StartCoroutine(ForceCursorToEnd(formatted.Length));

        isUserUpdating = true;
    }

    private IEnumerator ForceCursorToEnd(int position)
    {
        yield return new WaitForEndOfFrame();
        inputData.caretPosition = position;
        inputData.selectionAnchorPosition = position;
        inputData.selectionFocusPosition = position;
    }
}