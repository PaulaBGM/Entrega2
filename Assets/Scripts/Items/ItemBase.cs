using System;
using System.Collections;
using Interfaces;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Items
{
    [RequireComponent(typeof(SmoothTransform))]
    public abstract class ItemBase : MonoBehaviour, ICollectable, IInteractable, ISelectable
    {
        private bool _isCollected = false;
        private Collider2D _collider;
    
        private SmoothTransform _smoothTransform;
        
        public event Action OnCollect;
        public event Action OnUncollect;

        protected void Awake()
        {
            _smoothTransform = GetComponent<SmoothTransform>();
            _collider = GetComponent<Collider2D>();
        }

        public void Collect()
        {
            _isCollected = true;
            StartCoroutine(CollectedTick());
            _smoothTransform.ScaleSmooth(new Vector3(1.5f, 1.5f, 1.5f));
            
            OnCollect?.Invoke();
        }

        public virtual void Uncollect()
        {
            _isCollected = false;
            _smoothTransform.ScaleSmooth(Vector3.one);
            
            OnUncollect?.Invoke();
        }

        public Collider2D GetCollider() => _collider;

        public virtual void Interact()
        {
            Debug.Log("Interacting TestItem");
        }

        public void Select()
        { 
            Collect();
            _collider.enabled = false;
        }

        public void Deselect()
        {
            Uncollect();
            _collider.enabled = true;
        }

        private IEnumerator CollectedTick()
        {
            while (_isCollected)
            {
                _smoothTransform.MoveSmooth(
                    Camera.main!.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
                yield return null;
            }
        }
    }
} 
