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
        Debug.Log("[StampZone] Awake collider=" + (_collider != null));
    }

    public bool IsOverZone(Vector2 position)
    {
        bool result = _collider != null && _collider.enabled && _collider.OverlapPoint(position);
        Debug.Log("[StampZone] IsOverZone pos=" + position + " result=" + result);
        return result;
    }

    public void ApplyStamp(bool isApproved)
    {
        if (_stampApplied)
        {
            Debug.Log("[StampZone] ApplyStamp ignorado: ya aplicado");
            return;
        }

        _stampApplied = true;

        stampPreview.sprite = isApproved ? approveSprite : rejectSprite;
        stampPreview.enabled = true;

        var currentArtwork = GameManager.Instance.GetCurrentArtWork();

        if (currentArtwork != null)
        {
            Debug.Log("[StampZone] Aplicando sello sobre ArtWork existente. isApproved=" + isApproved);
            GameManager.Instance.ArtworkEvaluated(isApproved);
            return;
        }

        Debug.Log("[StampZone] No hay ArtWork actual. Buscando CaseData en CaseManager para aplicar consecuencias.");

        if (CaseManager.Instance == null)
        {
            Debug.LogError("[StampZone] CaseManager.Instance es NULL. No se pueden aplicar consecuencias.");
            return;
        }

        var caseData = CaseManager.Instance.GetCurrentCase();

        if (caseData == null)
        {
            Debug.LogError("[StampZone] CaseManager devolvió CaseData NULL.");
            return;
        }

        Debug.Log("[StampZone] Aplicando consecuencias directas desde CaseData: " + caseData.caseID + " isApproved=" + isApproved);
        GameManager.Instance.ArtworkEvaluated(caseData, isApproved);
    }

    public void EnableZone(bool enabled)
    {
        if (_collider != null)
        {
            _collider.enabled = enabled;
            Debug.Log("[StampZone] collider " + (enabled ? "ACTIVADO" : "DESACTIVADO"));
        }
    }

    public void ResetStamp()
    {
        _stampApplied = false;
        if (stampPreview != null)
            stampPreview.enabled = false;
        Debug.Log("[StampZone] ResetStamp ejecutado");
    }
}
