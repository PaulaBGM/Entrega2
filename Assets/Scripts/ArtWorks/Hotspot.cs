using System;
using TMPro;
using UnityEngine;

public class Hotspot : MonoBehaviour
{
    [SerializeField] private RenderingLayerMask revealLayer;

    [Header("Other components")]
    private SpriteRenderer _spriteRenderer;
    
    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        
        _spriteRenderer.renderingLayerMask = revealLayer;
    }
}
