using UnityEngine;

[CreateAssetMenu(fileName = "PageData", menuName = "UI/PageData")]
public class PageData : ScriptableObject
{
    [TextArea] public string bodyText;

    public Sprite[] images; // puede estar vacío
}
