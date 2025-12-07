using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class DocumentUIController : MonoBehaviour
{
<<<<<<< Updated upstream
<<<<<<< Updated upstream
    public static DocumentUIController Instance;
=======
    public static LetterUIController Instance { get; private set; }
>>>>>>> Stashed changes
=======
    public static LetterUIController Instance { get; private set; }
>>>>>>> Stashed changes

    [Header("UI")]
    [SerializeField] private GameObject letterPanel;
    [Header("Text Objects")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Button closeButton;

    [Header("Stamp Zone")]
    [SerializeField] private StampZone stampZone;

    [Header("Panel Background Sprites")]
    [SerializeField] private Image letterPanelImage;
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite lastPageSprite;

    private System.Action _onCloseCallback;
    private List<string> _pages = new List<string>();
    private int _currentPage = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (closeButton != null)
        {
            closeButton.onClick.RemoveAllListeners();
            closeButton.onClick.AddListener(OnCloseButtonPressed);
        }
    }

    public void ShowLetter_WithCallback(string formattedTitle, string formattedDescription, System.Action onClosed)
    {
        _onCloseCallback = onClosed;

<<<<<<< Updated upstream
<<<<<<< Updated upstream
        if (caseData.documents != null)
        {
            foreach (var doc in caseData.documents)
            {
                var inst = Instantiate(documentViewPrefab, documentsContainer);
                var view = inst.GetComponent<DocumentView>();
                view.Initialize(doc);
            }
        }
=======
        titleText.text = formattedTitle;
        GeneratePages(formattedDescription);
>>>>>>> Stashed changes
=======
        titleText.text = formattedTitle;
        GeneratePages(formattedDescription);
>>>>>>> Stashed changes

        _currentPage = 0;
        ShowPage(0);

        letterPanel.SetActive(true);
        UpdateStampZoneState();
    }

    private void GeneratePages(string fullText)
    {
        _pages.Clear();
        int maxChars = 350;

        for (int i = 0; i < fullText.Length; i += maxChars)
        {
            int length = Mathf.Min(maxChars, fullText.Length - i);
            _pages.Add(fullText.Substring(i, length));
        }
    }

    private void ShowPage(int index)
    {
        if (index < 0 || index >= _pages.Count)
            return;

        _currentPage = index;
        descriptionText.text = _pages[index];

        UpdateStampZoneState();
    }

    private void UpdateStampZoneState()
    {
        bool isLastPage = _currentPage == _pages.Count - 1;

        if (stampZone != null)
            stampZone.EnableZone(isLastPage);

        UpdatePanelSprite(isLastPage);
    }

    private void UpdatePanelSprite(bool isLastPage)
    {
        if (letterPanelImage == null) return;

        letterPanelImage.sprite = isLastPage ? lastPageSprite : normalSprite;
    }

    public void OnCloseButtonPressed()
    {
        if (_currentPage < _pages.Count - 1)
        {
            _currentPage++;
            ShowPage(_currentPage);
        }
        else
        {
            CloseLetter();
        }
    }

    public void CloseLetter()
    {
        letterPanel.SetActive(false);
        _onCloseCallback?.Invoke();
        _onCloseCallback = null;

        _pages.Clear();
        _currentPage = 0;

        UpdateStampZoneState();
    }
}
