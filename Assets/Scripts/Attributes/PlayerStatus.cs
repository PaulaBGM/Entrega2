using System;
using ArtWorks;
using Managers;
using ScriptableObjects.GameAttributes;
using UnityEngine;

namespace Attributes
{
    public class PlayerStatus : MonoBehaviour
    {
        [Range(0, 100)]
        [SerializeField] private int startReputation;
        [Range(0, 100)]
        [SerializeField] private int startEthic;
        [Range(0, 1000000)]
        [SerializeField] private int startFunds;
    
        private int _currentReputation;
        private int _currentEthic;
        private int _currentFunds;

        private Action<(int reputation, int ethic, int funds)> _onStatusUpdated;

        private void OnEnable()
        {
            GameManager.Instance?.SubscribeToOnArtworkEvaluated(HandleOnArtworkEvaluated);
        }

        private void Start()
        {
            _currentReputation = startReputation;
            _currentEthic = startEthic;
            _currentFunds = startFunds;
        }
        
        public (int reputation, int ethic, int funds) GetInitialStatus() =>
            (startReputation, startEthic, startFunds);

        private void HandleOnArtworkEvaluated(ArtWork artWork, bool hasPassed)
        {
            UpdateStatus(hasPassed ? artWork.AcceptAttributes : artWork.RejectAttributes);
        }

        public void UpdateStatus(GameAttributesDataSo gameAttributesData)
        {
            _currentReputation = Math.Clamp(_currentReputation + gameAttributesData.Reputation, 0, 100);
            _currentEthic = Math.Clamp(_currentEthic + gameAttributesData.Ethic, 0, 100);
            _currentFunds = Math.Clamp(_currentFunds + gameAttributesData.Funds, 0, 1000000);
            Debug.Log("Player Status Updated: " +
                      $"Reputation: {_currentReputation}, " +
                      $"Ethic: {_currentEthic}, " +
                      $"Funds: {_currentFunds}");
            
            _onStatusUpdated?.Invoke((_currentReputation, _currentEthic, _currentFunds));
        }
        
        public void SubscribeToOnStatusUpdated(Action<(int reputation, int ethic, int funds)> actionToSubscribe) =>
            _onStatusUpdated += actionToSubscribe;
        public void UnsubscribeToOnStatusUpdated(Action<(int reputation, int ethic, int funds)> actionToSubscribe) =>
            _onStatusUpdated -= actionToSubscribe;
    
        private void OnDisable()
        {
            GameManager.Instance?.UnsubscribeToOnArtworkEvaluated(HandleOnArtworkEvaluated);
        }
    }
}
