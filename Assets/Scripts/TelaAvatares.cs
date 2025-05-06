using UnityEngine;
using UnityEngine.UI;

public class TelaAvatares : MonoBehaviour
{
    [SerializeField] private TelaNovoPerfil telaNovoPerfil; // Referência ao script TelaNovoPerfil
    [SerializeField] private TelaGerenciador telaGerenciador; // Referência ao script TelaGerenciador

    // Método chamado pelo AvatarImage quando um avatar é selecionado
    public void SetAvatar(Sprite avatarSprite)
    {
            telaNovoPerfil.AtualizarAvatarSelecionado(avatarSprite);
            telaGerenciador.MostrarNovoPerfil(); // Desativa todas telas e ativa tela Novo perfil        
    }

}
