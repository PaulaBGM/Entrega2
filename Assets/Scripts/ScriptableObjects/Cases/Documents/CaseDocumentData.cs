using UnityEngine;

[CreateAssetMenu(fileName = "CaseDocuments", menuName = "TheExpertsEye/CaseDocumentsData")]
public class CaseDocumentsData : ScriptableObject
{
    public DocumentDefinition[] documents;
}
