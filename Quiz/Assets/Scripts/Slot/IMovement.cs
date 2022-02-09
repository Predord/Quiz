
namespace Quiz.Slot
{
    public interface IMovement
    {
        void BounceAwake(bool changeLevelOnComplete, float startScale = 1f, float midScale = 0.8f);

        void MoveSideToSide();
    }
}
