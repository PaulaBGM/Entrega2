using UnityEngine;

[System.Serializable]
public class PageImage
{
    public Sprite sprite;
    public Vector2 anchoredPosition;
    public Vector2 size;
    public bool preserveAspect = true;
    public int siblingIndex = 0;
}
