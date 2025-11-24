using System;
using ArtWorks;
using Managers;
using ScriptableObjects.GameAttributes;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private int startReputation;
    [SerializeField] private int startEthic;
    [SerializeField] private int startFunds;
    
    private int _currentReputation;
    private int _currentEthic;
    private int _currentFunds;

    private void OnEnable()
    {
        GameManager.Instance.SubscribeToOnArtworkEvaluated(HandleOnArtworkEvaluated);
    }

    private void Start()
    {
        _currentReputation = startReputation;
        _currentEthic = startEthic;
        _currentFunds = startFunds;
    }

    private void HandleOnArtworkEvaluated(ArtWork artWork, bool hasPassed)
    {
        UpdateStatus(hasPassed ? artWork.AcceptAttributes : artWork.RejectAttributes);
    }

    public void UpdateStatus(GameAttributesDataSO gameAttributesData)
    {
        _currentReputation = Math.Clamp(_currentReputation + gameAttributesData.Reputation, 0, 100);
        _currentEthic = Math.Clamp(_currentEthic + gameAttributesData.Ethic, 0, 100);
        _currentFunds = Math.Clamp(_currentFunds + gameAttributesData.Funds, 0, 100);
        Debug.Log("Player Status Updated: " +
                  $"Reputation: {_currentReputation}, " +
                  $"Ethic: {_currentEthic}, " +
                  $"Funds: {_currentFunds}");
    }
    
    private void OnDisable()
    {
        GameManager.Instance.UnsubscribeToOnArtworkEvaluated(HandleOnArtworkEvaluated);
    }
}
