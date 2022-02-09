using UnityEngine;
using UnityEngine.UI;

namespace Quiz.UI
{
    public class FindTextSetter : MonoBehaviour, ITextSetter
    {
        [SerializeField]
        private Text selectText;

        public void SetText(string text)
        {
            selectText.text = "Find " + text;
        }
    }
}
