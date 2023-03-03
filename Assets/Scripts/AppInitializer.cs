using UnityEngine;

namespace Assets.Scripts
{
    public class AppInitializer : MonoBehaviour
    {
        [SerializeField] private StorageSO _storageSO;

        private void Awake()
        {
            _storageSO.Init();
        }
    }
}


