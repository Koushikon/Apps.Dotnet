using System.Text.Json.Serialization;

namespace CaptchaV3.Models;

public class GoogleCaptchaResponse
{
    [JsonPropertyName("success")] public bool Success { get; set; } = default!;
    [JsonPropertyName("score")] public double Score { get; set; }
    [JsonPropertyName("challenge_ts")] public DateTime ChallengeTs { get; set; }
}