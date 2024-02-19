using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Assets.Scripts.Demos.UI
{
    public class ButtonsView : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image indicator1, indicator2;
        [SerializeField] private TextMeshProUGUI text1, text2;
        public event Action OnClick;

        public void SetButtonText(string text) => button.GetComponentInChildren<TextMeshProUGUI>().text = text;
        public void SetIndicator1Color(Color value) => indicator1.color = value;
        public void SetIndicator2Color(Color value) => indicator2.color = value;
        public void SetText1(string value) => text1.text = value;
        public void SetText2(string value) => text2.text = value;

        private void Awake() => button.onClick.AddListener(Click);

        private void Click() => OnClick?.Invoke();

        private void OnDestroy() => button.onClick.RemoveAllListeners();
    }
}