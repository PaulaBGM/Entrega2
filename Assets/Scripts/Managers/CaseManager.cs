using Managers;
using UnityEngine;

public class CaseManager : MonoBehaviour
{
    public static CaseManager Instance;

    public CaseDayData dayData;

    private int currentCaseIndex = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        GameManager.Instance.SubscribeToOnArtworkEvaluated(OnArtworkEvaluated);
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.UnsubscribeToOnArtworkEvaluated(OnArtworkEvaluated);
    }

    public CaseData GetCurrentCase()
    {
        return dayData.cases[currentCaseIndex];
    }

    private void OnArtworkEvaluated(ArtWorks.ArtWork artwork, bool hasPassed)
    {
        var caseData = GetCurrentCase();
        bool isCorrect = caseData.isGenuine == hasPassed;
        GoToNextCase();
    }

    public bool HasMoreCases()
    {
        return currentCaseIndex < dayData.cases.Count - 1;
    }

    public void GoToNextCase()
    {
        if (!HasMoreCases())
            return;

        currentCaseIndex++;
    }
}
