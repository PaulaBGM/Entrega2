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

    [Header("Sprites posibles")]
    [SerializeField] private Sprite[] envelopeSprites;

    private readonly List<CaseLetter> _pile = new List<CaseLetter>();

    private void Start()
    {
        SpawnPile();
    }

    private void SpawnPile()
    {
        for (int i = dayData.cases.Count - 1; i >= 0; i--)
        {
            CaseData caseData = dayData.cases[i];

            GameObject letterObj = Instantiate(caseLetterPrefab, transform);
            CaseLetter letter = letterObj.GetComponent<CaseLetter>();

            letter.caseData = caseData;

            int index = _pile.Count;
            letterObj.transform.localPosition = new Vector3(0, verticalOffset * index, 0);

            SpriteRenderer sr = letterObj.GetComponentInChildren<SpriteRenderer>();
            sr.sortingOrder = sortingBase + (index * sortingStep);

            if (envelopeSprites.Length > 0)
                sr.sprite = envelopeSprites[Random.Range(0, envelopeSprites.Length)];

            _pile.Add(letter);
        }

        UpdateInteraction();
    }

    private void UpdateInteraction()
    {
        for (int i = 0; i < _pile.Count; i++)
        {
            bool isTop = (i == _pile.Count - 1);
            _pile[i].SetInteractable(isTop);
        }
    }

    public void RemoveTopLetter(CaseLetter letter)
    {
        _pile.Remove(letter);
        Destroy(letter.gameObject);
        UpdateInteraction();
    }
}
