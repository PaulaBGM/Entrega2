using System.Collections;
using Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Items
{
    [RequireComponent(typeof(SmoothTransform))]
    public abstract class ItemBase : MonoBehaviour, ICollectable, IInteractable, ISelectable
    {
        private bool _isCollected = false;
    
        private SmoothTransform _smoothTransform;

        protected void Awake()
        {
            _smoothTransform = GetComponent<SmoothTransform>();
        }
    
        public void Collect()
        {
            _isCollected = true;
            StartCoroutine(CollectedTick());
            _smoothTransform.ScaleSmooth(new Vector3(1.5f, 1.5f, 1.5f));
        }

        public virtual void Uncollect()
        {
            _isCollected = false;
            _smoothTransform.ScaleSmooth(Vector3.one);
        }

        public virtual void Interact()
        {
            Debug.Log("Interacting TestItem");
        }

        public void Select()
        { 
            Collect();
        }

        public void Deselect()
        {
            Uncollect();
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
