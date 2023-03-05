using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "ExchangeManagerSO", menuName = "SO/Exchange/ExchangeManagerSO", order = 1)]
    public class ExchangeManagerSO : ScriptableObject
    {
        [SerializeField] private ExchangeViewModel _exchangeViewModel;
        [SerializeField] private ServerInteractionManagerSO _serverInteractionManager;
        [SerializeField] private UIViewModel _uiViewModel;

        private CoinsValues _currentCoinsValues;

        public void Init()
        {
            _currentCoinsValues = null;
            _exchangeViewModel.CoinsValues.Value = null;
        }

        private void OnEnable()
        {
            _exchangeViewModel.HandleExchangeItemClickEvent += HandleExchangeItemClick;
        }

        private void OnDisable()
        {
            _exchangeViewModel.HandleExchangeItemClickEvent -= HandleExchangeItemClick;
        }

        private void HandleExchangeItemClick(int id)
        {
            _exchangeViewModel.CurrentExchangeData.Value = _currentCoinsValues.GetList()[id];
            _uiViewModel.ShowExchangePopup();
        }

        public void HandleCoinsValuesUpdates(CoinsValues coinsValues)
        {
            _currentCoinsValues = coinsValues;
            _exchangeViewModel.CoinsValues.Value = _currentCoinsValues;
        }
    }
}


