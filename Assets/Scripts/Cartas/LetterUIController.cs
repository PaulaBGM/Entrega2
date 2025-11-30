using UnityEngine;
using TMPro;

public class LetterUIController : MonoBehaviour
{
    public static LetterUIController Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private GameObject letterPanel;
    [SerializeField] private TextMeshProUGUI letterText;

    private System.Action _onCloseCallback;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void ShowLetter_ReadOnly(string text)
    {
        if (letterPanel == null || letterText == null)
            return;

        _onCloseCallback = null;

        letterText.text = text;
        letterPanel.SetActive(true);
    }

    public void ShowLetter_WithCallback(string text, System.Action onClosed)
    {
        if (Instance == null)
            return;

        if (letterPanel == null || letterText == null)
            return;

        _onCloseCallback = onClosed;

        letterText.text = text;
        letterPanel.SetActive(true);
    }

    public void CloseLetter()
    {
        if (letterPanel != null)
            letterPanel.SetActive(false);

        if (_onCloseCallback != null)
        {
            _onCloseCallback.Invoke();
        }

        _onCloseCallback = null;
    }
}
