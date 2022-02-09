using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Quiz.UI
{
    public class FindText : MonoBehaviour, IFade
    {
        public float FadeDuration
        {
            get
            {
                return fadeDuration;
            }
            private set
            {
                fadeDuration = value;
            }
        }

        [SerializeField]
        private float fadeDuration = 1f;

        private Text text;

        private Tween fade;

        private void Awake()
        {
            text = GetComponent<Text>();
        }

        private void Start()
        {
            FadeIn();
        }

        public void FadeIn()
        {
            Fade(1, FadeDuration);
        }

        public void FadeOut()
        {
            Fade(0, FadeDuration);
        }

        private void Fade(float value, float duration)
        {
            fade?.Kill();

            fade = text.DOFade(value, duration);
        }
    }
}