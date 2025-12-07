using ArtWorks;
using Items;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using Interfaces;

public class CaseLetter : ItemBase, IInteractable
{
    public enum LetterState
    {
        Closed,
        Open,
        Sealed,
        Sent
    }

    public LetterState State { get; private set; } = LetterState.Closed;
    public CaseData caseData;

    private Transform _originalParent;
    private bool _canInteract = true;

    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Sprites")]
    [SerializeField] private Sprite closedSprite;
    [SerializeField] private Sprite openSprite;
    [SerializeField] private Sprite sealedSprite;

    [Header("Sprite Scales")]
    [SerializeField] private Vector3 closedScale = new Vector3(1f, 1f, 1f);
    [SerializeField] private Vector3 openScale = new Vector3(0.02f, 0.02f, 1f);
    [SerializeField] private Vector3 sealedScale = new Vector3(0.7f, 0.7f, 1f);

    protected override void Awake()
    {
        base.Awake();
        _originalParent = transform.parent;
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        if (spriteRenderer == null) return;

        switch (State)
        {
            case LetterState.Closed:
                spriteRenderer.sprite = closedSprite;
                spriteRenderer.transform.localScale = closedScale;
                break;

            case LetterState.Open:
                spriteRenderer.sprite = openSprite;
                spriteRenderer.transform.localScale = openScale;
                spriteRenderer.sortingOrder = 0;
                break;

            case LetterState.Sealed:
                spriteRenderer.sprite = sealedSprite;
                spriteRenderer.transform.localScale = sealedScale;
                break;

            case LetterState.Sent:
                break;
        }
    }

    public override void Select()
    {
        if (!_canInteract) return;
        if (State == LetterState.Sent) return;

        Collect();
    }

    public override void Deselect()
    {
        if (!_canInteract) return;
        if (State == LetterState.Sent) return;

        Uncollect();
    }

    public override void Collect()
    {
        base.Collect();
        transform.SetParent(null);
    }

    public override void Uncollect()
    {
        base.Uncollect();

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        if (State == LetterState.Closed)
        {
            var dropZone = FindFirstObjectByType<LetterDropZone>();
            if (dropZone != null && dropZone.IsOverZone(mousePos))
                OpenLetter();
        }
        else if (State == LetterState.Open)
        {
            var printer = FindFirstObjectByType<PrinterDropZone>();
            if (printer != null)
                printer.TryProcessLetter(this, mousePos);
        }
        else if (State == LetterState.Sealed)
        {
            var mailbox = FindFirstObjectByType<Mailbox>();
            if (mailbox != null && mailbox.IsOverZone(mousePos))
                SendLetter();
        }
    }

    private void OpenLetter()
    {
        State = LetterState.Open;
        UpdateSprite();

<<<<<<< Updated upstream
        DocumentUIController.Instance.ShowDocuments(
            caseData,
=======
        string title = string.IsNullOrWhiteSpace(caseData.title) ? "SIN TÍTULO" : caseData.title.Trim();
        string desc = string.IsNullOrWhiteSpace(caseData.description) ? "SIN DESCRIPCIÓN" : caseData.description.Trim();

        LetterUIController.Instance.ShowLetter_WithCallback(
            title,
            desc,
>>>>>>> Stashed changes
            () =>
            {
                ArtworkSpawner.Instance.SpawnArtworkForCurrentCase(caseData);
            }
        );
    }

    private void ReopenLetter()
    {
<<<<<<< Updated upstream
        DocumentUIController.Instance.ShowDocuments(
            caseData,
=======
        string title = string.IsNullOrWhiteSpace(caseData.title) ? "SIN TÍTULO" : caseData.title.Trim();
        string desc = string.IsNullOrWhiteSpace(caseData.description) ? "SIN DESCRIPCIÓN" : caseData.description.Trim();

        LetterUIController.Instance.ShowLetter_WithCallback(
            title,
            desc,
>>>>>>> Stashed changes
            null
        );
    }

    public void SealLetter()
    {
        if (State != LetterState.Open)
            return;

        State = LetterState.Sealed;
        UpdateSprite();

        var currentArtwork = ArtworkSpawner.Instance.GetCurrentArtwork();

        if (currentArtwork != null && currentArtwork.CaseData == caseData)
        {
            Destroy(currentArtwork.gameObject);
            ArtworkSpawner.Instance.ClearCurrentArtwork();
        }
    }

    private void SendLetter()
    {
        State = LetterState.Sent;

        var pile = FindFirstObjectByType<CaseLetterPile>();
        if (pile != null)
            pile.RemoveTopLetter(this);

        Destroy(gameObject);
    }
    public void SetInteractable(bool value)
    {
        _canInteract = value;
    }

    public override void Interact()
    {
        if (!_canInteract) return;
        if (State == LetterState.Sent) return;

        if (State == LetterState.Open || State == LetterState.Sealed)
            ReopenLetter();
    }
}
