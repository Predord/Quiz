using System;

namespace Quiz.Level
{
    public interface ILevel
    {
        int CurrentLevel { get; }

        event Action OnLevelChange;

        void ChangeLevel();
    }
}
