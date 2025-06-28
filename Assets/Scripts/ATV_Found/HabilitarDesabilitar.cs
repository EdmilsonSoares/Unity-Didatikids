using UnityEngine;
using System.Collections.Generic; // Para usar List
using UnityEngine.UI; // Para usar o componente Button (se o script for associado a um botão)

public class HabilitarDesabilitar : MonoBehaviour
{
    [SerializeField] private AtivadorDeTelas ativadorDeTelas;
    [SerializeField] private Coletar coletor01;
    [SerializeField] private Coletar coletor02;
    [SerializeField] private Coletar coletor03;
    [SerializeField] private Button btnChoose00;
    [SerializeField] private Button btnChoose01;
    [SerializeField] private Button btnChoose02;
    [SerializeField] private Button btnChoose03;
    [SerializeField] private Button btnOperador01;
    [SerializeField] private Button btnOperador02;
    [SerializeField] private Button btnCalcular;
    [SerializeField] private Button btnSoltar;
    [Tooltip("Arraste todos os GameObjects que têm o script ExibidorDeConjunto aqui.")]
    public List<ExibidorDeConjunto> exibidoresDeConjuntos;
    private ExibidorDeConjunto exibidorAtivoAtual; // Referência ao exibidor ativo atual para saber qual está "liberado"

    void Start()
    {
        // Inicializa TODOS os exibidores como DESABILITADOS no início do jogo.
        if (exibidoresDeConjuntos != null && exibidoresDeConjuntos.Count > 0)
        {
            foreach (ExibidorDeConjunto exibidor in exibidoresDeConjuntos)
            {
                if (exibidor != null)
                {
                    exibidor.DesabilitarInteracao(); // Todos começam desativados/transparentes
                    exibidor.ResetarPosicoesFilhos();
                }
            }
            exibidorAtivoAtual = null; // Ninguém ativo no início
            AdicionarListenerDosBotoes();
        }
        else
        {
            Debug.LogWarning("HabilitarDesabilitar: Nenhuma referência de ExibidorDeConjunto na lista! Por favor, atribua-os no Inspector.", this);
        }
    }

