// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var tweetRaw = TweetRaw.FromJson(jsonString);

namespace QuickType
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class TweetRaw
    {
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("id")]
        public double Id { get; set; }

        [JsonProperty("id_str")]
        public string IdStr { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("truncated")]
        public bool Truncated { get; set; }

        [JsonProperty("entities")]
        public TweetRawEntities Entities { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("in_reply_to_status_id")]
        public object InReplyToStatusId { get; set; }

        [JsonProperty("in_reply_to_status_id_str")]
        public object InReplyToStatusIdStr { get; set; }

        [JsonProperty("in_reply_to_user_id")]
        public object InReplyToUserId { get; set; }

        [JsonProperty("in_reply_to_user_id_str")]
        public object InReplyToUserIdStr { get; set; }

        [JsonProperty("in_reply_to_screen_name")]
        public object InReplyToScreenName { get; set; }

        [JsonProperty("user")]
        public RetweetedStatusUser User { get; set; }

        [JsonProperty("geo")]
        public object Geo { get; set; }

        [JsonProperty("coordinates")]
        public object Coordinates { get; set; }

        [JsonProperty("place")]
        public object Place { get; set; }

        [JsonProperty("contributors")]
        public object Contributors { get; set; }

        [JsonProperty("is_quote_status")]
        public bool IsQuoteStatus { get; set; }

        [JsonProperty("retweet_count")]
        public long RetweetCount { get; set; }

        [JsonProperty("favorite_count")]
        public long FavoriteCount { get; set; }

        [JsonProperty("favorited")]
        public bool Favorited { get; set; }

        [JsonProperty("retweeted")]
        public bool Retweeted { get; set; }

        [JsonProperty("lang")]
        public Lang Lang { get; set; }

        [JsonProperty("retweeted_status", NullValueHandling = NullValueHandling.Ignore)]
        public RetweetedStatus RetweetedStatus { get; set; }

        [JsonProperty("quoted_status_id", NullValueHandling = NullValueHandling.Ignore)]
        public double? QuotedStatusId { get; set; }

        [JsonProperty("quoted_status_id_str", NullValueHandling = NullValueHandling.Ignore)]
        public string QuotedStatusIdStr { get; set; }

        [JsonProperty("possibly_sensitive", NullValueHandling = NullValueHandling.Ignore)]
        public bool? PossiblySensitive { get; set; }

        [JsonProperty("extended_entities", NullValueHandling = NullValueHandling.Ignore)]
        public TweetRawExtendedEntities ExtendedEntities { get; set; }

        [JsonProperty("quoted_status", NullValueHandling = NullValueHandling.Ignore)]
        public TweetRawQuotedStatus QuotedStatus { get; set; }
    }

    public partial class TweetRawEntities
    {
        [JsonProperty("hashtags")]
        public Hashtag[] Hashtags { get; set; }

        [JsonProperty("symbols")]
        public object[] Symbols { get; set; }

        [JsonProperty("user_mentions")]
        public UserMention[] UserMentions { get; set; }

        [JsonProperty("urls")]
        public Url[] Urls { get; set; }

        [JsonProperty("media", NullValueHandling = NullValueHandling.Ignore)]
        public PurpleMedia[] Media { get; set; }
    }

    public partial class Hashtag
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("indices")]
        public long[] Indices { get; set; }
    }

    public partial class PurpleMedia
    {
        [JsonProperty("id")]
        public double Id { get; set; }

        [JsonProperty("id_str")]
        public string IdStr { get; set; }

        [JsonProperty("indices")]
        public long[] Indices { get; set; }

        [JsonProperty("media_url")]
        public Uri MediaUrl { get; set; }

        [JsonProperty("media_url_https")]
        public Uri MediaUrlHttps { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("display_url")]
        public string DisplayUrl { get; set; }

        [JsonProperty("expanded_url")]
        public Uri ExpandedUrl { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("sizes")]
        public Sizes Sizes { get; set; }

        [JsonProperty("source_status_id", NullValueHandling = NullValueHandling.Ignore)]
        public double? SourceStatusId { get; set; }

        [JsonProperty("source_status_id_str", NullValueHandling = NullValueHandling.Ignore)]
        public string SourceStatusIdStr { get; set; }

        [JsonProperty("source_user_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? SourceUserId { get; set; }

        [JsonProperty("source_user_id_str", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? SourceUserIdStr { get; set; }

        [JsonProperty("video_info", NullValueHandling = NullValueHandling.Ignore)]
        public PurpleVideoInfo VideoInfo { get; set; }
    }

    public partial class Sizes
    {
        [JsonProperty("thumb")]
        public Large Thumb { get; set; }

        [JsonProperty("small")]
        public Large Small { get; set; }

        [JsonProperty("medium")]
        public Large Medium { get; set; }

        [JsonProperty("large")]
        public Large Large { get; set; }
    }

    public partial class Large
    {
        [JsonProperty("w")]
        public long W { get; set; }

        [JsonProperty("h")]
        public long H { get; set; }

        [JsonProperty("resize")]
        public Resize Resize { get; set; }
    }

    public partial class PurpleVideoInfo
    {
        [JsonProperty("aspect_ratio")]
        public long[] AspectRatio { get; set; }

        [JsonProperty("variants")]
        public Variant[] Variants { get; set; }
    }

    public partial class Variant
    {
        [JsonProperty("bitrate", NullValueHandling = NullValueHandling.Ignore)]
        public long? Bitrate { get; set; }

        [JsonProperty("content_type")]
        public string ContentType { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }

    public partial class Url
    {
        [JsonProperty("url")]
        public Uri UrlUrl { get; set; }

        [JsonProperty("expanded_url")]
        public Uri ExpandedUrl { get; set; }

        [JsonProperty("display_url")]
        public string DisplayUrl { get; set; }

        [JsonProperty("indices")]
        public long[] Indices { get; set; }
    }

    public partial class UserMention
    {
        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public double Id { get; set; }

        [JsonProperty("id_str")]
        public string IdStr { get; set; }

        [JsonProperty("indices")]
        public long[] Indices { get; set; }
    }

    public partial class TweetRawExtendedEntities
    {
        [JsonProperty("media")]
        public PurpleMedia[] Media { get; set; }
    }

    public partial class TweetRawQuotedStatus
    {
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("id")]
        public double Id { get; set; }

        [JsonProperty("id_str")]
        public string IdStr { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("truncated")]
        public bool Truncated { get; set; }

        [JsonProperty("entities")]
        public RetweetedStatusEntities Entities { get; set; }

        [JsonProperty("extended_entities")]
        public QuotedStatusExtendedEntities ExtendedEntities { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("in_reply_to_status_id")]
        public object InReplyToStatusId { get; set; }

        [JsonProperty("in_reply_to_status_id_str")]
        public object InReplyToStatusIdStr { get; set; }

        [JsonProperty("in_reply_to_user_id")]
        public object InReplyToUserId { get; set; }

        [JsonProperty("in_reply_to_user_id_str")]
        public object InReplyToUserIdStr { get; set; }

        [JsonProperty("in_reply_to_screen_name")]
        public object InReplyToScreenName { get; set; }

        [JsonProperty("user")]
        public PurpleUser User { get; set; }

        [JsonProperty("geo")]
        public object Geo { get; set; }

        [JsonProperty("coordinates")]
        public object Coordinates { get; set; }

        [JsonProperty("place")]
        public object Place { get; set; }

        [JsonProperty("contributors")]
        public object Contributors { get; set; }

        [JsonProperty("is_quote_status")]
        public bool IsQuoteStatus { get; set; }

        [JsonProperty("retweet_count")]
        public long RetweetCount { get; set; }

        [JsonProperty("favorite_count")]
        public long FavoriteCount { get; set; }

        [JsonProperty("favorited")]
        public bool Favorited { get; set; }

        [JsonProperty("retweeted")]
        public bool Retweeted { get; set; }

        [JsonProperty("possibly_sensitive")]
        public bool PossiblySensitive { get; set; }

        [JsonProperty("lang")]
        public Lang Lang { get; set; }
    }

    public partial class RetweetedStatusEntities
    {
        [JsonProperty("hashtags")]
        public Hashtag[] Hashtags { get; set; }

        [JsonProperty("symbols")]
        public object[] Symbols { get; set; }

        [JsonProperty("user_mentions")]
        public UserMention[] UserMentions { get; set; }

        [JsonProperty("urls")]
        public Url[] Urls { get; set; }

        [JsonProperty("media", NullValueHandling = NullValueHandling.Ignore)]
        public FluffyMedia[] Media { get; set; }
    }

    public partial class FluffyMedia
    {
        [JsonProperty("id")]
        public double Id { get; set; }

        [JsonProperty("id_str")]
        public string IdStr { get; set; }

        [JsonProperty("indices")]
        public long[] Indices { get; set; }

        [JsonProperty("media_url")]
        public Uri MediaUrl { get; set; }

        [JsonProperty("media_url_https")]
        public Uri MediaUrlHttps { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("display_url")]
        public string DisplayUrl { get; set; }

        [JsonProperty("expanded_url")]
        public Uri ExpandedUrl { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("sizes")]
        public Sizes Sizes { get; set; }

        [JsonProperty("video_info", NullValueHandling = NullValueHandling.Ignore)]
        public FluffyVideoInfo VideoInfo { get; set; }

        [JsonProperty("additional_media_info", NullValueHandling = NullValueHandling.Ignore)]
        public AdditionalMediaInfo AdditionalMediaInfo { get; set; }
    }

    public partial class AdditionalMediaInfo
    {
        [JsonProperty("monetizable")]
        public bool Monetizable { get; set; }
    }

    public partial class FluffyVideoInfo
    {
        [JsonProperty("aspect_ratio")]
        public long[] AspectRatio { get; set; }

        [JsonProperty("duration_millis")]
        public long DurationMillis { get; set; }

        [JsonProperty("variants")]
        public Variant[] Variants { get; set; }
    }

    public partial class QuotedStatusExtendedEntities
    {
        [JsonProperty("media")]
        public FluffyMedia[] Media { get; set; }
    }

    public partial class PurpleUser
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("id_str")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long IdStr { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public object Url { get; set; }

        [JsonProperty("entities")]
        public PurpleEntities Entities { get; set; }

        [JsonProperty("protected")]
        public bool Protected { get; set; }

        [JsonProperty("followers_count")]
        public long FollowersCount { get; set; }

        [JsonProperty("friends_count")]
        public long FriendsCount { get; set; }

        [JsonProperty("listed_count")]
        public long ListedCount { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("favourites_count")]
        public long FavouritesCount { get; set; }

        [JsonProperty("utc_offset")]
        public object UtcOffset { get; set; }

        [JsonProperty("time_zone")]
        public object TimeZone { get; set; }

        [JsonProperty("geo_enabled")]
        public bool GeoEnabled { get; set; }

        [JsonProperty("verified")]
        public bool Verified { get; set; }

        [JsonProperty("statuses_count")]
        public long StatusesCount { get; set; }

        [JsonProperty("lang")]
        public Lang Lang { get; set; }

        [JsonProperty("contributors_enabled")]
        public bool ContributorsEnabled { get; set; }

        [JsonProperty("is_translator")]
        public bool IsTranslator { get; set; }

        [JsonProperty("is_translation_enabled")]
        public bool IsTranslationEnabled { get; set; }

        [JsonProperty("profile_background_color")]
        public string ProfileBackgroundColor { get; set; }

        [JsonProperty("profile_background_image_url")]
        public Uri ProfileBackgroundImageUrl { get; set; }

        [JsonProperty("profile_background_image_url_https")]
        public Uri ProfileBackgroundImageUrlHttps { get; set; }

        [JsonProperty("profile_background_tile")]
        public bool ProfileBackgroundTile { get; set; }

        [JsonProperty("profile_image_url")]
        public Uri ProfileImageUrl { get; set; }

        [JsonProperty("profile_image_url_https")]
        public Uri ProfileImageUrlHttps { get; set; }

        [JsonProperty("profile_banner_url")]
        public Uri ProfileBannerUrl { get; set; }

        [JsonProperty("profile_link_color")]
        public string ProfileLinkColor { get; set; }

        [JsonProperty("profile_sidebar_border_color")]
        public string ProfileSidebarBorderColor { get; set; }

        [JsonProperty("profile_sidebar_fill_color")]
        public string ProfileSidebarFillColor { get; set; }

        [JsonProperty("profile_text_color")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ProfileTextColor { get; set; }

        [JsonProperty("profile_use_background_image")]
        public bool ProfileUseBackgroundImage { get; set; }

        [JsonProperty("has_extended_profile")]
        public bool HasExtendedProfile { get; set; }

        [JsonProperty("default_profile")]
        public bool DefaultProfile { get; set; }

        [JsonProperty("default_profile_image")]
        public bool DefaultProfileImage { get; set; }

        [JsonProperty("following")]
        public object Following { get; set; }

        [JsonProperty("follow_request_sent")]
        public object FollowRequestSent { get; set; }

        [JsonProperty("notifications")]
        public object Notifications { get; set; }

        [JsonProperty("translator_type")]
        public TranslatorType TranslatorType { get; set; }
    }

    public partial class PurpleEntities
    {
        [JsonProperty("description")]
        public Description Description { get; set; }
    }

    public partial class Description
    {
        [JsonProperty("urls")]
        public Url[] Urls { get; set; }
    }

    public partial class RetweetedStatus
    {
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("id")]
        public double Id { get; set; }

        [JsonProperty("id_str")]
        public string IdStr { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("truncated")]
        public bool Truncated { get; set; }

        [JsonProperty("entities")]
        public RetweetedStatusEntities Entities { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("in_reply_to_status_id")]
        public double? InReplyToStatusId { get; set; }

        [JsonProperty("in_reply_to_status_id_str")]
        public string InReplyToStatusIdStr { get; set; }

        [JsonProperty("in_reply_to_user_id")]
        public long? InReplyToUserId { get; set; }

        [JsonProperty("in_reply_to_user_id_str")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? InReplyToUserIdStr { get; set; }

        [JsonProperty("in_reply_to_screen_name")]
        public string InReplyToScreenName { get; set; }

        [JsonProperty("user")]
        public RetweetedStatusUser User { get; set; }

        [JsonProperty("geo")]
        public object Geo { get; set; }

        [JsonProperty("coordinates")]
        public object Coordinates { get; set; }

        [JsonProperty("place")]
        public object Place { get; set; }

        [JsonProperty("contributors")]
        public object Contributors { get; set; }

        [JsonProperty("is_quote_status")]
        public bool IsQuoteStatus { get; set; }

        [JsonProperty("quoted_status_id", NullValueHandling = NullValueHandling.Ignore)]
        public double? QuotedStatusId { get; set; }

        [JsonProperty("quoted_status_id_str", NullValueHandling = NullValueHandling.Ignore)]
        public string QuotedStatusIdStr { get; set; }

        [JsonProperty("quoted_status", NullValueHandling = NullValueHandling.Ignore)]
        public RetweetedStatusQuotedStatus QuotedStatus { get; set; }

        [JsonProperty("retweet_count")]
        public long RetweetCount { get; set; }

        [JsonProperty("favorite_count")]
        public long FavoriteCount { get; set; }

        [JsonProperty("favorited")]
        public bool Favorited { get; set; }

        [JsonProperty("retweeted")]
        public bool Retweeted { get; set; }

        [JsonProperty("possibly_sensitive", NullValueHandling = NullValueHandling.Ignore)]
        public bool? PossiblySensitive { get; set; }

        [JsonProperty("lang")]
        public Lang Lang { get; set; }

        [JsonProperty("extended_entities", NullValueHandling = NullValueHandling.Ignore)]
        public QuotedStatusExtendedEntities ExtendedEntities { get; set; }
    }

    public partial class RetweetedStatusQuotedStatus
    {
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("id")]
        public double Id { get; set; }

        [JsonProperty("id_str")]
        public string IdStr { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("truncated")]
        public bool Truncated { get; set; }

        [JsonProperty("entities")]
        public TweetRawEntities Entities { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("in_reply_to_status_id")]
        public double? InReplyToStatusId { get; set; }

        [JsonProperty("in_reply_to_status_id_str")]
        public string InReplyToStatusIdStr { get; set; }

        [JsonProperty("in_reply_to_user_id")]
        public long? InReplyToUserId { get; set; }

        [JsonProperty("in_reply_to_user_id_str")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? InReplyToUserIdStr { get; set; }

        [JsonProperty("in_reply_to_screen_name")]
        public string InReplyToScreenName { get; set; }

        [JsonProperty("user")]
        public RetweetedStatusUser User { get; set; }

        [JsonProperty("geo")]
        public object Geo { get; set; }

        [JsonProperty("coordinates")]
        public object Coordinates { get; set; }

        [JsonProperty("place")]
        public object Place { get; set; }

        [JsonProperty("contributors")]
        public object Contributors { get; set; }

        [JsonProperty("is_quote_status")]
        public bool IsQuoteStatus { get; set; }

        [JsonProperty("retweet_count")]
        public long RetweetCount { get; set; }

        [JsonProperty("favorite_count")]
        public long FavoriteCount { get; set; }

        [JsonProperty("favorited")]
        public bool Favorited { get; set; }

        [JsonProperty("retweeted")]
        public bool Retweeted { get; set; }

        [JsonProperty("possibly_sensitive", NullValueHandling = NullValueHandling.Ignore)]
        public bool? PossiblySensitive { get; set; }

        [JsonProperty("lang")]
        public Lang Lang { get; set; }
    }

    public partial class RetweetedStatusUser
    {
        [JsonProperty("id")]
        public Id Id { get; set; }

        [JsonProperty("id_str")]
        public string IdStr { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("entities")]
        public FluffyEntities Entities { get; set; }

        [JsonProperty("protected")]
        public bool Protected { get; set; }

        [JsonProperty("followers_count")]
        public long FollowersCount { get; set; }

        [JsonProperty("friends_count")]
        public long FriendsCount { get; set; }

        [JsonProperty("listed_count")]
        public long ListedCount { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("favourites_count")]
        public long FavouritesCount { get; set; }

        [JsonProperty("utc_offset")]
        public object UtcOffset { get; set; }

        [JsonProperty("time_zone")]
        public object TimeZone { get; set; }

        [JsonProperty("geo_enabled")]
        public bool GeoEnabled { get; set; }

        [JsonProperty("verified")]
        public bool Verified { get; set; }

        [JsonProperty("statuses_count")]
        public long StatusesCount { get; set; }

        [JsonProperty("lang")]
        public Lang Lang { get; set; }

        [JsonProperty("contributors_enabled")]
        public bool ContributorsEnabled { get; set; }

        [JsonProperty("is_translator")]
        public bool IsTranslator { get; set; }

        [JsonProperty("is_translation_enabled")]
        public bool IsTranslationEnabled { get; set; }

        [JsonProperty("profile_background_color")]
        public ProfileBackgroundColor ProfileBackgroundColor { get; set; }

        [JsonProperty("profile_background_image_url")]
        public Uri ProfileBackgroundImageUrl { get; set; }

        [JsonProperty("profile_background_image_url_https")]
        public Uri ProfileBackgroundImageUrlHttps { get; set; }

        [JsonProperty("profile_background_tile")]
        public bool ProfileBackgroundTile { get; set; }

        [JsonProperty("profile_image_url")]
        public Uri ProfileImageUrl { get; set; }

        [JsonProperty("profile_image_url_https")]
        public Uri ProfileImageUrlHttps { get; set; }

        [JsonProperty("profile_link_color")]
        public string ProfileLinkColor { get; set; }

        [JsonProperty("profile_sidebar_border_color")]
        public ProfileSidebarBorderColor ProfileSidebarBorderColor { get; set; }

        [JsonProperty("profile_sidebar_fill_color")]
        public ProfileSidebarFillColor ProfileSidebarFillColor { get; set; }

        [JsonProperty("profile_text_color")]
        public string ProfileTextColor { get; set; }

        [JsonProperty("profile_use_background_image")]
        public bool ProfileUseBackgroundImage { get; set; }

        [JsonProperty("has_extended_profile")]
        public bool HasExtendedProfile { get; set; }

        [JsonProperty("default_profile")]
        public bool DefaultProfile { get; set; }

        [JsonProperty("default_profile_image")]
        public bool DefaultProfileImage { get; set; }

        [JsonProperty("following")]
        public object Following { get; set; }

        [JsonProperty("follow_request_sent")]
        public object FollowRequestSent { get; set; }

        [JsonProperty("notifications")]
        public object Notifications { get; set; }

        [JsonProperty("translator_type")]
        public TranslatorType TranslatorType { get; set; }

        [JsonProperty("profile_banner_url", NullValueHandling = NullValueHandling.Ignore)]
        public Uri ProfileBannerUrl { get; set; }
    }

    public partial class FluffyEntities
    {
        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public Description Url { get; set; }

        [JsonProperty("description")]
        public Description Description { get; set; }
    }

    public enum Resize { Crop, Fit };

    public enum Lang { En, Und };

    public enum TranslatorType { None, Regular };

    public enum ProfileBackgroundColor { C0Deed, D1Cdc1, F5F8Fa, The000000, The16181D };

    public enum ProfileSidebarBorderColor { B8Aa9C, C0Deed, The000000, The181A1E };

    public enum ProfileSidebarFillColor { Acaab6, B8Aa9C, Ddeef6, The000000 };

    public partial struct Id
    {
        public double? Double;
        public long? Integer;

        public static implicit operator Id(double Double) => new Id { Double = Double };
        public static implicit operator Id(long Integer) => new Id { Integer = Integer };
    }

    public partial class TweetRaw
    {
        public static TweetRaw[] FromJson(string json) => JsonConvert.DeserializeObject<TweetRaw[]>(json, QuickType.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this TweetRaw[] self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                ResizeConverter.Singleton,
                LangConverter.Singleton,
                TranslatorTypeConverter.Singleton,
                IdConverter.Singleton,
                ProfileBackgroundColorConverter.Singleton,
                ProfileSidebarBorderColorConverter.Singleton,
                ProfileSidebarFillColorConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ResizeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Resize) || t == typeof(Resize?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "crop":
                    return Resize.Crop;
                case "fit":
                    return Resize.Fit;
            }
            throw new Exception("Cannot unmarshal type Resize");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Resize)untypedValue;
            switch (value)
            {
                case Resize.Crop:
                    serializer.Serialize(writer, "crop");
                    return;
                case Resize.Fit:
                    serializer.Serialize(writer, "fit");
                    return;
            }
            throw new Exception("Cannot marshal type Resize");
        }

        public static readonly ResizeConverter Singleton = new ResizeConverter();
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class LangConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Lang) || t == typeof(Lang?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "en":
                    return Lang.En;
                case "und":
                    return Lang.Und;
            }
            throw new Exception("Cannot unmarshal type Lang");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Lang)untypedValue;
            switch (value)
            {
                case Lang.En:
                    serializer.Serialize(writer, "en");
                    return;
                case Lang.Und:
                    serializer.Serialize(writer, "und");
                    return;
            }
            throw new Exception("Cannot marshal type Lang");
        }

        public static readonly LangConverter Singleton = new LangConverter();
    }

    internal class TranslatorTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TranslatorType) || t == typeof(TranslatorType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "none":
                    return TranslatorType.None;
                case "regular":
                    return TranslatorType.Regular;
            }
            throw new Exception("Cannot unmarshal type TranslatorType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TranslatorType)untypedValue;
            switch (value)
            {
                case TranslatorType.None:
                    serializer.Serialize(writer, "none");
                    return;
                case TranslatorType.Regular:
                    serializer.Serialize(writer, "regular");
                    return;
            }
            throw new Exception("Cannot marshal type TranslatorType");
        }

        public static readonly TranslatorTypeConverter Singleton = new TranslatorTypeConverter();
    }

    internal class IdConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Id) || t == typeof(Id?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Integer:
                    var integerValue = serializer.Deserialize<long>(reader);
                    return new Id { Integer = integerValue };
                case JsonToken.Float:
                    var doubleValue = serializer.Deserialize<double>(reader);
                    return new Id { Double = doubleValue };
            }
            throw new Exception("Cannot unmarshal type Id");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (Id)untypedValue;
            if (value.Integer != null)
            {
                serializer.Serialize(writer, value.Integer.Value);
                return;
            }
            if (value.Double != null)
            {
                serializer.Serialize(writer, value.Double.Value);
                return;
            }
            throw new Exception("Cannot marshal type Id");
        }

        public static readonly IdConverter Singleton = new IdConverter();
    }

    internal class ProfileBackgroundColorConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ProfileBackgroundColor) || t == typeof(ProfileBackgroundColor?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "000000":
                    return ProfileBackgroundColor.The000000;
                case "16181D":
                    return ProfileBackgroundColor.The16181D;
                case "C0DEED":
                    return ProfileBackgroundColor.C0Deed;
                case "D1CDC1":
                    return ProfileBackgroundColor.D1Cdc1;
                case "F5F8FA":
                    return ProfileBackgroundColor.F5F8Fa;
            }
            throw new Exception("Cannot unmarshal type ProfileBackgroundColor");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ProfileBackgroundColor)untypedValue;
            switch (value)
            {
                case ProfileBackgroundColor.The000000:
                    serializer.Serialize(writer, "000000");
                    return;
                case ProfileBackgroundColor.The16181D:
                    serializer.Serialize(writer, "16181D");
                    return;
                case ProfileBackgroundColor.C0Deed:
                    serializer.Serialize(writer, "C0DEED");
                    return;
                case ProfileBackgroundColor.D1Cdc1:
                    serializer.Serialize(writer, "D1CDC1");
                    return;
                case ProfileBackgroundColor.F5F8Fa:
                    serializer.Serialize(writer, "F5F8FA");
                    return;
            }
            throw new Exception("Cannot marshal type ProfileBackgroundColor");
        }

        public static readonly ProfileBackgroundColorConverter Singleton = new ProfileBackgroundColorConverter();
    }

    internal class ProfileSidebarBorderColorConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ProfileSidebarBorderColor) || t == typeof(ProfileSidebarBorderColor?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "000000":
                    return ProfileSidebarBorderColor.The000000;
                case "181A1E":
                    return ProfileSidebarBorderColor.The181A1E;
                case "B8AA9C":
                    return ProfileSidebarBorderColor.B8Aa9C;
                case "C0DEED":
                    return ProfileSidebarBorderColor.C0Deed;
            }
            throw new Exception("Cannot unmarshal type ProfileSidebarBorderColor");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ProfileSidebarBorderColor)untypedValue;
            switch (value)
            {
                case ProfileSidebarBorderColor.The000000:
                    serializer.Serialize(writer, "000000");
                    return;
                case ProfileSidebarBorderColor.The181A1E:
                    serializer.Serialize(writer, "181A1E");
                    return;
                case ProfileSidebarBorderColor.B8Aa9C:
                    serializer.Serialize(writer, "B8AA9C");
                    return;
                case ProfileSidebarBorderColor.C0Deed:
                    serializer.Serialize(writer, "C0DEED");
                    return;
            }
            throw new Exception("Cannot marshal type ProfileSidebarBorderColor");
        }

        public static readonly ProfileSidebarBorderColorConverter Singleton = new ProfileSidebarBorderColorConverter();
    }

    internal class ProfileSidebarFillColorConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ProfileSidebarFillColor) || t == typeof(ProfileSidebarFillColor?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "000000":
                    return ProfileSidebarFillColor.The000000;
                case "ACAAB6":
                    return ProfileSidebarFillColor.Acaab6;
                case "B8AA9C":
                    return ProfileSidebarFillColor.B8Aa9C;
                case "DDEEF6":
                    return ProfileSidebarFillColor.Ddeef6;
            }
            throw new Exception("Cannot unmarshal type ProfileSidebarFillColor");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ProfileSidebarFillColor)untypedValue;
            switch (value)
            {
                case ProfileSidebarFillColor.The000000:
                    serializer.Serialize(writer, "000000");
                    return;
                case ProfileSidebarFillColor.Acaab6:
                    serializer.Serialize(writer, "ACAAB6");
                    return;
                case ProfileSidebarFillColor.B8Aa9C:
                    serializer.Serialize(writer, "B8AA9C");
                    return;
                case ProfileSidebarFillColor.Ddeef6:
                    serializer.Serialize(writer, "DDEEF6");
                    return;
            }
            throw new Exception("Cannot marshal type ProfileSidebarFillColor");
        }

        public static readonly ProfileSidebarFillColorConverter Singleton = new ProfileSidebarFillColorConverter();
    }
}
