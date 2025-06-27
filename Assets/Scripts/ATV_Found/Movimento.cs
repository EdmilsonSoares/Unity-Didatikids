// Script responsável pelo comportamento de segurar e mover objetos
using UnityEngine;

public class Movimento : MonoBehaviour
{
    private bool estaSendoArrastado = false; // Flag para saber se o objeto está sendo arrastado
    private Vector3 offset; // Deslocamento entre o centro do objeto e o ponto de toque inicial
    private Collider2D colisorObjeto;
    private Rigidbody2D rigidBody;
    private Vector3 posicaoOriginal; // NOVO: Variável para armazenar a posição inicial do objeto
    public delegate void ObjetoSoltoNoMundo(GameObject obj); //Delegate de nome ObjetoSoltoNoMundo. É o "contrato" do evento
    public static event ObjetoSoltoNoMundo AoSoltarObjeto; // Evento de nome AoSoltarObjeto. Para notificar quando o objeto é solto

    void Awake()
    {
       posicaoOriginal = transform.position; // Armazena a posição inicial do objeto assim que ele nasce (Start)
    }

    void Start()
    {
        colisorObjeto = GetComponent<Collider2D>();
        if (colisorObjeto == null)
        {
            enabled = false; // Desabilita o script se não houver collider
            return;
        }
        rigidBody = GetComponent<Rigidbody2D>();
        if (rigidBody == null)
        {
            enabled = false;
            return;
        }
    }

    void Update()
    {
        GerenciarToqueNoObjeto();
    }

    private void GerenciarToqueNoObjeto()
    {
        if (Input.touchCount > 0) // Verifica se há pelo menos um toque na tela
        {
            Touch toque = Input.GetTouch(0); // Pega o primeiro toque
            Vector3 coordenadas = Camera.main.ScreenToWorldPoint(toque.position); // Converte a posição do toque na tela para coordenadas de mundo
            coordenadas.z = 0; // Garante que o eixo Z esteja no plano 2D

            if (toque.phase == TouchPhase.Began) // Se começo do toque cai aqui
            {
                if (colisorObjeto.OverlapPoint(coordenadas)) // Verifica se o toque inicial está DENTRO do collider do objeto
                {
                    estaSendoArrastado = true;
                    offset = transform.position - coordenadas; // Calcula o offset: a diferença entre a posição do objeto e a posição do toque
                }
            }
            else if (toque.phase == TouchPhase.Moved) // Se deslizando o dedo na tela cai aqui
            {
                if (estaSendoArrastado)
                {
                    // O arrastar ocorre aqui
                    rigidBody.MovePosition(coordenadas + offset); // Não funciona no emulador, use apenas no dispositivo móvel
                }
            }
            else if (toque.phase == TouchPhase.Ended || toque.phase == TouchPhase.Canceled) // Se levantar o dedo ou se o sistema forçar a parada cai aqui
            {
                if (estaSendoArrastado) // Apenas se estava sendo arrastado
                {
                    if (AoSoltarObjeto != null) // Verifica se há inscritos no evento
                    {
                        AoSoltarObjeto(this.gameObject); // Dispara o evento quando o objeto é solto permitindo que outros scripts saibam que este objeto foi solto.
                    }
                }
                estaSendoArrastado = false; // Falso para que o objeto não continue sendo arrastado em um novo toque
            }
        }
    }

    // Retorna o objeto à sua posição original
    public void RetornarPosicaoOriginal()
    {
        estaSendoArrastado = false; // Se o objeto estiver sendo arrastado, paramos o arrasto para evitar conflitos.
        transform.position = posicaoOriginal;
    }
}
