
namespace Quiz.UI
{
    public interface IFade
    {
        float FadeDuration { get; }

        void FadeIn();

        void FadeOut();
    }
}
