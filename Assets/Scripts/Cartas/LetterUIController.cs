using UnityEngine;
using System;

public class LetterUIController : MonoBehaviour
{
    public static LetterUIController Instance;

    [SerializeField] private GameObject documentsPanel;
    [SerializeField] private Transform documentsContainer;
    [SerializeField] private GameObject documentViewPrefab;

    private Action onCloseCallback;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowDocuments(CaseData caseData, Action onClosed = null)
    {
        onCloseCallback = onClosed;
        ClearAllDocuments();

       // if (caseData.documents != null)
        /**{
            foreach (var doc in caseData.documents)
            {
                var inst = Instantiate(documentViewPrefab, documentsContainer);
                var view = inst.GetComponent<DocumentView>();
                view.Initialize(doc);
            }
        }**/

        documentsPanel.SetActive(true);
    }

    public void CloseDocuments()
    {
        documentsPanel.SetActive(false);
        ClearAllDocuments();
        onCloseCallback?.Invoke();
        onCloseCallback = null;
    }

    private void ClearAllDocuments()
    {
        foreach (Transform child in documentsContainer)
            Destroy(child.gameObject);
    }
}
