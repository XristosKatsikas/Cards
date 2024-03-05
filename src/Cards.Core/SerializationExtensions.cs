using System.Text.Json;

namespace Cards.Core
{
    public static class SerializationExtensions
    {
        public static string ToJson(this object @object, JsonSerializerOptions jsonSerializerOptions = default!)
        {
            if (@object == null)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            return JsonSerializer.Serialize(@object, jsonSerializerOptions);
        }

        public static T FromJson<T>(this string @string)
        {
            return @string.FromJson<T>();
        }

        public static T FromJson<T>(this string @string, JsonSerializerOptions jsonSerializerOptions = default!)
        {
            if (string.IsNullOrWhiteSpace(@string))
            {
                throw new ArgumentNullException(nameof(@string));
            }

            return JsonSerializer.Deserialize<T>(@string, jsonSerializerOptions)!;
        }

        public static T FromJson<T>(this Stream stream,
            JsonSerializerOptions jsonSerializerOptions = default!)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (stream.Length == 0)
            {
                throw new ArgumentException($"Empty {nameof(stream)}");
            }

            if (!stream.CanRead)
            {
                throw new ArgumentException($"{nameof(stream)} cannot be read");
            }

            var jsonBytes = new byte[stream.Length];
            stream.Read(jsonBytes, 0, jsonBytes.Length);

            var reader = new Utf8JsonReader(jsonBytes, true, default);

            return JsonSerializer.Deserialize<T>(ref reader, jsonSerializerOptions)!;
        }
    }
}
