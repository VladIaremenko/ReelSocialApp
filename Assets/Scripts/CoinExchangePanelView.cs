using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class CoinExchangePanelView : MonoBehaviour
    {
        [SerializeField] private ExchangeViewModel _exchangeViewModel;
        [SerializeField] private Button _closeButton;
        [SerializeField] private List<ExchangePanelBuyItemView> _buyItemViews;

        private void Awake()
        {
            _closeButton.onClick.AddListener(ClosePanel);
        }

        private void OnEnable()
        {
            _buyItemViews.ForEach(x => x.gameObject.SetActive(false));

            _exchangeViewModel.CoinsValues.AddListener(HandleCoinValuesUpdates);
        }

        private void OnDisable()
        {
            _exchangeViewModel.CoinsValues.RemoveListener(HandleCoinValuesUpdates);
        }

        private void HandleCoinValuesUpdates(CoinsValues item)
        {
            _buyItemViews[0].RefreshView(item.Option1);
            _buyItemViews[1].RefreshView(item.Option2);
            _buyItemViews[2].RefreshView(item.Option3);
            _buyItemViews[3].RefreshView(item.Option4);
            _buyItemViews[4].RefreshView(item.Option5);
        }

        private void ClosePanel()
        {
            gameObject.SetActive(false);
        }
    }
}