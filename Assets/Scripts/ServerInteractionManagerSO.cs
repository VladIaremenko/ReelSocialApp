using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "ServerInteractionManagerSO", menuName = "SO/Web/ServerInteractionManagerSO", order = 1)]
    public class ServerInteractionManagerSO : ScriptableObject
    {
        private readonly string LoginApi = "https://yareel.com/src/a.php?email=<email>&pass=<pass>";

        public void TryLogin(string username, string password)
        {
            var loginStr = LoginApi.Replace("<pass>", password).Replace("<email>", username);

            Debug.Log(loginStr);
            
            
        }

        private IEnumerator GetRequest(string uri)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();
                Debug.Log(webRequest.responseCode); 

                string[] pages = uri.Split('/');
                int page = pages.Length - 1;

            }
        }
    }
}


