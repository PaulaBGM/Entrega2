using UnityEngine;
using TMPro;

public class LetterUIController : MonoBehaviour
{
    public static LetterUIController Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private GameObject letterPanel;
    [SerializeField] private TextMeshProUGUI letterText;

    // Evento que se ejecuta cuando la carta se cierra SOLO si se pidió explícitamente
    private System.Action _onCloseCallback;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }
    public void ShowLetter_ReadOnly(string text)
    {
        _onCloseCallback = null;      // No ejecutar nada al cerrar
        letterText.text = text;
        letterPanel.SetActive(true);
    }

    public void ShowLetter_WithCallback(string text, System.Action onClosed)
    {
        _onCloseCallback = onClosed;  // Guardar acción
        letterText.text = text;
        letterPanel.SetActive(true);
    }

    public void CloseLetter()
    {
        letterPanel.SetActive(false);

        _onCloseCallback?.Invoke();
        _onCloseCallback = null; // limpiar
    }
}
