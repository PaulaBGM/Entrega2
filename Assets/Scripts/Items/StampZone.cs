using UnityEngine;
using UnityEngine.UI;
using ArtWorks;
using Managers;

public class StampZone : MonoBehaviour
{
    [SerializeField] private Image stampPreview;
    [SerializeField] private Sprite approveSprite;
    [SerializeField] private Sprite rejectSprite;

    private bool _stampApplied;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_stampApplied)
            return;

        if (other.TryGetComponent<StampItem>(out var stamp))
        {
            bool isApprove = stamp.IsApproveStamp;
            ApplyStamp(isApprove);
        }
    }

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
