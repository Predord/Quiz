using UnityEngine;
using UnityEngine.EventSystems;

namespace Quiz.Slot
{
    public class SlotPointerDownHandler : MonoBehaviour, IPointerDownHandler
    {
        private ISelection slotSelection;

        private void Awake()
        {
            slotSelection = GetComponent<ISelection>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            slotSelection.Pressed();
        }
    }
}
