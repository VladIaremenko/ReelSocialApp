using System;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "UIViewModel", menuName = "SO/UI/UIViewModel", order = 1)]
    public class UIViewModel : ScriptableObject
    {
        public event Action<bool> OnShowExchangePanelEvent = (x) => { };

        public void ShowExchangePanel()
        {
            OnShowExchangePanelEvent.Invoke(true);
        }

        public void CloseExchangePanel()
        {
            OnShowExchangePanelEvent.Invoke(false);
        }
    }
}


