using ArtWorks;
using Items;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class CaseLetter : ItemBase
{
    public CaseData caseData;
    private Transform _originalParent;

    protected override void Awake()
    {
        base.Awake();
        _originalParent = transform.parent;
    }

    public override void Select()
    {
        Collect();
    }

    public override void Deselect()
    {
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

        if (LetterDropZone.Instance != null && LetterDropZone.Instance.IsOverZone(mousePos))
        {
            OpenLetter();
        }
    }

    private void OpenLetter()
    {
        string fullText = $"{caseData.title}\n\n{caseData.description}";

        LetterUIController.Instance.ShowLetter_WithCallback(fullText, () =>
        {
            ArtworkSpawner.Instance.SpawnArtworkForCurrentCase();
        });
    }
}
