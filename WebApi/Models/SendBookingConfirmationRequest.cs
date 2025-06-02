using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class SendBookingConfirmationRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string EventId { get; set; } = null!;
}
