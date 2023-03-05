using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class ExchangeData
    {
        [JsonProperty("bonus")]
        public int Bonus { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("gold_price")]
        public int GoldPrice { get; set; }
    }


    public class CoinsValues
    {
        [JsonProperty("1")]
        public ExchangeData Option1 { get; set; }

        [JsonProperty("2")]
        public ExchangeData Option2 { get; set; }

        [JsonProperty("3")]
        public ExchangeData Option3 { get; set; }

        [JsonProperty("4")]
        public ExchangeData Option4 { get; set; }

        [JsonProperty("5")]
        public ExchangeData Option5 { get; set; }

        public List<ExchangeData> GetList()
        {
            List<ExchangeData> list = new List<ExchangeData>
            {
                Option1,
                Option2,
                Option3,
                Option4,
                Option5
            };

            return list;
        }
    }

    public class ExchangeDataResponce
    {
        [JsonProperty("coins_values")]
        public CoinsValues CoinsValues { get; set; }
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

        [JsonProperty("idx")]
        public int ExchangeItemId;

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
}


