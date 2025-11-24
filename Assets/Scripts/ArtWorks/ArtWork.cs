using System.Collections;
using Interfaces;
using ScriptableObjects.GameAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ArtWorks
{
    [RequireComponent(typeof(SmoothTransform))]
    public class ArtWork : MonoBehaviour, ICollectable, ISelectable
    {
        [field: SerializeField] public GameAttributesDataSO AcceptAttributes { get; private set;}
        [field: SerializeField] public GameAttributesDataSO RejectAttributes { get; private set;}
        
        private bool _isCollected;
        private bool _isSelectable = true;
    
        private SmoothTransform _smoothTransform;

        private void Awake()
        {
            _smoothTransform = GetComponent<SmoothTransform>();
        }
    
        public void Collect()
        {
            _isCollected = true;
            StartCoroutine(CollectedTick());
            _smoothTransform.ScaleSmooth(new Vector3(1.5f, 1.5f, 1.5f));
        }

        public void Uncollect()
        {
            _isCollected = false;
            _smoothTransform.ScaleSmooth(Vector3.one);
        }

        public void Select()
        { 
            if (!_isSelectable)
                return;
            
            Collect();
        }

        public void Deselect()
        {
            Uncollect();
        }
        
        public void StartSpawnBehavior(Vector2 movePosition)
        {
            StartCoroutine(SpawnBehaviorRoutine(movePosition));
        }

        private IEnumerator SpawnBehaviorRoutine(Vector2 movePosition)
        {
            _isSelectable = false;
            
            while ((movePosition - (Vector2)transform.position).sqrMagnitude > 0.01f)
            {
                _smoothTransform.MoveSmooth(movePosition);
                yield return null;
            }

            _isSelectable = true;
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
