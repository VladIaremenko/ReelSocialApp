using System;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "ServerInteractionViewModel", menuName = "SO/Web/ServerInteractionViewModel", order = 1)]
    public class ServerInteractionViewModel : ScriptableObject
    {
        public event Action OnLostSessionEvent = () => { };
        public event Action OnReceivedSessionEvent = () => { };

        public void HandleSessionLost()
        {
            OnLostSessionEvent.Invoke();
        }

        public void HandleSesseionReceived()
        {
            OnReceivedSessionEvent.Invoke();
        }
    }
}


