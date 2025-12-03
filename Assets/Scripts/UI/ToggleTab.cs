using UnityEngine;
using System.Collections;
using Interfaces;

public class ToggleTab : MonoBehaviour, ISelectable
{
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform endPos;
    [SerializeField] private float duration = 0.25f;

    private bool _isOpen;
    private Coroutine _anim;

    public void Select()
    {
        if (_anim != null) StopCoroutine(_anim);
        _anim = StartCoroutine(MoveTab(_isOpen ? endPos.position : startPos.position,
                                       _isOpen ? startPos.position : endPos.position));
        _isOpen = !_isOpen;
    }

    public void Deselect() { }

    private IEnumerator MoveTab(Vector3 from, Vector3 to)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            float a = Mathf.SmoothStep(0f, 1f, t / duration);
            transform.position = Vector3.Lerp(from, to, a);
            yield return null;
        }
        transform.position = to;
    }
}
