using UnityEngine;
using ArtWorks;

public class Lens : MonoBehaviour
{
    private SpriteRenderer smallSheet;
    private SpriteRenderer bigSheet;

    private void Start()
    {
        ArtWork art = ArtworkSpawner.Instance.GetCurrentArtwork();
        if (art == null) return;

        SpriteRenderer artRenderer = art.GetComponentInChildren<SpriteRenderer>();
        if (artRenderer == null) return;

        GameObject smallObj = new GameObject("SmallSheet");
        smallObj.transform.SetParent(transform);
        smallObj.transform.position = artRenderer.transform.position;

        smallSheet = smallObj.AddComponent<SpriteRenderer>();
        smallSheet.sprite = artRenderer.sprite;

        GameObject bigObj = new GameObject("BigSheet");
        bigObj.transform.SetParent(transform);
        bigObj.transform.position = smallObj.transform.position;
        bigObj.transform.localScale = Vector3.one * 2f;

        bigSheet = bigObj.AddComponent<SpriteRenderer>();
        bigSheet.sprite = artRenderer.sprite;
    }

    private void Update()
    {
        if (smallSheet == null || bigSheet == null) return;

        bigSheet.transform.position =
            smallSheet.transform.position * 2f - transform.position;
    }
}
