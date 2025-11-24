using System;
using ArtWorks;
using UnityEngine;

namespace Managers
{
    [DefaultExecutionOrder(-100)]
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
    
        private Action<ArtWork, bool> _onArtworkEvaluated;

        private ArtWork _currentArtwork;

        private void Awake()
        {
            if (Instance !=null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }
    
        public void SetCurrentArtWork(ArtWork artWork)
        {
            _currentArtwork = artWork;
        }

        public void ArtworkEvaluated(bool hasPassed)
        {
            _onArtworkEvaluated?.Invoke(_currentArtwork, hasPassed);
        }

        #region GameManager Subscriptions

        public void SubscribeToOnArtworkEvaluated(Action<ArtWork ,bool> actionToSubscribe) =>
            _onArtworkEvaluated += actionToSubscribe;
        public void UnsubscribeToOnArtworkEvaluated(Action<ArtWork, bool> actionToSubscribe) =>
            _onArtworkEvaluated += actionToSubscribe;

        #endregion
    }
}
