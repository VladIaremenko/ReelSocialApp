using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class UniversalConfirmationPopup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Button _closeButton;
        [SerializeField] private UIViewModel _uiViewModel;
        
        private void Awake()
        {
            _closeButton.onClick.AddListener(ClosePopup);
        }

        private void ClosePopup()
        {
            _uiViewModel.CloseExchangeResultPopup();
        }

        public void UpdateView(string str)
        {
            _text.text = str;
        }
    }
}


