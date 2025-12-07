using UnityEngine;

[System.Serializable]
public class PageImage
{
    public Sprite sprite;
    public Vector2 position;
    public Vector2 size;
    public bool preserveAspect = true;
    public int order = 0;
}
