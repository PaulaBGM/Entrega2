using UnityEngine;
using Interfaces;

public class DrawerLetters : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        CaseData caseData = CaseManager.Instance.GetCurrentCase();
        Debug.Log($"Carta abierta del caso: {caseData.caseID}");

        //Mostrar Carta
        //Cerrar carta
        //Cargar obra

    }
}
