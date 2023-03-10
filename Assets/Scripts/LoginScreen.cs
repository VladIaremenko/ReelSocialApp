using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class LoginScreen : MonoBehaviour
    {
        [SerializeField] private LoginPanelViewModel _loginPanelViewModel;
 
        [Header("UI")]
        [SerializeField] private TMP_InputField _loginInput;
        [SerializeField] private TMP_InputField _passwordInput;
        [SerializeField] private TextMeshProUGUI _statusText;
        [SerializeField] private Button _loginButton;

        private void Awake()
        {
            _loginButton.onClick.AddListener(HandleLogin);
        }

        private void OnEnable()
        {
            _loginPanelViewModel.CurrentPassword.AddListener(HandlePasswordInput);
            _loginPanelViewModel.CurrentUsername.AddListener(HandleUsernameInput);
            _loginPanelViewModel.CurrentLoginMessage.AddListener(HandleLoginMessage);

            HandleLoginMessage(string.Empty);
        }

        private void OnDisable()
        {
            _loginPanelViewModel.CurrentPassword.RemoveListener(HandlePasswordInput);
            _loginPanelViewModel.CurrentUsername.RemoveListener(HandleUsernameInput);
            _loginPanelViewModel.CurrentLoginMessage.RemoveListener(HandleLoginMessage);
        }

        private void HandleLoginMessage(string data)
        {
            _statusText.text = data;
        }

        private void HandleUsernameInput(string obj)
        {
            _loginInput.text = obj;

            _loginInput.enabled = false;
            _loginInput.enabled = true;
        }

        private void HandlePasswordInput(string obj)
        {
            _passwordInput.text = obj;
        }

        private void HandleLogin()
        {
            _loginPanelViewModel.CurrentLoginMessage.Value = "Waiting";
            _loginPanelViewModel.SubmitData(_loginInput.text, _passwordInput.text);
        }
    }
}