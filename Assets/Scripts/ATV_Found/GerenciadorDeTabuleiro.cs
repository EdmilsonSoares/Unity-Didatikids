// Script que recebe as listas geradas pelo script Sortear
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random; // Usar no embaralhamento

public class GerenciadorDeTabuleiro : MonoBehaviour
{
    // Lista de todos os objetos que exibirão conjuntos. Usamos 'ExibidorDeConjunto' em vez de 'GameObject' para ter acesso direto ao script.
    [Tooltip("Arraste todos os GameObjects que têm o script ExibidorDeConjunto aqui.")]
    public List<ExibidorDeConjunto> exibidoresDeConjuntos; // Lista de acesso aos scripts de cada Exibidor

    void OnEnable()
    {
        Sortear.AoTerminarSorteio += DistribuirConjuntos; // Assina o evento do Sortear quando este script é habilitado
    }

    void OnDisable()
    {
        Sortear.AoTerminarSorteio -= DistribuirConjuntos; // Desassina o evento quando este script é desabilitado
    }

    // Este método é chamado quando o evento 'AoTerminarSorteio' é disparado. Recebe as duas listas do Sortear
    private void DistribuirConjuntos(List<ConjuntoNaoValido> naoValidos, List<NumerosValidos> validos)
    {
        Debug.Log("GerTabul: Evento recebido. Iniciando distribuição...");

        Shuffle(exibidoresDeConjuntos); // Embaralha os exibidores antes de limpar ou distribuir

        foreach (var exibidor in exibidoresDeConjuntos) // Limpa qualquer conjunto anterior nos exibidores
        {
            Debug.Log("GerTabul: Reiniciando os Exibidores...");
            exibidor.LimparConjunto();
        }
        // === Lógica de Distribuição (ajustada para pegar da lista embaralhada) ===
        // Criar uma lista temporária para todos os conjuntos a serem distribuídos
        List<object> todosOsConjuntos = new List<object>(); // Usamos 'object' porque a lista conterá tipos diferentes
        todosOsConjuntos.AddRange(naoValidos); // Adiciona todos os conjuntos não válidos
        todosOsConjuntos.AddRange(validos);   // Adiciona o (os) conjunto(s) válido(s)
        // Embaralhar a ordem dos conjuntos também, para que o válido não seja sempre o último ou o primeiro
        Shuffle(todosOsConjuntos);

        // Agora, distribui os conjuntos embaralhados para os exibidores embaralhados
        for (int i = 0; i < todosOsConjuntos.Count && i < exibidoresDeConjuntos.Count; i++)
        {
            if (todosOsConjuntos[i] is ConjuntoNaoValido naoValido)
            {
                exibidoresDeConjuntos[i].AtribuirConjunto(naoValido);
            }
            else if (todosOsConjuntos[i] is NumerosValidos valido)
            {
                exibidoresDeConjuntos[i].AtribuirConjunto(valido);
            }
        }

        Debug.Log("GerTabul: Distribuição de conjuntos concluída.");
    }

    // Função de Embaralhamento (Fisher-Yates)
    private void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count); // Random.Range é exclusivo no limite superior para int
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}