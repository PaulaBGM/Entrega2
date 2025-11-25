using ArtWorks;
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

    private void OnArtworkEvaluated(ArtWork artwork, bool hasPassed)
    {
        CaseData currentCase = GetCurrentCase();

        bool correct = (currentCase.isGenuine == hasPassed);

        if (correct)
        {
            //recompensa
            Debug.Log($"Caso {currentCase.caseID} correcto. +{currentCase.rewardIfCorrect}");
            
        }
        else
        {
            Debug.Log($"Caso {currentCase.caseID} incorrecto. -{currentCase.penaltyIfWrong}");
        }

        GoToNextCase();
    }

    public void GoToNextCase()
    {
        currentCaseIndex++;

        if (currentCaseIndex >= dayData.cases.Count)
        {
            Debug.Log("Dia completado. No hay mas casos.");
            // Evento de fin de dia/resumen
            return;
        }

        Debug.Log($"Siguiente caso: {dayData.cases[currentCaseIndex].caseID}");

        // Cargar la carta del siguiente caso
        var nextCase = GetCurrentCase();
        LetterUIController.Instance.ShowLetter(nextCase.description);

        LetterUIController.Instance.OnLetterClosed = () =>
        {
            //CargarObra
        };
    }
}
