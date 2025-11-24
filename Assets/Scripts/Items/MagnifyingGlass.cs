using UnityEngine;
using UnityEngine.UI;
using Items;
using UnityEngine.InputSystem; // necesario para Mouse.current

public class MagnifyingGlass : ItemBase
{
    [Header("Magnifying Settings")]
    [SerializeField] private Camera zoomCamera;
    [SerializeField] private RawImage zoomImage;
    [SerializeField] private RectTransform lensRect;

    [SerializeField] private float zoomFactor = 2f;
    [SerializeField] private float lensRadius = 120f;

    private Collider2D targetCollider;
    private bool isActive = false;

    protected void Start()
    {
        // No override: ItemBase.Awake() ya funciona.
        lensRect.gameObject.SetActive(false);

        if (zoomCamera != null)
            zoomCamera.orthographicSize /= zoomFactor;
    }

    private void Update()
    {
        if (!isActive) return;

        Vector2 mouseWorldPos =
            Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        // Mover la lupa en UI
        lensRect.position = Mouse.current.position.ReadValue();

        // Mover cámara de zoom
        zoomCamera.transform.position =
            new Vector3(mouseWorldPos.x, mouseWorldPos.y, zoomCamera.transform.position.z);

        // Si salimos de la obra, se desactiva
        if (targetCollider != null && !targetCollider.OverlapPoint(mouseWorldPos))
            DisableLens();
    }

    // -------------------------
    //   OVERRIDE PERMITIDO
    // -------------------------
    public override void Interact()
    {
        Debug.Log("Lupa activada.");
    }

    // -------------------------
    //   SELECT / DESELECT
    //   (NO override, NO virtual)
    // -------------------------
    public new void Select()
    {
        base.Select();
        EnableLens();
    }

    public new void Deselect()
    {
        base.Deselect();
        DisableLens();
    }

    // -------------------------
    //   LÓGICA DE LA LUPA
    // -------------------------
    private void EnableLens()
    {
        lensRect.sizeDelta = new Vector2(lensRadius * 2f, lensRadius * 2f);
        lensRect.gameObject.SetActive(true);
        isActive = true;
    }

    private void DisableLens()
    {
        lensRect.gameObject.SetActive(false);
        isActive = false;
        targetCollider = null;
    }

    public void SetTargetCollider(Collider2D col)
    {
        targetCollider = col;
    }
}
