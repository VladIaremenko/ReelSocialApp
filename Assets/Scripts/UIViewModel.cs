using System;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "UIViewModel", menuName = "SO/UI/UIViewModel", order = 1)]
    public class UIViewModel : ScriptableObject
    {
        public event Action<bool> OnShowExchangePanelEvent = (x) => { };
        public event Action<bool> OnShowExchangePopupEvent = (x) => { };

        public event Action<string> OnConfirmationPopupEvent = (x) => { };

        public void ShowExchangePanel()
        {
            OnShowExchangePanelEvent.Invoke(true);
        }

        public void CloseExchangePanel()
        {
            OnShowExchangePanelEvent.Invoke(false);
        }

        public void ShowExchangePopup()
        {
            OnShowExchangePopupEvent.Invoke(true);
        }

        public void ClosePopupEvent()
        {
            OnShowExchangePopupEvent.Invoke(false);
        }

        public void HandleExchangeResult(string text)
        {
            OnConfirmationPopupEvent.Invoke(text);
        }
    }
}


