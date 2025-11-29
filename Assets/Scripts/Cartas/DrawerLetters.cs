using ArtWorks;
using Interfaces;
using UnityEngine;

public class DrawerLetters : MonoBehaviour, IInteractable
{

    private void Awake()
    {

    }

    public void Interact()
    {
        CaseData caseData = CaseManager.Instance.GetCurrentCase();

        string fullText =
            $"{caseData.title}\n\n{caseData.description}";

        LetterUIController.Instance.ShowLetter_WithCallback(
            fullText,
            () => ArtworkSpawner.Instance.SpawnArtworkForCurrentCase()
        );
    }
}
