using UnityEngine;

[CreateAssetMenu(fileName = "DocumentPage", menuName = "TheExpertsEye/DocumentPage")]
public class DocumentPageData : ScriptableObject
{
    [TextArea(3, 8)]
    public string bodyText;
    public PageImage[] images;
}
