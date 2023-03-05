using Coin.Assets.Misc;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "UserViewModel", menuName = "SO/UserData/UserViewModel", order = 1)]
    public class UserViewModel : ScriptableObject
    {
        public ObservableVariable<User> CurrentUserData = new ObservableVariable<User>();
    }
}


