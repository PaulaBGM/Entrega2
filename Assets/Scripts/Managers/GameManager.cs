using System;
using ArtWorks;
using Attributes;
using UnityEngine;

namespace Managers
{
    [DefaultExecutionOrder(-100)]
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        private Action<ArtWork, bool> _onArtworkEvaluated;
        private ArtWork _currentArtwork;

        [field: SerializeField] public PlayerStatus PlayerStatus { get; private set; }

        public Action<ArtWork> OnArtworkAssigned;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
            {
                Instance = this;
                Debug.Log("[GameManager] Inicializado.");
            }
        }

        public void SetCurrentArtWork(ArtWork artWork)
        {
            _currentArtwork = artWork;
            Debug.Log("[GameManager] SetCurrentArtWork: " + artWork);
            
            OnArtworkAssigned?.Invoke(artWork);
        }

        public ArtWork GetCurrentArtWork() => _currentArtwork;

        public void ArtworkEvaluated(bool hasPassed)
        {
            Debug.Log("[GameManager] ArtworkEvaluated llamado. hasPassed=" + hasPassed + " | currentArtwork=" + _currentArtwork);

            if (_currentArtwork == null)
            {
                Debug.LogError("[GameManager] ERROR: _currentArtwork es NULL en ArtworkEvaluated()");
            }

            _onArtworkEvaluated?.Invoke(_currentArtwork, hasPassed);
        }

        public void ArtworkEvaluated(CaseData caseData, bool hasPassed)
        {
            Debug.Log("[GameManager] ArtworkEvaluated (CaseData) llamado. case=" + (caseData != null ? caseData.caseID : "NULL") + " hasPassed=" + hasPassed);

            if (caseData == null)
            {
                Debug.LogError("[GameManager] ERROR: caseData es NULL en ArtworkEvaluated(CaseData)");
                _onArtworkEvaluated?.Invoke(null, hasPassed);
                return;
            }

            var attrs = hasPassed ? caseData.acceptConsequences : caseData.rejectConsequences;

            if (PlayerStatus == null)
            {
                Debug.LogError("[GameManager] ERROR: PlayerStatus es NULL al aplicar consecuencias directas");
            }
            else
            {
                PlayerStatus.UpdateStatus(attrs);
                Debug.Log("[GameManager] PlayerStatus actualizado desde CaseData: " + attrs);
            }

            _onArtworkEvaluated?.Invoke(null, hasPassed);
        }

        public void SubscribeToOnArtworkEvaluated(Action<ArtWork, bool> actionToSubscribe)
        {
            Debug.Log("[GameManager] Subscrito a OnArtworkEvaluated: " + actionToSubscribe.Method.Name);
            _onArtworkEvaluated += actionToSubscribe;
        }

        public void UnsubscribeToOnArtworkEvaluated(Action<ArtWork, bool> actionToUnsubscribe)
        {
            _onArtworkEvaluated -= actionToUnsubscribe;
        }
    }
}
