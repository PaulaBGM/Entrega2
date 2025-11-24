using UnityEngine;
using UnityEngine.InputSystem;
using Items;

public class MagnifyingGlass : ItemBase
{
    private Vector2 dragOffset;

    private void OnMouseDown()
    {
        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        dragOffset = mouseWorld - new Vector2(transform.position.x, transform.position.y);
    }

    private void OnMouseDrag()
    {
        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        transform.position = mouseWorld - dragOffset;
    }
}
