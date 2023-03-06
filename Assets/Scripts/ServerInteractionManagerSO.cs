using Newtonsoft.Json;
using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "ServerInteractionManagerSO", menuName = "SO/Web/ServerInteractionManagerSO", order = 1)]
    public class ServerInteractionManagerSO : ScriptableObject
    {
        [SerializeField] private ServerInteractionViewModel _serverInteractionViewModel;
        [SerializeField] private UIViewModel _uiViewModel;
        [SerializeField] private ExchangeManagerSO _exchangeManagerSO;
        [SerializeField] private UserDataManagerSO _userDataManagerSO;

        private const int succesResponceCode = 200;
        private const string LoginApi = "https://yareel.com/src/a.php?email=<email>&pass=<pass>";
        private const string PingApi = "https://server.yareel.com/users/ping";
        private const string ExchangeApiConfig = "https://ucdn.yareel.com/bundles/config.json";
        private const string ExchangeApi = "https://server.yareel.com/users/exchange_currency";

        private string _currentSessionID;
        private MonoBehaviour _monoBehaviour;
        private Coroutine _pingCoroutine;

        public void TryLogin(string username, string password, Action succesEvent, Action errorEvent)
        {
            var loginStr = LoginApi.Replace("<pass>", password).Replace("<email>", username);

            _monoBehaviour.StartCoroutine(LoginRequest(loginStr, succesEvent, errorEvent));
        }

        public void Init(MonoBehaviour monoBehaviour)
        {
            _currentSessionID = string.Empty;

            _monoBehaviour = monoBehaviour;

            StartPinging();

            _monoBehaviour.StartCoroutine(GetExchangeData());
        }

        private void StartPinging()
        {
            if (_pingCoroutine != null)
            {
                _monoBehaviour.StopCoroutine(_pingCoroutine);
            }

            _pingCoroutine = _monoBehaviour.StartCoroutine(Ping());
        }

        public void HandleExchangeItem(int id)
        {
            _monoBehaviour.StartCoroutine(HandleExchange(id));
        }

        private IEnumerator LoginRequest(string uri, Action succesEvent, Action errorEvent)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.responseCode == succesResponceCode && !string.IsNullOrEmpty(webRequest.downloadHandler.text))
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
            using (UnityWebRequest webRequest = UnityWebRequest.Get(ExchangeApiConfig))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.responseCode == succesResponceCode && !string.IsNullOrEmpty(webRequest.downloadHandler.text))
                {
                    var data = JsonConvert.DeserializeObject<ExchangeDataResponce>(webRequest.downloadHandler.text);
                    _exchangeManagerSO.HandleCoinsValuesUpdates(data.CoinsValues);
                }
                else
                {
                    Debug.Log("Error receiving exchange data");
                }
            }
        }

        private IEnumerator Ping()
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
                    var msg = new PingRequestData(new CustomUser() { SessionId = _currentSessionID });

                    var json = JsonConvert.SerializeObject(msg);

                    using (UnityWebRequest webRequest = UnityWebRequest.Post(PingApi, "POST"))
                    {
                        webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
                        webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
                        webRequest.SetRequestHeader("Content-Type", "application/json");

                        yield return webRequest.SendWebRequest();

                        if (webRequest.responseCode == succesResponceCode && !string.IsNullOrEmpty(webRequest.downloadHandler.text))
                        {
                            _serverInteractionViewModel.HandleSesseionReceived();

                            var responce = JsonConvert.DeserializeObject<PingResponceData>(webRequest.downloadHandler.text);

                            _userDataManagerSO.HandleUserData(responce.User);

                            _monoBehaviour.StartCoroutine(GetTexture(responce.User.AvatarThumb));
                        }
                        else
                        {
                            _serverInteractionViewModel.HandleSessionLost();
                            _currentSessionID = string.Empty;
                        }
                    }
                }

                yield return new WaitForSeconds(100f);
            }
        }

        private IEnumerator GetTexture(string url)
        {
            UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url);
            yield return webRequest.SendWebRequest();

            if (webRequest.responseCode != succesResponceCode)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                _userDataManagerSO.HandleTextureDownloaded(((DownloadHandlerTexture)webRequest.downloadHandler).texture);
            }
        }

        private IEnumerator HandleExchange(int id)
        {
            id++;

            var msg = new PingRequestData(new CustomUser() { SessionId = _currentSessionID, ExchangeItemId = id });

            var json = JsonConvert.SerializeObject(msg);

            using (UnityWebRequest webRequest = UnityWebRequest.Post(ExchangeApi, "POST"))
            {
                webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
                webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
                webRequest.SetRequestHeader("Content-Type", "application/json");

                yield return webRequest.SendWebRequest();

                var message = string.Empty;

                if (webRequest.responseCode == succesResponceCode && !string.IsNullOrEmpty(webRequest.downloadHandler.text))
                {
                    if (webRequest.downloadHandler.text.Contains("error"))
                    {
                        message = webRequest.downloadHandler.text;
                    }
                    else
                    {
                        StartPinging();
                        message = "Success";
                    }
                }
                else
                {
                    message = "Something went wrong";
                }

                _uiViewModel.ShowExchangeResultPopup(message);
            }
        }
    }
}