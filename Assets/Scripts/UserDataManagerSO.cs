﻿using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "UserDataManagerSO", menuName = "SO/UserData/UserDataManagerSO", order = 1)]
    public class UserDataManagerSO : ScriptableObject
    {
        [SerializeField] private StorageSO _storageSO;
        [SerializeField] private LoginPanelViewModel _loginPanelViewModel;

        private void OnDisable()
        {
            _loginPanelViewModel.SumbitLoginDataEvent -= HandleLoginSubmitEvent;
        }

        private void OnEnable()
        {
            _loginPanelViewModel.SumbitLoginDataEvent += HandleLoginSubmitEvent;
        }

        public void Init()
        {
            _loginPanelViewModel.CurrentUsername.Value = _storageSO.Username;
            _loginPanelViewModel.CurrentPassword.Value = _storageSO.Password;
        }

        private void HandleLoginSubmitEvent(SubmitDataContainer data)
        {
            _storageSO.Username = data.Username;
            _storageSO.Password = data.Password;

            _loginPanelViewModel.CurrentUsername.Value = data.Username;
            _loginPanelViewModel.CurrentPassword.Value = data.Password;
        }
    }
}

