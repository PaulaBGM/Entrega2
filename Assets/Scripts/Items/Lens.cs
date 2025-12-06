using System;
using ArtWorks;
using Managers;
using UnityEngine;

public class Lens : MonoBehaviour
{
    [SerializeField] private Transform smallSheet;
    [SerializeField] private Transform bigSheet;

    private bool triedFindingSmall = false;
    private bool triedFindingBig = false;

    private void OnEnable()
    {
        GameManager.Instance.OnArtworkAssigned += HandleOnArtworkAssigned;
    }

    private void HandleOnArtworkAssigned(ArtWork artWork)
    {
        smallSheet = artWork.SmallSheet;
        bigSheet = artWork.BigSheet;
    }

    void Update()
    {
        // // Buscar smallSheet si a�n no est� asignado
        // if (smallSheet == null && !triedFindingSmall)
        // {
        //     var obj = GameObject.FindWithTag("SmallSheet");
        //     if (obj != null)
        //     {
        //         smallSheet = obj.transform;
        //     }
        //     else
        //     {
        //         triedFindingSmall = true; // evitar buscar cada frame hasta que reaparezca
        //     }
        // }
        //
        // // Buscar bigSheet si a�n no est� asignado
        // if (bigSheet == null && !triedFindingBig)
        // {
        //     var obj = GameObject.FindWithTag("BigSheet");
        //     if (obj != null)
        //     {
        //         bigSheet = obj.transform;
        //     }
        //     else
        //     {
        //         triedFindingBig = true;
        //     }
        // }

        // Si no est�n listos, no hacemos c�lculo ni error
        if (smallSheet == null || bigSheet == null)
            return;

        // C�lculo principal cuando ya existen los dos transforms
        bigSheet.position = smallSheet.position * 2 - transform.position;
    }
    
    private void OnDisable()
    {
        GameManager.Instance.OnArtworkAssigned -= HandleOnArtworkAssigned;
    }
}
