using ArtWorks;
using Managers;
using UnityEngine;

namespace Items
{
    public sealed class UVFlashlight : ItemBase
    {
        [SerializeField] private GameObject uvSpriteMask;

        private bool _uvLightOn;

        public override void Interact()
        {
            base.Interact();
            ToggleUVLight();
        }

        private void ToggleUVLight()
        {
            _uvLightOn = !_uvLightOn;
            uvSpriteMask.SetActive(_uvLightOn);
        }

        public override void Collect()
        {
            base.Collect();
            
            if (GameManager.Instance.GetCurrentArtWork() is null)
                return;
            
            GameManager.Instance.GetCurrentArtWork()?.UpdateHotspots(Hotspot.HotspotsType.UV);
            GameManager.Instance.GetCurrentArtWork().BigSheet.gameObject.SetActive(false);
            GameManager.Instance.GetCurrentArtWork().SmallSheet.GetComponent<SpriteRenderer>().
                maskInteraction = SpriteMaskInteraction.None;
        }

        public override void Uncollect()
        {
            if (_uvLightOn) 
                ToggleUVLight();
            
            base.Uncollect();
        }
    }
}

