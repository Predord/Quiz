using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Quiz.UI
{
    public class LoadingPanel : MonoBehaviour, IFade
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

        private Image image;

        private Tween fade;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        public void FadeIn()
        {
            Fade(1f, FadeDuration);
        }

        public void FadeOut()
        {
            Fade(0f, FadeDuration);
        }

        private void Fade(float value, float duration)
        {
            fade?.Kill();

            fade = image.DOFade(value, duration);
        }
    }
}
