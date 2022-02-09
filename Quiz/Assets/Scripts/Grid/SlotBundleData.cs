using UnityEngine;

namespace Quiz.Grid
{
    [CreateAssetMenu(fileName = "New SlotBundleData", menuName = "Slot Bundle Data", order = 51)]
    public class SlotBundleData : ScriptableObject
    {
        [SerializeField]
        private bool rotateWidthHeight;

        public bool RotateWidthHeight => rotateWidthHeight;

        [SerializeField]
        private float slotScaleX;

        public float SlotScaleX => slotScaleX;

        [SerializeField]
        private float slotScaleY;

        public float SlotScaleY => slotScaleY;

        [SerializeField]
        private SlotData[] slotDatas;

        public SlotData[] SlotDatas => slotDatas;
    }
}