using System;
using UnityEngine;

namespace Quiz.Grid
{
    [Serializable]
    public class SlotData
    {
        [SerializeField]
        private string slotName;

        public string SlotName
        {
            get
            {
                return slotName;
            }
        }

        [SerializeField]
        private Sprite slotSprite;

        public Sprite SlotSprite
        {
            get
            {
                return slotSprite;
            }
        }
    }
}
