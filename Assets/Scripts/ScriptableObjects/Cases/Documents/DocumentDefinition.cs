using UnityEngine;

[CreateAssetMenu(fileName = "Document", menuName = "TheExpertsEye/DocumentDefinition")]
public class DocumentDefinition : ScriptableObject
{
    public DocumentType type;
    public Sprite background;
    public Vector2 initialPosition;
    public Vector2 initialScale = Vector2.one;
    public PageImage[] images;
}

