using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CaseDayData", menuName = "TheExpertsEye/CaseDayData")]
public class CaseDayData : ScriptableObject
{
    [Header("Identificación del día")]
    public string dayID;              

    [Header("Casos del día")]
    public List<CaseData> cases = new List<CaseData>();

    [Header("Completado")]
    public bool IsCompleted { get; set; } = false;
}
