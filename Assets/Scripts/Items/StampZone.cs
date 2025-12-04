using Interfaces;
using Managers;
using UnityEngine;
using UnityEngine.UI;

public class StampZone : MonoBehaviour, IInteractable
{
    [SerializeField] private Image stampPreview;
    [SerializeField] private Sprite approveSprite;
    [SerializeField] private Sprite rejectSprite;

    private bool _stampApplied;

    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    public bool IsOverZone(Vector2 position)
    {
        return _collider.OverlapPoint(position);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_stampApplied)
            return;

        if (other.TryGetComponent<StampItem>(out var stamp))
        {
            // Solo estampa si el sello está siendo sujetado
            if (stamp.IsHeld)
            {
                ApplyStamp(stamp.IsApproveStamp);
            }
        }
    }
    public void Interact() { }
    private void ApplyStamp(bool approved)
    {
        _stampApplied = true;

        stampPreview.sprite = approved ? approveSprite : rejectSprite;
        stampPreview.enabled = true;

        GameManager.Instance.ArtworkEvaluated(approved);
    }

    public void ResetStamp()
    {
        _stampApplied = false;
        stampPreview.enabled = false;
    }
}
