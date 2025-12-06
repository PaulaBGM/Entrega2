using ArtWorks;
using Items;
using Managers;

public class MagnifyingGlass : ItemBase
{
    public override void Collect()
    {
        base.Collect();
        GameManager.Instance.GetCurrentArtWork()?.UpdateHotspots(Hotspot.HotspotsType.Zoom);
    }
}
