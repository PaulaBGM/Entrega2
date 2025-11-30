using System.Collections.Generic;
using UnityEngine;

public class CaseLetterPile : MonoBehaviour
{
    [Header("Prefab de carta")]
    [SerializeField] private GameObject caseLetterPrefab;

    [Header("Datos del día")]
    [SerializeField] private CaseDayData dayData;

    [Header("Ajustes visuales")]
    [SerializeField] private float verticalOffset = -0.15f;
    [SerializeField] private int sortingBase = 10;
    [SerializeField] private int sortingStep = 2;

    private readonly Stack<CaseLetter> _pile = new Stack<CaseLetter>();

    private void Start()
    {
        SpawnPile();
    }

    private void SpawnPile()
    {
        int index = 0;

        foreach (CaseData caseData in dayData.cases)
        {
            GameObject letterObj = Instantiate(caseLetterPrefab, transform);
            CaseLetter letter = letterObj.GetComponent<CaseLetter>();
            letter.caseData = caseData;

            letterObj.transform.localPosition = new Vector3(0, verticalOffset * index, 0);

            SpriteRenderer sr = letterObj.GetComponentInChildren<SpriteRenderer>();
            sr.sortingOrder = sortingBase + (index * sortingStep);

            _pile.Push(letter);
            index++;
        }
    }

    public void RemoveTopLetter()
    {
        if (_pile.Count == 0) return;
        _pile.Pop();
    }

    public CaseLetter PeekTopLetter()
    {
        return _pile.Count > 0 ? _pile.Peek() : null;
    }

    public int RemainingLetters() => _pile.Count;
}
