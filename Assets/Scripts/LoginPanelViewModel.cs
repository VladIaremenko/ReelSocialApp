using Coin.Assets.Misc;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "LoginPanelViewModel", menuName = "SO/UserData/LoginPanelViewModel", order = 1)]
    public class LoginPanelViewModel : ScriptableObject
    {
        public ObservableVariable<string> CurrentUsername = new ObservableVariable<string>();
        public ObservableVariable<string> CurrentPassword = new ObservableVariable<string>();

        public event Action<SubmitDataContainer> SumbitLoginDataEvent = (x) => { };

        public void SubmitData(string username, string password)
        {
            SumbitLoginDataEvent.Invoke(new SubmitDataContainer(username, password));
        }
    }

    public class SubmitDataContainer
    {
        public string Username;
        public string Password;

        public SubmitDataContainer(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}


