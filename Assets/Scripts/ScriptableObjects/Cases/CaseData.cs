using UnityEngine;

[CreateAssetMenu(fileName = "CaseData", menuName = "TheExpertsEye/CaseData")]
public class CaseData : ScriptableObject
{
    public string caseID;
    public string title;
    [TextArea(3, 6)] public string description;
    public Sprite artworkSprite;
    [Tooltip("Verdadero = obra genuina")]
    public bool isGenuine = true;
    [Range(0, 1)] public float difficulty = 0.2f; // 0 fácil - 1 difícil
    [Tooltip("Lista de pistas. El orden define la dificultad de obtenerlas.")]
    public string[] clues;
    [Tooltip("Recompensa económica si autentificas correctamente (o penalización).")]
    public int rewardIfCorrect = 100;
    public int penaltyIfWrong = 50;
}
