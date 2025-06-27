// Recebe um conjunto do script GerenciadorDeTabuleiro
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class ExibidorDeConjunto : MonoBehaviour
{
    // Variáveis para armazenar o conjunto recebido
    private ConjuntoNaoValido meuConjuntoNaoValido; // Apenas um será preenchido por vez, dependendo do tipo de conjunto
    private NumerosValidos meuConjuntoValido; // Apenas um será preenchido por vez, dependendo do tipo de conjunto
    [Header("Referências para os scripts nos objetos filhos")]
    [SerializeField] private ValoresABC objetoA;
    [SerializeField] private ValoresABC objetoB;
    [SerializeField] private ValoresABC objetoC;
    private List<GameObject> objetosFilhosArrastaveis = new List<GameObject>();
    // IDs para as Sorting Layers
    private int defaultSortingLayerID;
    private int ativosSortingLayerID;

    void Awake()
    {
        if (objetoA == null || objetoB == null || objetoC == null)
        {
            Debug.LogError($"ExibConj: Um ou mais scripts ValoresABC não foram encontrados em {gameObject.name}.");
            enabled = false; // Desabilita este script para evitar NullReferenceException
        }
        objetosFilhosArrastaveis.Add(objetoA.gameObject);
        objetosFilhosArrastaveis.Add(objetoB.gameObject);
        objetosFilhosArrastaveis.Add(objetoC.gameObject);
        defaultSortingLayerID = SortingLayer.NameToID("Default");
        ativosSortingLayerID = SortingLayer.NameToID("Ativos");
    }

    // Método chamado pelo GerenciadorDeTabuleiro para atribuir o conjunto
    public void AtribuirConjunto(ConjuntoNaoValido conjunto)
    {
        meuConjuntoNaoValido = conjunto;
        meuConjuntoValido = null; // Garante que o outro tipo seja nulo
        //Debug.Log($"ExibConj: {gameObject.name} recebeu NÃO VÁLIDO: {conjunto.ToString()}");
        ExibirNumeros();
    }

    public void AtribuirConjunto(NumerosValidos conjunto) // sobrecarga do método
    {
        meuConjuntoValido = conjunto;
        meuConjuntoNaoValido = null; // Garante que o outro tipo seja nulo
        //Debug.Log($"ExibConj: {gameObject.name} recebeu VÁLIDO: {conjunto.ToString()}");
        ExibirNumeros();
    }

    // Método para atualizar a UI
    private void ExibirNumeros()
    {
        if (meuConjuntoValido != null)
        {
            // Atribui os valores aos scripts dos objetos filhos
            objetoA.SetNumero(meuConjuntoValido.A);
            objetoB.SetNumero(meuConjuntoValido.B);
            objetoC.SetNumero(meuConjuntoValido.C);
            Debug.Log($"ExibConj: Exibindo Válido: {meuConjuntoValido.A}, {meuConjuntoValido.B}, {meuConjuntoValido.C}");
        }
        else if (meuConjuntoNaoValido != null)
        {
            objetoA.SetNumero(meuConjuntoNaoValido.A);
            objetoB.SetNumero(meuConjuntoNaoValido.B);
            objetoC.SetNumero(meuConjuntoNaoValido.C);
            Debug.Log($"ExibConj: Exibindo Não Válido: {meuConjuntoNaoValido.A}, {meuConjuntoNaoValido.B}, {meuConjuntoNaoValido.C}");
        }
        else
        {
            // Debug.Log($"ExibConj: {gameObject.name} (ID: {idDoConjunto}) não possui conjunto atribuído.");
            objetoA.SetNumero(0); // Ou algum valor padrão, como 0 ou -1
            objetoB.SetNumero(0);
            objetoC.SetNumero(0);
        }
    }

    // Limpar exibição
    public void LimparConjunto()
    {
        meuConjuntoValido = null;
        meuConjuntoNaoValido = null;
        ExibirNumeros();
    }

    // Habilita o script Movimento, Collider, Rigidbody e opacidade total para todos os objetos filhos
    public void HabilitarInteracao()
    {
        foreach (GameObject obj in objetosFilhosArrastaveis)
        {
            if (obj != null)
            {
                Movimento movimento = obj.GetComponent<Movimento>();
                if (movimento != null)
                {
                    movimento.enabled = true; // Permite arrastar
                }

                Collider2D collider = obj.GetComponent<Collider2D>();
                if (collider != null)
                {
                    collider.enabled = true; // Habilita o collider para interação
                }

                Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.bodyType = RigidbodyType2D.Dynamic; // Permite movimento físico
                    rb.linearVelocity = Vector2.zero; // Zera qualquer velocidade residual
                    rb.angularVelocity = 0f;
                }

                SpriteRenderer sprite = obj.GetComponent<SpriteRenderer>();
                if (sprite != null)
                {
                    Color cor = sprite.color;
                    cor.a = 1f; // Opacidade total
                    sprite.color = cor;
                    sprite.sortingLayerID = ativosSortingLayerID; // Muda a Sorting Layer
                    sprite.sortingOrder = 0;
                }

                TMP_Text childTmpText = obj.GetComponentInChildren<TMP_Text>();
                if (childTmpText != null)
                {
                    Color textColor = childTmpText.color;
                    textColor.a = 1f;
                    childTmpText.color = textColor;

                    Canvas parentCanvas = childTmpText.GetComponentInParent<Canvas>();
                    if (parentCanvas != null)
                    {
                        parentCanvas.sortingLayerID = ativosSortingLayerID; // Muda a Sorting Layer
                        parentCanvas.sortingOrder = 1;
                    }
                }
            }
        }
        Debug.Log($"ExibidorDeConjunto '{gameObject.name}': Interação HABILITADA.");
    }

    // Desabilita o script Movimento, Collider, Rigidbody e reduz a opacidade para todos os objetos filhos
    public void DesabilitarInteracao()
    {
        foreach (GameObject obj in objetosFilhosArrastaveis)
        {
            if (obj != null)
            {
                Movimento movimento = obj.GetComponent<Movimento>();
                if (movimento != null)
                {
                    movimento.enabled = false; // Impede arrastar
                }

                Collider2D collider = obj.GetComponent<Collider2D>();
                if (collider != null)
                {
                    collider.enabled = false; // Desabilita o collider
                }

                Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.bodyType = RigidbodyType2D.Kinematic; // Fixa o objeto fisicamente
                    rb.linearVelocity = Vector2.zero; // Zera qualquer velocidade residual
                    rb.angularVelocity = 0f;
                }

                SpriteRenderer sprite = obj.GetComponent<SpriteRenderer>();
                if (sprite != null)
                {
                    Color cor = sprite.color;
                    cor.a = 0.5f; // 90% transparente
                    sprite.color = cor;
                    sprite.sortingLayerID = defaultSortingLayerID; // Muda a Sorting Layer
                    sprite.sortingOrder = 0;
                }

                TMP_Text childTmpText = obj.GetComponentInChildren<TMP_Text>();
                if (childTmpText != null)
                {
                    Color textColor = childTmpText.color;
                    textColor.a = 0.5f;
                    childTmpText.color = textColor;

                    Canvas parentCanvas = childTmpText.GetComponentInParent<Canvas>();
                    if (parentCanvas != null)
                    {
                        parentCanvas.sortingLayerID = defaultSortingLayerID; // Muda a Sorting Layer
                        parentCanvas.sortingOrder = 1;
                    }
                }
            }
        }
        Debug.Log($"ExibidorDeConjunto '{gameObject.name}': Interação DESABILITADA");
    }
    // Retorna todos os objetos filhos para suas posições originais
    public void ResetarPosicoesFilhos()
    {
        foreach (GameObject obj in objetosFilhosArrastaveis)
        {
            if (obj != null)
            {
                Movimento movimentoFilho = obj.GetComponent<Movimento>();
                if (movimentoFilho != null)
                {
                    movimentoFilho.RetornarPosicaoOriginal();
                }
            }
        }
        Debug.Log($"ExibidorDeConjunto '{gameObject.name}': Resetando posições dos filhos.");
    }
}