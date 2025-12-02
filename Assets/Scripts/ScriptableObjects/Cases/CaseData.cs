using ArtWorks;
using UnityEngine;

[CreateAssetMenu(fileName = "CaseData", menuName = "TheExpertsEye/CaseData")]
public class CaseData : ScriptableObject
{
    public string caseID;
    public string title;

    [TextArea(3, 6)]
    public string description;

    [Tooltip("Prefab de la obra asociada a este caso")]
    public GameObject artWorkPrefab;

    [Tooltip("Verdadero = obra genuina")]
    public bool isGenuine = true;

    [Tooltip("Lista de pistas. El orden define la dificultad de obtenerlas.")]
    public string[] clues;

    [Tooltip("Recompensa economica si autentificas correctamente (o penalizacion).")]
    public int rewardIfCorrect = 100;

    public int penaltyIfWrong = 50;
}
