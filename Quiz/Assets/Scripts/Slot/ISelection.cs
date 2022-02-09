
namespace Quiz.Slot
{
    public interface ISelection
    {
        bool IsSelected { get; }

        void Pressed();
    }
}
