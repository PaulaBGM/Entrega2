using UnityEngine;

public class Lens : MonoBehaviour
{
    [SerializeField] private Transform smallSheet;
    [SerializeField] private Transform bigSheet;

    private bool triedFindingSmall = false;
    private bool triedFindingBig = false;

    void Update()
    {
        // Buscar smallSheet si aún no está asignado
        if (smallSheet == null && !triedFindingSmall)
        {
            var obj = GameObject.FindWithTag("SmallSheet");
            if (obj != null)
            {
                smallSheet = obj.transform;
            }
            else
            {
                triedFindingSmall = true; // evitar buscar cada frame hasta que reaparezca
            }
        }

        // Buscar bigSheet si aún no está asignado
        if (bigSheet == null && !triedFindingBig)
        {
            var obj = GameObject.FindWithTag("BigSheet");
            if (obj != null)
            {
                bigSheet = obj.transform;
            }
            else
            {
                triedFindingBig = true;
            }
        }

        // Si no están listos, no hacemos cálculo ni error
        if (smallSheet == null || bigSheet == null)
            return;

        // Cálculo principal cuando ya existen los dos transforms
        bigSheet.position = smallSheet.position * 2 - transform.position;
    }
}
