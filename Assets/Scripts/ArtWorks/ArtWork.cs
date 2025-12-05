using System;
using System.Collections;
using Interfaces;
using ScriptableObjects.GameAttributes;
using UnityEngine;
using UnityEngine.InputSystem;
using Managers;

namespace ArtWorks
{
    [RequireComponent(typeof(SmoothTransform))]
    public class ArtWork : MonoBehaviour, ICollectable, ISelectable
    {
        [field: SerializeField] public GameAttributesDataSo AcceptAttributes { get; private set; }
        [field: SerializeField] public GameAttributesDataSo RejectAttributes { get; private set; }

        private bool _isCollected;
        private bool _isSelectable = true;

        private SmoothTransform _smoothTransform;
        private Collider2D _collider;

        [SerializeField] private SpriteRenderer spriteRenderer;

        public CaseData CaseData { get; private set; }
        public bool IsGenuine { get; private set; }

        private void Awake()
        {
            Debug.Log("[ArtWork] Awake: " + gameObject.name);
            _smoothTransform = GetComponent<SmoothTransform>();
            _collider = GetComponent<Collider2D>();

            if (spriteRenderer == null)
                spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        public void SetupFromCase(CaseData data)
        {
            Debug.Log("[ArtWork] SetupFromCase: " + data.caseID);

            CaseData = data;
            IsGenuine = data.isGenuine;

            AcceptAttributes = data.acceptConsequences;
            RejectAttributes = data.rejectConsequences;

            Debug.Log("[ArtWork] Accept=" + AcceptAttributes + " Reject=" + RejectAttributes);

            GameManager.Instance.SetCurrentArtWork(this);
            Debug.Log("[ArtWork] SetCurrentArtWork DONE");
        }

        public void Collect()
        {
            Debug.Log("[ArtWork] Collect");
            _isCollected = true;
            StartCoroutine(CollectedTick());
            _smoothTransform.ScaleSmooth(new Vector3(1.5f, 1.5f, 1.5f));
        }

        public void Uncollect()
        {
            Debug.Log("[ArtWork] Uncollect");
            _isCollected = false;
            _smoothTransform.ScaleSmooth(Vector3.one);
        }

        public Collider2D GetCollider() => _collider;

        public void Select()
        {
            if (!_isSelectable)
                return;

            Debug.Log("[ArtWork] Select");
            Collect();
        }

        public void Deselect()
        {
            Debug.Log("[ArtWork] Deselect");
            Uncollect();
        }

        public void StartSpawnBehavior(Vector2 movePosition)
        {
            Debug.Log("[ArtWork] StartSpawnBehavior");
            StartCoroutine(SpawnRoutine(movePosition));
        }

        private IEnumerator SpawnRoutine(Vector2 movePosition)
        {
            Debug.Log("[ArtWork] SpawnRoutine start");
            _isSelectable = false;

            while ((movePosition - (Vector2)transform.position).sqrMagnitude > 0.01f)
            {
                _smoothTransform.MoveSmooth(movePosition);
                yield return null;
            }

            _isSelectable = true;
            Debug.Log("[ArtWork] SpawnRoutine end");
        }

        private IEnumerator CollectedTick()
        {
            while (_isCollected)
            {
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                mouseWorldPos.z = 0;

                _smoothTransform.MoveSmooth(mouseWorldPos);
                yield return null;
            }
        }
    }
}
