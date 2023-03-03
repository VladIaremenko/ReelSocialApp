using UnityEngine;

namespace Assets.Scripts
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private ServerInteractionViewModel _uiViewModel;
        [SerializeField] private GameObject _loginPagePanel;

        private void OnEnable()
        {
            _uiViewModel.OnReceivedSessionEvent += HandleRecieveSession;
            _uiViewModel.OnLostSessionEvent += HandleLostSessionEvent;
        }

        private void OnDisable()
        {
            _uiViewModel.OnReceivedSessionEvent -= HandleRecieveSession;
            _uiViewModel.OnLostSessionEvent -= HandleLostSessionEvent;
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

