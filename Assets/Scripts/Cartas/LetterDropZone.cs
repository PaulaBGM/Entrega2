using UnityEngine;

public class LetterDropZone : MonoBehaviour
{
    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    public bool IsOverZone(Vector2 position)
    {
        return _collider.OverlapPoint(position);
    }
}
