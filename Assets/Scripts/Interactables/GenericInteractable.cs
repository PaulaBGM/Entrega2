using Interactables;
using UnityEngine;

public class GenericInteractable : MonoBehaviour, IInteractable
{
    public void Select()
    {
        Debug.Log($"{gameObject.name}: Selected");
    }

    public void Interact()
    {
        Debug.Log($"{gameObject.name}: Interact");
    }
}
