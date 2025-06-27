using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BotaoNivel : MonoBehaviour
{
    [SerializeField] private Button btnNumero;
    [SerializeField] private TMP_Text textNumero;
    [SerializeField] private AtivadorDeTelas ativadorDeTelas;

    void Awake()
    {
        btnNumero.onClick.AddListener(CarregarJogo);
    }

    private void CarregarJogo()
    {
        int num = int.Parse(textNumero.text);
        ativadorDeTelas.SetNivel(num);
        ativadorDeTelas.CarregarJogo();
    }

}
