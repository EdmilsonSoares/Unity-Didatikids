using UnityEngine;
using TMPro;
using System.Collections;

public class ValoresABC : MonoBehaviour
{
    [SerializeField] private TMP_Text textNumero; // Referência para o TextMeshProUGUI no próprio GameObject
    [SerializeField] private GameObject animacaoGameObject; // NOVO: Referência para o GameObject "Anim"
    private float animationDuration = 0.5f; // NOVO: Duração da animação em segundos

    // Propriedade pública para leitura, privada para escrita ---
    public int Numero // Note o 'N' maiúsculo para a propriedade pública
    {
        get { return _numero; } // Outros scripts podem ler 'ValoresABC.Numero'
        private set // Apenas dentro desta classe 'ValoresABC' o valor pode ser definido diretamente
        {
            _numero = value; // Atribui ao campo de apoio

            AtualizarTexto(); // Atualiza a UI quando o valor muda
            StartCoroutine(PlayAnimationRoutine()); // Coroutine para a sequência de animação
        }
    }
    private int _numero; // Campo privado que armazena o valor real

    void Awake()
    {
        if (textNumero == null)
        {
            Debug.LogError($"ValoresABC: Objeto '{gameObject.name}' não possui um componente TMP_Text. ");
            enabled = false;
        }
        AtualizarTexto(); // Inicializa o texto para o valor padrão de _numero (que é 0 para int)
    }

    public void SetNumero(int novoNumero)
    {
        Numero = novoNumero; // Isso chamará o 'set' privado da propriedade 'Numero'
    }

    // Método privado para atualizar o TextMeshProUGUI
    private void AtualizarTexto()
    {
        if (textNumero != null)
        {
            textNumero.text = _numero.ToString(); // Usa o campo privado para pegar o valor
        }
    }

    // NOVO: Coroutine para controlar a sequência da animação
    private IEnumerator PlayAnimationRoutine()
    {
        // 1. Desativar textNumero
        if (textNumero != null)
        {
            textNumero.gameObject.SetActive(false);
        }

        // 2. Ativar objeto com animação
        if (animacaoGameObject != null)
        {
            animacaoGameObject.SetActive(true);
        }
        else
        {
            // Se o objeto 'Anim' não foi encontrado reativa o texto imediatamente
            if (textNumero != null) textNumero.gameObject.SetActive(true);
            yield break; // Sai da Coroutine
        }

        if (textNumero != null)
        {
            Color textColor = textNumero.color;
            textColor.a = 0.5f;
            textNumero.color = textColor;
        }

        // 3. Deixar objeto ativado por pelo menos 'animationDuration' segundos
        // Isso pausa a execução da coroutine por X segundos
        yield return new WaitForSeconds(animationDuration);

        // 4. Desativar objeto com animação
        if (animacaoGameObject != null)
        {
            animacaoGameObject.SetActive(false);
        }

        // 5. Ativar textNumero
        if (textNumero != null)
        {
            textNumero.gameObject.SetActive(true);
        }
    }

}