using ArtWorks;
using Interfaces;
using UnityEngine;

public class DrawerLetters : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        CaseData caseData = CaseManager.Instance.GetCurrentCase();

        Debug.Log($"Carta abierta del caso: {caseData.caseID}");

        string fullText =
            $"{caseData.title}\n\n{caseData.description}";

        LetterUIController.Instance.ShowLetter(fullText);

        LetterUIController.Instance.OnLetterClosed = () =>
        {
            ArtworkSpawner.Instance.SpawnArtworkForCurrentCase();
        };
    }
}
