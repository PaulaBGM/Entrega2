using System;
using Attributes;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CaseBookUI : MonoBehaviour
    {
        private PlayerStatus _playerStatus;

        [Header("Status UI")]
        [SerializeField] private TMP_Text fundsTMP;
        [SerializeField] private TMP_Text ethicTMP;
        [SerializeField] private TMP_Text reputationTMP;

        [Header("Artwork UI")]
        [SerializeField] private Image artworkImage;

        [Header("Letter UI")]
        [SerializeField] private TMP_Text letterTitleTMP;
        [SerializeField] private TMP_Text letterBodyTMP;

        [Header("Case Result UI")]
        [SerializeField] private TMP_Text resultTMP;

        [Header("Navigation")]
        [SerializeField] private Button previousCaseButton;
        [SerializeField] private Button nextCaseButton;

        private int _uiCaseIndex = 0;

        private void OnEnable()
        {
            _playerStatus = GameManager.Instance.PlayerStatus;
            _playerStatus.SubscribeToOnStatusUpdated(UpdateStatusTMP);

            _uiCaseIndex = CaseManager.Instance.GetCurrentCaseIndex();

            RefreshStatus();
            RefreshCaseInfo();
            UpdateNavigationButtons();
        }

        public void RefreshStatus()
        {
            var status = _playerStatus.GetCurrentStatus();
            UpdateStatusTMP(status);
        }

        private void UpdateStatusTMP((int reputation, int ethic, int funds) status)
        {
            fundsTMP.text = $"{status.funds}€";
            ethicTMP.text = $"{status.ethic}%";
            reputationTMP.text = $"{status.reputation}%";
        }

        public void RefreshCaseInfo()
        {
            var caseData = CaseManager.Instance.GetCaseAt(_uiCaseIndex);
            if (caseData == null)
                return;

            letterTitleTMP.text = caseData.title;
            letterBodyTMP.text = caseData.description;

            var prefab = caseData.artWorkPrefab;
            if (prefab != null)
            {
                var sr = prefab.GetComponentInChildren<SpriteRenderer>();
                artworkImage.sprite = sr != null ? sr.sprite : null;
            }

            bool result = CaseManager.Instance.GetCaseResult(_uiCaseIndex);
            resultTMP.text = result ? "Correcto" : "Incorrecto";
            resultTMP.color = result ? Color.green : Color.red;
        }

        public void NextCase()
        {
            if (_uiCaseIndex >= CaseManager.Instance.GetCaseCount() - 1)
                return;

            _uiCaseIndex++;
            RefreshCaseInfo();
            UpdateNavigationButtons();
        }

        public void PreviousCase()
        {
            if (_uiCaseIndex <= 0)
                return;

            _uiCaseIndex--;
            RefreshCaseInfo();
            UpdateNavigationButtons();
        }

        private void UpdateNavigationButtons()
        {
            previousCaseButton.interactable = _uiCaseIndex > 0;
            nextCaseButton.interactable = _uiCaseIndex < CaseManager.Instance.GetCaseCount() - 1;
        }

        private void OnDisable()
        {
            _playerStatus.UnsubscribeToOnStatusUpdated(UpdateStatusTMP);
        }
    }
}
