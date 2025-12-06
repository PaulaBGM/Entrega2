using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class DocumentView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI bodyText;
    [SerializeField] private RectTransform imagesContainer;
    [SerializeField] private GameObject imagePrefab;
    [SerializeField] private Button nextPageButton;
    [SerializeField] private Button prevPageButton;

    private DocumentData data;
    private int currentPage = 0;
    private readonly List<GameObject> spawnedImages = new List<GameObject>();

    public void Initialize(DocumentData d)
    {
        data = d;
        titleText.text = d.documentTitle;
        nextPageButton.onClick.RemoveAllListeners();
        prevPageButton.onClick.RemoveAllListeners();
        nextPageButton.onClick.AddListener(NextPage);
        prevPageButton.onClick.AddListener(PrevPage);
        ShowPage(0);
    }

    private void ShowPage(int index)
    {
        if (data == null || data.pages.Length == 0)
        {
            bodyText.text = "";
            ClearImages();
            prevPageButton.interactable = false;
            nextPageButton.interactable = false;
            return;
        }

        currentPage = Mathf.Clamp(index, 0, data.pages.Length - 1);
        var page = data.pages[currentPage];

        bodyText.text = page.bodyText;
        ClearImages();

        if (page.images != null)
        {
            foreach (var pi in page.images)
            {
                if (pi == null || pi.sprite == null) continue;

                var inst = Instantiate(imagePrefab, imagesContainer);
                var img = inst.GetComponent<Image>();
                var rt = inst.GetComponent<RectTransform>();

                img.sprite = pi.sprite;
                img.preserveAspect = pi.preserveAspect;

                if (pi.size != Vector2.zero)
                {
                    rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Abs(pi.size.x));
                    rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Abs(pi.size.y));
                }
                else
                {
                    var w = pi.sprite.rect.width;
                    var h = pi.sprite.rect.height;
                    rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, w);
                    rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, h);
                }

                rt.anchoredPosition = pi.anchoredPosition;
                inst.transform.SetSiblingIndex(pi.siblingIndex);
                spawnedImages.Add(inst);
            }
        }

        prevPageButton.interactable = currentPage > 0;
        nextPageButton.interactable = currentPage < data.pages.Length - 1;
    }

    private void ClearImages()
    {
        for (int i = spawnedImages.Count - 1; i >= 0; i--)
        {
            if (spawnedImages[i] != null) Destroy(spawnedImages[i]);
        }
        spawnedImages.Clear();
    }

    private void NextPage() => ShowPage(currentPage + 1);
    private void PrevPage() => ShowPage(currentPage - 1);

    private void OnDestroy()
    {
        ClearImages();
    }
}
