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

        private void OnDisable()
        {
            if (_playerStatus != null)
                _playerStatus.UnsubscribeToOnStatusUpdated(UpdateStatusTMP);
        }

        public void RefreshStatus()
        {
            var status = _playerStatus.GetCurrentStatus();
            UpdateStatusTMP(status);
        }

        private void UpdateStatusTMP((int reputation, int ethic, int funds) status)
        {
            if (fundsTMP != null) fundsTMP.text = $"{status.funds}€";
            if (ethicTMP != null) ethicTMP.text = $"{status.ethic}%";
            if (reputationTMP != null) reputationTMP.text = $"{status.reputation}%";
        }

        public void RefreshCaseInfo()
        {
            var caseData = CaseManager.Instance.GetCaseAt(_uiCaseIndex);
            if (caseData == null)
                return;

            if (letterTitleTMP != null)
                letterTitleTMP.text = caseData.title;

            if (letterBodyTMP != null)
                letterBodyTMP.text = caseData.description;

            if (artworkImage != null)
            {
                var prefab = caseData.artWorkPrefab;
                if (prefab != null)
                {
                    var sr = prefab.GetComponentInChildren<SpriteRenderer>();
                    artworkImage.sprite = sr != null ? sr.sprite : null;
                }
                else
                {
                    artworkImage.sprite = null;
                }
            }

            bool result = CaseManager.Instance.GetCaseResult(_uiCaseIndex);

            if (resultTMP != null)
            {
                resultTMP.text = result ? "Correcto" : "Incorrecto";
                resultTMP.color = result ? Color.green : Color.red;
            }
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
            if (previousCaseButton != null)
                previousCaseButton.interactable = _uiCaseIndex > 0;

            if (nextCaseButton != null)
                nextCaseButton.interactable = _uiCaseIndex < CaseManager.Instance.GetCaseCount() - 1;
        }

        public void CloseBook()
        {
            gameObject.SetActive(false);
        }
    }
}
