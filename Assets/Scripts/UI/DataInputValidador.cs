using UnityEngine;
using TMPro;
using System;

public class DataInputValidador : MonoBehaviour
{
    [SerializeField] private TMP_Text feedbackText; // Referência para a caixa de texto TMP
    public string dataFormato = "dd/MM/yyyy";
    private TMP_InputField tmpInputField;
    public Action<DateTime> OnValidDateEntered;

    void Start()
    {
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
            Debug.Log("Data válida inserida: " + dataAnalizada.ToString("yyyy-MM-dd"));
            feedbackText.text = "Data válida: " + dataAnalizada.ToString("dd/MM/yyyy"); // Exibe na caixa de texto
            OnValidDateEntered?.Invoke(dataAnalizada); // Invoca um evento com a data válida
        }
        else
        {
            Debug.LogWarning("Data inválida inserida. Formato esperado: " + dataFormato);
            feedbackText.text = "Data inválida. Formato: " + dataFormato; // Exibe na caixa de texto
            // Aqui você pode exibir uma mensagem de erro para o usuário
        }
    }
}