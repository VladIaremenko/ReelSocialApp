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
            var list = item.GetList();

            for (int i = 0; i < list.Count; i++)
            {
                _buyItemViews[i].UpdateView(list[i], i);
            }
        }

        private void ClosePanel()
        {
            gameObject.SetActive(false);
        }
    }
}