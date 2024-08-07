using System.ComponentModel.DataAnnotations;

namespace Domain.Songs;

public record Duration
{
    [Required]
    [Range(0, 59)]
    public required int Minutes { get; init; }
    [Required]
    [Range(0, 59)]
    public required int Seconds { get; init; }
}