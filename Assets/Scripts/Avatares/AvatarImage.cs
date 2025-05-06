using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AvatarImage : MonoBehaviour
{
    [SerializeField] private TelaAvatares telaAvatares; // Referência ao scrip TelaAvatares
    [SerializeField] private Sprite avatarSprite; // O Sprite (imagem) deste avatar

    // Este método será chamado pelo Event Trigger
    public void QuandoClicada(BaseEventData eventData)
    {        
        telaAvatares.SetAvatar(avatarSprite); // Quando a imagem é clicada, notifica o AvatarSelector
    }
}