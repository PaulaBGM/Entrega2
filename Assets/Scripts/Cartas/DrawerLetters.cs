using UnityEngine;
using Interfaces;

public class DrawerLetters : MonoBehaviour, IInteractable
{
    [TextArea] public string testLetterContent;

    public void Interact()
    {
        LetterUIController.Instance.ShowLetter(testLetterContent);

        LetterUIController.Instance.OnLetterClosed = () =>
        {
            //CargarObra
        };
    }
}
