using System.Text.Json.Serialization;

namespace Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum reactionType
{
    Like,
    Love,
    Laughing,
    Sad,
    Mad,
    Wow
}