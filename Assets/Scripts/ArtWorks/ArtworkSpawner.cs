using Managers;
using UnityEngine;

namespace ArtWorks
{
    public class ArtworkSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] artworkPrefabs; //PROVISIONAL, CAMBIAR POR LISTA QUE SE VACÍA PARA EL SISTEMA DE DIA
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Transform objectivePoint;
        
        private ArtWork _artWorkSpawned;
        
        private void OnEnable()
        {
            GameManager.Instance.SubscribeToOnArtworkEvaluated(HandleOnArtworkEvaluated);
        }
        
        private void Start()
        {
            SpawnNextArtwork();
        }

        private void HandleOnArtworkEvaluated(ArtWork _, bool __)
        {
            SpawnNextArtwork();
        }

        public void SpawnNextArtwork()
        {
            if (_artWorkSpawned != null)
            {
                Destroy(_artWorkSpawned.gameObject);
            }
            
            _artWorkSpawned = Instantiate(
                artworkPrefabs[Random.Range(0, artworkPrefabs.Length)], 
                spawnPoint.position, 
                Quaternion.identity).
                GetComponent<ArtWork>();
            
            _artWorkSpawned.StartSpawnBehavior(objectivePoint.position);
            
            GameManager.Instance.SetCurrentArtWork(_artWorkSpawned);
        }
        
        private void OnDisable()
        {
            GameManager.Instance.UnsubscribeToOnArtworkEvaluated(HandleOnArtworkEvaluated);
        }
    }
}
