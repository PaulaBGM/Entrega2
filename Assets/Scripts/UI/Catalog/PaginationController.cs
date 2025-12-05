using UnityEngine;
using UnityEngine.Events;

public class PaginationController : MonoBehaviour
{
    [SerializeField] private int totalPages = 1;
    private int currentLeftPage = 0;

    public UnityEvent<int> OnSpreadChanged;

    public void SetTotalPages(int count)
    {
        totalPages = Mathf.Max(1, count);
        currentLeftPage = 0;
        OnSpreadChanged?.Invoke(currentLeftPage);
    }

    public void NextSpread()
    {
        int next = currentLeftPage + 2;
        if (next <= totalPages - 1)
        {
            currentLeftPage = next;
            OnSpreadChanged?.Invoke(currentLeftPage);
        }
    }

    public void PreviousSpread()
    {
        int prev = currentLeftPage - 2;
        if (prev >= 0)
        {
            currentLeftPage = prev;
            OnSpreadChanged?.Invoke(currentLeftPage);
        }
    }
}
