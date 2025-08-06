using Newtonsoft.Json;

namespace Intellimen.Helpers
{
    public static class SessionExtension
    {
        public static Dictionary<string, object>? GetTempValues(this ISession session) =>
           session.GetObjectFromJson<Dictionary<string, object>?>("TempValues");

        public static void SetTempValues(this ISession session, Dictionary<string, object> values) =>
            session.SetObjectAsJson("TempValues", values);

        public static void SetObjectAsJson(this ISession session, string key, object value) =>
            session.SetString(key, JsonConvert.SerializeObject(value));

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
