using System.Text.Json.Serialization;

namespace Web.Models;

public class CspViolation
{
    [JsonPropertyName("csp-report")]
    public CspReport? CstReport { get; set; }
}