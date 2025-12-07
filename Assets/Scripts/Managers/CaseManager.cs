using Managers;
using UnityEngine;
using ArtWorks;

public class CaseManager : MonoBehaviour
{
    public static CaseManager Instance;

    public CaseDayData dayData;

    private int currentCaseIndex = 0;
    private bool[] caseResults;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        caseResults = new bool[dayData.cases.Count];
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

    public int GetCurrentCaseIndex()
    {
        return currentCaseIndex;
    }

    public CaseData GetCaseAt(int index)
    {
        if (index < 0 || index >= dayData.cases.Count)
            return null;

        return dayData.cases[index];
    }

    public int GetCaseCount()
    {
        return dayData.cases.Count;
    }

    public bool GetCaseResult(int index)
    {
        if (index < 0 || index >= caseResults.Length)
            return false;

        return caseResults[index];
    }

    public void StartCase()
    {
        var caseData = GetCurrentCase();
        //DocumentUIController.Instance.ShowDocuments(caseData, OnDocumentsClosed);
    }

    private void OnDocumentsClosed()
    {
        ArtworkSpawner.Instance.SpawnArtworkForCurrentCase(GetCurrentCase());
    }

    private void OnArtworkEvaluated(ArtWork artwork, bool hasPassed)
    {
        var caseData = GetCurrentCase();
        bool isCorrect = caseData.isGenuine == hasPassed;

        caseResults[currentCaseIndex] = isCorrect;

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
        StartCase();
    }
}
