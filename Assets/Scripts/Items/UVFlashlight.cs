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
            GameManager.Instance.GetCurrentArtWork()?.UpdateHotspots(Hotspot.HotspotsType.UV);   
        }

        public override void Uncollect()
        {
            if (_uvLightOn) 
                ToggleUVLight();
            
            base.Uncollect();
        }
    }
}

