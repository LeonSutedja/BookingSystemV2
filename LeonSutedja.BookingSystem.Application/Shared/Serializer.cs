using Newtonsoft.Json;

namespace LeonSutedja.BookingSystem.Shared
{
    public static class Serializer
    {
        public static string ToJson<T>(this T toSerialize)
        {
            return JsonConvert.SerializeObject(toSerialize);
        }
    }
}
