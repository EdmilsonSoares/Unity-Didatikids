using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VerificarOperacao : MonoBehaviour
{
    [SerializeField] private Button btnCalcular;
    [SerializeField] private Button btnAvancar;
    [SerializeField] private Button btnBack;
    [SerializeField] private TMP_Text textNivel;
    [SerializeField] private TMP_Text textNumber;
    [SerializeField] private TMP_Text textFeedback;
    [SerializeField] private TMP_Text textResultado;
    // Referências aos scripts
    [SerializeField] private AtivadorDeTelas ativadorDeTelas;
    [SerializeField] private Sortear sortear;
    [SerializeField] private Coletar coletar01;
    [SerializeField] private Coletar coletar02;
    [SerializeField] private Coletar coletar03;
    [SerializeField] private BotaoOperador botaoOperador01;
    [SerializeField] private BotaoOperador botaoOperador02;
    [SerializeField] private HabilitarDesabilitar habilitarDesabilitar;
    private int A, B, C;
    private int resultado;
    private bool isSomaOpe01;
    private bool isSomaOpe02;
    private int nivel = 1;
    private int tentativas = 5;

    void Awake()
    {
        btnCalcular.onClick.AddListener(PegarNumero);
        btnAvancar.onClick.AddListener(SubirNivel);
        btnBack.onClick.AddListener(Recarregar);

    }

    void Start()
    {
        btnAvancar.gameObject.SetActive(false);
        textNivel.text = "Nível " + nivel.ToString() + "\nChances " + tentativas.ToString();
        textNumber.text = nivel.ToString();
        textResultado.text = "?";
        textFeedback.text = "";
    }

    private void Recarregar()
    {
        GameManager.Instance.Carregar("Found");
    }

    private void PegarNumero()
    {
        isSomaOpe01 = botaoOperador01.GetOperador();
        isSomaOpe02 = botaoOperador02.GetOperador();

        if (!coletar01 || !coletar02 || !coletar03)
        {
            Debug.LogWarning("Coletor não atribuido no inspector");
            return;
        }

        if (!coletar01.IsColetorOcupado() || !coletar02.IsColetorOcupado() || !coletar03.IsColetorOcupado())
        {
            textResultado.text = "?";
            Debug.LogWarning("Calcular: Faltando coletor. Cálculo impedido.");
            return;
        }

        GameObject obj;
        ValoresABC scriptValoresABC;

        obj = coletar01.GetObjetoColetado(); // Pega o GameObject coletado
        scriptValoresABC = obj.GetComponent<ValoresABC>();
        if (scriptValoresABC != null)
            A = scriptValoresABC.Numero;

        obj = coletar02.GetObjetoColetado();
        scriptValoresABC = obj.GetComponent<ValoresABC>();
        if (scriptValoresABC != null)
            B = scriptValoresABC.Numero;

        obj = coletar03.GetObjetoColetado();
        scriptValoresABC = obj.GetComponent<ValoresABC>();
        if (scriptValoresABC != null)
            C = scriptValoresABC.Numero;

        FazerCalculo();
        textResultado.text = resultado.ToString();
        Avaliar();

    }

    private void FazerCalculo()
    {
        if (!isSomaOpe01 && !isSomaOpe02)
        {
            resultado = A - B - C;
        }
        else if (!isSomaOpe01 && isSomaOpe02)
        {
            resultado = A - B + C;
        }
        else if (isSomaOpe01 && !isSomaOpe02)
        {
            resultado = A + B - C;
        }
        else // isSomaOpe01 && isSomaOpe02
        {
            resultado = A + B + C;
        }
    }

    private void Avaliar()
    {
        if (resultado != nivel) // Se errar
        {
            tentativas--;
            textNivel.text = "Nível " + nivel.ToString() + "\nChances " + tentativas.ToString();
            coletar01.LiberarObjetoColetado();
            coletar02.LiberarObjetoColetado();
            coletar03.LiberarObjetoColetado();

            if (tentativas < 1)
            {
                ativadorDeTelas.MostrarTela("GameOver");
            }
        }
        else
        {
            textFeedback.text = "Acertou";
            btnAvancar.gameObject.SetActive(true);
            habilitarDesabilitar.SetBotoes(false);
            Time.timeScale = 0;
        }

    }

    private void SubirNivel()
    {
        nivel++;
        if (nivel > 5)
        {
            ativadorDeTelas.MostrarTela("Victory");
            Time.timeScale = 1;
            return;
        }

        habilitarDesabilitar.ResetarTodosExibidores();
        sortear.nivelDaTorre = nivel;
        sortear.Roletar();
        textNivel.text = "Nível " + nivel.ToString() + "\nChances " + tentativas.ToString();
        textNumber.text = nivel.ToString();
        textFeedback.text = "";
        textResultado.text = "?";
        btnAvancar.gameObject.SetActive(false);
        habilitarDesabilitar.SetBotoes(true);
        Time.timeScale = 1;

    }

}
