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

    public override void Select()
    {
         if (State == LetterState.Sent)
            return;

        Collect();

    }

    public override void Deselect()
    {
        if (State == LetterState.Sent)
            return;

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
                SendLetter();
        }

    }

    private void OpenLetter()
    {
        State = LetterState.Open;

        string fullText = $"{caseData.title}\n\n{caseData.description}";

        LetterUIController.Instance.ShowLetter_WithCallback(fullText, () =>
        {
            ArtworkSpawner.Instance.SpawnArtworkForCurrentCase();
        });
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
        Destroy(gameObject);
    }
}
