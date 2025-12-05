using Managers;
using UnityEngine;

namespace ArtWorks
{
    public class ArtworkSpawner : MonoBehaviour
    {
        public static ArtworkSpawner Instance { get; private set; }

        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Transform objectivePoint;

        private ArtWork _artWorkSpawned;

        private void Awake()
        {
            Instance = this;
            Debug.Log("[ArtworkSpawner] Awake");
        }

        public ArtWork GetCurrentArtwork()
        {
            Debug.Log("[ArtworkSpawner] GetCurrentArtwork: " + _artWorkSpawned);
            return _artWorkSpawned;
        }

        public void SpawnArtworkForCurrentCase(CaseData caseData)
        {
            Debug.Log("[ArtworkSpawner] SpawnArtworkForCurrentCase: " + caseData.caseID);

            if (_artWorkSpawned != null)
            {
                Debug.Log("[ArtworkSpawner] Destroy previous");
                Destroy(_artWorkSpawned.gameObject);
            }

            if (caseData == null || caseData.artWorkPrefab == null)
            {
                Debug.LogError("[ArtworkSpawner] CaseData o prefab nulos");
                return;
            }

            GameObject obj = Instantiate(caseData.artWorkPrefab, spawnPoint.position, Quaternion.identity);
            Debug.Log("[ArtworkSpawner] Instantiated: " + obj.name);

            _artWorkSpawned = obj.GetComponent<ArtWork>();

            if (_artWorkSpawned != null)
            {
                Debug.Log("[ArtworkSpawner] SetupFromCase");
                _artWorkSpawned.SetupFromCase(caseData);

                Debug.Log("[ArtworkSpawner] StartSpawnBehavior");
                _artWorkSpawned.StartSpawnBehavior(objectivePoint.position);

                Debug.Log("[ArtworkSpawner] SetCurrentArtWork");
                GameManager.Instance.SetCurrentArtWork(_artWorkSpawned);
            }
            else
            {
                Debug.LogError("[ArtworkSpawner] El prefab no tiene ArtWork");
            }
        }
    }
}
