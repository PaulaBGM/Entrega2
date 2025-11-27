using UnityEngine;
using TMPro;

public class LetterUIController : MonoBehaviour
{
    public static LetterUIController Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private GameObject letterPanel;
    [SerializeField] private TextMeshProUGUI letterText;

    public System.Action OnLetterClosed;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void ShowLetter(string text)
    {
        letterText.text = text;
        letterPanel.SetActive(true);
    }

    public void CloseLetter()
    {
        letterPanel.SetActive(false);

        OnLetterClosed?.Invoke();
        OnLetterClosed = null; // Limpiar suscriptores
    }
}
