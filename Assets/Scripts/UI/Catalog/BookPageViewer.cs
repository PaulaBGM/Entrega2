using UnityEngine;

public class BookPageViewer : MonoBehaviour
{
    [SerializeField] private PageViewer leftPage;
    [SerializeField] private PageViewer rightPage;

    [Header("Pages")]
    [SerializeField] private PageData[] pages;

    [Header("Pagination")]
    [SerializeField] private PaginationController pagination;

    private void Start()
    {
        if (pages != null && pages.Length > 0)
        {
            SetPages(pages);
            if (pagination != null)
                pagination.SetTotalPages(pages.Length);
        }
    }

    public void SetPages(PageData[] p)
    {
        pages = p;
        LoadSpread(0);
    }

    public void LoadSpread(int leftIndex)
    {
        if (pages == null || pages.Length == 0)
            return;

        PageData left = (leftIndex >= 0 && leftIndex < pages.Length)
            ? pages[leftIndex]
            : null;

        PageData right = (leftIndex + 1 >= 0 && leftIndex + 1 < pages.Length)
            ? pages[leftIndex + 1]
            : null;

        leftPage.Load(left);
        rightPage.Load(right);
    }
}
