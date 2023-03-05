using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "ServerInteractionManagerSO", menuName = "SO/Web/ServerInteractionManagerSO", order = 1)]
    public class ServerInteractionManagerSO : ScriptableObject
    {
        [SerializeField] private ServerInteractionViewModel _serverInteractionViewModel;
        [SerializeField] private UserDataManagerSO _userDataManagerSO;

        private readonly string LoginApi = "https://yareel.com/src/a.php?email=<email>&pass=<pass>";
        private readonly string PingApi = "https://server.yareel.com/users/ping";
        private readonly string ExchangeApi = "https://ucdn.yareel.com/bundles/config.json";

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
                yield return webRequest.SendWebRequest();

                if (webRequest.responseCode == 200 && !string.IsNullOrEmpty(webRequest.downloadHandler.text))
                {
                    _currentSessionID = webRequest.downloadHandler.text;
                    succesEvent.Invoke();
                }
                else
                {
                    _serverInteractionViewModel.HandleSessionLost();
                    errorEvent.Invoke();
                }
            }
        }

        private IEnumerator GetExchangeData()
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(ExchangeApi))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.responseCode == 200 && !string.IsNullOrEmpty(webRequest.downloadHandler.text))
                {
                    Debug.Log(webRequest.downloadHandler.text);
                }
                else
                {
                    
                }
            }
        }

        private IEnumerator Ping(Action succesEvent, Action errorEvent)
        {
            while (true)
            {
                if (string.IsNullOrEmpty(_currentSessionID))
                {
                    yield return new WaitForEndOfFrame();
                    continue;
                }
                else
                {
                    var msg = new PingRequestData(new User() { SessionId = _currentSessionID});
   
                    var json = JsonConvert.SerializeObject(msg);

                    using (UnityWebRequest webRequest = UnityWebRequest.Post(PingApi, "POST"))
                    {
                        webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
                        webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
                        webRequest.SetRequestHeader("Content-Type", "application/json");

                        yield return webRequest.SendWebRequest();

                        if (webRequest.responseCode == 200 && !string.IsNullOrEmpty(webRequest.downloadHandler.text))
                        {
                            _serverInteractionViewModel.HandleSesseionReceived();

                            var responce = JsonConvert.DeserializeObject<PingResponceData>(webRequest.downloadHandler.text);

                            _userDataManagerSO.HandleUserData(responce.User);

                            _monoBehaviour.StartCoroutine(GetTexture(responce.User.AvatarThumb));

                            succesEvent.Invoke();
                        }
                        else
                        {
                            _serverInteractionViewModel.HandleSessionLost();
                            _currentSessionID = string.Empty;
                            errorEvent.Invoke();
                        }
                    }
                }

                yield return new WaitForSeconds(3f);
            }
        }

        private IEnumerator GetTexture(string url)
        {
            UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url);
            yield return webRequest.SendWebRequest();

            if (webRequest.responseCode != 200)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                _userDataManagerSO.HandleTextureDownloaded(((DownloadHandlerTexture)webRequest.downloadHandler).texture);
            }
        }
    }
}