using UnityEngine;
using TMPro;

public class LetterUIController : MonoBehaviour
{
    public static LetterUIController Instance;

    [Header("UI")]
    [SerializeField] private GameObject letterPanel;
    [SerializeField] private TextMeshProUGUI letterText;

    public System.Action OnLetterClosed;

    private void Awake()
    {
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
    }
}
