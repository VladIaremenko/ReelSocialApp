using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class UIManager : MonoBehaviour
    {
        [Header("ViewModels")]
        [SerializeField] private ServerInteractionViewModel _serverInteractionViewModel;
        [SerializeField] private UIViewModel _uiViewModel;

        [Header("Panels")]
        [SerializeField] private GameObject _loginPagePanel;
        [SerializeField] private GameObject _exchangePanel;
        [SerializeField] private GameObject _exchangePopupPanel;

        private void Awake()
        {
            _exchangePanel.SetActive(false);
            _loginPagePanel.SetActive(true);
        }

        private void OnEnable()
        {
            _serverInteractionViewModel.OnReceivedSessionEvent += HandleRecieveSession;
            _serverInteractionViewModel.OnLostSessionEvent += HandleLostSessionEvent;
            _uiViewModel.OnShowExchangePanelEvent += HandleShowExchangePanelEvent;
            _uiViewModel.OnShowExchangePopupEvent += HandleShowExchangePopupEvent;
            _uiViewModel.OnConfirmationPopupEvent += HancleConfirmationPopupEvent;
        }

        private void OnDisable()
        {
            _serverInteractionViewModel.OnReceivedSessionEvent -= HandleRecieveSession;
            _serverInteractionViewModel.OnLostSessionEvent -= HandleLostSessionEvent;
            _uiViewModel.OnShowExchangePanelEvent -= HandleShowExchangePanelEvent;
            _uiViewModel.OnShowExchangePopupEvent -= HandleShowExchangePopupEvent;
            _uiViewModel.OnConfirmationPopupEvent -= HancleConfirmationPopupEvent;
        }

        private void HancleConfirmationPopupEvent(string str)
        {
            
        }

        private void HandleShowExchangePopupEvent(bool state)
        {
            _exchangePopupPanel.SetActive(state);
        }

        private void HandleShowExchangePanelEvent(bool state)
        {
            _exchangePanel.SetActive(state);
        }

        private void HandleLostSessionEvent()
        {
            _loginPagePanel.SetActive(true);
        }

        private void HandleRecieveSession()
        {
            _loginPagePanel.SetActive(false);
        }
    }
}

