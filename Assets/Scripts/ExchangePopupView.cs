using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ExchangePopupView : MonoBehaviour
    {
        [SerializeField] private UIViewModel _uiViewModel;
        [SerializeField] private ExchangeViewModel _exchangeViewModel;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private Button _confirmButton;
        [SerializeField] private Button _cancelButton;

        private const string DescriptionTemplate = "Exchange <basic> <sprite=1> for <premium> <sprite=0>";

        private void Awake()
        {
            _confirmButton.onClick.AddListener(HandleConfirmation);
            _cancelButton.onClick.AddListener(HandleCanclel);
        }

        private void HandleCanclel()
        {
            _uiViewModel.ClosePopupEvent();
        }

        private void HandleConfirmation()
        {
            _uiViewModel.ClosePopupEvent();
        }

        private void OnEnable()
        {
            _exchangeViewModel.CurrentExchangeData.AddListener(UpdateView);
        }

        private void Disable()
        {
            _exchangeViewModel.CurrentExchangeData.RemoveListener(UpdateView);
        }

        private void UpdateView(ExchangeData data)
        {
            _descriptionText.text =
                DescriptionTemplate.Replace("<basic>", data.GoldPrice.ToString())
                .Replace("<premium>", data.Amount.ToString());
        }
    }
}


