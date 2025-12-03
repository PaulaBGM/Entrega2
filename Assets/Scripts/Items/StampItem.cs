using UnityEngine;
using Items;
using UnityEngine.InputSystem;

public class StampItem : ItemBase
{
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
