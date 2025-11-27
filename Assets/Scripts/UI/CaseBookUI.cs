using System;
using Attributes;
using Managers;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CaseBookUI : MonoBehaviour
    {
        private PlayerStatus _playerStatus;
        
        [SerializeField] private TMP_Text fundsTMP;
        [SerializeField] private TMP_Text ethicTMP;
        [SerializeField] private TMP_Text reputationTMP;

        private void OnEnable()
        {
            _playerStatus = GameManager.Instance.PlayerStatus;
            _playerStatus.SubscribeToOnStatusUpdated(UpdateStatusTMP);
        }

        private void Start()
        {
            var (reputation, ethic, funds) = _playerStatus.GetInitialStatus();
            UpdateStatusTMP((reputation, ethic, funds));
        }

        private void UpdateStatusTMP((int reputation, int ethic, int funds) statusData)
        {
            fundsTMP.text = $"{statusData.funds}€";
            ethicTMP.text = $"{statusData.ethic}%";
            reputationTMP.text = $"{statusData.reputation}%";
        }
        
        private void OnDisable()
        {
            _playerStatus.UnsubscribeToOnStatusUpdated(UpdateStatusTMP);
        }
    }
}
