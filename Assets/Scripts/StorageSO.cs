using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "StorageSO", menuName = "SO/Storage/StorageSO", order = 1)]
    public class StorageSO : ScriptableObject
    {
        private readonly string LoginKey = "LoginKey";
        private readonly string PasswordKey = "PasswordKey";

        public void Init()
        {
            _login = PlayerPrefs.GetString(LoginKey, string.Empty);
            _password = PlayerPrefs.GetString(PasswordKey, string.Empty);
        }

        private string _login;
        public string Login
        {
            get { return _login; }
            set
            {
                PlayerPrefs.SetString(LoginKey, value);
                _login = value;
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                PlayerPrefs.SetString(Password, value);
                _password = value;
            }
        }
    }
}


