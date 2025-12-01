using ArtWorks;
using UnityEngine;

[CreateAssetMenu(fileName = "CaseData", menuName = "TheExpertsEye/CaseData")]
public class CaseData : ScriptableObject
{
    public string caseID;
    public string title;
    [TextArea(3, 6)] public string description;
    public ArtWork artWorkPrefab;
    [Tooltip("Verdadero = obra genuina")]
    public bool isGenuine = true;
    [Range(0, 1)] public float difficulty = 0.2f; // 0 f�cil - 1 dif�cil
    [Tooltip("Lista de pistas. El orden define la dificultad de obtenerlas.")]
    public string[] clues;
    [Tooltip("Recompensa econ�mica si autentificas correctamente (o penalizaci�n).")]
    public int rewardIfCorrect = 100;
    public int penaltyIfWrong = 50;
}
