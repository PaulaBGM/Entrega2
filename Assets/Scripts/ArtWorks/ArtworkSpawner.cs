using Managers;
using UnityEngine;

namespace ArtWorks
{
    public class ArtworkSpawner : MonoBehaviour
    {
        [Header("Prefab ÚNICO de Artwork")]
        [SerializeField] private GameObject artworkPrefab;

        [Header("Spawn Settings")]
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Transform objectivePoint;

        private ArtWork _artWorkSpawned;

        private void OnEnable()
        {
            GameManager.Instance?.SubscribeToOnArtworkEvaluated(HandleOnArtworkEvaluated);
        }

        private void Start()
        {
            SpawnArtworkForCurrentCase();
        }

        private void OnDisable()
        {
            GameManager.Instance?.UnsubscribeToOnArtworkEvaluated(HandleOnArtworkEvaluated);
        }

        private void HandleOnArtworkEvaluated(ArtWork _, bool __)
        {
            SpawnArtworkForCurrentCase();
        }

        private void SpawnArtworkForCurrentCase()
        {
            if (_artWorkSpawned != null)
            {
                Destroy(_artWorkSpawned.gameObject);
                _artWorkSpawned = null;
            }

            CaseData caseData = CaseManager.Instance.GetCurrentCase();
            if (caseData == null)
            {
                Debug.Log("No hay más casos. Día completado.");
                return;
            }

            GameObject obj = Instantiate(
                artworkPrefab,
                spawnPoint.position,
                Quaternion.identity
            );

            _artWorkSpawned = obj.GetComponent<ArtWork>();
            if (_artWorkSpawned == null)
            {
                Debug.LogError("El prefab Artwork no contiene el componente ArtWork.");
                return;
            }

            _artWorkSpawned.SetupFromCase(caseData);

            _artWorkSpawned.StartSpawnBehavior(objectivePoint.position);

            GameManager.Instance?.SetCurrentArtWork(_artWorkSpawned);

            Debug.Log($"Spawned artwork for case: {caseData.caseID}");
        }
    }
}
