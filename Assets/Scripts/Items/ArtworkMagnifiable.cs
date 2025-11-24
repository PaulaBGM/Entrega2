using UnityEngine;

public class ArtworkMagnifiable : MonoBehaviour
{
    private MagnifyingGlass magnifier;

    private void Start()
    {
        magnifier = Object.FindFirstObjectByType<MagnifyingGlass>();
        // magnifier = Object.FindAnyObjectByType<MagnifyingGlass>();
    }

    private void OnMouseEnter()
    {
        magnifier.SetTargetCollider(GetComponent<Collider2D>());
    }

    private void OnMouseExit()
    {
        magnifier.SetTargetCollider(null);
    }
}
