using UnityEngine;

[System.Serializable]
public struct DocumentTypePrefab
{
    public DocumentType type;
    public GameObject prefab;
}

public class DocumentSpawner : MonoBehaviour
{
    public static DocumentSpawner Instance;

    [SerializeField] private RectTransform canvasDocumentArea;
    [SerializeField] private GameObject defaultDocumentPrefab;
    [SerializeField] private DocumentTypePrefab[] overrides;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnDocuments(CaseDocumentsData docs)
    {
        foreach (Transform t in canvasDocumentArea)
            Destroy(t.gameObject);

        foreach (var def in docs.documents)
        {
            var prefab = GetPrefabForType(def);
            var inst = Instantiate(prefab, canvasDocumentArea);
            var view = inst.GetComponent<DocumentView>();
            view.Setup(def);
        }
    }

    private GameObject GetPrefabForType(DocumentDefinition def)
    {
        foreach (var o in overrides)
        {
            if (o.type == def.type && o.prefab != null)
                return o.prefab;
        }
        return defaultDocumentPrefab;
    }

    public void ClearDocuments()
    {
        foreach (Transform t in canvasDocumentArea)
            Destroy(t.gameObject);
    }
}
