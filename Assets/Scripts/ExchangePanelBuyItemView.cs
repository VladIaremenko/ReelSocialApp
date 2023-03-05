using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class ExchangePanelBuyItemView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _premCurrencyText;
        [SerializeField] private TextMeshProUGUI _bonusText;
        [SerializeField] private TextMeshProUGUI _basicCurrencyText;

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


