using UnityEngine;
using UnityEngine.EventSystems;

public class AvatarImage : MonoBehaviour
{
    [SerializeField] private Sprite avatarSprite; // O Sprite atribuido no inspector
    [SerializeField] private TelaNovoPerfil telaNovoPerfil; // Referência ao script TelaNovoPerfil
    [SerializeField] private TelaGerenciador telaGerenciador; // Referência ao script TelaGerenciador

    // Este método será chamado pelo Event Trigger
    public void QuandoClicada(BaseEventData eventData)
    {
        telaNovoPerfil.AtualizarAvatarSelecionado(avatarSprite); //Meodo do script TelaNovoPerfil
        telaGerenciador.MostrarTela("NovoPerfil"); // Desativa todas telas e ativa tela Novo perfil*/
    }
}