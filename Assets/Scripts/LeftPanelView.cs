using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class LeftPanelView : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private UserViewModel _userViewModel;
        [SerializeField] private Button _openPanelButton;
        [SerializeField] private Button _closePanelButton;
        [SerializeField] private GameObject _panelContent;

        [Header("Content")]
        [SerializeField] private Text _usernameText;
        [SerializeField] private Text _basicCurrencyText;
        [SerializeField] private Text _premiumCurrencyText;

        private void Awake()
        {
            _openPanelButton.onClick.AddListener(TogglePanel);
            _closePanelButton.onClick.AddListener(TogglePanel);
        }

        private void OnEnable()
        {
            _userViewModel.CurrentUserData.AddListener(HandleUserDataChanged);
        }

        private void OnDisable()
        {
            _userViewModel.CurrentUserData.RemoveListener(HandleUserDataChanged);
        }

        private void HandleUserDataChanged(User user)
        {
            _usernameText.text = user.nickname;
            _basicCurrencyText.text = user.coins.ToString();
            _premiumCurrencyText.text = user.gold.ToString();
        }

        private void TogglePanel()
        {
            _panelContent.gameObject.SetActive(!_panelContent.gameObject.activeInHierarchy);
        }
    }
}


