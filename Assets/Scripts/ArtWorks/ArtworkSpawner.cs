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
        }

        public ArtWork GetCurrentArtwork()
        {
            return _artWorkSpawned;
        }

        public void ClearCurrentArtwork()
        {
            _artWorkSpawned = null;
        }

        public void SpawnArtworkForCurrentCase(CaseData caseData)
        {
            if (_artWorkSpawned != null)
                Destroy(_artWorkSpawned.gameObject);

            if (caseData == null || caseData.artWorkPrefab == null)
                return;

            GameObject obj = Instantiate(caseData.artWorkPrefab, spawnPoint.position, Quaternion.identity);

            _artWorkSpawned = obj.GetComponent<ArtWork>();

            if (_artWorkSpawned != null)
            {
                _artWorkSpawned.SetupFromCase(caseData);
                _artWorkSpawned.StartSpawnBehavior(objectivePoint.position);
            }
            
            GameManager.Instance?.SetCurrentArtWork(_artWorkSpawned);
        }
    }
}
