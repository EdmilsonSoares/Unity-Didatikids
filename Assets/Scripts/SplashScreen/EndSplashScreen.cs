using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSplashScreen : MonoBehaviour
{
    [SerializeField] private string sceneToLoad; // Nome da cena que você quer carregar

    // Este método será chamado por um Evento de Animação
    public void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
            SceneManager.LoadScene(this.sceneToLoad);
    }
}