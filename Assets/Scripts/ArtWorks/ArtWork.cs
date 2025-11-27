using System.Collections;
using Interfaces;
using ScriptableObjects.GameAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ArtWorks
{
    [RequireComponent(typeof(SmoothTransform))]
    public class ArtWork : MonoBehaviour, ISelectable
    {
        [Header("Atributos del Sistema de Decisión")]
        [field: SerializeField] public GameAttributesDataSo AcceptAttributes { get; private set; }
        [field: SerializeField] public GameAttributesDataSo RejectAttributes { get; private set; }

        [Header("Referencias Visuales")]
        [SerializeField] private SpriteRenderer spriteRenderer;
        // Asigna esto en el prefab del ArtWork si usas sprites
        // Si tu obra es 3D, puedes dejarlo vacío o sustituir por MeshRenderer

        // Datos del Case
        public CaseData CaseData { get; private set; }
        public bool IsGenuine { get; private set; }

        private bool _isCollected;
        private bool _isSelectable = true;

        private SmoothTransform _smoothTransform;

        private void Awake()
        {
            _smoothTransform = GetComponent<SmoothTransform>();

            if (spriteRenderer == null)
                spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        public void SetupFromCase(CaseData data)
        {
            CaseData = data;
            IsGenuine = data.isGenuine;

            if (spriteRenderer != null && data.artworkSprite != null)
            {
                spriteRenderer.sprite = data.artworkSprite;
            }
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
                Vector3 mouseWorldPos =
                    Camera.main!.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                mouseWorldPos.z = 0;

                _smoothTransform.MoveSmooth(mouseWorldPos);
                yield return null;
            }
        }
    }
}
