using System;
using UnityEngine;

namespace Quiz.Level
{
    [Serializable]
    public class LevelData
    {
        [SerializeField]
        private int rowCount;

        public int RowCount => rowCount;

        [SerializeField]
        private int columnCount;

        public int ColumnCount => columnCount;
    }
}
