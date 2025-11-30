using UnityEngine;

public class Mailbox : MonoBehaviour
{
    public static Mailbox Instance;
    private Collider2D _collider;

    private void Awake()
    {
        Instance = this;
        _collider = GetComponent<Collider2D>();
    }

    public bool IsOverZone(Vector2 pos)
    {
        return _collider.OverlapPoint(pos);
    }
}
