using Coin.Assets.Misc;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "ExchangeViewModel", menuName = "SO/Exchange/ExchangeViewModel", order = 1)]
    public class ExchangeViewModel : ScriptableObject
    {
        public ObservableVariable<CoinsValues> CoinsValues = new ObservableVariable<CoinsValues>();
    }
}


