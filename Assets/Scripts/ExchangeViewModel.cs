using Coin.Assets.Misc;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "ExchangeViewModel", menuName = "SO/Exchange/ExchangeViewModel", order = 1)]
    public class ExchangeViewModel : ScriptableObject
    {
        public ObservableVariable<CoinsValues> CoinsValues = new ObservableVariable<CoinsValues>();
        public ObservableVariable<ExchangeData> CurrentExchangeData = new ObservableVariable<ExchangeData>();

        public event Action<int> HandleExchangeItemClickEvent = (x) => { };

        public void HandleExchangeItemClick(int id)
        {
            HandleExchangeItemClickEvent.Invoke(id);
        }
    }
}


