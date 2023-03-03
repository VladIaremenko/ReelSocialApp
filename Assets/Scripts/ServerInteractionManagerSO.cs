using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "ServerInteractionManagerSO", menuName = "SO/Web/ServerInteractionManagerSO", order = 1)]
    public class ServerInteractionManagerSO : ScriptableObject
    {
        private readonly string LoginApi = "https://yareel.com/src/a.php?email=<email>&pass=<pass>";
        private string _currentSessionID;
        private MonoBehaviour _monoBehaviour;

        public void TryLogin(string username, string password, Action succesEvent, Action errorEvent)
        {
            var loginStr = LoginApi.Replace("<pass>", password).Replace("<email>", username);

            Debug.Log(loginStr);

            _monoBehaviour.StartCoroutine(GetRequest(loginStr, succesEvent, errorEvent));        
        }

        public void Init(MonoBehaviour monoBehaviour)
        {
            _monoBehaviour = monoBehaviour;
        }

        private IEnumerator GetRequest(string uri, Action succesEvent, Action errorEvent)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();
 
                string[] pages = uri.Split('/');
                int page = pages.Length - 1;

                if(webRequest.responseCode == 200 && !string.IsNullOrEmpty(webRequest.downloadHandler.text))
                {
                    _currentSessionID = webRequest.downloadHandler.text;
                    succesEvent.Invoke();
                }
                else
                {
                    errorEvent.Invoke();
                }
            }
        }
    }
}


