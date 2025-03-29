namespace CaptchaV2_AsService.Models;

public class GoogleCaptchaConfig
{
    public int? Version { get; set; }
    public string SiteKey { get; set; } = default!;
    public string SecretKey { get; set; } = default!;
}