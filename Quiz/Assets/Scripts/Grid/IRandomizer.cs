
namespace Quiz.Grid 
{
    public interface IRandomizer
    {
        int RandomIndex { get; }

        int RandomSlotBundleIndex { get; }
    }
}
