using ArtWorks;
using Interfaces;
using UnityEngine;

public class CaseLetter : MonoBehaviour, ISelectable, ICollectable, IInteractable
{
    [Header("Datos del caso")]
    public CaseData caseData;

    private bool _isCollected = false;
    private bool _isSelected = false;

    private Collider2D _collider;
    private Vector3 _originalPosition;
    private Transform _originalParent;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _originalParent = transform.parent;
        _originalPosition = transform.position;
    }

    public void Select()
    {
        _isSelected = true;
    }

    public void Deselect()
    {
        _isSelected = false;
    }

    public void Collect()
    {
        _isCollected = true;
        _originalParent = transform.parent;
        transform.SetParent(null);
    }

    public void Uncollect()
    {
        _isCollected = false;
        transform.SetParent(_originalParent);
        transform.position = _originalPosition;
    }

    public Collider2D GetCollider() => _collider;

    public void Interact()
    {
        LetterUIController.Instance.ShowLetter_WithCallback(
            $"{caseData.title}\n\n{caseData.description}",
            () =>
            {
                ArtworkSpawner.Instance.SpawnArtworkForCurrentCase();

                _originalParent = transform.parent;
            }
        );
    }

    private Vector3 _offset;
    private bool _dragging = false;

    private void OnMouseDown()
    {
        Collect();
        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       // mousePos.z = 0;
        //_offset = transform.position - mousePos;
        _dragging = true;
    }

    private void OnMouseDrag()
    {
        if (!_dragging) return;
        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mousePos.z = 0;
        //transform.position = mousePos + _offset;
    }

    private void OnMouseUp()
    {
        _dragging = false;
    }
}
