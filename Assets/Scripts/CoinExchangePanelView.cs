using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class CoinExchangePanelView : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        private void Awake()
        {
            _closeButton.onClick.AddListener(ClosePanel);
        }

        private void ClosePanel()
        {
            gameObject.SetActive(false);
        }
    }
}