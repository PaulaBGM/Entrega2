using UnityEngine;

[CreateAssetMenu(fileName = "DocumentData", menuName = "TheExpertsEye/DocumentData")]
public class DocumentData : ScriptableObject
{
    public string documentTitle;
    public Sprite backgroundSprite;
    public DocumentPageData[] pages;
}
