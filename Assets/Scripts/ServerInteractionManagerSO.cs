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
    [JsonProperty("user")]
    public User User { get; set; }
    public List<int> items { get; set; }
    public List<int> slots { get; set; }
    public Counters counters { get; set; }
    public List<Rating> ratings { get; set; }
    public NextQuest next_quest { get; set; }
}

public class User
{
    [JsonProperty("session_id")]
    public string SessionId;

    [JsonProperty("energy")]
    public int Energy { get; set; }

    [JsonProperty("settings_mask")]
    public int SettingsMask { get; set; }

    [JsonProperty("last_activity")]
    public int LastActivity { get; set; }

    [JsonProperty("available")]
    public bool Available { get; set; }

    [JsonProperty("bar_id")]
    public int BarId { get; set; }

    [JsonProperty("login")]
    public string Login { get; set; }

    [JsonProperty("sex")]
    public bool Sex { get; set; }

    [JsonProperty("country_iso")]
    public string CountryIso { get; set; }

    [JsonProperty("city")]
    public string City { get; set; }

    [JsonProperty("lang")]
    public string Lang { get; set; }

    [JsonProperty("nickname")]
    public string Nickname { get; set; }

    [JsonProperty("coins")]
    public int Coins { get; set; }

    [JsonProperty("personal_info")]
    public string PersonalInfo { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("likes_count")]
    public int LikesCount { get; set; }

    [JsonProperty("invisible")]
    public bool Invisible { get; set; }

    [JsonProperty("daily_logins")]
    public int DailyLogins { get; set; }

    [JsonProperty("achiv_points")]
    public int AchivPoints { get; set; }

    [JsonProperty("affiliate_id")]
    public string AffiliateId { get; set; }

    [JsonProperty("ref_domain")]
    public string RefDomain { get; set; }

    [JsonProperty("orientation")]
    public int Orientation { get; set; }

    [JsonProperty("latitude")]
    public int Latitude { get; set; }

    [JsonProperty("longitude")]
    public int Longitude { get; set; }

    [JsonProperty("gold")]
    public int Gold { get; set; }

    [JsonProperty("height")]
    public int Height { get; set; }

    [JsonProperty("body_id")]
    public int BodyId { get; set; }

    [JsonProperty("hair_id")]
    public int HairId { get; set; }

    [JsonProperty("eye_id")]
    public int EyeId { get; set; }

    [JsonProperty("photo_count")]
    public int PhotoCount { get; set; }

    [JsonProperty("online")]
    public bool Online { get; set; }

    [JsonProperty("interests_mask")]
    public string InterestsMask { get; set; }

    [JsonProperty("languiges_mask")]
    public string LanguigesMask { get; set; }

    [JsonProperty("profile_bonus_mask")]
    public int ProfileBonusMask { get; set; }

    [JsonProperty("fantasies")]
    public string Fantasies { get; set; }

    [JsonProperty("ban")]
    public bool Ban { get; set; }

    [JsonProperty("del")]
    public bool Del { get; set; }

    [JsonProperty("affiliate_extra")]
    public object AffiliateExtra { get; set; }

    [JsonProperty("avatar_storage_id")]
    public int AvatarStorageId { get; set; }

    [JsonProperty("login_exists")]
    public bool LoginExists { get; set; }

    [JsonProperty("moder_mask")]
    public int ModerMask { get; set; }

    [JsonProperty("bad_mail")]
    public bool BadMail { get; set; }

    [JsonProperty("login_cnt")]
    public int LoginCnt { get; set; }

    [JsonProperty("utm_data")]
    public string UtmData { get; set; }

    [JsonProperty("sponsor_tier")]
    public object SponsorTier { get; set; }

    [JsonProperty("sponsor_until")]
    public object SponsorUntil { get; set; }

    [JsonProperty("ref_level")]
    public int RefLevel { get; set; }

    [JsonProperty("vip")]
    public bool Vip { get; set; }

    [JsonProperty("age")]
    public int Age { get; set; }

    [JsonProperty("birth_day")]
    public bool BirthDay { get; set; }

    [JsonProperty("avatar_thumb")]
    public string AvatarThumb { get; set; }

    [JsonProperty("room_link_code")]
    public string RoomLinkCode { get; set; }

    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("link_for_referral")]
    public string LinkForReferral { get; set; }

    [JsonProperty("referral_promo_code")]
    public string ReferralPromoCode { get; set; }

    [JsonProperty("avatar")]
    public string Avatar { get; set; }

    [JsonProperty("payment_system")]
    public int PaymentSystem { get; set; }

    [JsonProperty("games_with_bot")]
    public int GamesWithBot { get; set; }

    [JsonProperty("photo_shows_left")]
    public int PhotoShowsLeft { get; set; }

    [JsonProperty("invites_left")]
    public int InvitesLeft { get; set; }

    [JsonProperty("room_id")]
    public int RoomId { get; set; }

    [JsonProperty("link_to_room")]
    public string LinkToRoom { get; set; }

    [JsonProperty("energy_remaining")]
    public int EnergyRemaining { get; set; }
}