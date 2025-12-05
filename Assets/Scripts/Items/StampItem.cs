using UnityEngine;
using Items;
using UnityEngine.InputSystem;

public class StampItem : ItemBase
{
    [SerializeField] private bool isApproveStamp;
    public bool IsApproveStamp => isApproveStamp;

    private bool _isHeld;

    public override void Select()
    {
        base.Select();
        _isHeld = true;
    }

    public override void Deselect()
    {
        base.Deselect();
        _isHeld = false;

        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 position2D = new Vector2(worldPos.x, worldPos.y);

        Collider2D[] hits = Physics2D.OverlapPointAll(position2D);

        StampZone foundZone = null;

        foreach (var h in hits)
        {
            if (h.TryGetComponent(out StampZone zone))
            {
                foundZone = zone;
                break;
            }
        }

        if (foundZone != null && foundZone.IsOverZone(position2D))
        {
            foundZone.ApplyStamp(isApproveStamp);
        }
    }

    private void Update()
    {
        if (_isHeld)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            worldPos.z = 0f;
            transform.position = worldPos;
        }
    }
}
