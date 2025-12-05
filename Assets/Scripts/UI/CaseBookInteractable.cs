using UnityEngine;
using Interfaces;

public class CaseBookInteractable : MonoBehaviour, IInteractable, ISelectable
{
    [SerializeField] private GameObject caseBookUIPanel;
    [SerializeField] private SpriteRenderer highlightSprite;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color selectedColor = Color.cyan;

    private bool _isSelected = false;

    public void Interact()
    {
        if (caseBookUIPanel == null)
        {
            Debug.LogError($"[{name}] CaseBookUIPanel no asignado.");
            return;
        }

        bool wasActive = caseBookUIPanel.activeSelf;
        caseBookUIPanel.SetActive(!wasActive);

        if (!wasActive && caseBookUIPanel.TryGetComponent(out UI.CaseBookUI ui))
            ui.RefreshStatus();
    }

    public void Select()
    {
        _isSelected = true;
        if (highlightSprite != null)
            highlightSprite.color = selectedColor;
    }

    public void Deselect()
    {
        _isSelected = false;
        if (highlightSprite != null)
            highlightSprite.color = normalColor;
    }
}
