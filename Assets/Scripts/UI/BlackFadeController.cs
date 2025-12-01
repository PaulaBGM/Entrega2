using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BlackFadeController : MonoBehaviour
    {
        private Image _blackFadeImage;
        [SerializeField] private float fadeDuration = 3f;

        private Coroutine _currentFade;

        private void Awake()
        {
            _blackFadeImage = GetComponentInChildren<Image>();
        }

        public void FadeIn() 
        {
            StartFade(0f, 1f);
        }

        public void FadeOut()
        {
            StartFade(1f, 0f);
        }

        private void StartFade(float start, float end)
        {
            if (_currentFade != null)
                StopCoroutine(_currentFade);

            _currentFade = StartCoroutine(FadeCoroutine(start, end));
        }

        private IEnumerator FadeCoroutine(float start, float end)
        {
            float t = 0f;

            while (t < 1f)
            {
                t += Time.deltaTime / fadeDuration;
                float alpha = Mathf.Lerp(start, end, t);

                Color c = _blackFadeImage.color;
                c.a = alpha;
                _blackFadeImage.color = c;

                yield return null;
            }
        }
    }
}