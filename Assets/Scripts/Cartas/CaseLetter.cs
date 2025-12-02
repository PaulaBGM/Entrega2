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

    [Header("Sprites")]
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

    // Permite que solo la carta superior de la pila sea interactuable
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
            if (LetterDropZone.Instance != null && LetterDropZone.Instance.IsOverZone(mousePos))
            {
                OpenLetter();
            }
        }
        else if (State == LetterState.Sealed)
        {
            if (Mailbox.Instance != null && Mailbox.Instance.IsOverZone(mousePos))
            {
                SendLetter();
            }
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

        CaseLetterPile pile = FindFirstObjectByType<CaseLetterPile>()
;
        if (pile != null)
            pile.RemoveTopLetter(this);

        Destroy(gameObject);
    }
}
