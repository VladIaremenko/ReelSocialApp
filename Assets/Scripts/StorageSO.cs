using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "StorageSO", menuName = "SO/Storage/StorageSO", order = 1)]
    public class StorageSO : ScriptableObject
    {
        private readonly string UsernameKey = "UsernameKey";
        private readonly string PasswordKey = "PasswordKey";

        public void Init()
        {
            _username = PlayerPrefs.GetString(UsernameKey, string.Empty);
            _password = PlayerPrefs.GetString(PasswordKey, string.Empty);
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                PlayerPrefs.SetString(UsernameKey, value);
                _username = value;
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                PlayerPrefs.SetString(PasswordKey, value);
                _password = value;
            }
        }
    }
}


