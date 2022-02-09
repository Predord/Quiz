using UnityEngine;
using Quiz.Level;

namespace Quiz.Grid
{
    public class SlotGridRandomizer : MonoBehaviour, IRandomizer
    {
        public int RandomIndex { get; private set; }

        public int RandomSlotBundleIndex { get; private set; }

        private IBundle bundle;
        private IGridData grid;
        private ILevel level;

        private void Awake()
        {
            bundle = GetComponent<IBundle>();
            grid = GetComponent<IGridData>();
            level = GetComponent<ILevel>();

            RandomizeIndexBundle();
        }

        private void OnEnable()
        {
            level.OnLevelChange += RandomizeIndexBundle;
        }

        private void OnDisable()
        {
            level.OnLevelChange -= RandomizeIndexBundle;
        }

        private void RandomizeIndexBundle()
        {
            RandomIndex = Random.Range(0, grid.ColumnCount * grid.RowCount);
            RandomSlotBundleIndex = Random.Range(0, bundle.BundleSize);
        }
    }
}
