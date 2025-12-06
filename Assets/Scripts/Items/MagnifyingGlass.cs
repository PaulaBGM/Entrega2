using ArtWorks;
using Items;
using Managers;
using UnityEngine;

public class MagnifyingGlass : ItemBase
{
    public override void Collect()
    {
        base.Collect();
        
        if (GameManager.Instance.GetCurrentArtWork() is null)
            return;
        
        GameManager.Instance.GetCurrentArtWork()?.UpdateHotspots(Hotspot.HotspotsType.Zoom);
        GameManager.Instance.GetCurrentArtWork().BigSheet.gameObject.SetActive(true);
        GameManager.Instance.GetCurrentArtWork().SmallSheet.GetComponent<SpriteRenderer>().
            maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
    }
}
