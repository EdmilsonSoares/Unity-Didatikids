using UnityEngine;
using UnityEngine.UI;

public class Coletar : MonoBehaviour
{
    [SerializeField] private Button btnLiberar;
    private GameObject objetoColetado;
    private Collider2D colisorColetador; // Referência ao Collider2D do coletador
    private Vector3 posicaoDeColeta; // Onde o objeto será "sugado" para dentro do coletor
    private Movimento scriptMovimentoObjetoColetado;
    private Rigidbody2D rbObjetoColetado;
    private Vector3 posicaoInicialObjetoColetado; // Posição inicial do objeto antes de ser coletado (para o reset)
    // Evento para notificar quando um objeto é coletado ou liberado
    public delegate void ColetorEstadoEventHandler(GameObject obj, bool coletado);
    public static event ColetorEstadoEventHandler OnColetorEstadoChanged;

    void Awake()
    {
        colisorColetador = GetComponent<Collider2D>();
        if (colisorColetador == null)
        {
            enabled = false;
            return;
        }
        if (!colisorColetador.isTrigger)
        {
            Debug.LogWarning($"Coletar: O Collider2D do '{gameObject.name}' deve estar marcado como Is Trigger no Inspector para funcionar como coletador!", this);
        }
        posicaoDeColeta = transform.position; // Define a posição central do coletor como o ponto de coleta
        objetoColetado = null; // Começa sem objeto
        btnLiberar.onClick.AddListener(LiberarObjetoColetado);
    }

    void OnEnable()
    {
        // Se o Movimento.cs dispara o evento AoSoltarObjeto, ainda podemos usá-lo para depuração
        // ou para lógica secundária, mas a coleta principal será no OnTriggerEnter2D.
        Movimento.AoSoltarObjeto += VerificarObjetoSolto;
    }

    void OnDisable()
    {
        Movimento.AoSoltarObjeto -= VerificarObjetoSolto;
    }

    // Método chamado quando um Collider2D entra na área de trigger deste coletador
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Se já há um objeto coletado ou o objeto que entrou não tem o script ValoresABC, ignora.
        if (objetoColetado != null || other.GetComponent<ValoresABC>() == null)
        {
            return;
        }
        ColetarObjeto(other.gameObject); // LÓGICA DE COLETA
    }

    // Método para Coletar o Objeto
    private void ColetarObjeto(GameObject objParaColetar)
    {
        if (objParaColetar == null) return;
        objetoColetado = objParaColetar;
        posicaoInicialObjetoColetado = objetoColetado.transform.position; // Guarda a posição inicial para o reset

        // Desabilitar o script de movimento do objeto coletado
        scriptMovimentoObjetoColetado = objetoColetado.GetComponent<Movimento>();
        if (scriptMovimentoObjetoColetado != null)
        {
            scriptMovimentoObjetoColetado.enabled = false;
        }

        // Definir o Rigidbody2D do objeto para Kinematic (fixa, mas ainda interage como trigger)
        rbObjetoColetado = objetoColetado.GetComponent<Rigidbody2D>();
        if (rbObjetoColetado != null)
        {
            rbObjetoColetado.bodyType = RigidbodyType2D.Kinematic;
            rbObjetoColetado.linearVelocity = Vector2.zero;
            rbObjetoColetado.angularVelocity = 0f;
        }
        objetoColetado.transform.position = posicaoDeColeta; // "Sugá-lo" para a posição central do coletor
        Debug.Log($"Coletar: '{objetoColetado.name}' sugado para a posição de coleta {posicaoDeColeta}.");
        // Notificar outros sistemas (se necessário)
        ValoresABC scriptValoresABC = objetoColetado.GetComponent<ValoresABC>();
        int numeroDoObjeto = (scriptValoresABC != null) ? scriptValoresABC.Numero : 0;
        Debug.Log($"Coletor: Objeto '{objetoColetado.name}' COLETADO com valor: {numeroDoObjeto}");
        OnColetorEstadoChanged?.Invoke(objetoColetado, true); // Dispara evento
    }

    // Método para verificar se o objeto solto está dentro do coletor. Agora usado mais para feedback ou lógica secundária.
    private void VerificarObjetoSolto(GameObject objetoSolto)
    {
        // Se o objeto solto é o que já está coletado, ou se o coletor já está ocupado,
        // apenas loga para depuração. A coleta em si já deveria ter acontecido.
        if (objetoSolto == objetoColetado)
        {
            Debug.Log($"Coletar: '{objetoSolto.name}' (já coletado) foi solto novamente.");
        }
        else if (objetoColetado != null)
        {
            Debug.Log($"Coletar: '{objetoSolto.name}' foi solto, mas o coletor já está ocupado por '{objetoColetado.name}'.");
        }
    }

    // --- NOVO: Método Público para "Liberar" o objeto (para o Botão de Reset) ---
    public void LiberarObjetoColetado()
    {
        if (objetoColetado != null)
        {
            Debug.Log($"Coletar: Liberando objeto '{objetoColetado.name}' do coletador '{gameObject.name}'.");

            if (scriptMovimentoObjetoColetado != null)
            {
                scriptMovimentoObjetoColetado.enabled = true; // Reabilitar o script de movimento
            }

            if (rbObjetoColetado != null)
            {
                rbObjetoColetado.bodyType = RigidbodyType2D.Dynamic; // Restaurar o Rigidbody2D para Dynamic
                rbObjetoColetado.linearVelocity = Vector2.zero; // Opcional: Zera velocidade para que ele não saia voando
                rbObjetoColetado.angularVelocity = 0f;
            }
            //objetoColetado.transform.SetParent(null); // Desaninhar o objeto (se foi aninhado)
            objetoColetado.transform.position = posicaoInicialObjetoColetado; // Retorna o objeto para sua posição inicial
            GameObject tempObjetoLiberado = objetoColetado; // Salva para o evento
            objetoColetado = null; // Limpa a referência no coletor
            scriptMovimentoObjetoColetado = null;
            rbObjetoColetado = null; // Limpa referência do Rigidbody
            OnColetorEstadoChanged?.Invoke(tempObjetoLiberado, false); // Dispara evento
        }
        else
        {
            Debug.Log($"Coletar: Coletor '{gameObject.name}' já está vazio.");
        }
    }
    // --- Métodos para verificar estado do coletor ---
    public bool IsColetorOcupado()
    {
        return objetoColetado != null;
    }
    public GameObject GetObjetoColetado()
    {
        return objetoColetado;
    }
    

}