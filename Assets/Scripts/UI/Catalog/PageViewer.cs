using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PageViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text bodyTMP;
    [SerializeField] private Image[] imageSlots;

    public void Load(PageData page)
    {
        if (page == null)
        {
            bodyTMP.text = "";
            foreach (var img in imageSlots)
                img.gameObject.SetActive(false);
            return;
        }

        bodyTMP.text = page.bodyText;

        for (int i = 0; i < imageSlots.Length; i++)
        {
            if (page.images != null &&
                i < page.images.Length &&
                page.images[i] != null)
            {
                imageSlots[i].sprite = page.images[i];
                imageSlots[i].gameObject.SetActive(true);
            }
            else
            {
                imageSlots[i].gameObject.SetActive(false);
            }
        }
    }
}
