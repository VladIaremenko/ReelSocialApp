using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ExchangePanelBuyItemView : MonoBehaviour
    {
        [Header("ViewModels")]
        [SerializeField] private ExchangeViewModel _exchangeViewModel;

        [Header("UI")]
        [SerializeField] private TextMeshProUGUI _premCurrencyText;
        [SerializeField] private TextMeshProUGUI _bonusText;
        [SerializeField] private TextMeshProUGUI _basicCurrencyText;
        [SerializeField] private Button _buyButton;

        private void Awake()
        {
            _buyButton.onClick.AddListener(HandleBuyItemClick);
        }

        private void HandleBuyItemClick()
        {
            _exchangeViewModel.HandleExchangeItemClick(transform.GetSiblingIndex() + 1);
        }

        public void RefreshView(ExchangeData data)
        {
            _premCurrencyText.text = data.GoldPrice.ToString();
            _basicCurrencyText.text = data.Amount.ToString();
            _bonusText.text = $"+{data.Bonus} %";

            _bonusText.gameObject.SetActive(data.Bonus > 0);

            gameObject.SetActive(true);
        }
    }
}


