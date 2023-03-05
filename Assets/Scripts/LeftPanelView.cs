using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class LeftPanelView : MonoBehaviour
    {
        [Header("ViewModels")]
        [SerializeField] private UserViewModel _userViewModel;
        [SerializeField] private UIViewModel _uiViewModel;

        [Header("UI")]
        [SerializeField] private Button _openPanelButton;
        [SerializeField] private Button _closePanelButton;
        [SerializeField] private GameObject _panelContent;
        [SerializeField] private Button _exchanePanelButton;

        [Header("Content")]
        [SerializeField] private Text _usernameText;
        [SerializeField] private Text _ageText;
        [SerializeField] private Text _basicCurrencyText;
        [SerializeField] private Text _premiumCurrencyText;
        [SerializeField] private RawImage _userPhoto;

        private void Awake()
        {
            _openPanelButton.onClick.AddListener(TogglePanel);
            _closePanelButton.onClick.AddListener(TogglePanel);
            _exchanePanelButton.onClick.AddListener(ShowExchangePanel);
        }

        private void OnEnable()
        {
            _userViewModel.CurrentUserData.AddListener(HandleUserDataChanged);
            _userViewModel.CurrentUserTexture.AddListener(HandleTextureChanged);
        }

        private void OnDisable()
        {
            _userViewModel.CurrentUserData.RemoveListener(HandleUserDataChanged);
            _userViewModel.CurrentUserTexture.RemoveListener(HandleTextureChanged);
        }

        private void HandleTextureChanged(Texture text)
        {
            _userPhoto.texture = text;
        }

        private void ShowExchangePanel()
        {
            _uiViewModel.ShowExchangePanel();
        }

        private void HandleUserDataChanged(User user)
        {
            _usernameText.text = user.Nickname;
            _ageText.text = user.Age.ToString();
            _basicCurrencyText.text = user.Coins.ToString();
            _premiumCurrencyText.text = user.Gold.ToString();
        }

        private void TogglePanel()
        {
            _panelContent.gameObject.SetActive(!_panelContent.gameObject.activeInHierarchy);
        }
    }
}


