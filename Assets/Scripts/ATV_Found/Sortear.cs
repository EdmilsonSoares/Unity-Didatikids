using UnityEngine;
using System.Collections.Generic;

public class ConjuntoNaoValido
{
    public int A;
    public int B;
    public int C;
    public ConjuntoNaoValido(int n1, int n2, int n3) // Construtor
    {
        A = n1; B = n2; C = n3;
    }
    public override string ToString() // Método para facilitar a exibição dos números no Debug.Log
    {
        return $"({A}, {B}, {C})";
    }
}

public class NumerosValidos
{
    public int A;
    public int B;
    public int C;
    public NumerosValidos(int n1, int n2, int n3) // Construtor
    {
        A = n1; B = n2; C = n3;
    }
    public override string ToString() // Método para facilitar a exibição dos números no Debug.Loga
    {
        return $"({A}, {B}, {C})";
    }
}

public class Sortear : MonoBehaviour
{
    [SerializeField] private AtivadorDeTelas ativadorDeTelas;
    private int limitValid = 0;
    private int limitNotValid = 0;
    private List<ConjuntoNaoValido> listaNaoValidos;
    private List<NumerosValidos> listaValidos;
    private int nivelDaTorre = 0;
    private bool isValid = false;
    private int calculos;
    private int A;
    private int B;
    private int C;

    public delegate void ConjuntosGeradosEventHandler(List<ConjuntoNaoValido> naoValidos, List<NumerosValidos> validos);
    public static event ConjuntosGeradosEventHandler AoTerminarSorteio; // Static para outros scripts assinarem diretamente sem precisar de uma referência

    void OnEnable()
    {
        limitValid = ativadorDeTelas.GetLimitValid();
        limitNotValid = ativadorDeTelas.GetLimitNotValid();
        nivelDaTorre = ativadorDeTelas.GetNivel();
    }

    void Start()
    {
        Roletar();
    }

    public void Roletar()
    {
        System.Random random = new System.Random();
        listaNaoValidos = new List<ConjuntoNaoValido>();
        listaValidos = new List<NumerosValidos>();
        isValid = false;
        calculos = 0;
        int tentativas = 0;

        do
        {
            isValid = false;
            while (!isValid)
            {
                tentativas++;
                A = random.Next(0, 10);
                B = random.Next(0, 10);
                C = random.Next(0, 10);
                //Debug.Log($"SORTEAR: Tentativa {tentativas}: A={A} B={B} C={C}");
                // Permutações
                if (Verificar(A, B, C)) { isValid = true; break; }
                if (Verificar(A, C, B)) { isValid = true; break; }
                if (Verificar(B, A, C)) { isValid = true; break; }
                if (Verificar(B, C, A)) { isValid = true; break; }
                if (Verificar(C, A, B)) { isValid = true; break; }
                if (Verificar(C, B, A)) { isValid = true; break; }
                // Após testar todas permutações e variações de operação, nesse ponto do código o número não é válido
                if (listaNaoValidos.Count < limitNotValid)
                {
                    listaNaoValidos.Add(new ConjuntoNaoValido(A, B, C));
                    break;
                }
            }
            if (listaValidos.Count < limitValid && isValid)
            {
                listaValidos.Add(new NumerosValidos(A, B, C));
            }
            // Se achar um número valido mas não atingir o mínimo da lista de não válidos deve rodar denovo
        } while (listaNaoValidos.Count < limitNotValid || listaValidos.Count < limitValid);


        // Descomente para exibir no console todos os trios
        /*Debug.Log("\n----------------------------------------------");
        for (int i = 0; i < listaNaoValidos.Count; i++)
        {
            ConjuntoNaoValido conjAtual = listaNaoValidos[i];
            Debug.Log($"SORTEAR: NÃO VÁLIDOS no índice {i}: {conjAtual.ToString()}");
        }
        Debug.Log("\n----------------------------------------------");
        for (int i = 0; i < listaValidos.Count; i++)
        {
            NumerosValidos conjAtual = listaValidos[i];
            Debug.Log($"SORTEAR: NUMEROS VÁLIDOS no índice {i}: {conjAtual.ToString()}");
        }
        Debug.Log($"SORTEAR: Calculos {calculos}");*/

        if (AoTerminarSorteio != null)
        {
            AoTerminarSorteio(listaNaoValidos, listaValidos); // Dispara o evento ao terminar o sorteio passando as duas listas
        }
    }

    private bool Verificar(int n1, int n2, int n3)
    {
        // Combinações de operações
        if (ApenasSubtracao(n1, n2, n3))
        {
            //Debug.LogWarning($"SORTEAR: Válido encontrado em APENAS SUBTRAÇÃO");
            return true;
        }
        else if (SubtracaoSoma(n1, n2, n3))
        {
            //Debug.LogWarning($"SORTEAR: Válido encontrado em SUBTRAÇÃO SOMA");
            return true;
        }
        else if (SomaSubtracao(n1, n2, n3))
        {
           //Debug.LogWarning($"SORTEAR: Válido encontrado em SOMA SUBTRAÇÃO");
            return true;
        }
        else if (ApenasSoma(n1, n2, n3))
        {
            //Debug.LogWarning($"SORTEAR: Válido encontrado em APENAS SOMA");
            return true;
        }
        return false;
    }

    private bool ApenasSubtracao(int n1, int n2, int n3)
    {
        calculos++;
        int X = n1 - n2 - n3;
        if (X == nivelDaTorre)
        {
            return true;
        }
        return false;
    }

    private bool SubtracaoSoma(int n1, int n2, int n3)
    {
        calculos++;
        int X = n1 - n2 + n3;
        if (X == nivelDaTorre)
        {
            return true;
        }
        return false;
    }

    private bool SomaSubtracao(int n1, int n2, int n3)
    {
        calculos++;
        int X = n1 + n2 - n3;
        if (X == nivelDaTorre)
        {
            return true;
        }
        return false;
    }

    private bool ApenasSoma(int n1, int n2, int n3)
    {
        calculos++;
        int X = n1 + n2 + n3;
        if (X == nivelDaTorre)
        {
            return true;
        }
        return false;
    }



}
