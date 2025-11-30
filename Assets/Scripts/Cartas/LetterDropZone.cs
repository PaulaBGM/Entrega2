using UnityEngine;

public class LetterDropZone : MonoBehaviour
{
    public static LetterDropZone Instance;
    private Collider2D _collider;

    private void Awake()
    {
        Instance = this;
        _collider = GetComponent<Collider2D>();
    }

    public bool IsOverZone(Vector2 position)
    {
        return _collider.OverlapPoint(position);
    }
}
