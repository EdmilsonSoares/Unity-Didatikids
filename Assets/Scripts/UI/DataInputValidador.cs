using UnityEngine;
using TMPro;
using System;
using System.Text.RegularExpressions;
using System.Diagnostics;

public class DataInputValidador : MonoBehaviour
{
    [SerializeField] private TMP_Text feedbackText; // Referência para a caixa de texto TMP
    public string dataFormato = "dd/MM/yyyy";
    private TMP_InputField tmpInputField;
    public Action<DateTime> OnValidDateEntered;

    void Start()
    {
        if (tmpInputField != null)
        {
            UnityEngine.Debug.Log("teste2");

            tmpInputField.onValueChanged.AddListener(FormatDate); //Formata a data enquanto o usuário digita
        }
        tmpInputField = GetComponent<TMP_InputField>();
        if (tmpInputField != null)
        {
            // Registra a função ValidateChar ao evento onValidateInput para validar caracteres durante a digitação
            tmpInputField.onValidateInput += ValidateChar;
            // Adiciona o método ValidateDate à lista de listeners (ouvintes) do evento onEndEdit do tmpInputField
            tmpInputField.onEndEdit.AddListener(ValidateDate);
        }
    }

    // Função que corresponde à assinatura do delegado OnValidateInput para validar caracteres permitidos
    public char ValidateChar(string text, int pos, char ch)
    {
        if (char.IsDigit(ch) || ch == '/')
        {
            return ch;
        }
        else
        {
            return '\0';
        }
    }

    private void ValidateDate(string text)
    {
        DateTime dataAnalizada; // Variável para armazenar a data analisada
        /* A próxima linha tenta analisar a string digitada pelo usuario (text)
         * como uma data no formato especificado (dataFormato). Também verifica
         * ano bissexto e se o dia, mês e ano são válidos.*/
        if (DateTime.TryParseExact(text, dataFormato, null, System.Globalization.DateTimeStyles.None, out dataAnalizada))
        {
            UnityEngine.Debug.Log("Data válida inserida: " + dataAnalizada.ToString("yyyy-MM-dd"));
            feedbackText.text = ""; // Limpa o feedbackText se data válida
            OnValidDateEntered?.Invoke(dataAnalizada); // Invoca um evento com a data válida
        }
        else
        {
            UnityEngine.Debug.LogWarning("Data inválida inserida. Formato esperado: " + dataFormato);
            feedbackText.text = "Data inválida. Use Dia/Mês/Ano"; // Exibe erro no feedbackText
        }
    }

    void FormatDate(string input)
    {
        UnityEngine.Debug.Log(input);
        // Limita a quantidade de caracteres
        if (input.Length > 8)
            input = input.Substring(0, 8);

        // Formata a string para DD/MM/AAAA
        string formatted = "";
        if (input.Length > 0)
            formatted += input.Substring(0, Mathf.Min(2, input.Length));
        if (input.Length > 2)
            formatted += "/" + input.Substring(2, Mathf.Min(2, input.Length - 2));
        if (input.Length > 4)
            formatted += "/" + input.Substring(4, Mathf.Min(4, input.Length - 4));

        // Define o valor formatado no campo de entrada
        tmpInputField.text = formatted;
    }
}