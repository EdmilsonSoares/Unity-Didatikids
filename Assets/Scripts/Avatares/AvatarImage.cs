using UnityEngine;
using UnityEngine.EventSystems;

public class AvatarImage : MonoBehaviour
{
    [SerializeField] private Sprite avatarSprite; // O Sprite atribuido no inspector
    [SerializeField] private string avatarPath;
    [SerializeField] private TelaNovoPerfil telaNovoPerfil; // Referência ao script TelaNovoPerfil
    [SerializeField] private GameObject telaAvatares; // Referencia ao CanvasAvatares
    

    // Este método será chamado pelo Event Trigger
    public void QuandoClicada(BaseEventData eventData)
    {
        telaNovoPerfil.AtualizarAvatarSelecionado(avatarSprite, avatarPath); //Metodo do script TelaNovoPerfil
        telaAvatares.SetActive(false);
    }
}