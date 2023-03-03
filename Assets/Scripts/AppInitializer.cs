using UnityEngine;

namespace Assets.Scripts
{
    public class AppInitializer : MonoBehaviour
    {
        [SerializeField] private StorageSO _storageSO;
        [SerializeField] private UserDataManagerSO _userDataManagerSO;

        private void Awake()
        {
            _storageSO.Init();
            _userDataManagerSO.Init();
        }
    }
}


