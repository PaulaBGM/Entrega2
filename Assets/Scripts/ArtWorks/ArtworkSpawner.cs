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
            Instance = this;
        }

        public void SpawnArtworkForCurrentCase()
        {
            if (_artWorkSpawned != null)
                Destroy(_artWorkSpawned.gameObject);

            CaseData caseData = CaseManager.Instance.GetCurrentCase();
            if (caseData == null)
            {
                Debug.Log("No hay más casos.");
                return;
            }

            GameObject obj = Instantiate(artworkPrefab, spawnPoint.position, Quaternion.identity);

            _artWorkSpawned = obj.GetComponent<ArtWork>();
            _artWorkSpawned.SetupFromCase(caseData);
            _artWorkSpawned.StartSpawnBehavior(objectivePoint.position);

            GameManager.Instance.SetCurrentArtWork(_artWorkSpawned);
        }
    }
}
