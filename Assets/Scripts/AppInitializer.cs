using UnityEngine;

namespace Assets.Scripts
{
    public class AppInitializer : MonoBehaviour
    {
        [SerializeField] private StorageSO _storageSO;
        [SerializeField] private UserDataManagerSO _userDataManagerSO;
        [SerializeField] private ServerInteractionManagerSO _serverInteractionManagerSO;
        [SerializeField] private ExchangeManagerSO _exchangeManagerSO;

        private void Awake()
        {
            _storageSO.Init();
            _exchangeManagerSO.Init();
            _userDataManagerSO.Init();
            _serverInteractionManagerSO.Init(this);
        }
    }
}


