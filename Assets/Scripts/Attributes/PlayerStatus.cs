using System;
using ArtWorks;
using Managers;
using ScriptableObjects.GameAttributes;
using UnityEngine;

namespace Attributes
{
    public class PlayerStatus : MonoBehaviour
    {
        [SerializeField] private int startReputation;
        [SerializeField] private int startEthic;
        [SerializeField] private int startFunds;

        private int _currentReputation;
        private int _currentEthic;
        private int _currentFunds;

        private Action<(int reputation, int ethic, int funds)> _onStatusUpdated;

        private void OnEnable()
        {
            Debug.Log("[PlayerStatus] OnEnable");
            GameManager.Instance.SubscribeToOnArtworkEvaluated(Handle);
        }

        private void Start()
        {
            Debug.Log("[PlayerStatus] Start");
            _currentReputation = startReputation;
            _currentEthic = startEthic;
            _currentFunds = startFunds;
        }

        public (int reputation, int ethic, int funds) GetInitialStatus()
        {
            return (startReputation, startEthic, startFunds);
        }

        private void Handle(ArtWork artWork, bool passed)
        {
            Debug.Log("[PlayerStatus] Handle artWork=" + artWork + " passed=" + passed);

            if (artWork == null)
            {
                Debug.LogError("[PlayerStatus] ERROR artWork NULL");
                return;
            }

            UpdateStatus(passed ? artWork.AcceptAttributes : artWork.RejectAttributes);
        }

        public void UpdateStatus(GameAttributesDataSo d)
        {
            if (d == null)
            {
                Debug.LogError("[PlayerStatus] ERROR data NULL");
                return;
            }

            _currentReputation = Mathf.Clamp(_currentReputation + d.Reputation, 0, 100);
            _currentEthic = Mathf.Clamp(_currentEthic + d.Ethic, 0, 100);
            _currentFunds = Mathf.Clamp(_currentFunds + d.Funds, 0, 1_000_000);

            Debug.Log("[PlayerStatus] Updated rep=" + _currentReputation + " ethic=" + _currentEthic + " funds=" + _currentFunds);

            _onStatusUpdated?.Invoke((_currentReputation, _currentEthic, _currentFunds));
        }

        public void SubscribeToOnStatusUpdated(Action<(int reputation, int ethic, int funds)> a)
        {
            _onStatusUpdated += a;
        }

        public void UnsubscribeToOnStatusUpdated(Action<(int reputation, int ethic, int funds)> a)
        {
            _onStatusUpdated -= a;
        }

        private void OnDisable()
        {
            Debug.Log("[PlayerStatus] OnDisable");
            GameManager.Instance.UnsubscribeToOnArtworkEvaluated(Handle);
        }
    }
}
