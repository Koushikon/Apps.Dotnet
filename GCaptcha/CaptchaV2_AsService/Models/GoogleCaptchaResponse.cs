using System.Text.Json.Serialization;

namespace CaptchaV2_AsService.Models;

public class GoogleCaptchaResponse
{
    [JsonPropertyName("success")] public bool Success { get; set; }
    [JsonPropertyName("challenge_ts")] public DateTime ChallengeTs { get; set; }
    [JsonPropertyName("error-codes")] public List<string> ErrorCodes { get; set; } = default!;
}