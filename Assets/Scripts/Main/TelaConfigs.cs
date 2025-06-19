using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
// ============================================
       // SCRIPT DESATUALIZADO NÃO USAR
// ============================================
public class TelaConfigs : MonoBehaviour
{
    [SerializeField] private Button btnSoundEffecs;
    [SerializeField] private Button btnProfileSettings;
    [SerializeField] private Button btnLogOut;
    [SerializeField] private Image imgSoundOn;
    [SerializeField] private Image imgSoundOff;
    [SerializeField] private TelaGerenciador telaGerenciador;
    private void Awake()
    {
        btnSoundEffecs.onClick.AddListener(ToggleSoundEffects);
        btnProfileSettings.onClick.AddListener(ProfileSettings);
        btnLogOut.onClick.AddListener(LogOut);
    }
    public void ProfileSettings()
    {
        telaGerenciador.MostrarTela("NovoPerfil");
    }
    public void ToggleSoundEffects()
    {
        if (imgSoundOn.enabled)
        {
            imgSoundOn.enabled = false;
            imgSoundOff.enabled = true;
        }
        else
        {
            imgSoundOn.enabled = true;
            imgSoundOff.enabled = false;
        }
    }
    public void LogOut()
    {
        telaGerenciador.MostrarTela("Login");
    }
}