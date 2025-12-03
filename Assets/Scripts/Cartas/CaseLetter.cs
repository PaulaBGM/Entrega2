using ArtWorks;
using Items;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class CaseLetter : ItemBase
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
    [SerializeField] private Sprite closedSprite;
    [SerializeField] private Sprite sealedSprite;

    protected override void Awake()
    {
        base.Awake();
        _originalParent = transform.parent;

        if (spriteRenderer != null && closedSprite != null)
            spriteRenderer.sprite = closedSprite;
    }

    public void SetInteractable(bool value)
    {
        _canInteract = value;
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

        string title = string.IsNullOrWhiteSpace(caseData.title) ? "SIN TÍTULO" : caseData.title.Trim();
        string desc = string.IsNullOrWhiteSpace(caseData.description) ? "SIN DESCRIPCIÓN" : caseData.description.Trim();

        LetterUIController.Instance.ShowLetter_WithCallback(
            title,
            desc,
            () =>
            {
                ArtworkSpawner.Instance.SpawnArtworkForCurrentCase(caseData);
            }
        );
    }

    public void SealLetter()
    {
        if (State != LetterState.Open)
            return;

        State = LetterState.Sealed;

        if (spriteRenderer != null && sealedSprite != null)
            spriteRenderer.sprite = sealedSprite;
    }

    private void SendLetter()
    {
        State = LetterState.Sent;

        CaseLetterPile pile = FindFirstObjectByType<CaseLetterPile>();
        if (pile != null)
            pile.RemoveTopLetter(this);

        Destroy(gameObject);
    }
}
