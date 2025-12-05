using UnityEngine;
using UnityEngine.UI;
using Managers;

public class StampZone : MonoBehaviour
{
    [SerializeField] private Image stampPreview;
    [SerializeField] private Sprite approveSprite;
    [SerializeField] private Sprite rejectSprite;

    private Collider2D _collider;
    private bool _stampApplied;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    public bool IsOverZone(Vector2 position)
    {
        return _collider != null && _collider.enabled && _collider.OverlapPoint(position);
    }

    public void ApplyStamp(bool isApproved)
    {
        if (_stampApplied)
            return;

        _stampApplied = true;

        stampPreview.sprite = isApproved ? approveSprite : rejectSprite;
        stampPreview.enabled = true;

        GameManager.Instance.ArtworkEvaluated(isApproved);
    }

    public void EnableZone(bool enabled)
    {
        if (_collider != null)
            _collider.enabled = enabled;
    }

    public void ResetStamp()
    {
        _stampApplied = false;
        if (stampPreview != null)
            stampPreview.enabled = false;
    }
}
