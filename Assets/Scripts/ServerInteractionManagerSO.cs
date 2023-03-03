using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "ServerInteractionManagerSO", menuName = "SO/Web/ServerInteractionManagerSO", order = 1)]
    public class ServerInteractionManagerSO : ScriptableObject
    {
        private readonly string LoginApi = "https://yareel.com/src/a.php?email=<email>&pass=<pass>";
        private readonly string PingApi = "https://server.yareel.com/users/ping";

        private string _currentSessionID;
        private MonoBehaviour _monoBehaviour;

        public void TryLogin(string username, string password, Action succesEvent, Action errorEvent)
        {
            var loginStr = LoginApi.Replace("<pass>", password).Replace("<email>", username);

            _monoBehaviour.StartCoroutine(LoginRequest(loginStr, succesEvent, errorEvent));
        }

        public void Init(MonoBehaviour monoBehaviour, Action successEvent, Action errorEvent)
        {
            _currentSessionID = string.Empty;

            _monoBehaviour = monoBehaviour;

            _monoBehaviour.StartCoroutine(Ping(successEvent, errorEvent));
        }

        private IEnumerator LoginRequest(string uri, Action succesEvent, Action errorEvent)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                if (webRequest.responseCode == 200 && !string.IsNullOrEmpty(webRequest.downloadHandler.text))
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

        private IEnumerator Ping(Action succesEvent, Action errorEvent)
        {
            while (true)
            {
                if (!string.IsNullOrEmpty(_currentSessionID))
                {
                    WWWForm form = new WWWForm();
                    form.AddField("user", _currentSessionID);

                    using (UnityWebRequest webRequest = UnityWebRequest.Post(PingApi, form))
                    {
                        yield return webRequest.SendWebRequest();

                        Debug.Log(webRequest.responseCode);

                        if (webRequest.responseCode == 200 && !string.IsNullOrEmpty(webRequest.downloadHandler.text))
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

                yield return new WaitForSeconds(3f);
            }
        }
    }
}


