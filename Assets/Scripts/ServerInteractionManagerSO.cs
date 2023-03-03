using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
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
                if (string.IsNullOrEmpty(_currentSessionID))
                {
                    yield return new WaitForEndOfFrame();
                    continue;
                }
                else
                {
                    var msg = new PingRequestData(new User() { session_id = _currentSessionID});
   
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

                            Debug.Log(webRequest.downloadHandler.text);

                            var responce = JsonConvert.DeserializeObject<PingResponceData>(webRequest.downloadHandler.text);

                            Debug.Log(responce.user.login);

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
    }
}


public class PingRequestData
{
    public User user;

    public PingRequestData(User user)
    {
        this.user = user;
    }
}

public class Counters
{
    public int friendship_requests { get; set; }
    public int friends { get; set; }
    public int friends_online { get; set; }
    public int ignored { get; set; }
    public int messages { get; set; }
    public int rewards { get; set; }
}

public class NextQuest
{
    public int quest_id { get; set; }
    public int coins { get; set; }
}

public class Rating
{
    public int rating_id { get; set; }
    public int interval_id { get; set; }
    public int rating { get; set; }
}

public class PingResponceData
{
    public User user { get; set; }
    public List<int> items { get; set; }
    public List<int> slots { get; set; }
    public Counters counters { get; set; }
    public List<Rating> ratings { get; set; }
    public NextQuest next_quest { get; set; }
}

public class User
{
    public string session_id;
    public int energy { get; set; }
    public int settings_mask { get; set; }
    public int last_activity { get; set; }
    public bool available { get; set; }
    public int bar_id { get; set; }
    public string login { get; set; }
    public bool sex { get; set; }
    public string country_iso { get; set; }
    public string city { get; set; }
    public string lang { get; set; }
    public string nickname { get; set; }
    public int coins { get; set; }
    public string personal_info { get; set; }
    public string status { get; set; }
    public int likes_count { get; set; }
    public bool invisible { get; set; }
    public int daily_logins { get; set; }
    public int achiv_points { get; set; }
    public string affiliate_id { get; set; }
    public string ref_domain { get; set; }
    public int orientation { get; set; }
    public int latitude { get; set; }
    public int longitude { get; set; }
    public int gold { get; set; }
    public int height { get; set; }
    public int body_id { get; set; }
    public int hair_id { get; set; }
    public int eye_id { get; set; }
    public int photo_count { get; set; }
    public bool online { get; set; }
    public string interests_mask { get; set; }
    public string languiges_mask { get; set; }
    public int profile_bonus_mask { get; set; }
    public string fantasies { get; set; }
    public bool ban { get; set; }
    public bool del { get; set; }
    public object affiliate_extra { get; set; }
    public int avatar_storage_id { get; set; }
    public bool login_exists { get; set; }
    public int moder_mask { get; set; }
    public bool bad_mail { get; set; }
    public int login_cnt { get; set; }
    public string utm_data { get; set; }
    public object sponsor_tier { get; set; }
    public object sponsor_until { get; set; }
    public int ref_level { get; set; }
    public bool vip { get; set; }
    public int age { get; set; }
    public bool birth_day { get; set; }
    public string room_link_code { get; set; }
    public int id { get; set; }
    public string link_for_referral { get; set; }
    public string referral_promo_code { get; set; }
    public string no_avatar { get; set; }
    public string no_avatar_thumb { get; set; }
    public int payment_system { get; set; }
    public int games_with_bot { get; set; }
    public int photo_shows_left { get; set; }
    public int invites_left { get; set; }
    public int room_id { get; set; }
    public string link_to_room { get; set; }
    public int energy_remaining { get; set; }
}