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
        protected bool _isCollected = false;
        protected Collider2D _collider;
        protected SmoothTransform _smoothTransform;

        public event Action OnCollect;
        public event Action OnUncollect;

        protected virtual void Awake()
        {
            _smoothTransform = GetComponent<SmoothTransform>();
            _collider = GetComponent<Collider2D>();
        }

        public virtual void Collect()
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

        public virtual Collider2D GetCollider() => _collider;

        public virtual void Interact() { }

        public virtual void Select()
        {
            Collect();
        }

        public virtual void Deselect()
        {
            Uncollect();
        }

        protected IEnumerator CollectedTick()
        {
            while (_isCollected)
            {
                Vector3 mousePos = Camera.main!.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                mousePos.z = 0;
                _smoothTransform.MoveSmooth(mousePos);
                yield return null;
            }
        }
    }
}
