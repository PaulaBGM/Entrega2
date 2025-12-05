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

        Collider2D hit = Physics2D.OverlapPoint(position2D);

        if (hit != null && hit.TryGetComponent(out StampZone zone))
        {
            if (zone.IsOverZone(position2D))
            {
                zone.ApplyStamp(isApproveStamp);
            }
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
