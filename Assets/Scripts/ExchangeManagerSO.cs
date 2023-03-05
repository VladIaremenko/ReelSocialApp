using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "ExchangeManagerSO", menuName = "SO/Exchange/ExchangeManagerSO", order = 1)]
    public class ExchangeManagerSO : ScriptableObject
    {
        [SerializeField] private ExchangeViewModel _exchangeViewModel;
        private CoinsValues _currentCoinsValues;

        public void Init()
        {
            _currentCoinsValues = null;
            _exchangeViewModel.CoinsValues.Value = null;
        }

        public void HandleCoinsValuesUpdates(CoinsValues coinsValues)
        {
            _currentCoinsValues = coinsValues;
            _exchangeViewModel.CoinsValues.Value = _currentCoinsValues;
        }
    }
}


