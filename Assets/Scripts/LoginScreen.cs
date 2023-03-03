using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class LoginScreen : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _loginInput;
        [SerializeField] private TMP_InputField _passwordInput;
        [SerializeField] private Button _loginButton;

        private void Awake()
        {
            _loginButton.onClick.AddListener(HandleLogin);
        }

        private void HandleLogin()
        {
            Debug.Log($"{_loginInput.text}_{_passwordInput.text}");
        }
    }
}