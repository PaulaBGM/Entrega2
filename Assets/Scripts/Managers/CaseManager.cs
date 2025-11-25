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
        CaseData caseData = GetCurrentCase();

        bool isCorrect = (caseData.isGenuine == hasPassed);

        if (isCorrect)
        {
            Debug.Log($"Caso {caseData.caseID} correcto. Recompensa: +{caseData.rewardIfCorrect}");
        }
        else
        {
            Debug.Log($"Caso {caseData.caseID} incorrecto. Penalización: -{caseData.penaltyIfWrong}");
        }

        GoToNextCase();
    }

    public bool HasMoreCases()
    {
        return currentCaseIndex < dayData.cases.Count - 1;
    }

    public void GoToNextCase()
    {
        if (!HasMoreCases())
        {
            Debug.Log("Día completado. No hay más casos.");
            //Sacar resumen del dia

            return;
        }

        currentCaseIndex++;
        Debug.Log($"Avanzando al siguiente caso: {dayData.cases[currentCaseIndex].caseID}");
    }
}
