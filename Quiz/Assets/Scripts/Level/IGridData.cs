using System;

namespace Quiz.Level
{
    public interface IGridData
    {
        int RowCount { get; }
        int ColumnCount { get; }

        event Action OnNewGridCreate;
    }
}
