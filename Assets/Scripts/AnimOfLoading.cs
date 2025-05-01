// Anexar este script a algum GameObject na cena de loading
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class AnimOfLoading : MonoBehaviour
{
    [SerializeField] private float loadingTime = 1f; // Tempo mínimo em segundos para mostrar a tela de loading
    private float startTime;
    
    void Start()
    {
        startTime = Time.time; // Armazena o tempo de início
        StartCoroutine(CarregarAsync(LoadGerenciador.Instance.NomeProxCena)); // Inicia o carregamento assíncrono da próxima cena
    }

    private IEnumerator CarregarAsync(String nomeProxCena)
    {
        AsyncOperation carregamento = SceneManager.LoadSceneAsync(nomeProxCena);
        carregamento.allowSceneActivation = false;

        while (!carregamento.isDone)
        {
            // Manter a cena de loading ativa e a animação rodando
            // Você pode adicionar aqui lógica para exibir progresso se desejar
            yield return null;

            if (carregamento.progress >= 0.9f)
            {
                if ((Time.time - startTime) >= loadingTime)
                {
                    carregamento.allowSceneActivation = true; // Ativa a próxima cena
                }
            }
        }
        // A próxima cena foi carregada e ativada.
        // Destrói a cena de loading (e este script junto com ela)
        Destroy(gameObject.scene.GetRootGameObjects()[0]); // Destrói o objeto raiz da cena de loading
    }
}