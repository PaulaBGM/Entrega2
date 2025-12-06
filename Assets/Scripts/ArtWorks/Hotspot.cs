using UnityEngine;

namespace ArtWorks
{
    public class Hotspot : MonoBehaviour
    {
        public enum HotspotsType
        {
            UV,
            Zoom
        }
    
        [SerializeField] private HotspotsType hotspotsType;
        public HotspotsType HotspotType => hotspotsType;

        [Header("Other components")]
        private SpriteRenderer _spriteRenderer;
    
        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }
}
