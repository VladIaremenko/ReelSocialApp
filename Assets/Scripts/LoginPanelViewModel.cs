using Coin.Assets.Misc;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "LoginPanelViewModel", menuName = "SO/Type/LoginPanelViewModel", order = 1)]
    public class LoginPanelViewModel : ScriptableObject
    {
        public ObservableVariable<string> CurrentLogin = new ObservableVariable<string>();
        public ObservableVariable<string> CurrentPassword = new ObservableVariable<string>();
    }
}


