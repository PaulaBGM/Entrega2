using UnityEngine;
using Interfaces;

public class WorldItemUIOpener : MonoBehaviour, ISelectable
{
    [SerializeField] private GameObject uiPanel;

    private bool _isSelected;

    private void Start()
    {
        if (uiPanel != null)
            uiPanel.SetActive(false);
    }

    public void Select()
    {
        _isSelected = true;

        if (uiPanel != null)
            uiPanel.SetActive(true);
    }

    public void Deselect()
    {
        _isSelected = false;

        if (uiPanel != null)
            uiPanel.SetActive(false);
    }
}