    // Metodo para adicionar listeners dependendo da quantidade de botões na tela
    private void AdicionarListenerDosBotoes()
    {
        if (ativadorDeTelas.GetLevelDificult() == 1)
        {
            btnChoose00.onClick.AddListener(() => AtivarExibidorPorBotao(exibidoresDeConjuntos[0]));
            btnChoose01.onClick.AddListener(() => AtivarExibidorPorBotao(exibidoresDeConjuntos[1]));
        }
        else if (ativadorDeTelas.GetLevelDificult() == 2)
        {
            btnChoose00.onClick.AddListener(() => AtivarExibidorPorBotao(exibidoresDeConjuntos[0]));
            btnChoose01.onClick.AddListener(() => AtivarExibidorPorBotao(exibidoresDeConjuntos[1]));
            btnChoose02.onClick.AddListener(() => AtivarExibidorPorBotao(exibidoresDeConjuntos[2]));
        }
        else if (ativadorDeTelas.GetLevelDificult() == 3)
        {
            btnChoose00.onClick.AddListener(() => AtivarExibidorPorBotao(exibidoresDeConjuntos[0]));
            btnChoose01.onClick.AddListener(() => AtivarExibidorPorBotao(exibidoresDeConjuntos[1]));
            btnChoose02.onClick.AddListener(() => AtivarExibidorPorBotao(exibidoresDeConjuntos[2]));
            btnChoose03.onClick.AddListener(() => AtivarExibidorPorBotao(exibidoresDeConjuntos[3]));
        }
    }
    // NOVO MÉTODO: Chamado diretamente pelos botões na UI
    public void AtivarExibidorPorBotao(ExibidorDeConjunto exibidorParaAtivar)
    {
        if (exibidorParaAtivar == null)
        {
            Debug.LogWarning("HabilitarDesabilitar: O exibidor passado pelo botão é nulo.", this);
            return;
        }
        // Se o exibidor clicado já é o ativo, não faz nada
        if (exibidorAtivoAtual == exibidorParaAtivar)
        {
            Debug.Log($"HabilitarDesabilitar: Exibidor '{exibidorParaAtivar.name}' já está ativo. Nenhuma mudança.");
            return;
        }
        // 1. Desabilita o exibidor que estava ativo anteriormente, se houver e reseta suas posições
        if (exibidorAtivoAtual != null)
        {
            // Se os objetos do exibidor estiverem coletados, solta eles para desocupar os coletores
            if (coletor01.IsColetorOcupado())
                coletor01.LiberarObjetoColetado();
            if (coletor02.IsColetorOcupado())
                coletor02.LiberarObjetoColetado();
            if (coletor03.IsColetorOcupado())
                coletor03.LiberarObjetoColetado();

            exibidorAtivoAtual.DesabilitarInteracao();
            exibidorAtivoAtual.ResetarPosicoesFilhos();
            Debug.Log($"HabilitarDesabilitar: Desativando '{exibidorAtivoAtual.name}'.");
        }
        // 2. Ativa o novo exibidor
        exibidorAtivoAtual = exibidorParaAtivar;
        exibidorAtivoAtual.HabilitarInteracao();
        //exibidorAtivoAtual.ResetarPosicoesFilhos();
        Debug.Log($"HabilitarDesabilitar: Ativando '{exibidorParaAtivar.name}'.");
        // 3. Desativa TODOS os OUTROS exibidores (que não são o 'exibidorParaAtivar')
        foreach (ExibidorDeConjunto exibidor in exibidoresDeConjuntos)
        {
            if (exibidor != null && exibidor != exibidorParaAtivar)
            {
                exibidor.DesabilitarInteracao();
                exibidor.ResetarPosicoesFilhos();
                Debug.Log($"HabilitarDesabilitar: Outro exibidor '{exibidor.name}' desativado.");
            }
        }
    }

    // Método para resetar todos os exibidores para um estado inicial (útil para botão de reset do jogo)
    public void ResetarTodosExibidores()
    {
        if (exibidoresDeConjuntos != null)
        {
            // Se os objetos do exibidor estiverem coletados, solta eles para desocupar os coletores
            if (coletor01.IsColetorOcupado())
                coletor01.LiberarObjetoColetado();
            if (coletor02.IsColetorOcupado())
                coletor02.LiberarObjetoColetado();
            if (coletor03.IsColetorOcupado())
                coletor03.LiberarObjetoColetado();

            foreach (ExibidorDeConjunto exibidor in exibidoresDeConjuntos)
            {
                if (exibidor != null)
                {
                    exibidor.DesabilitarInteracao(); // Deixa todos desativados e transparentes
                    exibidor.ResetarPosicoesFilhos();
                }
            }
        }
        exibidorAtivoAtual = null; // Nenhum exibidor está ativo após o reset
        Debug.Log("HabilitarDesabilitar: Todos os exibidores foram resetados.");
    }

    public void SetBotoes(bool estado)
    {
        if (ativadorDeTelas.GetLevelDificult() == 1)
        {
            btnChoose00.interactable = estado;
            btnChoose01.interactable = estado;
        }
        else if (ativadorDeTelas.GetLevelDificult() == 2)
        {
            btnChoose00.interactable = estado;
            btnChoose01.interactable = estado;
            btnChoose02.interactable = estado;
        }
        else if (ativadorDeTelas.GetLevelDificult() == 3)
        {
            btnChoose00.interactable = estado;
            btnChoose01.interactable = estado;
            btnChoose02.interactable = estado;
            btnChoose03.interactable = estado;
        }
        btnOperador01.interactable = estado;
        btnOperador02.interactable = estado;
        btnCalcular.interactable = estado;
        btnSoltar.interactable = estado;
    }
}