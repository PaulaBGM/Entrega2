using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DocumentView : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image background;
    [SerializeField] private RectTransform imageContainer;
    [SerializeField] private GameObject imagePrefab;

    private Vector2 dragOffset;
    private readonly List<GameObject> spawned = new List<GameObject>();

    public void Setup(DocumentDefinition def)
    {
        background.sprite = def.background;

        var rt = GetComponent<RectTransform>();
        rt.anchoredPosition = def.initialPosition;
        rt.localScale = def.initialScale;

        foreach (var img in def.images)
        {
            var inst = Instantiate(imagePrefab, imageContainer);
            var im = inst.GetComponent<Image>();
            var rt2 = inst.GetComponent<RectTransform>();

            im.sprite = img.sprite;
            im.preserveAspect = img.preserveAspect;

            rt2.sizeDelta = img.size;
            rt2.anchoredPosition = img.position;
            inst.transform.SetSiblingIndex(img.order);

            spawned.Add(inst);
        }
    }

    public void OnPointerDown(PointerEventData e)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            GetComponent<RectTransform>(),
            e.position,
            e.pressEventCamera,
            out dragOffset
        );
    }

    public void OnBeginDrag(PointerEventData e) { }

    public void OnDrag(PointerEventData e)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(),
            e.position,
            e.pressEventCamera,
            out var pos
        );
        transform.GetComponent<RectTransform>().anchoredPosition = pos - dragOffset;
    }

    public void OnEndDrag(PointerEventData e) { }
}
