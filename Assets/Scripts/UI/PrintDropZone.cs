using UnityEngine;

public class PrinterDropZone : MonoBehaviour
{
    [SerializeField] private GameObject letterPanelPrefab;
    [SerializeField] private Vector3 panelOffset = new Vector3(1f, 0f, 0f);

    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    public bool IsOverZone(Vector2 position)
    {
        return _collider.OverlapPoint(position);
    }

    public void TryProcessLetter(CaseLetter letter, Vector2 mousePos)
    {
        if (!IsOverZone(mousePos))
            return;

        if (letter.State == CaseLetter.LetterState.Open)
        {
            letter.SealLetter();
            CreateGrayscaleCopy();
        }
    }

    private void CreateGrayscaleCopy()
    {
        if (letterPanelPrefab == null)
            return;

        GameObject copy = Instantiate(
            letterPanelPrefab,
            transform.position + panelOffset,
            Quaternion.identity
        );

        SpriteRenderer sr = copy.GetComponentInChildren<SpriteRenderer>();

        if (sr != null)
        {
            Material m = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
            m.color = new Color(0.3f, 0.3f, 0.3f);
            sr.material = m;
        }
    }
}
