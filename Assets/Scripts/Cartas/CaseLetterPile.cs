using System.Collections.Generic;
using UnityEngine;

public class CaseLetterPile : MonoBehaviour
{
    [Header("Prefab de carta")]
    [SerializeField] private GameObject caseLetterPrefab;

    [Header("Datos del día")]
    [SerializeField] private CaseDayData dayData;

    private Stack<CaseLetter> _pile = new Stack<CaseLetter>();

    private void Start()
    {
        SpawnPile();
    }

    private void SpawnPile()
    {
        foreach (CaseData caseData in dayData.cases)
        {
            GameObject letterObj = Instantiate(caseLetterPrefab, transform);
            CaseLetter letter = letterObj.GetComponent<CaseLetter>();
            letter.caseData = caseData;

            // Posición inicial sobre el montón
            letterObj.transform.localPosition = Vector3.zero + Vector3.up * (_pile.Count * 0.02f);
            _pile.Push(letter);
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
