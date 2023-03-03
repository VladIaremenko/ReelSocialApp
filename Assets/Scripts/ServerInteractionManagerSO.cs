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
                    _serverInteractionViewModel.HandleSessionLost();
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
                    var item = new Message();
                    item.user = new User();
                    item.user.session_id = _currentSessionID; ;

                    var json = JsonConvert.SerializeObject(item);

                    Debug.Log(json);

                    var request = new UnityWebRequest(PingApi, "POST");
                    byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
                    request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
                    request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
                    request.SetRequestHeader("Content-Type", "application/json");
                    yield return request.SendWebRequest();
                    Debug.Log("Status Code: " + request.responseCode);
                }
                else
                {
                    _serverInteractionViewModel.HandleSessionLost();
                }

                yield return new WaitForSeconds(3f);
            }
        }
    }

    public class Message
    {
        public User user;
    }

    public class User
    {
        public string session_id;
    }
}