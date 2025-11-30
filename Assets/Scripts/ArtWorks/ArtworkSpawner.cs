using Managers;
using UnityEngine;

namespace ArtWorks
{
    public class ArtworkSpawner : MonoBehaviour
    {
        public static ArtworkSpawner Instance { get; private set; }

        [SerializeField] private GameObject artworkPrefab;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Transform objectivePoint;

        private ArtWork _artWorkSpawned;

        private void Awake()
        {
            Debug.Log("ArtworkSpawner → Awake ejecutado");
            Instance = this;

            Debug.Log($"artworkPrefab asignado? {artworkPrefab != null}");
            Debug.Log($"spawnPoint asignado? {spawnPoint != null}");
            Debug.Log($"objectivePoint asignado? {objectivePoint != null}");
        }

        public void SpawnArtworkForCurrentCase()
        {
            Debug.Log("SpawnArtworkForCurrentCase llamado");

            if (artworkPrefab == null)
            {
                Debug.LogError("❌ ERROR: artworkPrefab es NULL");
                return;
            }

            if (spawnPoint == null)
            {
                Debug.LogError("❌ ERROR: spawnPoint es NULL");
                return;
            }

            if (objectivePoint == null)
            {
                Debug.LogError("❌ ERROR: objectivePoint es NULL");
                return;
            }

            if (_artWorkSpawned != null)
                Destroy(_artWorkSpawned.gameObject);

            CaseData caseData = CaseManager.Instance.GetCurrentCase();
            if (caseData == null)
            {
                Debug.Log("No hay más casos.");
                return;
            }

            Debug.Log("Instanciando obra…");

            GameObject obj = Instantiate(artworkPrefab, spawnPoint.position, Quaternion.identity);
            Debug.Log("Instancia creada correctamente");

            _artWorkSpawned = obj.GetComponentInChildren<ArtWork>();
            Debug.Log($"ArtWork asignado? {_artWorkSpawned != null}");

            _artWorkSpawned.SetupFromCase(caseData);
            _artWorkSpawned.StartSpawnBehavior(objectivePoint.position);

            GameManager.Instance.SetCurrentArtWork(_artWorkSpawned);
        }
    }
}
