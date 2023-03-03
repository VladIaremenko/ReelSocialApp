using UnityEngine;

namespace Assets.Scripts
{
    public class AppInitializer : MonoBehaviour
    {
        [SerializeField] private StorageSO _storageSO;
        [SerializeField] private UserDataManagerSO _userDataManagerSO;
        [SerializeField] private ServerInteractionManagerSO _serverInteractionManagerSO;

        private void Awake()
        {
            _storageSO.Init();
            _userDataManagerSO.Init();
            _serverInteractionManagerSO.Init(this
                , () => { Debug.Log("Success"); }
                , () => { Debug.Log("Error"); });
        }
    }
}


