using ArtWorks;
using UnityEngine;
using ScriptableObjects.GameAttributes;

[CreateAssetMenu(fileName = "CaseData", menuName = "TheExpertsEye/CaseData")]
public class CaseData : ScriptableObject
{
    public string caseID;
    public string title;

    [TextArea(3, 6)]
    public string description;

    public GameObject artWorkPrefab;

    public bool isGenuine = true;

    public Hotspot[] hotspots;

<<<<<<< Updated upstream
    public DocumentData[] documents;

=======
    [Header("Consecuencias")]
>>>>>>> Stashed changes
    public GameAttributesDataSo acceptConsequences;
    public GameAttributesDataSo rejectConsequences;
}
