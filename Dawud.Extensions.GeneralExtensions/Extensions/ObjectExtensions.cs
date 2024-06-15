using Newtonsoft.Json;
namespace Dawud.Extensions.GeneralExtensions.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNull<T>(this T input) where T : class
        {
            return input == null;
        }

        public static bool IsNotNull<T>(this T input) where T : class
        {
            return input != null;
        }

        public static string ToJson(this object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        public static T FromJson<T>(this string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }
    }
}