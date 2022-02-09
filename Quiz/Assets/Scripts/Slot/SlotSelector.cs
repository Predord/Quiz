using UnityEngine;
using Quiz.Grid;

namespace Quiz.Slot
{
    public class SlotSelector : MonoBehaviour, ISelection
    {
        public bool IsSelected { get; private set; }

        private IMovement movement;

        private void Awake()
        {
            movement = GetComponent<IMovement>();

            SetSlot();
        }

        private void SetSlot()
        {
            if (transform.GetComponentInParent<IRandomizer>().RandomIndex == transform.GetSiblingIndex())
            {
                IsSelected = true;
            }
        }

        public void Pressed()
        {
            if (IsSelected)
            {
                movement.BounceAwake(true);
            }
            else
            {
                movement.MoveSideToSide();
            }
        }
    }
}
